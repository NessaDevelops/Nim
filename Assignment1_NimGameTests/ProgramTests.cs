using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment1_NimGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_NimGame.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void TestGame011()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 1, 1);
            bool expected = false;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame057()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 5, 7);
            bool expected = false;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame054()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 5, 4);
            bool expected = true;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame013()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 1, 3);
            bool expected = false;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame016()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 1, 6);
            bool expected = false;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame036()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 3, 6);
            bool expected = true;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame032()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 3, 2);
            bool expected = true;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame052()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 5, 2);
            bool expected = false;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestGame046()
        {
            // arrange
            Models.BoardState state = new Models.BoardState(0, 4, 6);
            bool expected = true;
            // act
            bool actual = Program.TestGame(state);
            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}