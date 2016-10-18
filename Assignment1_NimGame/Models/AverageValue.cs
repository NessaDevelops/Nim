using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class AverageValue
    {
        private decimal _value;
        private int _count;

        public AverageValue(decimal boardStateValue, int countBoardState)
        {
            _value = boardStateValue;
            _count = countBoardState;
        }

        public decimal GetValue
        {
            get { return _value; }
        }

        public int GetCount
        {
            get { return _count; }
        }
    }
}
