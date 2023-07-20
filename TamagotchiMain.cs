using Timer = System.Timers.Timer;
using System;


class TamagotchiMain
{
    static void Main()
    {
        Console.WriteLine("Bem-vindo ao Tamagotchi!");
        Console.Write("Digite o nome do seu Tamagotchi: ");
        string? nome = Console.ReadLine();
        if (string.IsNullOrEmpty(nome))
        {
            Console.WriteLine("Nome inválido. O Tamagotchi não pode ser criado.");
            return;
        }

        Tamagotchi tamagotchi = new Tamagotchi(nome);
        Game game = new Game(tamagotchi);
        game.Run();
    }
}