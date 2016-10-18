using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class BoardState
    {
        private int topRow;
        private int middleRow;
        private int bottomRow;

        public BoardState(int firstRow, int secondRow, int thirdRow)
        {
            topRow = firstRow;
            middleRow = secondRow;
            bottomRow = thirdRow;
        }

        public override string ToString()
        {
            string state = "Top: " + topRow + " Mid: " + middleRow + " Bottom: " + bottomRow;
            return state;
        }

        public bool CheckIfStatesSame(Row[] rows)
        {
            bool isEqual = false;
            if (this.topRow == rows[0].RowSize && this.middleRow == rows[1].RowSize && this.bottomRow == rows[2].RowSize)
            {
                isEqual = true;
            }
            return isEqual;
        }
    }
}