using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    abstract public class Player
    {
        private int _wins;
        private Dictionary<BoardState, Move> _moves;

        public Player()
        {
            _wins = 0;
            _moves = new Dictionary<BoardState, Move>();
        }

        abstract public Move MakeMove(Row[] rows, Dictionary<BoardState, List<Move>> boardStates);

        abstract public void PrintKnownMoves();

        abstract public Move Turn(Player currentTurn, Row[] _rows, Dictionary<BoardState, List<Move>> boardStates);

        public Dictionary<BoardState, Move> Moves
        {
            get
            {
                return _moves;
            }
            set
            {
                _moves = value;
            }
        }

        public int Wins
        {
            get
            {
                return _wins;
            }
            set
            {
                _wins = value;
            }
        }
    }
}
