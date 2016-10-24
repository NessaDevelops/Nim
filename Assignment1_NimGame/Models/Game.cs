using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models
{
    public class Game
    {
        // ARE NEEDED IN MULTIPLE FUNCTIONS ... GLOBALLY REQUIRED
        private View view = new View();
        public Row[] _rows;

        private List<Player> players = new List<Player>();
        private Player currentTurn;

        // MY BOARD STATES CONTAIN A BOARD STATE (AMOUNT OF PIECES ON EACH ROW). MY MOVES CONTAIN THE ROW/# PIECES & AN AVERAGE 
        // VALUE WHICH INCLUDES THE ACTUAL WEIGHTED VALUE AND HOW MANY TIMES THE BOARD STATES HAS BEEN FOUND
        private static Dictionary<BoardState, List<Move>> boardStates = new Dictionary<BoardState, List<Move>>();
        private BoardState previousState;

        private bool gameOver;
        
        public bool Start(bool testGame, BoardState testState)
        {
            int testGamesPlayed = 0;
            bool testStateGood = true;

            int gameMode;
            bool quitGame = false;

            // FOR TESTING GAME SETS GAME MODE TO AI VS AI 
            if (testGame)
            {
                gameMode = 2;
                CreatePlayers(gameMode);
            }
            else // IF NOT TESTING ASKS USER TO PICK GAME MODE
            {
                gameMode = view.SelectGameMode();
                CreatePlayers(gameMode);
            }

            while (!quitGame)
            {
                const int row1Size = 3;
                const int row2Size = 5;
                const int row3Size = 7;

                // RESETS GAME TO INITIAL VALUES
                gameOver = false;
                foreach (Player player in players)
                {
                    player.Moves.Clear();
                }
                currentTurn = players[0];
                _rows = new Row[] { new Row(row1Size), new Row(row2Size), new Row(row3Size) };
                previousState = new BoardState(_rows[0].RowSize, _rows[1].RowSize, _rows[2].RowSize);

                // KEEPS PRINTING BOARD AND TAKING TURNS UNTIL NO PIECES LEFT
                while (!gameOver)
                {
                    PrintBoard();
                    TakeTurn();

                    // CHECK FOR GAME OVER AFTER EACH TURN
                    GameOver();

                    if (gameOver)
                    {
                        EndGame();
                    }
                    ChangeTurn();
                }
                // IF IT'S NOT A TEST GAME I CHECK IF USER WANTS TO PLAY ANOTHER GAME
                if (!testGame)
                {
                    if (view.QuitGame())
                    {
                        quitGame = true;
                    }
                } else // IF IT IS A TEST GAME, INCREASE NUMBER OF TEST GAMES DONE BY 1
                {
                    ++testGamesPlayed;
                    // CHECK TO SEE IF 200 TEST GAMES WERE PLAYED... IF SO QUIT GAME
                    if (testGamesPlayed == 800)
                    {
                        testStateGood = TestBoardState(testState);
                        quitGame = true;
                    }
                }
            }
            return testStateGood;
        }

        public void CreatePlayers(int gameMode)
        {
            Player player1 = new RealPlayer();
            Player player2 = new RealPlayer();
            Player randomAi = new RandomAI();
            Player smartAi = new SmartAI();

            currentTurn = player1;

            switch (gameMode)
            {
                case 0:
                    players.Add(player1);
                    players.Add(player2);
                    break;
                case 1:
                    players.Add(player1);
                    players.Add(smartAi);
                    break;
                case 2:
                    currentTurn = randomAi;
                    players.Add(randomAi);
                    players.Add(smartAi);
                    break;
            }
        }

        public void TakeTurn()
        {
            MakeMove(currentTurn.Turn(currentTurn, _rows, boardStates));
            Console.WriteLine(_rows[0].RowSize + "/" + _rows[1].RowSize + "/" + _rows[2].RowSize);
        }

        public void ChangeTurn()
        {
            int index = players.FindIndex(i => i == currentTurn);
            if (index + 1 < players.Count())
            {
                currentTurn = players[index + 1];
            } else
            {
                currentTurn = players[0];
            }
        }

        public void MakeMove(Move chosenMove)
        {
            previousState = new BoardState(_rows[0].RowSize, _rows[1].RowSize, _rows[2].RowSize);
            if (_rows[chosenMove.Row - 1].RemovePieces(chosenMove.NumToRemove))
            {
                currentTurn.Moves.Add(previousState, new Move(chosenMove.Row, chosenMove.NumToRemove, new AverageValue(0, 0)));
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
            currentTurn.Wins++;
            GetBoardStates();
            view.EndGame(boardStates, currentTurn, players[0], players[1]);
        }

        public void GetBoardStates()
        {
            int negativeOrPostive;
            foreach (Player player in players)
            {
                if (player == currentTurn)
                {
                    negativeOrPostive = 1;
                } else
                {
                    negativeOrPostive = -1;
                }
                StoreBoardStates(player.Moves, negativeOrPostive);
            }
        }

        public void StoreBoardStates(Dictionary<BoardState, Move> playerTurns, decimal negativeOrPostive)
        {
            decimal value = 0;
            decimal min = 1;
            int length = playerTurns.Count();
            foreach (KeyValuePair<BoardState, Move> item in playerTurns)
            {
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

        public static bool TestBoardState(BoardState state)
        {
            bool goodState = true;
            decimal moveValue = 0;

            foreach(KeyValuePair<BoardState, List<Move>> item in boardStates)
            {
                if(item.Key.ToString() == state.ToString())
                {
                    foreach(Move thisMove in item.Value)
                    {
                        moveValue += thisMove.AverageValue.GetValue;
                    }
                }
            }

            if (moveValue < 0)
            {
                goodState = false;
            }
            return goodState;
        }
    }
}