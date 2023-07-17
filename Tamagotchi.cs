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
        private int saude;
        private int taxaEnvelhecimento;

        public Tamagotchi(string nome)
        {
            this.nome = nome;
            fome = 100;
            felicidade = 100;
            alegria = 100;
            idade = 0;
            energia = 100;
            saude = 100;
            taxaEnvelhecimento = 1; // Defina a taxa de envelhecimento desejada
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
            if (idade % taxaEnvelhecimento == 0)
            {
                idade++;
            }
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

            VerificarSaude();
        }

        public void VerificarSaude()
        {
            if (fome <= 30 || felicidade <= 30 || alegria <= 30 || energia <= 30)
            {
                saude -= 10;
                if (saude < 0)
                    saude = 0;
            }
            else if (fome >= 80 && felicidade >= 80 && alegria >= 80 && energia >= 80)
            {
                saude += 10;
                if (saude > 100)
                    saude = 100;
            }
        }

        public void ExibirEstado()
        {
            Console.WriteLine("Nome: " + nome);
            Console.WriteLine("Idade: " + idade);
            Console.WriteLine("Fome: " + fome);
            Console.WriteLine("Felicidade: " + felicidade);
            Console.WriteLine("Saúde: " + saude);
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

        public bool VerificarMorte()
        {
            return fome <= 0 || felicidade <= 0 || alegria <= 0 || energia <= 0;
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
                    tamagotchi.Envelhecer();
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

                ExibirMenu();
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

                if (tamagotchi.VerificarMorte())
                {
                    Console.WriteLine("Seu Tamagotchi morreu!");
                    jogando = false;
                }
            }
            threadAtualizacao.Join();

            Console.WriteLine("Obrigado por jogar o Tamagotchi!");
        }

        static void ExibirMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Escolha uma ação:");
            Console.WriteLine("1. Alimentar");
            Console.WriteLine("2. Dar carinho");
            Console.WriteLine("3. Brincar");
            Console.WriteLine("4. Dormir");
            Console.WriteLine("5. Atualizar");
            Console.WriteLine("6. Sair");
            Console.Write("Opção: ");
        }
    }
}
