using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Logic.Calculators
{
    public class ConwayCalculator : ICellValueCalculator
    {
        private static int rangeWidth = 1;
        private static int rangeHeight = 1;

        // if i am dead and have exactly 3 neighbors alive, get alive
        // if i am alive and have 2 or 3 neighbors, stay alive
        // otherwise die
        int ICellValueCalculator.Calculate(int[,] area)
        {
            var neighBorsSum = CalculateNeighborsSum(area);

            if (area[rangeWidth,rangeHeight] == 0 && neighBorsSum == 3)
            {
                return 1;
            }
            else if (area[rangeWidth, rangeHeight] == 1 && new int[] { 2, 3 }.Contains(neighBorsSum))
            {
                return 1;
            }

            return 0;
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
