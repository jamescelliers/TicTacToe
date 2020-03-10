using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TicTacToeConsole
{
    public class TicTacToeMain
    {

        public event EventHandler<char[]> GameChanged;
        public event EventHandler<Player> GameFinished;

        /// <summary>
        /// The method that returns player input
        /// </summary>
        /// <returns></returns>
        public delegate int inputDelegate();
        public inputDelegate _inputDelegate { get; private set; }



        public char[] GameArray { get; private set; }
        public char EmptyChar { get; } = '-';
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public int turnCount { get; private set; } = 0;
        public bool IsPlayer1Turn { get; private set; } = true;

        //consteuctor
        public TicTacToeMain()
        {
            GameArray = new char[9];

        }

        /// <summary>
        /// initializes the game and assigns the input delegate
        /// </summary>
        /// <param name="inputDel">The method that returns input as an int</param>
        public void Initialize(inputDelegate inputDel)
        {
            _inputDelegate = inputDel;
            Player1 = new Player('O', 'X', false, true, this, Player.CPUDifficulty.Easy);
            Player2 = new Player('X', '0', true, false, this, Player.CPUDifficulty.Easy);
            RestartGame();
        }

        /// <summary>
        /// Resets Game data and loop
        /// </summary>
        public void RestartGame()
        {
            for (int i = 0; i < GameArray.Length; i++)
            {
                GameArray[i] = EmptyChar;
            }
            turnCount = 0;
            GameLoop();
        }

        //loops every turn
        void GameLoop()
        {
            while (turnCount <= 9)
            {
                Player currentPlayer;
                if (IsPlayer1Turn)
                {
                    currentPlayer = Player1;
                }
                else
                {
                    currentPlayer = Player2;
                }
                int turnInt = currentPlayer.Turn();
                GameArray[turnInt - 1] = currentPlayer.PlayerChar;
                IsPlayer1Turn = !IsPlayer1Turn;
                turnCount++;
                GameChanged?.Invoke(this, GameArray);
                if (CheckForWinner(currentPlayer.PlayerChar))
                {
                    GameFinished?.Invoke(this, currentPlayer);
                }
            }
        }

        // Checks if a winning coniditon has occured
        internal bool CheckForWinner(char playerChar)
        {
            if (turnCount == 9)
            {
                GameFinished?.Invoke(this, null);
            }
            bool output = false; //output = IsAWinner;
            List<int> occupiedPlaces = new List<int>();
            for (int i = 0; i < GameArray.Length; i++)
            {
                if (GameArray[i] == playerChar)
                {
                    occupiedPlaces.Add(i);
                }
            }
            if (CheckForItemsInList(occupiedPlaces, 1, 2, 3) ||
                CheckForItemsInList(occupiedPlaces, 4, 5, 6) ||
                CheckForItemsInList(occupiedPlaces, 7, 8, 9) ||
                CheckForItemsInList(occupiedPlaces, 3, 5, 7) ||
                CheckForItemsInList(occupiedPlaces, 1, 4, 7) ||
                CheckForItemsInList(occupiedPlaces, 2, 5, 8) ||
                CheckForItemsInList(occupiedPlaces, 3, 6, 9) ||
                CheckForItemsInList(occupiedPlaces, 1, 5, 9))
            {
                output = true;
            }
            return output;
        }


        //Chekcs for specific condition, used only by CheckForWinner
        bool CheckForItemsInList(List<int> list, int i1, int i2, int i3)
        {
            bool output = false;
            i1--;
            i2--;
            i3--;
            if (list.Contains(i1) && list.Contains(i2) && list.Contains(i3))
            {
                output = true;
            }
            return output;
        }


        //Checks if a char item in the game array equals EmptyChar and returns true if its taken
        public bool IsGameArrayElementTaken(int i)
        {
            bool isTaken = false;
            if (GameArray[i - 1] != EmptyChar)
            {
                isTaken = true;
            }
            return isTaken;
        }


        List<int> GetCornerCoordinatesAsList()
        {
            return new List<int>() { 1, 3, 7, 9 };
        }
        List<int> GetEdgeCoorinatesAsList()
        {
            return new List<int>() { 2, 4, 6, 8 };
        }

        //used IsPlayer1trurn and finds opposite player. Maybe use a param instead?
        //The players contain an EnemyChar property so dont need to complicate things.
        List<int> GetEnemyPositions()
        {
            List<int> enemyPositions = new List<int>();
            Player enemyPlayer = Player1;
            if (IsPlayer1Turn)
            {
                enemyPlayer = Player2;
            }
            for (int i = 0; i < GameArray.Length; i++)
            {
                if (GameArray[i] == enemyPlayer.PlayerChar)
                {
                    enemyPositions.Add(i);
                }
            }
            return enemyPositions;
        }

    }
}