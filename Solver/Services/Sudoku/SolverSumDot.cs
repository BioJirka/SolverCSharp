using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverSumDot : ISolver
    {
        private readonly List<List<Coordinates>>[,] _sumFields;
        private const int _prioritySum = (int)Priority.Mid;
        private const int _priorityNonSum = (int)Priority.Low;

        public SolverSumDot(List<Coordinates> sumFields, bool areAllSumDotsMarked, int rowCnt, int colCnt)
        {
            _sumFields = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _sumFields[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in sumFields)
            {
                _sumFields[coordinates.Row - 1, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text == "Black" ? "X" : "O" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _sumFields[coordinates.Row - 1, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text == "Black" ? "X" : "O" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _sumFields[coordinates.Row, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text == "Black" ? "X" : "O" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _sumFields[coordinates.Row, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text == "Black" ? "X" : "O" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
            }
            if (!areAllSumDotsMarked)
                return;
            bool isFound;
            for (int i = 1; i < rowCnt; i++)
            {
                for (int j = 1; j < colCnt; j++)
                {
                    isFound = false;
                    foreach (List<Coordinates> area in _sumFields[i - 1, j - 1])
                    {
                        if (area.Any(x => x.Row == i && x.Col == j) && area.Any(x => x.Row == i + 1 && x.Col == j) && area.Any(x => x.Row == i && x.Col == j + 1) && area.Any(x => x.Row == i + 1 && x.Col == j + 1))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (!isFound)
                    {
                        _sumFields[i - 1, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "O" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _sumFields[i - 1, j].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "O" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _sumFields[i, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "O" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _sumFields[i, j].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "O" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _prioritySum * _sumFields[i - 1, j - 1].Where(x => x[0].Text == "X").Count() + _priorityNonSum * _sumFields[i - 1, j - 1].Where(x => x[0].Text == "O").Count();
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            int emptyCnt;
            int maxNumber;
            int subSum;
            foreach (List<Coordinates> area in _sumFields[row - 1, col - 1])
            {
                emptyCnt = 0;
                maxNumber = 0;
                subSum = 0;
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        emptyCnt++;
                    else
                    {
                        maxNumber = Math.Max(maxNumber, grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number);
                        subSum += grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number;
                    }
                }
                subSum -= maxNumber;
                switch (area[0].Text)
                {
                    case "X":
                        switch (emptyCnt)
                        {
                            case 3:
                                foreach (Coordinates coordinates in area)
                                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                        HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, Math.Max(number - 1, numRange - number), number + 1, numRange);
                                break;
                            case 2:
                                foreach (Coordinates coordinates in area)
                                {
                                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                        HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, Math.Max(maxNumber - subSum, numRange - maxNumber - subSum + 1), maxNumber + subSum, numRange);
                                }
                                break;
                            case 1:
                                foreach (Coordinates coordinates in area)
                                {
                                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                        HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, new List<int> { maxNumber - subSum, maxNumber + subSum }, numRange);
                                }
                                break;
                        }
                        break;
                    case "O":
                        if (emptyCnt != 1)
                            break;
                        foreach (Coordinates coordinates in area)
                        {
                            if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                HelperBan.BanList(grid, coordinates.Row, coordinates.Col, new List<int> { maxNumber - subSum, maxNumber + subSum }, numRange);
                        }
                        break;
                }
            }
        }
    }
}