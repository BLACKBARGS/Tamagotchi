using Tamagotchi.GameLogic;
namespace Tamagotchi;

internal abstract class TamagotchiMain
{
    private static void Main()
    {
        Console.WriteLine("Bem-vindo ao Tamagotchi!");
        Console.Write("Digite o nome do seu Tamagotchi: ");
        var nome = Console.ReadLine();
        if (string.IsNullOrEmpty(nome))
        {
            Console.WriteLine("Nome inválido. O Tamagotchi não pode ser criado.");
            return;
        }

        var tamagotchi = new TamagotchiPet(nome);
        var game = new Game(tamagotchi);
        game.Run();
    }
}