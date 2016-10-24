using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class RandomAI : Player
    {
        private Random rnd = new Random();
        private List<BoardState> MoveStates = new List<BoardState>();

        public override Move MakeMove(Row[] rows, Dictionary<BoardState, List<Move>> boardStates)
        {
            int row = MakeRowChoice(rows.Count(), rows);
            int numToRemove = MakeRandomChoice(rows[row - 1].RowSize);
            AverageValue avgVal = new AverageValue(0, 0);
            Move move = new Move(row, numToRemove, avgVal);
            return move;
        }

        public int MakeRowChoice(int max, Row[] rows)
        {
            int choice = 1;
            while (rows[choice - 1].RowSize == 0)
            {
                choice = MakeRandomChoice(max);
            }
            return choice;
        }

        public int MakeRandomChoice(int max)
        {
            int choice = rnd.Next(1, max + 1);
            return choice;
        }

        public override void PrintKnownMoves()
        {
            throw new NotImplementedException();
        }
    }
}
