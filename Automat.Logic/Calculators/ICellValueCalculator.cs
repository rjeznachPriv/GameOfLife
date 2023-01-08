namespace Automat.Logic.Calculators
{
    public interface ICellValueCalculator
    {
        public int GetRangetHeight();
        public int GetRangeWidth();
        public int Calculate(int[,] area);
    }
}
