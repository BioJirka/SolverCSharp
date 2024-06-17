using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverSumCorner : ISolver
    {
        private readonly List<List<Coordinates>>[,] _sumFields;
        private const int _priority = (int)Priority.Low;

        public SolverSumCorner(List<Coordinates> sumFields, int rowCnt, int colCnt)
        {
            _sumFields = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _sumFields[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in sumFields)
            {
                _sumFields[coordinates.Row - 1, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _sumFields[coordinates.Row - 1, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _sumFields[coordinates.Row, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _sumFields[coordinates.Row, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priority * _sumFields[i - 1, j - 1].Count;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    foreach (List<Coordinates> area in _sumFields[i - 1, j - 1])
                        HelperSolver.BanSum(grid, i, j, numRange, area[0].Number, 4);
                }
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            int subSum;
            int emptyCnt;
            foreach (List<Coordinates> area in _sumFields[row - 1, col - 1])
            {
                subSum = 0;
                emptyCnt = 0;
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        emptyCnt++;
                    else
                        subSum += grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number;
                }
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        HelperSolver.BanSum(grid, coordinates.Row, coordinates.Col, numRange, area[0].Number - subSum, emptyCnt);
                }
            }
        }
    }
}