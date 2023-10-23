namespace Automat.Logic.Calculators
{
    public class ColorfulCalculator : ICellValueCalculator
    {
        private static int rangeWidth = 1;
        private static int rangeHeight = 1;

        private static int neighborsAmount = 8;


        int ICellValueCalculator.Calculate(int[,] area)
        {
            var neighBorsSum = CalculateNeighborsSum(area);
            var avg = neighBorsSum / neighborsAmount;

            var randomGenerator = new Random();

            int randomNumber = randomGenerator.Next(avg / 2, (int)Math.Floor(avg + 0.5 * avg) );

            return randomNumber;
        }

        private int CalculateNeighborsSum(int[,] area)
        {
            return area.Cast<int>().Sum() - area[rangeWidth, rangeHeight];
        }

        int ICellValueCalculator.GetRangetHeight()
        {
            return rangeHeight;
        }

        int ICellValueCalculator.GetRangeWidth()
        {
            return rangeWidth;
        }
    }
}
