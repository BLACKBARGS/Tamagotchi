using System;
using System.Timers;
using System.Threading;
using Timer = System.Timers.Timer;

class Tamagotchi 
{  
    public string Version { get; private set; } = "1.1"; 
    private const int MaxValue = 100;
    private const int MinValue = 0;
    private const int MaxAttributeSum = 320;
    private const int AttributeIncrement = 10;
    private const int AttributeDecrement = 10;
    private const int MaxIntelligence = 100;
    private const int IntelligenceIncrement = 10;
    private Thread tamagotchiThread;
    //private const int IntelligenceDecrement = 10;
    private const int EnergyDecrementFood = 10;
    private const int EnergyDecrementAffection = 1;
    private const int EnergyDecrementPlay = 2;
    private const int EnergyDecrementMedicine = 2;

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
        Fome = MaxValue;
        Felicidade = MaxValue;
        Alegria = MaxValue;
        Idade = 0;
        Energia = MaxValue;
        Saude = MaxValue;
        Inteligencia = 50;
        estaMorto = false;
        timerAtualizacao = new Timer(10000);
        timerAtualizacao.Elapsed += AtualizarTamagotchi;
        timerAtualizacao.Start();

        tamagotchiThread = new Thread(ManterTamagotchiRodando);
        tamagotchiThread.Start();
    }

    private void ManterTamagotchiRodando()
    {
        while (!estaMorto)
        {
            Thread.Sleep(10000);
        }
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
        Inteligencia += IntelligenceIncrement;
        Inteligencia = Math.Min(Inteligencia, MaxIntelligence);
    }

    public void Alimentar()
    {
        Fome += AttributeIncrement;
        Fome = Math.Min(Fome, MaxValue);

        Energia -= EnergyDecrementFood;
        Energia = Math.Max(Energia, MinValue);
    }

    public void DarCarinho()
    {
        Felicidade += AttributeIncrement;
        Felicidade = Math.Min(Felicidade, MaxValue);

        Energia -= EnergyDecrementAffection;
        Energia = Math.Max(Energia, MinValue);
    }

    public void Brincar()
    {
        Alegria += AttributeIncrement;
        Alegria = Math.Min(Alegria, MaxValue);

        Energia -= EnergyDecrementPlay;
        Energia = Math.Max(Energia, MinValue);
    }

    public void DarRemedio()
    {
        Saude += AttributeIncrement;
        Saude = Math.Min(Saude, MaxValue);

        Energia -= EnergyDecrementMedicine;
        Energia = Math.Max(Energia, MinValue);
    }

    public void AtualizarEstado()
    {
        Fome -= AttributeDecrement;
        Fome = Math.Max(Fome, MinValue);

        Felicidade -= AttributeDecrement;
        Felicidade = Math.Max(Felicidade, MinValue);

        Alegria -= AttributeDecrement;
        Alegria = Math.Max(Alegria, MinValue);

        Energia -= AttributeDecrement;
        Energia = Math.Max(Energia, MinValue);

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

        if (somaAtributos >= MaxAttributeSum)
        {
            Saude += AttributeIncrement;
            Saude = Math.Min(Saude, MaxValue);
        }
        else
        {
            Saude -= AttributeDecrement;
            Saude = Math.Max(Saude, MinValue);
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

    public void ExibirTamagotchiMorto()
    {
        Console.WriteLine(@"

           .^._.^.
           | X X |
          ( '---' )
         .'___V___'.
         | /     \ |
           \ /-\ /
            V   V
       
        ");
    }
}