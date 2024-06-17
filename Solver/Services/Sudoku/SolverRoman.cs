using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverRoman : ISolver
    {
        private readonly List<Coordinates>[,] _romanFields;
        private const int _priorityRoman = (int)Priority.Mid;
        private const int _priorityNonRoman = (int)Priority.Low;

        public SolverRoman(List<Coordinates> romanFields, bool areAllRomansMarked, int rowCnt, int colCnt)
        {
            _romanFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _romanFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (Coordinates coordinates in romanFields)
            {
                switch (coordinates.Direction)
                {
                    case "S":
                        _romanFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Text = coordinates.Text });
                        _romanFields[coordinates.Row, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text });
                        break;
                    case "E":
                        _romanFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Text = coordinates.Text });
                        _romanFields[coordinates.Row - 1, coordinates.Col].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text });
                        break;
                }
            }
            if (!areAllRomansMarked)
                return;
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    if (!_romanFields[i - 1, j - 1].Any(x => x.Row == i + 1 && x.Col == j) && i < rowCnt)
                    {
                        _romanFields[i - 1, j - 1].Add(new Coordinates { Row = i + 1, Col = j, Text = "O" });
                        _romanFields[i, j - 1].Add(new Coordinates { Row = i, Col = j, Text = "O" });
                    }
                    if (!_romanFields[i - 1, j - 1].Any(x => x.Row == i && x.Col == j + 1) && j < colCnt)
                    {
                        _romanFields[i - 1, j - 1].Add(new Coordinates { Row = i, Col = j + 1, Text = "O" });
                        _romanFields[i - 1, j].Add(new Coordinates { Row = i, Col = j, Text = "O" });
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priorityRoman * _romanFields[i - 1, j - 1].Where(x => x.Text == "X" || x.Text == "V").Count() + _priorityNonRoman * _romanFields[i - 1, j - 1].Where(x => x.Text == "O").Count();
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    foreach (Coordinates coordinates in _romanFields[i - 1, j - 1])
                    {
                        switch (coordinates.Text)
                        {
                            case "X":
                                HelperBan.BanRange(grid, i, j, 10, numRange, numRange);
                                break;
                            case "V":
                                HelperBan.BanRange(grid, i, j, 5, numRange, numRange);
                                break;
                        }
                    }
                }
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates coordinates in _romanFields[row - 1, col - 1])
            {
                switch (coordinates.Text)
                {
                    case "X":
                        HelperBan.BanInverse(grid, coordinates.Row, coordinates.Col, 10 - number, numRange);
                        break;
                    case "V":
                        HelperBan.BanInverse(grid, coordinates.Row, coordinates.Col, 5 - number, numRange);
                        break;
                    case "O":
                        HelperBan.BanList(grid, coordinates.Row, coordinates.Col, new List<int> { 5 - number, 10 - number }, numRange);
                        break;
                }
            }
        }
    }
}