using Moq;
using NUnit.Framework;
using SnakesAndLadders.ClassLibrary;
using System.Collections.Generic;
using SnakesAndLadders.ClassLibrary.Configs;
using SnakesAndLadders.ClassLibrary.Contracts;
using SnakesAndLadders.ClassLibrary.Impl;

namespace SnakesAndLadders.UnitTest
{
    public class SnakeAndLadderTest
    {

        /* Given the game is started
        When the token is placed on the board
        Then the token is on square 1 */

        [Test]
        [TestCase(1,100)]
        public void GivenNewGame_TokensAreInSquare1(int initialPosition, int lastPosition, short diceMinValue = 1, short diceMaxValue = 6)
        {
            var mockDice = new Mock<IDice>();
            var boardConfig = new BoardConfig(initialPosition, lastPosition);
            var board = new Board(mockDice.Object);

            board.InitializeNewGame(new List<Token>() 
            {
                new Token(),
                new Token()
            }, new DiceConfig(diceMinValue, diceMaxValue), boardConfig);
            
            foreach(var player in board.TokenPlayers)
            {
                Assert.AreEqual(player.CurrentPosition, initialPosition);
            }
        }

        /* Given the token is on square 1
        When the token is moved 3 spaces
        Then the token is on square 4 */

        [Test]
        [TestCase(3, 4)]
        public void GivenTheTokenOnSquare1_TheTokenIsMoved3Spaces_TokenIsIn4Square(int movements, int finalPosition, int initialPosition = 1, int boardFinalPosition = 100)
        {
            var player = new Token();
            player.CurrentPosition = initialPosition;

            player.Movement(movements, boardFinalPosition);
            Assert.AreEqual(player.CurrentPosition, finalPosition);
            
        }

        /* Given the token is on square 1
        When the token is moved 3 spaces
        And then it is moved 4 spaces
        Then the token is on square 8 */

        [Test]
        [TestCase(3, 4, 8)]
        public void GivenTheTokenOnSquare1_TheTokenIsMoved3Spaces_ThenTheTokenIsMoved4Spaces_TokenIsIn8Square(int firsMovement, int secondMovement, int finalPosition, int boardInitialPosition = 1, int boardFinalPosition = 100)
        {
            var player = new Token();
            player.CurrentPosition = boardInitialPosition;
            player.Movement(firsMovement, boardFinalPosition);
            player.Movement(secondMovement, boardFinalPosition);
            Assert.AreEqual(player.CurrentPosition, finalPosition);
        }



        /* Given the token is on square 97
        When the token is moved 3 spaces
        Then the token is on square 100
        And the player has won the game */

        [Test]
        [TestCase(97, 3)]
        public void GivenTheTokenOnSquare97_TheTokenIsMoved3Spaces_ThenTheTokenWins(int initialPosition, int movement, int boardInitialPosition = 1, int boardFinalPosition = 100)
        {

            var mockDice = new Mock<IDice>();
            var boardConfig = new BoardConfig(boardInitialPosition, boardFinalPosition);
            var board = new Board(mockDice.Object);
            board.BoardConfig = boardConfig;
            var player = new Token();
            player.CurrentPosition = initialPosition;
            player.Movement(movement, boardFinalPosition);

            board.TokenPlayers.Add(player);

            Assert.IsTrue(board.HasWon(player));

        }


        /* Given the token is on square 97
        When the token is moved 4 spaces
        Then the token is on square 97
        And the player has not won the game */

        [Test]
        [TestCase(97, 4)]
        public void GivenTheTokenOnSquare97_TheTokenIsMoved4Spaces_ThenTheTokenContinuesInSquare97(int initialPosition, int movement, int boardInitialPosition = 1, int boardFinalPosition = 100)
        {

            var boardConfig = new BoardConfig(boardInitialPosition, boardFinalPosition);
            var mockDice = new Mock<IDice>();
            var board = new Board(mockDice.Object);
            board.BoardConfig = boardConfig;

            var player = new Token();
            player.CurrentPosition = initialPosition;
            player.Movement(movement, boardFinalPosition);

            board.TokenPlayers.Add(player);

            Assert.AreEqual(player.CurrentPosition,initialPosition);
            Assert.IsFalse(board.HasWon(player));

        }


        /* Given the game is started
        When the player rolls a die
        Then the result should be between 1-6 inclusive */

        [Test]
        [TestCase(1, 6)]
        public void WhenThePlayerRollsADice_ThenTheResultShouldBeBetween1And6(short minValue, short maxValue)
        {
            var diceConfig = new DiceConfig(minValue, maxValue);
            var dice = new Dice();
            dice.AddConfig(diceConfig);

            Assert.IsTrue(dice.Roll() >= minValue);
            Assert.IsTrue(dice.Roll() <= maxValue);
        }


        /* Given the player rolls a 4
        When they move their token
        Then the token should move 4 spaces */

        [Test]
        [TestCase(4)]
        public void GivenThePlayerRolls4_ThenTheTokenShouldMove4Spaces(int movement, int boardInitialPosition = 1, int boardFinalPosition = 100)
        {

            var mockDice = new Mock<IDice>();
            mockDice.Setup(p => p.Roll()).Returns(movement);


            var boardConfig = new BoardConfig(boardInitialPosition, boardFinalPosition);
            var board = new Board(mockDice.Object);
            board.BoardConfig = boardConfig;

            var player = new Token();
            player.CurrentPosition = boardInitialPosition;
            board.TokenPlayers.Add(player);
            board.PlayerMovement(player);

            Assert.AreEqual(player.CurrentPosition, boardInitialPosition + movement);

        }
    }
}
