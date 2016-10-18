using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1_NimGame.enums;

namespace Assignment1_NimGame.Models
{
    public class View
    {

        public int SelectGameMode()
        {
            return CheckForInt("Enter 0 for player vs player, 1 for player vs cpu, or 2 for cpu vs cpu", 0, 2);
        }

        public int SelectRow(Player turn, Row[] _rows)
        {
            bool isValid = false;
            int row = 1;
            while (!isValid)
            {
                row = CheckForInt(turn + " enter the row you wish to take piece/pieces from", 1, _rows.Count());
                if (_rows[row - 1].RowSize >= 1)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("That row has no pieces, pick a different row");
                }
            }
            return row;
        }

        public int SelectPieces(int row, Row[] _rows)
        {
            return CheckForInt("Enter the number of pieces you wish to remove from row " + row, 1, _rows[row - 1].RowSize);
        }

        public int CheckForInt(string message, int min, int max)
        {
            bool isValid = false;
            int result = 0;

            while (!isValid)
            {
                Console.WriteLine(message);
                string userInput = Console.ReadLine();
                isValid = int.TryParse(userInput, out result) && result >= min && result <= max;

                if (!isValid)
                {
                    Console.WriteLine("You must enter a valid integer value between " + min + " and " + max);
                }
            }
            return result;
        }
    }
}