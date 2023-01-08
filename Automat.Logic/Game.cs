using Automat.Logic.Calculators;
using System.Linq;

namespace Automat.Logic
{
    public class Game
    {
        private ICellValueCalculator _cellValueCalculator;

        public int[,] Board { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public static int DEFAULT_FOR_NULL = 0;

        public static event EventHandler GameAdvancedBy1Step;

        public Game(int size, ICellValueCalculator cellValueCalculator)
        {
            Board = new int[size, size];

            Width = Board.GetLength(0);
            Height = Board.GetLength(0);

            _cellValueCalculator = cellValueCalculator;
        }

        public Game(int[,] board, ICellValueCalculator cellValueCalculator)
        {
            if (board.Rank != 2)
            {
                throw new Exception("Array dimension must be 2!");
            }

            if (false)
            {    //TODO: check if not jagged and square
                throw new Exception("Array must not be jagged, array must be square!");
            }

            Board = board;

            Width = Board.GetLength(0);
            Height = Board.GetLength(0);

            _cellValueCalculator = cellValueCalculator;

        }

        public int NextStep()
        {
            var newBoard = new int[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int newValue = CalculateCellValue(x, y);
                    newBoard[x, y] = newValue;
                }
            }

            Board = newBoard;       // TODO: ref? value?

            GameAdvancedBy1Step(this, EventArgs.Empty);
            return 0;
        }

        private int CalculateCellValue(int x, int y)
        {
            var area = CreateAreaForField(x, y);
            return _cellValueCalculator.Calculate(area);
        }

        private int[,] CreateAreaForField(int givenX, int givenY)
        {
            int[,] area = new int[_cellValueCalculator.GetRangeWidth() * 2 + 1, _cellValueCalculator.GetRangetHeight() * 2 + 1];
            int areaY = 0;
            for (int boardY = givenY - _cellValueCalculator.GetRangetHeight(); areaY < area.GetLength(0); boardY++)
            {
                int areaX = 0;
                for (int boardX = givenX - _cellValueCalculator.GetRangeWidth(); areaX < area.GetLength(0); boardX++)
                {
                    if (boardX >= 0 && boardY >= 0 && boardX < Board.GetLength(0) && boardY < Board.GetLength(0))
                        area[areaX, areaY] = Board[boardX, boardY];
                    else
                        area[areaX, areaY] = DEFAULT_FOR_NULL;
                    areaX++;
                }
                areaY++;
            }
            return area;
        }

        public void ShiftConwayField(Tuple<int, int> coordinates)
        {
            Board[coordinates.Item1, coordinates.Item2] = 1 - Board[coordinates.Item1, coordinates.Item2];
        }
    }
}