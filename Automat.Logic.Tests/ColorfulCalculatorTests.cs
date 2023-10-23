using Automat.Logic.Calculators;
using Newtonsoft.Json;

namespace Automat.Logic.Tests
{
    public class ColorfulCalculatorTests
    {
        private ICellValueCalculator _calculator;
        [SetUp]
        public void Setup()
        {
            _calculator = new ColorfulCalculator();
        }

        // if i am dead and have exactly 3 neighbors alive, get alive
        // if i am alive and have 2 or 3 neighbors, stay alive
        // otherwise die
        [Test]
        [TestCase("[[64,0,0],[0,0,0],[0,0,0]]", 4, 12)]
        [TestCase("[[0,0,0], [64,0,0],[0,0,0]]", 4, 12)]
        [TestCase("[[64,0,64],[64,0,64],[0,0,0]]", 16, 48)]
        public void Test1(string areaJSON, int graterThan, int lowerThan)
        {
            int[,] area = JsonConvert.DeserializeObject<int[,]>(areaJSON);
            var actualResult = _calculator.Calculate(area);

            Assert.That(actualResult, Is.GreaterThanOrEqualTo(graterThan));
            Assert.That(actualResult, Is.LessThanOrEqualTo(lowerThan));
        }
    }
}