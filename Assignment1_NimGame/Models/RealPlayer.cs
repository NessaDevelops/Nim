using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class RealPlayer : Player
    {
        View view = new View();

        public override Move MakeMove(Row[] rows, Dictionary<BoardState, List<Move>> boardStates)
        {
            throw new NotImplementedException();
        }

        public override void PrintKnownMoves()
        {
            throw new NotImplementedException();
        }

        public override Move Turn(Player currentTurn, Row[] _rows, Dictionary<BoardState, List<Move>> boardStates)
        {
            // GETS USER INPUT FOR ROW / PIECES
            int row = view.SelectRow(currentTurn, _rows);
            int numToRemove = view.SelectPieces(row, _rows);

            Move move = new Move(row, numToRemove, new AverageValue(0, 1));

            return move;
        }
    }
}
