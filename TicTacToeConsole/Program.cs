using System;
using System.Collections.Generic;

namespace TicTacToeConsole
{
    class Program
    {
        static TicTacToeMain game = new TicTacToeMain();

        static bool isP1CPU = true;
        static bool isP2CPU = true;


        static void Main(string[] args)
        {
            Console.WriteLine("TicTacToe Console UI");
            game.GameChanged += Game_GameChanged;
            game.GameFinished += Game_GameFinished;

            Dictionary<string, ConsoleMenuBase.ConfirmOption> dict = new Dictionary<string, ConsoleMenuBase.ConfirmOption>();
            ConsoleMenuBase.ConfirmOption confirmOption = PlayOption;
            dict.Add("play", confirmOption);
            confirmOption = SettingsOption;
            dict.Add("Settings", confirmOption);
            confirmOption = ExitOption;
            dict.Add("Exit", confirmOption);


            ConsoleMenuBase menuScreen = new ConsoleMenuBase("TicTocToe Main Menu", dict);
            menuScreen.OnConfirm += MenuScreen_OnConfirm;


        }

        private static void MenuScreen_OnConfirm(object sender, int e)
        {

        }

        public static void PlayOption()
        {
            Console.Clear();
            game.Initialize(GetPlayerInput);
        }

        public static void SettingsOption()
        {
            Dictionary<string, ConsoleMenuBase.ConfirmOption> dict = new Dictionary<string, ConsoleMenuBase.ConfirmOption>();
            ConsoleMenuBase.ConfirmOption confirmOption = PlayOption;
            dict.Add($"Player 1 CPU? = {isP1CPU}", confirmOption);
            //confirmOption = SettingsOption;
            dict.Add($"Player 2 CPU? = {isP2CPU}", confirmOption);
            // confirmOption = ExitOption;
            dict.Add("Back", confirmOption);


            ConsoleMenuBase menuScreen = new ConsoleMenuBase("TicTocToe Settings", dict);
        }
        public static void ExitOption()
        {
            Console.WriteLine("Closing Window....");
            Environment.Exit(0);
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
