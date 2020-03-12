using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TicTacToeConsole
{
    public class Player
    {
        public enum CPUDifficulty
        {
            Easy,
            Normal,
            Impossible
        }
        public char PlayerChar { get; private set; }
        public char OpponentChar { get; private set; }
        public bool IsCPU { get; set; }
        public bool IsPlayer1 { get; private set; }
        public CPUDifficulty? Difficulty { get; set; }

        public Player(char playerChar, char oppChar, bool isCPU, bool isPlayer1, CPUDifficulty? difficulty = null)
        {
            PlayerChar = playerChar;
            OpponentChar = oppChar;
            IsCPU = isCPU;
            IsPlayer1 = isPlayer1;
            //Checks if difficulty is null and then sets default as fallback.
            if (difficulty == null)
            {
                Difficulty = CPUDifficulty.Normal;
            }
            //If diffulty is not null then set it.
            else
            {
                Difficulty = difficulty;
            }
        }

        private int CPUMove(TicTacToeMain game)
        {
            //int movePosition = 0;

            int cpuMove = 1;
            switch (Difficulty)
            {
                case CPUDifficulty.Easy:
                    CPUMoveEasy(game);
                    break;
                case CPUDifficulty.Normal:
                    cpuMove = CPUMoveNormal(game);
                    break;
                case CPUDifficulty.Impossible:
                    cpuMove = CPUMoveImpossible(game);
                    break;
                default:
                    CPUMoveEasy(game);
                    break;
            }
            return cpuMove;
        }
        private int PlayerMove(TicTacToeMain game)
        {
            int playerInput = game._inputDelegate();
            while (game.IsGameArrayElementTaken(playerInput))
            {
                playerInput = game._inputDelegate();
            }
            return playerInput;
        }
        //wrapper for CPUMove and PlayerMove
        public int Turn(TicTacToeMain game)
        {
            //check if taken
            int output = 0;
            if (IsCPU)
            {
                output = CPUMove(game);
            }
            else
            {
                output = PlayerMove(game);
            }
            return output;
        }

        private int CPUMoveImpossible(TicTacToeMain game)
        {
            List<int> enemyPositions = game.GetEnemyPositions();
            //int cpuMove = 0;
            for (int i = 0; i < enemyPositions.Count; i++)
            {

                Console.WriteLine($"enemy = {OpponentChar} at position {enemyPositions[i]}");
                enemyPositions[i] ++;
            }
            switch (game.turnCount)
            {
                case 0:
                    return 1;
                case 1:

                    if (enemyPositions.Contains(5))
                    {
                        return 1;
                    }
                    else
                    {
                        return 5;
                    }
                case 2:
                    //if (enemyPositions.Intersect(GetEdgeCoorinatesAsList()).Any())
                    //{
                    //    return 5;
                    //}
                    //else if (enemyPositions.Contains(5))
                    //{
                    //    return 9;
                    //}
                    //else if (enemyPositions.Contains(2) || enemyPositions.Contains(4))
                    //{
                    //    return 9;
                    //}
                    //else
                    //{
                    //    return 7;
                    //}
                    if (enemyPositions.Contains(5)){
                        return 9;
                    }
                    else
                    {
                        return 5;
                    }
                //case 3:

                //    break;
                default:
                    return CPUMoveEasy(game);
            }

        }

        private int CPUMoveNormal(TicTacToeMain game)
        {
            return CPUMoveEasy(game);
        }

        private int CPUMoveEasy(TicTacToeMain game)
        {
            Random rnd = new Random();
            int cpuMove = rnd.Next(1, 10);
            while (game.IsGameArrayElementTaken(cpuMove))
            {
                cpuMove = rnd.Next(1, 10);
            }
            return cpuMove;
        }

        List<int> GetCornerCoordinatesAsList()
        {
            return new List<int>() { 1, 3, 7, 9 };
        }
        List<int> GetEdgeCoorinatesAsList()
        {
            return new List<int>() { 2, 4, 6, 8 };
        }

    }

}
