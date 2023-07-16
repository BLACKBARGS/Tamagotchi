using System;
using System.Threading;

namespace TamagotchiGame
{

class Tamagotchi
{
    private string nome;
    private int fome;
    private int felicidade;
    private int alegria;
    private int idade;
    private int energia;

    public Tamagotchi(string nome)
    {
        this.nome = nome;
        fome = 0;
        felicidade = 0;
        alegria = 0;
        idade = 0;
        energia = 100;
    }

    public void Alimentar()
    {
        fome += 20;
        if (fome > 100)
            fome = 100;

        energia -= 5;
        if (energia < 0)
            energia = 0;
    }

    public void DarCarinho()
    {
        felicidade += 30;
        if (felicidade > 100)
            felicidade = 100;

        energia -= 1;
        if (energia < 0)
            energia = 0;
    }

    public void Brincar()
    {
        alegria += 20;
        if (alegria > 100)
            alegria = 100;

        energia -= 2;
        if (energia < 0)
            energia = 0;
    }

    public void Dormir()
    {
        energia = 100;
    }

    public void Envelhecer()
    {
        idade++;
    }

    public void AtualizarEstado()
    {
        fome -= 1;
        if (fome < 0)
            fome = 0;

        felicidade -= 1;
        if (felicidade < 0)
            felicidade = 0;

        alegria -= 1;
        if (alegria < 0)
            alegria = 0;

        energia -= 1;
        if (energia < 0)
            energia = 0;
    }

    public void ExibirEstado()
    {
        Console.WriteLine("Nome: " + nome);
        Console.WriteLine("Idade: " + idade);
        Console.WriteLine("Fome: " + fome);
        Console.WriteLine("Felicidade: " + felicidade);
        Console.WriteLine("Alegria: " + alegria);
        Console.WriteLine("Energia: " + energia);
    }

    public void ExibirTamagotchi()
    {
        Console.WriteLine(@"
      .^._.^.
      | @ @ |
     ( '---' )
    .'___V___'.
    | /     \ |
      \ /-\ /
       V   V
        ");
    }

    public bool EstaTriste()
    {
        return fome < 30;
    }

    public bool EstaEntediado()
    {
        return felicidade < 30;
    }

    public bool EstaCansado()
    {
        return energia < 30;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Bem-vindo ao Tamagotchi!");
        Console.Write("Digite o nome do seu Tamagotchi: ");
        string? nome = Console.ReadLine();

        if (string.IsNullOrEmpty(nome))
        {
            Console.WriteLine("Nome inválido. O Tamagotchi não pode ser criado.");
            return; // Sai do programa se o nome for inválido
        }

        Tamagotchi tamagotchi = new Tamagotchi(nome);

        bool jogando = true;
        Thread threadAtualizacao = new Thread(() =>
        {
            while (jogando)
            {
                tamagotchi.AtualizarEstado();
                Thread.Sleep(1000);
            }
        });
        threadAtualizacao.Start();
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

            Console.WriteLine();
            Console.WriteLine("Escolha uma ação:");
            Console.WriteLine("1. Alimentar");
            Console.WriteLine("2. Dar carinho");
            Console.WriteLine("3. Brincar");
            Console.WriteLine("4. Dormir");
            Console.WriteLine("5. Atualizar");
            Console.WriteLine("6. Sair");
            Console.Write("Opção: ");
            string? opcao = Console.ReadLine();

            tamagotchi.Envelhecer();
            tamagotchi.AtualizarEstado();

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
                tamagotchi.AtualizarEstado();
                    break;
                

                case "6":
                    jogando = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Thread.Sleep(1000); // Aguarda 1 segundo antes de atualizar o estado novamente
        }
        threadAtualizacao.Join();

        Console.WriteLine("Obrigado por jogar o Tamagotchi!");
        }
    }
}
