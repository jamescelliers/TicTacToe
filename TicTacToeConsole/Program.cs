﻿using System;
using System.Collections.Generic;

namespace TicTacToeConsole
{
    class Program
    {
        static TicTacToeMain game = new TicTacToeMain();

        static bool IsP1CPU = true;
        static bool IsP2CPU = true;
        static Player.CPUDifficulty P1Difficulty = Player.CPUDifficulty.Normal;
        static Player.CPUDifficulty P2Difficulty = Player.CPUDifficulty.Normal;

        static void Main(string[] args)
        {
            Console.WriteLine("TicTacToe Console UI");
            game.OnGameChanged += Game_OnGameChanged;
            game.OnGameFinished += Game_OnGameFinished;

            MainMenu();

            //menuScreen.OnConfirm += MenuScreen_OnConfirm;


        }


        public static void MainMenu()
        {
            Dictionary<string, ConsoleMenuBase.ConfirmOption> dict = new Dictionary<string, ConsoleMenuBase.ConfirmOption>();
            ConsoleMenuBase.ConfirmOption confirmOption = PlayOption;
            dict.Add("play", confirmOption);
            confirmOption = SettingsOption;
            dict.Add("Settings", confirmOption);
            confirmOption = ExitOption;
            dict.Add("Exit", confirmOption);

            ConsoleMenuBase menuScreen = new ConsoleMenuBase("TicTocToe Main Menu", dict);
        }
        private static void SettingsMenu()
        {
            Dictionary<string, ConsoleMenuBase.ConfirmOption> dict = new Dictionary<string, ConsoleMenuBase.ConfirmOption>();
            ConsoleMenuBase.ConfirmOption confirmOption = Settings_IsP1CPUOption;
            dict.Add($"Player 1 CPU? = {IsP1CPU}", confirmOption);
            confirmOption = Settings_IsP2CPUOption;
            dict.Add($"Player 2 CPU? = {IsP2CPU}", confirmOption);
            confirmOption = Settings_P1Difficulty;
            dict.Add($"Player 1 Difficulty = {P1Difficulty}", confirmOption);
            confirmOption = Settings_P2Difficulty;
            dict.Add($"Player 2 Difficulty = {P2Difficulty}", confirmOption);
            confirmOption = Settings_BackOption;
            dict.Add("Back", confirmOption);


            ConsoleMenuBase menuScreen = new ConsoleMenuBase("TicTocToe Settings", dict);
        }
        public static void PlayOption()
        {
            Console.Clear();
            game.Initialize(GetPlayerInput, IsP1CPU, IsP2CPU, P1Difficulty, P2Difficulty) ;
        }
        public static void SettingsOption()
        {
            SettingsMenu();
        }
        public static void ExitOption()
        {
            Console.WriteLine("Closing Window....");
            Environment.Exit(0);
        }
        public static void Settings_P1Difficulty()
        {
            if(P1Difficulty == (Player.CPUDifficulty) 2)
            {
                P1Difficulty = 0;
            }
            else
            {
            P1Difficulty++;
            }

            SettingsOption();
        }
        public static void Settings_P2Difficulty()
        {
            if (P2Difficulty == (Player.CPUDifficulty)2)
            {
                P2Difficulty = 0;
            }
            else
            {
                P2Difficulty++;
            }

            SettingsOption();
        }
        public static void Settings_IsP1CPUOption()
        {
            IsP1CPU = !IsP1CPU;
            SettingsOption();
        }
        public static void Settings_IsP2CPUOption()
        {
            IsP2CPU = !IsP2CPU;
            SettingsOption();
        }
        public static void Settings_BackOption()
        {
            MainMenu();
        }

        private static void Game_OnGameFinished(object sender, Player e)
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
            MainMenu();

        }
        private static void Game_OnGameChanged(object sender, char[] e)
        {
            string turnText = "";
            string charString = "";
            if (game.IsPlayer1Turn)
            {
                turnText = "1";
                charString = game.Player1.PlayerChar.ToString();

            }
            else
            {
                turnText = "2";
                charString = game.Player2.PlayerChar.ToString();
            }
            Console.WriteLine($"Player {turnText} Turn ({charString}) \n");
            Console.WriteLine($"{FormatArrayToString(e)} \n");
            Console.WriteLine($"Player {turnText} Turn ({charString}) \n");
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
