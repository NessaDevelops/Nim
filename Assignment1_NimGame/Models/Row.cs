using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class Row
    {
        private List<char> pieces = new List<char>();

        public void printRow()
        {
            for (int j = 0; j < RowSize; ++j)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }

        public int RowSize { get; set; }

        public Row(int size)
        {
            RowSize = size;
            for (int j = 0; j < size; ++j)
            {
                pieces.Add('*');
            }
        }

        public bool RemovePieces(int num)
        {
            bool validRemove = false;
            if (num <= pieces.Count() && num > 0)
            {
                for (int j = 0; j < num; ++j)
                {
                    pieces.Remove(pieces.Last());
                }
                RowSize = RowSize - num;
                validRemove = true;
            }
            return validRemove;
        }
    }
}