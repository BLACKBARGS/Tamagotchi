using System;
using System.Timers;
using Timer = System.Timers.Timer;

class Tamagotchi 
{
    public string Nome { get; private set; }
    public int Fome { get; private set; }
    public int Felicidade { get; private set; }
    public int Alegria { get; private set; }
    public int Idade { get; private set; }
    public int Energia { get; private set; }
    public int Saude { get; private set; }
    public int Inteligencia { get; private set; }
    private Timer timerAtualizacao;
    private bool estaMorto;
    public Tamagotchi(string nome)
    {
        Nome = nome;
        Fome = 100;
        Felicidade = 100;
        Alegria = 100;
        Idade = 0;
        Energia = 100;
        Saude = 100;
        Inteligencia = 50;
        estaMorto = false;
        timerAtualizacao = new Timer(1000);
        timerAtualizacao.Elapsed += AtualizarTamagotchi;
        timerAtualizacao.Start();
    }

    private void AtualizarTamagotchi(object? sender, ElapsedEventArgs e)
    {
        if (!estaMorto)
        {
            AtualizarEstado();
            Envelhecer();
        }
        else
        {
            timerAtualizacao.Stop();
        }
    }

    public void AumentarInteligencia()
    {
        Inteligencia += 10;
        if (Inteligencia > 100)
            Inteligencia = 100;
    }

    public void Alimentar()
    {
        Fome += 20;
        if (Fome > 100)
            Fome = 100;

        Energia -= 5;
        if (Energia < 0)
            Energia = 0;
    }

    public void DarCarinho()
    {
        Felicidade += 30;
        if (Felicidade > 100)
            Felicidade = 100;

        Energia -= 1;
        if (Energia < 0)
            Energia = 0;
    }

    public void Brincar()
    {
        Alegria += 20;
        if (Alegria > 100)
            Alegria = 100;

        Energia -= 2;
        if (Energia < 0)
            Energia = 0;
    }

    public void DarRemedio()
    {
        Saude += 20;
        if (Saude > 100)
            Saude = 100;

        Energia -= 2;
        if (Energia < 0)
            Energia = 0;
    }

    public void AtualizarEstado()
    {
        Fome -= 1;
        if (Fome < 0)
            Fome = 0;

        Felicidade -= 1;
        if (Felicidade < 0)
            Felicidade = 0;

        Alegria -= 1;
        if (Alegria < 0)
            Alegria = 0;

        Energia -= 1;
        if (Energia < 0)
            Energia = 0;

        VerificarSaude();
    }

    public void Dormir()
    {
        Energia = 100;
    }

    public void Envelhecer()
    {
        Idade++;
    }

    public bool EstaTriste()
    {
        return Fome < 30;
    }

    public bool EstaEntediado()
    {
        return Felicidade < 30;
    }

    public bool EstaDoente()
    {
        return Saude < 30;
    }

    public bool EstaCansado()
    {
        return Energia < 30;
    }

    public bool VerificarMorte()
    {
        return Fome <= 0 || Felicidade <= 0 || Alegria <= 0 || Energia <= 0 || Saude <= 0;
    }

    public void VerificarSaude()
    {
        int somaAtributos = Fome + Felicidade + Alegria + Energia;

        if (somaAtributos >= 320)
        {
            Saude += 10;
            if (Saude > 100)
                Saude = 100;
        }
        else
        {
            Saude -= 10;
            if (Saude < 0)
                Saude = 0;
        }
    }

    public void ExibirEstado()
    {
        Console.WriteLine("Nome: " + Nome);
        Console.WriteLine("Idade: " + Idade);
        Console.WriteLine("Fome: " + Fome);
        Console.WriteLine("Felicidade: " + Felicidade);
        Console.WriteLine("Saúde: " + Saude);
        Console.WriteLine("Alegria: " + Alegria);
        Console.WriteLine("Energia: " + Energia);
        Console.WriteLine("Inteligência: " + Inteligencia);
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
}