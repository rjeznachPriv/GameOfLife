using Automat.Logic.Calculators;
using Moq;
using System.Linq;
using System.Xml.Linq;

namespace Automat.Logic.Tests
{
    public class GameTests
    {
        private Calculators.ICellValueCalculator _calculator;
        private Game _game;
        private static int BOARD_SIZE = 3;

        private void GameAdvancedBy1Step(object sender, EventArgs e)
        {
        }

        [SetUp]
        public void Setup()
        {
            var calculatorMock = new Mock<ICellValueCalculator>();
            calculatorMock.Setup(s => s.Calculate(It.IsAny<int[,]>())).Returns(1);
            _calculator = calculatorMock.Object;
            _game = new Game(BOARD_SIZE, _calculator);
            Game.GameAdvancedBy1Step += this.GameAdvancedBy1Step;
        }

        [Test]
        public void Test1()
        {
            _game.NextStep();
            Assert.That(_game.Board.Flatten(), Is.EqualTo(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 }));
        }
    }

    public static class Helpers {
        public static int[] Flatten(this int[,] arg) {
            var list = new List<int>();
            for (var i = 0; i< arg.GetLength(0); i++)
            {
                for (var j = 0; j < arg.GetLength(1); j++) {
                    list.Add(arg[i, j]);
                }
            }

            return list.ToArray();
        }
    }
}
