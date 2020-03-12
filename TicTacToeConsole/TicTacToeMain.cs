using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TicTacToeConsole
{
    public class TicTacToeMain
    {

        public event EventHandler<char[]> OnGameChanged;
        public event EventHandler<Player> OnGameFinished;

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
        public void Initialize(inputDelegate inputDel, bool isP1CPU, bool isP2CPU,
            Player.CPUDifficulty p1diff, Player.CPUDifficulty p2diff)
        {
            _inputDelegate = inputDel;
            Player1 = new Player('O', 'X', isP1CPU, true, p1diff);
            Player2 = new Player('X', 'O', isP2CPU, false, p2diff);
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
            OnGameChanged?.Invoke(this, GameArray);
            GameLoop();
        }

        /// <summary>
        /// Game Loop, each loop is a turn
        /// </summary>
        private protected void GameLoop()
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
                int turnInt = currentPlayer.Turn(this);
                GameArray[turnInt - 1] = currentPlayer.PlayerChar;
                IsPlayer1Turn = !IsPlayer1Turn;
                turnCount++;
                OnGameChanged?.Invoke(this, GameArray);
                if (CheckForWinner(currentPlayer.PlayerChar) || turnCount == 9)
                {
                    if (CheckForWinner(currentPlayer.PlayerChar))
                    {
                        OnGameFinished?.Invoke(this, currentPlayer);
                    }
                    else
                    { 
                        OnGameFinished?.Invoke(this,null);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a winning coniditon has occured
        /// </summary>
        /// <param name="playerChar">char to check if won</param>
        /// <returns>player char has matched 3!</returns>
        internal bool CheckForWinner(char playerChar)
        {
            bool output = false; //output = IsAWinner;
            List<int> occupiedPlaces = new List<int>(); //list of postions occupied by player
            for (int i = 0; i < GameArray.Length; i++) //fill list
            {
                if (GameArray[i] == playerChar)
                {
                    occupiedPlaces.Add(i);
                }
            }
            foreach (var item in WinConditions()) // cycle through win conditions and see if occupied places matches
            {
                if(CheckForItemsInList(occupiedPlaces, item))
                {
                    output = true;
                }
                
            }
            return output;
        }

        /// <summary>
        /// checks if a list contains all the items in a int[] array
        /// </summary>
        /// <param name="list">The list to check</param>
        /// <param name="winCondition">The array to check data with list against</param>
        /// <returns>whether the list contains all the items from int array</returns>
        bool CheckForItemsInList(List<int> list, int[] winCondition)
        {
            bool output = false;
            winCondition[0]--;
            winCondition[1]--;
            winCondition[2]--;
            if (winCondition.All(l => list.Contains(l)))
            {
                output = true;
            }
            return output;
        }

        /// <summary>
        /// Checks if a char item in the game array equals EmptyChar and returns true if its taken
        /// </summary>
        /// <param name="i">position in game array to check</param>
        /// <returns>whether the positions is already taken</returns>
        public bool IsGameArrayElementTaken(int i)
        {
            bool isTaken = false;
            if (GameArray[i - 1] != EmptyChar)
            {
                isTaken = true;
            }
            return isTaken;
        }

        /// <summary>
        /// returns a list of in arrays, each array contains a win condtion
        /// eg int[1,2,3] or int[3,5,7]
        /// </summary>
        /// <returns>a list of int arrays with each int[] containing a win condition </returns>
        public List<int[]> WinConditions()
        {
            //int[] temp = new int[8];
            List<int[]> list = new List<int[]>();
            list.Add(new int[3] { 1, 2, 3 });
            list.Add(new int[3] { 4, 5, 6 });
            list.Add(new int[3] { 7, 8, 9 });
            list.Add(new int[3] { 3, 5, 7 });
            list.Add(new int[3] { 1, 4, 7 });
            list.Add(new int[3] { 2, 5, 8 });
            list.Add(new int[3] { 3, 6, 9 });
            list.Add(new int[3] { 1, 5, 9 });
            return list;
        }

        /// <summary>
        /// Returns edges coordinates (not corners)
        /// </summary>
        /// <returns>list of 1, 3, 7 ,9</returns>
        List<int> GetCornerCoordinatesAsList()
        {
            return new List<int>() { 1, 3, 7, 9 };
        }
        /// <summary>
        /// Returns corner corordinates (not edge)
        /// </summary>
        /// <returns> List of 2, 4, 6 ,8</returns>
        List<int> GetEdgeCoorinatesAsList()
        {
            return new List<int>() { 2, 4, 6, 8 };
        }
        /// <summary>
        /// Uses IsPlayer1Turn to find opponent char and returns a list of positions 
        /// oppupied in the game array.
        /// </summary>
        /// <returns>A List of positions in the game array occupied by oppnent char</returns>
        public List<int> GetEnemyPositions()
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