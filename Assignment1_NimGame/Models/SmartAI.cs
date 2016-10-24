using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class SmartAI : Player
    {
        private Random rnd = new Random();

        private Dictionary<BoardState, int> boardStates = new Dictionary<BoardState, int>();

        public int MakeRowChoice(int max, Row[] rows)
        {
            int choice = 1;
            while (rows[choice - 1].RowSize == 0)
            {
                choice = MakeRandomChoice(max);
            }
            return choice;
        }


        public override Move MakeMove(Row[] rows, Dictionary<BoardState, List<Move>> boardStates)
        {
            bool madeSmartMove = false;

            int row = MakeRowChoice(rows.Count(), rows);
            int numToRemove = MakeRandomChoice(rows[row - 1].RowSize);
            AverageValue avgVal = new AverageValue(0, 0);

            Move move = new Move(row, numToRemove, avgVal);

            // CHECK TO SEE IF CURRENT BOARD STATE IS SAVED IN KNOWN MOVES
            while (!madeSmartMove)
            {
                foreach (KeyValuePair<BoardState, List<Move>> item in boardStates)
                {
                    if (item.Key.CheckIfStatesSame(rows) && !madeSmartMove)
                    {
                        foreach (Move thisMove in item.Value)
                        {
                            if (!madeSmartMove && thisMove.AverageValue.GetValue >= 0)
                            {
                                row = thisMove.Row;
                                numToRemove = thisMove.NumToRemove;

                                if (rows[row - 1].RemovePieces(numToRemove))
                                {
                                    madeSmartMove = true;
                                }
                            }
                        }
                    }
                }
                madeSmartMove = true;
            }
            return move;
        }

        public int MakeRandomChoice(int max)
        {
            int choice = rnd.Next(1, max + 1);
            return choice;
        }

        public int BoardStateValues(BoardState boardState)
        {
            var value = 0;

            if(boardStates.ContainsKey(boardState))
            {
                value = boardStates[boardState];
            }

            return value;
        }

        public override void PrintKnownMoves()
        {
            throw new NotImplementedException();
        }
    }
}