using System;
using System.Collections.Generic;
using System.Text;

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

        public TicTacToeMain Parent { get; private set; }
        public char PlayerChar { get; private set; }
        public char OpponentChar { get; private set; }
        public bool IsCPU { get; set; }
        public bool IsPlayer1 { get; private set; }
        public CPUDifficulty? Difficulty { get; set; }

        public Player(char playerChar, char oppChar, bool isCPU, bool isPlayer1, TicTacToeMain parent, CPUDifficulty? difficulty = null)
        {
            Parent = parent;
            PlayerChar = playerChar;
            IsCPU = isCPU;
            IsPlayer1 = isPlayer1;
            //Checks if difficulty is null and then sets default as fallback.
            if(difficulty == null)
            {
                Difficulty = CPUDifficulty.Normal;
            }
            //If diffulty is not null then set it.
            else
            {
                Difficulty = difficulty;
            }
        }

        public int CPUMove()
        {
            //int movePosition = 0;

            int cpuMove = 1;
            //switch (difficulty)
            //{
            //    case Difficulty.Easy:
            //        Random rnd = new Random();
            //        cpuMove = rnd.Next(1, 10);
            //        while (IsGameArrayElementTaken(cpuMove))
            //        {
            //            cpuMove = rnd.Next(1, 10);
            //        }
            //        break;
            //    case Difficulty.Normal:

            //        break;
            //    case Difficulty.Impossible:
            //        //List<int> enemyPositions = GetEnemyPositions();
            //        //if (turnCount == 0)
            //        //{
            //        //    return 1;
            //        //}
            //        //else if(turnCount == 1)
            //        //{

            //        //    if (enemyPositions.Contains(5))
            //        //    {
            //        //        return 1;
            //        //    }
            //        //    else
            //        //    {
            //        //        return 5;
            //        //    }
            //        //}else if(turnCount == 2)
            //        //{
            //        //    if (enemyPositions.Intersect(GetEdgeCoorinatesAsList()).Any())
            //        //    {
            //        //        foreach (var item in enemyPositions)
            //        //        {
            //        //            Console.WriteLine($"{item}");
            //        //        }
            //        //        if (enemyPositions[0] == 2 || enemyPositions[0] == 4)
            //        //        {
            //        //            return 9;
            //        //        }
            //        //        else
            //        //        {
            //        //            return 7;
            //        //        }
            //        //    }
            //        //    else
            //        //    {
            //        //        if(enemyPositions[1])
            //        //    }
            //        //}

            //        //else
            //        //{
            //        //    rnd = new Random();
            //        //    cpuMove = rnd.Next(1, 10);
            //        //    while (IsGameArrayElementTaken(cpuMove))
            //        //    {
            //        //        cpuMove = rnd.Next(1, 10);
            //        //    }
            //        //}
            //        break;
            //    default:
            //        break;
            //}
            return cpuMove;
        }

        public int PlayerMove()
        {
            int playerInput = Parent._inputDelegate();
            while (Parent.IsGameArrayElementTaken(playerInput))
            {
                playerInput = Parent._inputDelegate();
            }
            return playerInput;
        }

        //wrapper for CPUMove and PlayerMove
        public int Turn()
        {
            int output = 0;
            if (IsCPU)
            {
                output = CPUMove();
            }
            else
            {
                output = PlayerMove();
            }
            return output;
        }
    }
}
