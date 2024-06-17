using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverConsecutiveDot : ISolver
    {
        private readonly List<List<Coordinates>>[,] _consecutiveFields;
        private const int _priorityConsecutiveBlackWhite = (int)Priority.Mid;
        private const int _priorityConsecutiveNoDot = (int)Priority.Low;

        public SolverConsecutiveDot(List<Coordinates> consecutiveFields, bool areAllConsecutivesMarked, int rowCnt, int colCnt)
        {
            _consecutiveFields = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _consecutiveFields[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in consecutiveFields)
            {
                _consecutiveFields[coordinates.Row - 1, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _consecutiveFields[coordinates.Row - 1, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _consecutiveFields[coordinates.Row, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _consecutiveFields[coordinates.Row, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
            }
            if (!areAllConsecutivesMarked)
                return;
            bool isFound;
            for (int i = 1; i < rowCnt; i++)
            {
                for (int j = 1; j < colCnt; j++)
                {
                    isFound = false;
                    foreach (List<Coordinates> area in _consecutiveFields[i - 1, j - 1])
                    {
                        if (area.Any(x => x.Row == i && x.Col == j) && area.Any(x => x.Row == i + 1 && x.Col == j) && area.Any(x => x.Row == i && x.Col == j + 1) && area.Any(x => x.Row == i + 1 && x.Col == j + 1))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (!isFound)
                    {
                        _consecutiveFields[i - 1, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "None" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j } });
                        _consecutiveFields[i - 1, j].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j + 1, Text = "None" }, new Coordinates { Row = i + 1, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i, Col = j } });
                        _consecutiveFields[i, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i + 1, Col = j, Text = "None" }, new Coordinates { Row = i, Col = j }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _consecutiveFields[i, j].Add(new List<Coordinates> { new Coordinates { Row = i + 1, Col = j + 1, Text = "None" }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i, Col = j }, new Coordinates { Row = i, Col = j + 1 } });
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priorityConsecutiveBlackWhite * _consecutiveFields[i - 1, j - 1].Where(x => x[0].Text == "Black" || x[0].Text == "White").Count() + _priorityConsecutiveNoDot * _consecutiveFields[i - 1, j - 1].Where(x => x[0].Text == "None").Count();
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            List<int> numbers;
            int emptyCnt;
            foreach (List<Coordinates> area in _consecutiveFields[row - 1, col - 1])
            {
                numbers = new List<int>();
                emptyCnt = 0;
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        emptyCnt++;
                    else
                        numbers.Add(grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number);
                }
                numbers.Sort();
                switch (area[0].Text)
                {
                    case "Black":
                        foreach (Coordinates coordinates in area)
                        {
                            if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                            {
                                HelperBan.Ban(grid, coordinates.Row, coordinates.Col, number, numRange);
                                HelperBan.BanRangeInverse(grid, coordinates.Row, coordinates.Col, numbers[^1] - 3, numbers[0] + 3, numRange);
                            }
                        }
                        break;
                    case "White":
                        foreach (Coordinates coordinates in area)
                        {
                            if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, number - 1, number + 1, numRange);
                        }
                        break;
                    case "None":
                        if (emptyCnt != 1)
                            break;
                        if (numbers[0] == numbers[1] || numbers[1] == numbers[2])
                            break;
                        if (numbers[0] + 1 == numbers[1] && numbers[1] + 1 == numbers[2])
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, new List<int> { numbers[0] - 1, numbers[2] + 1 }, numRange);
                            }
                            break;
                        }
                        if (numbers[0] + 1 == numbers[1] && numbers[1] + 2 == numbers[2])
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.Ban(grid, coordinates.Row, coordinates.Col, numbers[1] + 1, numRange);
                            }
                            break;
                        }
                        if (numbers[0] + 2 == numbers[1] && numbers[1] + 1 == numbers[2])
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.Ban(grid, coordinates.Row, coordinates.Col, numbers[1] - 1, numRange);
                            }
                            break;
                        }
                        if (numbers[0] + 1 < numbers[1] && numbers[1] + 1 < numbers[2])
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, new List<int> { numbers[0] - 1, numbers[0], numbers[0] + 1, numbers[1] - 1, numbers[1], numbers[1] + 1, numbers[2] - 1, numbers[2], numbers[2] + 1 }, numRange);
                            }
                            break;
                        }
                        break;
                }
            }
        }
    }
}