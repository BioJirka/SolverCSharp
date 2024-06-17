using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverDifference : ISolver
    {
        private readonly List<Coordinates>[,] _differenceFields;
        private const int _priority = (int)Priority.Mid;

        public SolverDifference(List<Coordinates> differenceFields, int rowCnt, int colCnt)
        {
            _differenceFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _differenceFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (Coordinates coordinates in differenceFields)
            {
                switch (coordinates.Direction)
                {
                    case "S":
                        _differenceFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Number = coordinates.Number });
                        _differenceFields[coordinates.Row, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number });
                        break;
                    case "E":
                        _differenceFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Number = coordinates.Number });
                        _differenceFields[coordinates.Row - 1, coordinates.Col].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number });
                        break;
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priority * _differenceFields[i - 1, j - 1].Count;
            }
        } 

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    foreach (Coordinates coordinates in _differenceFields[i - 1, j - 1])
                        HelperBan.BanRange(grid, i, j, numRange + 1 - coordinates.Number, coordinates.Number, numRange);
                }
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates coordinates in _differenceFields[row - 1, col - 1])
                HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, new List<int> { number - coordinates.Number, coordinates.Number + number }, numRange);
        }
    }
}