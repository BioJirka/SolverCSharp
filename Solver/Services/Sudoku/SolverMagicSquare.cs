using Solver.Sudoku.Models;

namespace Solver.Sudoku.Services
{
    public class SolverMagicSquare : ISolver
    {
        private readonly List<List<Coordinates>> _magicSquareFields;
        private readonly List<List<Coordinates>>[,] _magicSquareFieldsMapping;
        private readonly List<List<int>> _indexCombinations;
        private const int _priority = (int)Priority.High;

        public SolverMagicSquare(List<Coordinates> magicSquareFields, int rowCnt, int colCnt)
        {
            _magicSquareFields = new List<List<Coordinates>>();
            _magicSquareFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            _indexCombinations = new List<List<int>> {
                new List<int> { 0, 1, 2 }, 
                new List<int> { 3, 4, 5 }, 
                new List<int> { 6, 7, 8 }, 
                new List<int> { 0, 3, 6 }, 
                new List<int> { 1, 4, 7 }, 
                new List<int> { 2, 5, 8 }, 
                new List<int> { 0, 4, 8 }, 
                new List<int> { 2, 4, 6 }, 
                };
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _magicSquareFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in magicSquareFields)
            {
                _magicSquareFields.Add(new List<Coordinates> { 
                    new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, 
                    new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, 
                    new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 2 }, 
                    new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, 
                    new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 }, 
                    new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 2 }, 
                    new Coordinates { Row = coordinates.Row + 2, Col = coordinates.Col }, 
                    new Coordinates { Row = coordinates.Row + 2, Col = coordinates.Col + 1 }, 
                    new Coordinates { Row = coordinates.Row + 2, Col = coordinates.Col + 2 }, 
                });
            }
            foreach (List<Coordinates> area in _magicSquareFields)
            {
                foreach (Coordinates coordinates in area)
                    _magicSquareFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(area);
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (List<Coordinates> area in _magicSquareFields)
            {
                foreach (Coordinates coordinates in area)
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            List<int> numbers;
            int sum = 0;
            int subSum;
            int emptyCnt;
            foreach (List<Coordinates> area in _magicSquareFieldsMapping[row - 1, col - 1])
            {
                numbers = new List<int>();
                foreach (Coordinates coordinates in area)
                    numbers.Add(grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number);
                foreach (List<int> indexes in _indexCombinations)
                {
                    if (numbers[indexes[0]] != 0 && numbers[indexes[1]] != 0 && numbers[indexes[2]] != 0)
                    {
                        sum = numbers[indexes[0]] + numbers[indexes[1]] + numbers[indexes[2]];
                        break;
                    }
                }
                if (sum == 0)
                    continue;
                foreach (List<int> indexes in _indexCombinations)
                {
                    subSum = 0;
                    emptyCnt = 3;
                    foreach (int index in indexes)
                    {
                        if (numbers[index] != 0)
                        {
                            subSum += numbers[index];
                            emptyCnt--;
                        }
                    }
                    foreach (int index in indexes)
                    {
                        if (grid.Fields[area[index].Row - 1, area[index].Col - 1].Number == 0)
                            HelperSolver.BanSum(grid, area[index].Row, area[index].Col, numRange, sum - subSum, emptyCnt);
                    }
                }
            }
        }
    }
}