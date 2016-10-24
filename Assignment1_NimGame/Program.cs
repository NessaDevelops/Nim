using Assignment1_NimGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BoardState testState = new BoardState(0, 0, 0);
            Game game = new Game();
            game.Start(false, testState);
        }

        public static bool TestGame(BoardState testState)
        {
            bool goodState;
            Game game = new Game();
            goodState = game.Start(true, testState);
            return goodState;
        }
    }
}
