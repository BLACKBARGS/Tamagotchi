using Timer = System.Timers.Timer;
using System;
class Game
{
    private Tamagotchi tamagotchi;
    public Game(Tamagotchi tamagotchi)
    {
        this.tamagotchi = tamagotchi;
    }

    public void Run()
    {
        bool jogando = true;

        while (jogando)
        {
            Console.Clear();
            tamagotchi.ExibirTamagotchi();
            tamagotchi.ExibirEstado();

            if (tamagotchi.EstaTriste())
                Console.WriteLine("Seu Tamagotchi está triste!");

            if (tamagotchi.EstaEntediado())
                Console.WriteLine("Seu Tamagotchi está entediado!");

            if (tamagotchi.EstaCansado())
                Console.WriteLine("Seu Tamagotchi está cansado!");

            if (tamagotchi.EstaDoente())
                Console.WriteLine("Seu Tamagotchi está doente!");

            string? opcao = ExibirMenu();

            tamagotchi.AtualizarEstado();
            tamagotchi.Envelhecer();

            switch (opcao)
            {
                case "1":
                    tamagotchi.Alimentar();
                    Console.WriteLine("Você alimentou o Tamagotchi!");
                    break;

                case "2":
                    tamagotchi.DarCarinho();
                    Console.WriteLine("Você deu carinho ao Tamagotchi!");
                    break;

                case "3":
                    tamagotchi.Brincar();
                    Console.WriteLine("Você brincou com o Tamagotchi!");
                    break;

                case "4":
                    tamagotchi.Dormir();
                    Console.WriteLine("Seu Tamagotchi dormiu e recuperou a energia!");
                    break;

                case "5":
                    tamagotchi.DarRemedio();
                    Console.WriteLine("Seu Tamagotchi tomou remédio!");
                    break;

                case "6":
                    tamagotchi.AumentarInteligencia();
                    Console.WriteLine("A inteligência do Tamagotchi aumentou!");
                    break;

                case "7":
                    jogando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            if (tamagotchi.VerificarMorte())
            {
                tamagotchi.ExibirTamagotchiMorto();
                Console.WriteLine("Seu Tamagotchi morreu! ");
                jogando = false;
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        Console.WriteLine("Obrigado por jogar o Tamagotchi!");
    }

    static string? ExibirMenu()
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