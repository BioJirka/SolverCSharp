using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverMinDifference : ISolver
    {
        private readonly List<Coordinates>[,] _minDifferenceFields;
        private const int _priorityBigDifference = (int)Priority.Mid;
        private const int _prioritySmallDifference = (int)Priority.Low;

        public SolverMinDifference(List<List<Coordinates>> minDifferenceFields, int rowCnt, int colCnt)
        {
            _minDifferenceFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _minDifferenceFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (List<Coordinates> area in minDifferenceFields)
            {
                for (int k = 0; k < area.Count - 1; k++)
                {
                    _minDifferenceFields[area[k].Row - 1, area[k].Col - 1].Add(new Coordinates { Row = area[k + 1].Row, Col = area[k + 1].Col, Number = area[0].Number });
                    _minDifferenceFields[area[k + 1].Row - 1, area[k + 1].Col - 1].Add(new Coordinates { Row = area[k].Row, Col = area[k].Col, Number = area[0].Number });
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    foreach (Coordinates coordinates in _minDifferenceFields[i - 1, j - 1])
                        grid.Fields[i - 1, j - 1].Priority += coordinates.Number > 3 ? _priorityBigDifference : _prioritySmallDifference;
                }
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            int maxDiff;
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    if (_minDifferenceFields[i - 1, j - 1].Count == 0)
                        continue;
                    maxDiff = _minDifferenceFields[i - 1, j - 1].Max(x => x.Number);
                    if (maxDiff <= numRange / 2)
                        continue;
                    HelperBan.BanRange(grid, i, j, numRange + 1 - maxDiff, maxDiff, numRange);
                }
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates coordinates in _minDifferenceFields[row - 1, col - 1])
                HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, number - coordinates.Number + 1, number + coordinates.Number - 1, numRange);
        }
    }
}