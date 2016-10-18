using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class Move
    {
        private int _row;
        private int _numToRemove;
        private AverageValue _averageValue;

        public Move(int row, int numToRemove, AverageValue averageValue)
        {
            _row = row;
            _numToRemove = numToRemove;
            _averageValue = averageValue;

        }
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        public int NumToRemove
        {
            get
            {
                return _numToRemove;
            }
            set
            {
                _numToRemove = value;
            }
        }

        public AverageValue AverageValue
        {
            get
            {
                return _averageValue;
            }
            set
            {
                _averageValue = value;
            }
        }
    }
}