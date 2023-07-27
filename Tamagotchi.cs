using System.Timers;
using Timer = System.Timers.Timer;

namespace Tamagotchi;

public class Tamagotchi
{
    public string Version { get; private set; } = "1.1";
    private const int MaxValue = 100;
    private const int MinValue = 0;
    private const int MaxAttributeSum = 100;
    private const int AttributeIncrement = 10;
    private const int AttributeDecrement = 2;
    private const int MaxIntelligence = 100;
    private const int IntelligenceIncrement = 30;
    private const int EnergyDecrementFood = 5;
    private const int EnergyDecrementAffection = 10;
    private const int EnergyDecrementPlay = 2;
    private const int EnergyDecrementMedicine = 2;
    private const int SleepInterval = 10000;
    private const int TimerInterval = 10000;

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
        Inteligencia = 25;
        _estaMorto = false;
        _timerAtualizacao = new Timer(TimerInterval);
        _timerAtualizacao.Elapsed += AtualizarTamagotchi;
        _timerAtualizacao.Start();

        var tamagotchiThread = new Thread(ManterTamagotchiRodando);
        tamagotchiThread.Start();
    }

    private void ManterTamagotchiRodando()
    {
        while (!_estaMorto)
        {
            Thread.Sleep(SleepInterval);
        }
    }

    private void AtualizarTamagotchi(object? send, ElapsedEventArgs e)
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
        Inteligencia = Math.Min(Inteligencia + IntelligenceIncrement, MaxIntelligence);
    }
    
    public bool EstaTriste() 
    {
        return Fome < 15;
    }

    public bool EstaEntediado()
    {
        return Felicidade < 20;
    }

    public bool EstaDoente()
    {
        return Saude < 10;
    }

    public bool EstaCansado()
    {
        return Energia < 25;
    }

    public bool VerificarMorte()
    {
        return Fome <= 0 || Felicidade <= 0 || Alegria <= 0 || Energia <= 0 || Saude <= 0;
    }
    public void Alimentar()
    {
        Fome = Math.Min(Fome + AttributeIncrement, MaxValue);
        Energia = Math.Max(Energia - EnergyDecrementFood, MinValue);
    }

    public void DarCarinho()
    {
        Felicidade = Math.Min(Felicidade + AttributeIncrement, MaxValue);
        Energia = Math.Max(Energia - EnergyDecrementAffection, MinValue);
    }

    public void Brincar()
    {
        Alegria = Math.Min(Alegria + AttributeIncrement, MaxValue);
        Energia = Math.Max(Energia - EnergyDecrementPlay, MinValue);
    }

    public void DarRemedio()
    {
        Saude = Math.Min(Saude + AttributeIncrement, MaxValue);
        Energia = Math.Max(Energia - EnergyDecrementMedicine, MinValue);
    }

    public void AtualizarEstado()
    {
        Fome = Math.Max(Fome - AttributeDecrement, MinValue);
        Felicidade = Math.Max(Felicidade - AttributeDecrement, MinValue);
        Alegria = Math.Max(Alegria - AttributeDecrement, MinValue);
        Energia = Math.Max(Energia - AttributeDecrement, MinValue);
        VerificarSaude();
    }
    
    public void Dormir() => Energia = MaxValue;

    public void Envelhecer() => Idade++;

    private void VerificarSaude()
    {
        var somaAtributos = Fome + Felicidade + Alegria + Energia;

        Saude = somaAtributos >= MaxAttributeSum ? Math.Min(Saude + AttributeIncrement, MaxValue) : Math.Max(Saude - AttributeDecrement, MinValue);
    }

    public void ExibirEstado()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Idade: {Idade}");
        Console.WriteLine($"Fome: {Fome}");
        Console.WriteLine($"Felicidade: {Felicidade}");
        Console.WriteLine($"Saúde: {Saude}");
        Console.WriteLine($"Alegria: {Alegria}");
        Console.WriteLine($"Energia: {Energia}");
        Console.WriteLine($"Inteligência: {Inteligencia}");
    }

    public static void ExibirTamagotchi()
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

    public static void ExibirTamagotchiMorto()
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


    