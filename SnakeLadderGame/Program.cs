using System;

namespace SnakeLadderGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Board size?");
            int boardsize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Number of players?");
            int players = Convert.ToInt32(Console.ReadLine());
            Game g = new Game(boardsize, players);
            g.Play();
        }
    }
}
