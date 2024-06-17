using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverInequality : ISolver
    {
        private readonly List<Coordinates>[,] _inequalityFields;
        private const int _priority = (int)Priority.Low;

        public SolverInequality(List<Coordinates> inequalityFields, int rowCnt, int colCnt, int numRange)
        {
            _inequalityFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _inequalityFields[i - 1, j - 1] = new List<Coordinates>();
            }
            int[,,,] inequalityMapping = new int[rowCnt, colCnt, rowCnt, colCnt];
            foreach (Coordinates coordinates in inequalityFields)
            {
                if (coordinates.Direction == "S")
                {
                    if (coordinates.Text == "Higher")
                        inequalityMapping[coordinates.Row - 1, coordinates.Col - 1, coordinates.Row, coordinates.Col - 1] = 1;
                    else
                        inequalityMapping[coordinates.Row, coordinates.Col - 1, coordinates.Row - 1, coordinates.Col - 1] = 1;
                }
                else
                {
                    if (coordinates.Text == "Higher")
                        inequalityMapping[coordinates.Row - 1, coordinates.Col - 1, coordinates.Row - 1, coordinates.Col] = 1;
                    else
                        inequalityMapping[coordinates.Row - 1, coordinates.Col, coordinates.Row - 1, coordinates.Col - 1] = 1;
                }
            }
            bool isThereChange;
            for (int k = 1; k <= numRange; k++)
            {
                isThereChange = false;
                for (int i = 1; i <= rowCnt; i++)
                {
                    for (int j = 1; j <= colCnt; j++)
                    {
                        for (int ii = 1; ii <= rowCnt; ii++)
                        {
                            for (int jj = 1; jj <= colCnt; jj++)
                            {
                                for (int iii = 1; iii <= rowCnt; iii++)
                                {
                                    for (int jjj = 1; jjj <= colCnt; jjj++)
                                    {
                                        if (inequalityMapping[i - 1, j - 1, ii - 1, jj - 1] > 0 && inequalityMapping[ii - 1, jj - 1, iii - 1, jjj - 1] > 0 && inequalityMapping[i - 1, j - 1, iii - 1, jjj - 1] < inequalityMapping[i - 1, j - 1, ii - 1, jj - 1] + inequalityMapping[ii - 1, jj - 1, iii - 1, jjj - 1])
                                        {
                                            isThereChange = true;
                                            inequalityMapping[i - 1, j - 1, iii - 1, jjj - 1] = inequalityMapping[i - 1, j - 1, ii - 1, jj - 1] + inequalityMapping[ii - 1, jj - 1, iii - 1, jjj - 1];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (!isThereChange)
                    break;
            }
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    for (int ii = 1; ii <= rowCnt; ii++)
                    {
                        for (int jj = 1; jj <= colCnt; jj++)
                        {
                            if (inequalityMapping[i - 1, j - 1, ii - 1, jj - 1] > 0)
                            {
                                _inequalityFields[i - 1, j - 1].Add(new Coordinates { Row = ii, Col = jj, Text = "Higher", Number = inequalityMapping[i - 1, j - 1, ii - 1, jj - 1] });
                                _inequalityFields[ii - 1, jj - 1].Add(new Coordinates { Row = i, Col = j, Text = "Lower", Number = inequalityMapping[i - 1, j - 1, ii - 1, jj - 1] });
                            }
                        }
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priority * _inequalityFields[i - 1, j - 1].Count;
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
                    foreach (Coordinates coordinates in _inequalityFields[i - 1, j - 1])
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
            foreach (Coordinates coordinates in _inequalityFields[row - 1, col - 1])
            {
                if (coordinates.Text == "Higher")
                    HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, 1, number + coordinates.Number - 1, numRange);
                else
                    HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, number - coordinates.Number + 1, numRange, numRange);
            }
        }
    }
}