using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverClock : ISolver
    {
        private readonly List<List<Coordinates>>[,] _clockFields;
        private const int _priorityClock = (int)Priority.Mid;
        private const int _priorityNonClock = (int)Priority.Low;

        public SolverClock(List<Coordinates> clockFields, bool areAllClocksMarked, int rowCnt, int colCnt)
        {
            _clockFields = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _clockFields[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in clockFields)
            {
                switch (coordinates.Text)
                {
                    // order in these lists is important here
                    case "Black":
                        _clockFields[coordinates.Row - 1, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = "X" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col } });
                        _clockFields[coordinates.Row - 1, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Text = "X" }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col } });
                        _clockFields[coordinates.Row, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Text = "X" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                        _clockFields[coordinates.Row, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1, Text = "X" }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 } });
                        break;
                    case "White":
                        _clockFields[coordinates.Row - 1, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = "X" }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 } });
                        _clockFields[coordinates.Row - 1, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Text = "X" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                        _clockFields[coordinates.Row, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Text = "X" }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col } });
                        _clockFields[coordinates.Row, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1, Text = "X" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col } });
                        break;
                    case "None":
                        _clockFields[coordinates.Row - 1, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = "O" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col } });
                        _clockFields[coordinates.Row - 1, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Text = "O" }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col } });
                        _clockFields[coordinates.Row, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Text = "O" }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                        _clockFields[coordinates.Row, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1, Text = "O" }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 } });
                        break;
                }
            }
            if (!areAllClocksMarked)
                return;
            bool isFound;
            for (int i = 1; i < rowCnt; i++)
            {
                for (int j = 1; j < colCnt; j++)
                {
                    isFound = false;
                    foreach (List<Coordinates> area in _clockFields[i - 1, j - 1])
                    {
                        if (area.Any(x => x.Row == i && x.Col == j) && area.Any(x => x.Row == i + 1 && x.Col == j) && area.Any(x => x.Row == i && x.Col == j + 1) && area.Any(x => x.Row == i + 1 && x.Col == j + 1))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (!isFound)
                    {
                        _clockFields[i - 1, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "O" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j } });
                        _clockFields[i - 1, j].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j + 1, Text = "O" }, new Coordinates { Row = i + 1, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i, Col = j } });
                        _clockFields[i, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i + 1, Col = j, Text = "O" }, new Coordinates { Row = i, Col = j }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _clockFields[i, j].Add(new List<Coordinates> { new Coordinates { Row = i + 1, Col = j + 1, Text = "O" }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i, Col = j }, new Coordinates { Row = i, Col = j + 1 } });
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priorityClock * _clockFields[i - 1, j - 1].Where(x => x[0].Text == "X").Count() + _priorityNonClock * _clockFields[i - 1, j - 1].Where(x => x[0].Text == "O").Count();
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            List<int> numbers;
            List<bool> emptyCells;
            List<int> duplicitNumbers;
            foreach (List<Coordinates> area in _clockFields[row - 1, col - 1])
            {
                numbers = new List<int> { grid.Fields[area[1].Row - 1, area[1].Col - 1].Number, grid.Fields[area[2].Row - 1, area[2].Col - 1].Number, grid.Fields[area[3].Row - 1, area[3].Col - 1].Number };
                emptyCells = new List<bool> { numbers[0] == 0, numbers[1] == 0, numbers[2] == 0 };
                switch (area[0].Text)
                {
                    case "X":
                        if (emptyCells[0] && emptyCells[1] && emptyCells[2])
                        {
                            HelperBan.BanRange(grid, area[1].Row, area[1].Col, number - 2, number, numRange);
                            HelperBan.BanRange(grid, area[2].Row, area[2].Col, number - 1, number + 1, numRange);
                            HelperBan.BanRange(grid, area[3].Row, area[3].Col, number, number + 2, numRange);
                            if (number == 1)
                            {
                                HelperBan.BanRange(grid, area[1].Row, area[1].Col, numRange - 1, numRange, numRange);
                                HelperBan.Ban(grid, area[2].Row, area[2].Col, numRange, numRange);
                            }
                            else if (number == 2)
                                HelperBan.Ban(grid, area[1].Row, area[1].Col, numRange, numRange);
                            else if (number == numRange - 1)
                                HelperBan.Ban(grid, area[3].Row, area[3].Col, 1, numRange);
                            else if (number == numRange)
                            {
                                HelperBan.Ban(grid, area[2].Row, area[2].Col, 1, numRange);
                                HelperBan.BanRange(grid, area[3].Row, area[3].Col, 1, 2, numRange);
                            }
                        }
                        else if (emptyCells[0] && emptyCells[1] && !emptyCells[2])
                        {
                            if (number < numbers[2])
                            {
                                HelperBan.BanRangeInverse(grid, area[1].Row, area[1].Col, number + 1, numbers[2] - 2, numRange);
                                HelperBan.BanRangeInverse(grid, area[2].Row, area[2].Col, number + 2, numbers[2] - 1, numRange);
                            }
                            else
                            {
                                HelperBan.BanRange(grid, area[1].Row, area[1].Col, numbers[2] - 1, number, numRange);
                                HelperBan.BanRange(grid, area[2].Row, area[2].Col, numbers[2], number + 1, numRange);
                                if (numbers[2] == 1)
                                    HelperBan.Ban(grid, area[1].Row, area[1].Col, numRange, numRange);
                                else if (number == numRange)
                                    HelperBan.Ban(grid, area[2].Row, area[2].Col, 1, numRange);
                            }
                        }
                        else if (emptyCells[0] && !emptyCells[1] && emptyCells[2])
                        {
                            if (number < numbers[1])
                            {
                                HelperBan.BanRangeInverse(grid, area[1].Row, area[1].Col, number + 1, numbers[1] - 1, numRange);
                                HelperBan.BanRange(grid, area[3].Row, area[3].Col, number, numbers[1], numRange);
                            }
                            else
                            {
                                HelperBan.BanRange(grid, area[1].Row, area[1].Col, numbers[1], number, numRange);
                                HelperBan.BanRangeInverse(grid, area[3].Row, area[3].Col, numbers[1] + 1, number - 1, numRange);
                            }
                        }
                        else if (!emptyCells[0] && emptyCells[1] && emptyCells[2])
                        {
                            if (numbers[0] < number)
                            {
                                HelperBan.BanRangeInverse(grid, area[2].Row, area[2].Col, numbers[0] + 1, number - 2, numRange);
                                HelperBan.BanRangeInverse(grid, area[3].Row, area[3].Col, numbers[0] + 2, number - 1, numRange);
                            }
                            else
                            {
                                HelperBan.BanRange(grid, area[2].Row, area[2].Col, number - 1, numbers[0], numRange);
                                HelperBan.BanRange(grid, area[3].Row, area[3].Col, number, numbers[0] + 1, numRange);
                                if (number == 1)
                                    HelperBan.Ban(grid, area[2].Row, area[2].Col, numRange, numRange);
                                else if (numbers[0] == numRange)
                                    HelperBan.Ban(grid, area[3].Row, area[3].Col, 1, numRange);
                            }
                        }
                        else if (emptyCells[0] && !emptyCells[1] && !emptyCells[2])
                        {
                            if (number < numbers[1])
                                HelperBan.BanRangeInverse(grid, area[1].Row, area[1].Col, number + 1, numbers[1] - 1, numRange);
                            else
                                HelperBan.BanRange(grid, area[1].Row, area[1].Col, numbers[1], number, numRange);
                        }
                        else if (!emptyCells[0] && emptyCells[1] && !emptyCells[2])
                        {
                            if (numbers[0] < numbers[2])
                                HelperBan.BanRangeInverse(grid, area[2].Row, area[2].Col, numbers[0] + 1, numbers[2] - 1, numRange);
                            else
                                HelperBan.BanRange(grid, area[2].Row, area[2].Col, numbers[2], numbers[0], numRange);
                        }
                        else if (!emptyCells[0] && !emptyCells[1] && emptyCells[2])
                        {
                            if (numbers[1] < number)
                                HelperBan.BanRangeInverse(grid, area[3].Row, area[3].Col, numbers[1] + 1, number - 1, numRange);
                            else
                                HelperBan.BanRange(grid, area[3].Row, area[3].Col, number, numbers[1], numRange);
                        }
                        break;
                    case "O":
                        duplicitNumbers = numbers.Where(x => x != 0).ToList();
                        duplicitNumbers.Add(number);
                        duplicitNumbers = duplicitNumbers.GroupBy(x => x).SelectMany(x => x.Skip(1)).ToList();
                        if (duplicitNumbers.Count > 0)
                            break;
                        if (emptyCells[0] && !emptyCells[1] && !emptyCells[2])
                        {
                            if (Math.Min(number, numbers[1]) < numbers[2] && numbers[2] < Math.Max(number, numbers[1]))
                                HelperBan.BanRangeInverse(grid, area[1].Row, area[1].Col, Math.Min(number, numbers[1]), Math.Max(number, numbers[1]), numRange);
                            else
                                HelperBan.BanRange(grid, area[1].Row, area[1].Col, Math.Min(number, numbers[1]) + 1, Math.Max(number, numbers[1]) - 1, numRange);
                        }
                        else if (!emptyCells[0] && emptyCells[1] && !emptyCells[2])
                        {
                            if (Math.Min(numbers[0], numbers[2]) < number && number < Math.Max(numbers[0], numbers[2]))
                                HelperBan.BanRangeInverse(grid, area[2].Row, area[2].Col, Math.Min(numbers[0], numbers[2]), Math.Max(numbers[0], numbers[2]), numRange);
                            else
                                HelperBan.BanRange(grid, area[2].Row, area[2].Col, Math.Min(numbers[0], numbers[2]) + 1, Math.Max(numbers[0], numbers[2]) - 1, numRange);
                        }
                        else if (!emptyCells[0] && !emptyCells[1] && emptyCells[2])
                        {
                            if (Math.Min(number, numbers[1]) < numbers[0] && numbers[0] < Math.Max(number, numbers[1]))
                                HelperBan.BanRangeInverse(grid, area[3].Row, area[3].Col, Math.Min(number, numbers[1]), Math.Max(number, numbers[1]), numRange);
                            else
                                HelperBan.BanRange(grid, area[3].Row, area[3].Col, Math.Min(number, numbers[1]) + 1, Math.Max(number, numbers[1]) - 1, numRange);
                        }
                        break;
                }
            }
        }
    }
}