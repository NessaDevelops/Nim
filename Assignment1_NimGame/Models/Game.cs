using Assignment1_NimGame.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class Game
    {
        const int _numRows = 3;
        const int row1Size = 3;
        const int row2Size = 5;
        const int row3Size = 7;

        private View view = new View();

        public Row[] _rows;

        private Dictionary<BoardState, Move> player1Turns = new Dictionary<BoardState, Move>();
        private Dictionary<BoardState, Move> player2Turns = new Dictionary<BoardState, Move>();

        // MY BOARD STATES CONTAIN A BOARD STATE (AMOUNT OF PIECES ON EACH ROW). MY MOVES CONTAIN THE ROW/# PIECES & AN AVERAGE 
        // VALUE WHICH INCLUDES THE ACTUAL WEIGHTED VALUE AND HOW MANY TIMES THE BOARD STATES HAS BEEN FOUND
        private static Dictionary<BoardState, List<Move>> boardStates = new Dictionary<BoardState, List<Move>>();

        public Player turn = Player.Player1;
        private NimPlayer computerPlayer1;
        private NimPlayer computerPlayer2;

        // # OF WINS EACH PLAYER HOLDS
        private int player1Wins = 0;
        private int player2Wins = 0;

        private bool gameOver;
        private int gameMode;
        private bool quitGame = false;
        

        public void Start()
        {
            computerPlayer1 = new RandomAI();
            computerPlayer2 = new SmartAI();

            gameMode = view.SelectGameMode();

            while (!quitGame)
            {
                // RESETS GAME TO INITIAL VALUES
                turn = Player.Player1;
                gameOver = false;
                player1Turns.Clear();
                player2Turns.Clear();
                _rows = new Row[] { new Row(row1Size), new Row(row2Size), new Row(row3Size) };

                // KEEPS PRINTING BOARD AND TAKING TURNS UNTIL NO PIECES LEFT
                while (!gameOver)
                {
                    PrintBoard();
                    TakeTurn(gameMode);
                    // CHECK FOR GAME OVER AFTER EACH TURN
                    GameOver();

                    if (gameOver)
                    {
                        EndGame();
                    }
                }
                // THIS IS WHERE I CHECK IF USER WANTS TO COMPLETELY EXIT GAME
                if (view.QuitGame())
                {
                    quitGame = true;
                }
            }
        }

        public void TakeTurn(int gamemode)
        {
            // TURN TAKING IS BASED ON GAME MODE
            switch (gamemode)
            {
                // PLAYER VS PLAYER
                case 0:
                    PlayerTurn();
                    break;
                // PLAYER VS AI
                case 1:
                    if (turn == Player.Player2)
                    {
                        ComputerTurn(computerPlayer2);
                    }
                    else
                    {
                        PlayerTurn();
                    }
                    break;
                // AI (RANDOM) VS AI (SMART/LEARNING)
                case 2:
                    if (turn == Player.Player1)
                    {
                        ComputerTurn(computerPlayer1);
                    }
                    else
                    {
                        ComputerTurn(computerPlayer2);
                    }
                    break;
            }
        }

        public void PlayerTurn()
        {
            // GETS USER INPUT FOR ROW / PIECES
            int row = view.SelectRow(turn, _rows);
            int numToRemove = view.SelectPieces(row, _rows);

            MakeMove(row, numToRemove);
        }

        public void ComputerTurn(NimPlayer computerPlayer)
        {
            int row, numToRemove;
            // COMPUTER 2 USES LEARNING SYSTEM
            if (turn == Player.Player2)
            {
                Move move = computerPlayer.MakeMove(_rows, boardStates);
                row = move.Row;
                numToRemove = move.NumToRemove;
            }
            // COMPUTER 1 DOES NOT USE LEARNING SYSTEM
            else
            {
                Move move = computerPlayer.MakeMove(_rows, boardStates);
                row = move.Row;
                numToRemove = move.NumToRemove;
            }
            Console.WriteLine("Computer " + turn + " takes " + numToRemove + " from row " + row);
            
            MakeMove(row, numToRemove);
            Console.WriteLine(_rows[0].RowSize + "/" + _rows[1].RowSize + "/" + _rows[2].RowSize);
        }

        public void ChangeTurn()
        {
            if (turn.Equals(Player.Player1))
            {
                turn = Player.Player2;
            }
            else
            {
                turn = Player.Player1;
            }
        }

        public void MakeMove(int row, int numToRemove)
        {
            if (_rows[row - 1].RemovePieces(numToRemove))
            {
                ChangeTurn();

                // ADD COMPLETED MOVE TO LIST OF BOARD STATES FOR WHAT PLAYER MADE IT
                if (turn == Player.Player1)
                {
                    player1Turns.Add((new BoardState(_rows[0].RowSize, _rows[1].RowSize, _rows[2].RowSize)), new Move(row, numToRemove, new AverageValue(0, 0)));
                }
                else
                {
                    player2Turns.Add((new BoardState(_rows[0].RowSize, _rows[1].RowSize, _rows[2].RowSize)), new Move(row, numToRemove, new AverageValue(0, 0)));
                }
            }
        }

        public void GameOver()
        {
            gameOver = true;
            for (int j = 0; j < _rows.Count(); ++j)
            {
                if (_rows[j].RowSize != 0)
                {
                    gameOver = false;
                    break;
                }
            }
        }

        public void EndGame()
        {
            IncrementWins();
            view.EndGame(boardStates, turn, player1Wins, player2Wins);
            StoreBoardStates();
        }

        public void IncrementWins()
        {
            if (turn == Player.Player1)
            {
                ++player1Wins;
            }
            else
            {
                ++player2Wins;
            }
        }

        public void StoreBoardStates()
        {
            decimal value = 0;
            decimal min = 1;
            int length = player1Turns.Count();

            foreach (KeyValuePair<BoardState, Move> item in player1Turns)
            {
                var negativeOrPostive = turn == Player.Player1 ? -1 : 1;

                value = negativeOrPostive * min / length;

                ++min;

                if (!IsStateStored(item.Key))
                {
                    boardStates.Add(item.Key, new List<Move>()
                        {
                            new Move(item.Value.Row, item.Value.NumToRemove, new AverageValue(value, 1))
                        }
                    );
                }
                else
                {
                    if (!IsMoveStored(item.Key, item.Value))
                    {
                        foreach(KeyValuePair<BoardState, List<Move>> item2 in boardStates)
                        {
                            if(item2.Key.ToString() == item.Key.ToString())
                            {
                                item2.Value.Add(new Move(item.Value.Row, item.Value.NumToRemove, new AverageValue(value, 1)));
                            }
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<BoardState, List<Move>> item2 in boardStates)
                        {
                            if (item2.Key.ToString() == item.Key.ToString())
                            {
                                foreach(Move thatMove in item2.Value)
                                {
                                    if(thatMove.Row == item.Value.Row && thatMove.NumToRemove == item.Value.NumToRemove)
                                    {
                                        var average = thatMove.AverageValue;
                                        thatMove.AverageValue = new AverageValue(average.GetValue + value / average.GetCount, average.GetCount + 1);
                                    }
                                }                                    
                            }
                        }
                    }
                }
            }

            value = 0;
            min = 1;
            length = player2Turns.Count();

            foreach (KeyValuePair<BoardState, Move> item in player2Turns)
            {
                var negativeOrPostive = turn == Player.Player1 ? 1 : -1;

                value = negativeOrPostive * min / length;

                ++min;

                if (!IsStateStored(item.Key))
                {

                    boardStates.Add(item.Key, new List<Move>()
                        {
                            new Move(item.Value.Row, item.Value.NumToRemove, new AverageValue(value, 1))
                        }
                    );
                }
                else
                {

                    if (!IsMoveStored(item.Key, item.Value))
                    {
  
                        foreach (KeyValuePair<BoardState, List<Move>> item2 in boardStates)
                        {
                            if (item2.Key.ToString() == item.Key.ToString())
                            {
                                item2.Value.Add(new Move(item.Value.Row, item.Value.NumToRemove, new AverageValue(value, 1)));
                            }
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<BoardState, List<Move>> item2 in boardStates)
                        {
                            if (item2.Key.ToString() == item.Key.ToString())
                            {
                                foreach (Move thatMove in item2.Value)
                                {
                                    if (thatMove.Row == item.Value.Row && thatMove.NumToRemove == item.Value.NumToRemove)
                                    {
                                        var average = thatMove.AverageValue;
                                        thatMove.AverageValue = new AverageValue(average.GetValue + value / average.GetCount, average.GetCount + 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool IsStateStored(BoardState state)
        {
            bool duplicate = false;
            foreach(KeyValuePair<BoardState, List<Move>> item in boardStates)
            {
                if(item.Key.ToString().Equals(state.ToString()))
                {
                    duplicate = true;
                }
            }
            return duplicate;
        }

        public bool IsMoveStored(BoardState state, Move move)
        {
            bool duplicate = false;
            
            foreach(KeyValuePair<BoardState, List<Move>> item in boardStates)
            {
                if(item.Key.ToString() == state.ToString())
                {
                    List<Move> myMoves = item.Value;
                    foreach(Move thisMove in myMoves)
                    {
                        if(thisMove.Row == move.Row && thisMove.NumToRemove == move.NumToRemove)
                        {
                            duplicate = true;
                        }
                    }
                }
            }
            return duplicate;
        }

        public void PrintBoard()
        {
            for (int j = 0; j < _rows.Count(); ++j)
            {
                _rows[j].printRow();
            }
        }
        

        // UNIT TESTING METHOD
        public static bool GoodMove(BoardState state)
        {
            bool goodMove = false;
            decimal value = 0;
            foreach(KeyValuePair<BoardState, List<Move>> item in boardStates)
            {
                if(item.Key.ToString() == state.ToString())
                {
                    foreach(Move thisMove in item.Value)
                    {
                        value += thisMove.AverageValue.GetValue;
                        if(value >= 0)
                        {
                            goodMove = true;
                        }
                    }
                }
            }
            return goodMove;
        }
    }
}