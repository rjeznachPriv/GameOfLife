using Automat.Logic.Calculators;
using Newtonsoft.Json;

namespace Automat.Logic.Tests
{
    public class ConwayCalculatorTests
    {
        private ICellValueCalculator _calculator;
        [SetUp]
        public void Setup()
        {
            _calculator = new ConwayCalculator();
        }

        // if i am dead and have exactly 3 neighbors alive, get alive
        // if i am alive and have 2 or 3 neighbors, stay alive
        // otherwise die
        [Test]
        [TestCase("[[1,1,1],[0,0,0],[0,0,0]]", 1)]
        [TestCase("[[0,0,0],[0,0,0],[1,1,1]]", 1)]
        
        [TestCase("[[0,0,0],[0,1,0],[1,1,1]]", 1)]
        [TestCase("[[0,0,0],[0,1,0],[0,1,1]]", 1)]

        [TestCase("[[1,1,0],[0,1,0],[0,0,0]]", 1)]
        [TestCase("[[1,1,0],[0,1,0],[1,0,0]]", 1)]

        [TestCase("[[1,1,1],[1,1,1],[1,1,1]]", 0)]
        [TestCase("[[1,1,1],[1,1,1],[1,1,0]]", 0)]
        [TestCase("[[1,1,1],[1,1,1],[1,0,0]]", 0)]
        [TestCase("[[1,1,1],[1,1,1],[0,0,0]]", 0)]
        [TestCase("[[1,1,1],[1,1,0],[0,0,0]]", 0)]
        [TestCase("[[0,0,1],[0,0,0],[0,0,0]]", 0)]
        [TestCase("[[0,0,0],[0,0,0],[0,0,0]]", 0)]
        public void Test1(string areaJSON, int expectedResult)
        {
            int[,] area = JsonConvert.DeserializeObject<int[,]>(areaJSON);
            var actualResult = _calculator.Calculate(area);

            Assert.That(actualResult, Is.GreaterThanOrEqualTo(0));
            Assert.That(actualResult, Is.LessThanOrEqualTo(1));
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}