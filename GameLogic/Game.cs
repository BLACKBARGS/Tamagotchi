using System.Text.Json;
namespace Tamagotchi.GameLogic;

internal class Game
{
    private TamagotchiPet _tamagotchi;
    public Game(TamagotchiPet tamagotchi) => this._tamagotchi = tamagotchi;

    public void Run()
    {
        var jogando = true;

        while (jogando)
        {
            Console.Clear();
            TamagotchiPet.ExibirTamagotchi();
            _tamagotchi.ExibirEstado();

            if (_tamagotchi.EstaTriste)
                Console.WriteLine("Seu Tamagotchi está triste!");

            if (_tamagotchi.EstaEntediado)
                Console.WriteLine("Seu Tamagotchi está entediado!");

            if (_tamagotchi.EstaCansado)
                Console.WriteLine("Seu Tamagotchi está cansado!");

            if (_tamagotchi.EstaDoente)
                Console.WriteLine("Seu Tamagotchi está doente!");

            var opcao = ExibirMenu();

            _tamagotchi.AtualizarEstado();
            _tamagotchi.Envelhecer();

            switch (opcao)
            {
                case "1":
                    _tamagotchi.Alimentar();
                    Console.WriteLine("Você alimentou o Tamagotchi!");
                    break;

                case "2":
                    _tamagotchi.DarCarinho();
                    Console.WriteLine("Você deu carinho ao Tamagotchi!");
                    break;

                case "3":
                    _tamagotchi.Brincar();
                    Console.WriteLine("Você brincou com o Tamagotchi!");
                    break;

                case "4":
                    _tamagotchi.Dormir();
                    Console.WriteLine("Seu Tamagotchi dormiu e recuperou a energia!");
                    break;

                case "5":
                    _tamagotchi.DarRemedio();
                    Console.WriteLine("Seu Tamagotchi tomou remédio!");
                    break;

                case "6":
                    _tamagotchi.AumentarInteligencia();
                    Console.WriteLine("A inteligência do Tamagotchi aumentou!");
                    break;

                case "7":
                    jogando = false;
                    break;
                
                case "8":
                    SaveGame();
                    Console.WriteLine("Jogo salvo!");
                    break;

                case "9":
                    if (LoadGame())
                    Console.WriteLine("Jogo carregado!");
                    else
                    Console.WriteLine("Falha ao carregar o jogo!");
                    break;

                case "10":
                    jogando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;    
            }

            if (_tamagotchi.VerificarMorte)
            {
                TamagotchiPet.ExibirTamagotchiMorto();
                Console.WriteLine("Seu Tamagotchi morreu! ");
                break;
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        Console.WriteLine("Obrigado por jogar o Tamagotchi!");
        
    }
    private static string? ExibirMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Escolha uma ação:");
        Console.WriteLine("1. Alimentar");
        Console.WriteLine("2. Dar carinho");
        Console.WriteLine("3. Brincar");
        Console.WriteLine("4. Dormir");
        Console.WriteLine("5. Dar remédio");
        Console.WriteLine("6. Aumentar inteligência");
        Console.WriteLine("7. Sair");
        Console.WriteLine("8. Salvar Jogo");
        Console.WriteLine("9. Carregar Jogo");
        Console.WriteLine("10. Sair");
        Console.Write("Opção: ");

        return Console.ReadLine();
    }

    private void SaveGame()
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(_tamagotchi);
            File.WriteAllText("tamagotchi_save.json", jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Falha ao salvar o jogo: {ex.Message}");
        }
    }

    private bool LoadGame()
    {
        return LoadGame(_tamagotchi);
    }

    private bool LoadGame(TamagotchiPet tamagotchi)
    {
        if (File.Exists("tamagotchi_save.json"))
        {
            try
            {
                string jsonString = File.ReadAllText("tamagotchi_save.json");
                TamagotchiPet? loadedTamagotchi = JsonSerializer.Deserialize<TamagotchiPet>(jsonString);
                if (loadedTamagotchi != null)
                {
                    tamagotchi = loadedTamagotchi;
                    return true;
                }
                else
                {
                    Console.WriteLine("Falha ao carregar o jogo: Dados inválidos no arquivo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha ao carregar o jogo: {ex.Message}");
            }
        }   
        else
        {
            Console.WriteLine("Arquivo de save não encontrado.");
        }
        return false;
    }
}