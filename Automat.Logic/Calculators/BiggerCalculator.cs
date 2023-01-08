using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Logic.Calculators
{
    public class BiggerCalculator : ICellValueCalculator
    {
        private static int rangeWidth = 2;
        private static int rangeHeight = 2;

        int ICellValueCalculator.Calculate(int[,] area)
        {
            var neighBorsSum = CalculateNeighborsSum(area);

            if (area[rangeWidth, rangeHeight] == 0 && neighBorsSum == 4)
            {
                return 1;
            }
            else if (area[rangeWidth, rangeHeight] == 1 && new int[] { 2, 3, 4, 5 }.Contains(neighBorsSum)) {
                return 1;
            }

            return 0;
        }

        private int CalculateNeighborsSum(int[,] area)
        {
            return area.Cast<int>().Sum() - area[rangeWidth,rangeHeight];
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
