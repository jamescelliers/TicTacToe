using System;

namespace TicTacToeConsole
{
    class Program
    {
        static TicTacToeMain game = new TicTacToeMain();

        static void Main(string[] args)
        {
            Console.WriteLine("TicTacToe Console UI");
            game.GameChanged += Game_GameChanged;
            game.GameFinished += Game_GameFinished;

            game.Initialize(GetPlayerInput) ;
        }

        private static void Game_GameFinished(object sender, Player e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string printText = "";
            if (e == null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                printText = "Draw!!, No one won this time...";
            }
            else if (e.IsPlayer1)
            {
                printText = "Player 1 Won this battle...";
            }
            else
            {
                printText = "Player 2 Won this battle...";
            }
            Console.WriteLine(printText);
            Console.ResetColor();
            //Console.WriteLine($"{FormatArrayToString(game.GameArray)}");
            Console.ReadLine();
            //Console.Clear();
            game.RestartGame();

        }

        private static void Game_GameChanged(object sender, char[] e)
        {
            Console.WriteLine($"{FormatArrayToString(e)}");
        }

        public static int GetPlayerInput()
        {
            int i;
            bool isValid = true;
            do
            {
                if (isValid == false)
                {
                    Console.WriteLine("Invalid input");
                }
                Console.WriteLine("Input number between 1 an 9\n");
                isValid = false;
            } while (Int32.TryParse(Console.ReadLine(), out i) == false || i < 1 || i > 9);
            return i;
        }


        static string FormatArrayToString(char[] c)
        {
            string s = new string(c);
            s = s.Insert(3, "\n");
            s = s.Insert(7, "\n");
            s = s.Insert(11, "\n");
            return s;
        }

    }
}
