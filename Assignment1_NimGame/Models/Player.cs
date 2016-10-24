using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    abstract public class Player
    {
        private int wins = 0;
        private Dictionary<BoardState, Move> moves = new Dictionary<BoardState, Move>();

        abstract public Move MakeMove(Row[] rows, Dictionary<BoardState, List<Move>> boardStates);

        abstract public void PrintKnownMoves();

    }
}
