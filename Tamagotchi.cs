using System.Timers;
using Timer = System.Timers.Timer;

namespace Tamagotchi;

class Tamagotchi
{
    public string Version { get; private set; } = "1.1";
    private const int MaxValue = 100;
    private const int MinValue = 0;
    private const int MaxAttributeSum = 320;
    private const int AttributeIncrement = 10;
    private const int AttributeDecrement = 2;
    private const int MaxIntelligence = 100;
    private const int IntelligenceIncrement = 30;
    private readonly Thread tamagotchiThread;
    //private const int IntelligenceDecrement = 10;
    private const int EnergyDecrementFood = 5;
    private const int EnergyDecrementAffection = 10;
    private const int EnergyDecrementPlay = 2;
    private const int EnergyDecrementMedicine = 2;

    private string Nome { get; set; }
    private int Fome { get; set; }
    private int Felicidade { get; set; }
    private int Alegria { get; set; }
    private int Idade { get; set; }
    private int Energia { get; set; }
    private int Saude { get; set; }
    private int Inteligencia { get; set; }

    private readonly Timer _timerAtualizacao;
    private readonly bool _estaMorto;

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
        _estaMorto = false;
        _timerAtualizacao = new Timer(10000);
        _timerAtualizacao.Elapsed += AtualizarTamagotchi;
        _timerAtualizacao.Start();

        tamagotchiThread = new Thread(ManterTamagotchiRodando);
        tamagotchiThread.Start();
    }

    private void ManterTamagotchiRodando()
    {
        while (!_estaMorto)
        {
            Thread.Sleep(10000);
        }
    }

    private void AtualizarTamagotchi(object? sender, ElapsedEventArgs e)
    {
        if (!_estaMorto)
        {
            AtualizarEstado();
            Envelhecer();
        }
        else
        {
            _timerAtualizacao.Stop();
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

    private void VerificarSaude()
    {
        var somaAtributos = Fome + Felicidade + Alegria + Energia;

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