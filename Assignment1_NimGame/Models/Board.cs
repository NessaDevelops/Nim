using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class Board
    {
        const int _numRows = 3;
        const int row1Size = 3;
        const int row2Size = 5;
        const int row3Size = 7;

        char[][] board = new char[_numRows][];

        private List<char> pieces = new List<char>();

        public Board()
        {
            //Jagged Array (Array of Arrays)
            board[0] = new char[row1Size] { '*', '*', '*' }; ;
            board[1] = new char[row2Size] { '*', '*', '*', '*', '*' };
            board[2] = new char[row3Size] { '*', '*', '*', '*', '*', '*', '*' };

            //RowSize = size;
            //for (int j = 0; j < size; ++j)
            //{
            //    pieces.Add('*');
            //}
        }

        public void printBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                System.Console.Write("Element({0}): ", i);

                for (int j = 0; j < board[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", board[i][j], j == (board[i].Length - 1) ? "" : " ");

                    for (int k = 0; k < board[j].Length; k++)
                    {
                        System.Console.Write("");
                    }
                }
                System.Console.WriteLine();
            }
        }

        public int RowSize { get; set; }

        public bool RemovePieces(int num)
        {
            bool result = false;
            RowSize = RowSize - num;
            if (num < pieces.Count() || num > 0)
            {
                for (int j = 0; j < num; ++j)
                {
                    pieces.Remove(pieces.Last());
                }
                result = true;
            }
            return result;
        }
    }
}
