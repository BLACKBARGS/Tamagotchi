namespace Tamagotchi;

internal class Game
{
    private readonly Tamagotchi _tamagotchi;
    public Game(Tamagotchi tamagotchi)
    {
        this._tamagotchi = tamagotchi;
    }

    public void Run()
    {
        var jogando = true;

        while (jogando)
        {
            Console.Clear();
            _tamagotchi.ExibirTamagotchi();
            _tamagotchi.ExibirEstado();

            if (_tamagotchi.EstaTriste())
                Console.WriteLine("Seu Tamagotchi está triste!");

            if (_tamagotchi.EstaEntediado())
                Console.WriteLine("Seu Tamagotchi está entediado!");

            if (_tamagotchi.EstaCansado())
                Console.WriteLine("Seu Tamagotchi está cansado!");

            if (_tamagotchi.EstaDoente())
                Console.WriteLine("Seu Tamagotchi está doente!");

            string? opcao = ExibirMenu();

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

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            if (_tamagotchi.VerificarMorte())
            {
                _tamagotchi.ExibirTamagotchiMorto();
                Console.WriteLine("Seu Tamagotchi morreu! ");
                jogando = false;
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
        Console.Write("Opção: ");

        return Console.ReadLine();
    }
}