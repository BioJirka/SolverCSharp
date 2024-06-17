using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverEvenOddArea : ISolver
    {
        private readonly List<int> _evenNumbers;
        private readonly List<int> _oddNumbers;
        private readonly List<List<Coordinates>> _evenOddAreaFields;
        private readonly List<List<Coordinates>>[,] _evenOddAreaFieldsMapping;
        private const int _priorityBigArea = (int)Priority.Mid;
        private const int _prioritySmallArea = (int)Priority.Low;

        public SolverEvenOddArea(List<List<Coordinates>> evenOrOddFields, int rowCnt, int colCnt, int numRange)
        {
            _evenNumbers = new List<int>();
            _oddNumbers = new List<int>();
            _evenOddAreaFields = evenOrOddFields;
            _evenOddAreaFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int k = 2; k <= numRange; k += 2)
                _evenNumbers.Add(k);
            for (int k = 1; k <= numRange; k += 2)
                _oddNumbers.Add(k);
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _evenOddAreaFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (List<Coordinates> area in _evenOddAreaFields)
            {
                foreach (Coordinates coordinates in area)
                    _evenOddAreaFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(area);
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            int priority;
            foreach (List<Coordinates> area in _evenOddAreaFields)
            {
                priority = area.Count < 4 ? _prioritySmallArea : _priorityBigArea;
                foreach (Coordinates coordinates in area)
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            bool numberIsEven = number % 2 == 0;
            int nonEmptyCount;
            foreach (List<Coordinates> area in _evenOddAreaFieldsMapping[row - 1, col - 1])
            {
                nonEmptyCount = 0;
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number != 0)
                        nonEmptyCount++;
                }
                if (nonEmptyCount > 1)
                    continue;
                foreach (Coordinates coordinates in area)
                {   
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                    {
                        if (numberIsEven)
                            HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _oddNumbers, numRange);
                        else
                            HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _evenNumbers, numRange);
                    }
                }
            }
        }
    }
}