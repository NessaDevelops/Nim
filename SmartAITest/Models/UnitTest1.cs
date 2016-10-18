using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment1_NimGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Models.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void GoodMoveTest()
        {
            // arrange
            BoardState state = new BoardState(0, 1, 1);
            bool expected = false;
            // act
            
            bool actual = Assignment1_NimGame.Models.Game.GoodMove(state);
            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}