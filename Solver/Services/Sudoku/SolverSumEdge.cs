using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverSumEdge : ISolver
    {
        private readonly List<Coordinates>[,] _sumFields;
        private const int _priority = (int)Priority.Mid;

        public SolverSumEdge(List<Coordinates> sumFields, int rowCnt, int colCnt)
        {
            _sumFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _sumFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (Coordinates coordinates in sumFields)
            {
                switch (coordinates.Direction)
                {
                    case "S":
                        _sumFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Number = coordinates.Number });
                        _sumFields[coordinates.Row, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number });
                        break;
                    case "E":
                        _sumFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Number = coordinates.Number });
                        _sumFields[coordinates.Row - 1, coordinates.Col].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number });
                        break;
                }
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
                    foreach (Coordinates coordinates in _sumFields[i - 1, j - 1])
                        HelperBan.BanRangeInverse(grid, i, j, coordinates.Number - numRange, coordinates.Number - 1, numRange);
                }
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates coordinates in _sumFields[row - 1, col - 1])
                HelperBan.BanInverse(grid, coordinates.Row, coordinates.Col, coordinates.Number - number, numRange);
        }
    }
}