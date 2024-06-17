using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverThermometer : ISolver
    {
        private readonly List<Coordinates>[,] _thermometerFields;
        private const int _priority = (int)Priority.Low;

        public SolverThermometer(List<List<Coordinates>> thermometerFields, int rowCnt, int colCnt)
        {
            _thermometerFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _thermometerFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (List<Coordinates> area in thermometerFields)
            {
                for (int k = 0; k < area.Count; k++)
                {
                    for (int kk = k + 1; kk < area.Count; kk++)
                    {
                        _thermometerFields[area[k].Row - 1, area[k].Col - 1].Add(new Coordinates { Row = area[kk].Row, Col = area[kk].Col, Number = kk - k, Text = "Higher" });
                        _thermometerFields[area[kk].Row - 1, area[kk].Col - 1].Add(new Coordinates { Row = area[k].Row, Col = area[k].Col, Number = kk - k, Text = "Lower" });
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i < rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priority * _thermometerFields[i - 1, j - 1].Count;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            int[,][] InequalityInitialBan = new int[rowCnt, colCnt][];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    InequalityInitialBan[i - 1, j - 1] = new int[2] { 1, numRange };
            }
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    foreach (Coordinates coordinates in _thermometerFields[i - 1, j - 1])
                    {
                        if (coordinates.Text == "Higher")
                        {
                            InequalityInitialBan[i - 1, j - 1][1] = Math.Min(InequalityInitialBan[i - 1, j - 1][1], numRange - coordinates.Number);
                            InequalityInitialBan[coordinates.Row - 1, coordinates.Col - 1][0] = Math.Max(InequalityInitialBan[coordinates.Row - 1, coordinates.Col - 1][0], coordinates.Number + 1);
                        }
                    }
                }
            }
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    HelperBan.BanRangeInverse(grid, i, j, InequalityInitialBan[i - 1, j - 1][0], InequalityInitialBan[i - 1, j - 1][1], numRange);
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates coordinates in _thermometerFields[row - 1, col - 1])
            {
                if (coordinates.Text == "Higher")
                    HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, 1, number + coordinates.Number - 1, numRange);
                else
                    HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, number - coordinates.Number + 1, numRange, numRange);
            }
        }
    }
}