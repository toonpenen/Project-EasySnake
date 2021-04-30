using System;

namespace Example1
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetWindowSize(40, 40);
            Console.SetBufferSize(40, 40);
            Console.SetCursorPosition(13, 11);
            Console.WriteLine("Snake Game");
            Console.SetCursorPosition(13, 13);
            Console.WriteLine("Save Game: S");
            Console.SetCursorPosition(13, 15);
            Console.WriteLine("Navigate with arrows");
            Console.SetCursorPosition(13, 17);
            Console.WriteLine("Spacebar to pause");
            Console.SetCursorPosition(13, 19);
            Console.WriteLine("Choose an option:");
            Console.SetCursorPosition(13, 21);
            Console.WriteLine("N: Start Game");
            Console.SetCursorPosition(13, 23);
            Console.WriteLine("E: Exit");
            Console.SetCursorPosition(13, 25);
            Console.Write("Select an option: ");
            var input = Console.ReadLine();
            if (input == null) return;
            var choice = input.ToUpper();
            switch (choice)
            {
                case "N":
                {
                    Console.Clear();
                    var game = new Game();
                    while (game.IsRunning)
                    {
                        game.KeyPressed(Console.ReadKey(true));
                    }

                    break;
                }
                case "E":
                    Console.Clear();
                    Console.SetCursorPosition(15, 20);
                    Console.WriteLine("Good Bye");
                    break;
            }
        }
    }
}

