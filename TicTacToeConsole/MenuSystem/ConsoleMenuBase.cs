using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TicTacToeConsole
{
    public class ConsoleMenuBase
    {
        public delegate void ConfirmOption();

        private int selected = 0;
        public string Title { get; set; }
        public Dictionary<string, ConfirmOption> Options { get; private set; }
        public int Selected 
        {
            get => selected;
            private set
            {
                if (value < 0)
                {
                    selected = Options.Count - 1 ;
                }
                else if( value >= Options.Count)
                {

                    selected = 0;
                }
                else
                {
                    selected = value;
                }
            }
        }
        public ConsoleMenuBase(string title, Dictionary<string, ConfirmOption> _options)
        {
            Title = title;
            Options = _options;
            DisplayElements();
        }

        void DisplayElements()
        {
            Console.Clear();
            Console.WriteLine("Use Up / Down Arrow Keys to change " +
                "selection and Space or Enter or Confirm\n");
            Console.WriteLine(Title) ;
            for (int i = 0; i < Options.Count; i++)
            {
                string displayString = "  ";
                if (i == Selected)
                {
                    displayString = "* ";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine($"{displayString} {Options.Keys.ElementAt(i)}");
                Console.ResetColor();
            }
            ReadInput();
        }

        void ReadInput()
        {
            ConsoleKey input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.Backspace:
                    Console.Clear();
                    Console.WriteLine("Closing Window....");
                    Environment.Exit(0);
                    break;
                case ConsoleKey.Enter:
                    ConfirmSelected();
                    break;
                case ConsoleKey.Spacebar:
                    ConfirmSelected();
                    break;
                case ConsoleKey.UpArrow:
                    Selected--;
                    break;
                case ConsoleKey.DownArrow:
                    Selected++;
                    break;
                default:
                    break;
            }
            DisplayElements();

        }

        void ConfirmSelected()
        {
            //OnConfirm?.Invoke(this, selected);
            ConfirmOption c = Options.Values.ElementAt(Selected);
            c();

        }


    }
}
