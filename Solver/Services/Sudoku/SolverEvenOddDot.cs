using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverEvenOddDot : ISolver
    {
        private readonly List<int> _evenNumbers;
        private readonly List<int> _oddNumbers;
        private readonly List<List<Coordinates>>[,] _evenOddFields;
        private const int _priorityBlack = (int)Priority.Mid;
        private const int _priorityNonBlack = (int)Priority.Low;

        public SolverEvenOddDot(List<Coordinates> evenOrOddFields, bool areAllEvenOddDotsMarked, int rowCnt, int colCnt, int numRange)
        {
            _evenNumbers = new List<int>();
            _oddNumbers = new List<int>();
            _evenOddFields = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int k = 2; k <= numRange; k += 2)
                _evenNumbers.Add(k);
            for (int k = 1; k <= numRange; k += 2)
                _oddNumbers.Add(k);
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _evenOddFields[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in evenOrOddFields)
            {
                _evenOddFields[coordinates.Row - 1, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _evenOddFields[coordinates.Row - 1, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _evenOddFields[coordinates.Row, coordinates.Col - 1].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _evenOddFields[coordinates.Row, coordinates.Col].Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
            }
            if (!areAllEvenOddDotsMarked)
                return;
            bool isFound;
            for (int i = 1; i < rowCnt; i++)
            {
                for (int j = 1; j < colCnt; j++)
                {
                    isFound = false;
                    foreach (List<Coordinates> area in _evenOddFields[i - 1, j - 1])
                    {
                        if (area.Any(x => x.Row == i && x.Col == j) && area.Any(x => x.Row == i + 1 && x.Col == j) && area.Any(x => x.Row == i && x.Col == j + 1) && area.Any(x => x.Row == i + 1 && x.Col == j + 1))
                        {
                            isFound = true;
                            break;
                        }
                    }
                    if (!isFound)
                    {
                        _evenOddFields[i - 1, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "None" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _evenOddFields[i - 1, j].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "None" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _evenOddFields[i, j - 1].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "None" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                        _evenOddFields[i, j].Add(new List<Coordinates> { new Coordinates { Row = i, Col = j, Text = "None" }, new Coordinates { Row = i, Col = j + 1 }, new Coordinates { Row = i + 1, Col = j }, new Coordinates { Row = i + 1, Col = j + 1 } });
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priorityBlack * _evenOddFields[i - 1, j - 1].Where(x => x[0].Text == "Black").Count() + _priorityNonBlack * _evenOddFields[i - 1, j - 1].Where(x => x[0].Text == "White" || x[0].Text == "None").Count();
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            bool numberIsEven = number % 2 == 0;
            int emptyCnt;
            int evenCnt;
            int oddCnt;
            foreach (List<Coordinates> area in _evenOddFields[row - 1, col - 1])
            {
                emptyCnt = 0;
                evenCnt = 0;
                oddCnt = 0;
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        emptyCnt++;
                    else if(grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number % 2 == 0)
                        evenCnt++;
                    else
                        oddCnt++;
                }
                switch (area[0].Text)
                {
                    case "Black":
                        if (emptyCnt < 3)
                            break;
                        foreach (Coordinates coordinates in area)
                        {   
                            if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                            {
                                if (numberIsEven)
                                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _oddNumbers, numRange);
                                else
                                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _evenNumbers, numRange);
                            }
                        }
                        break;
                    case "White":
                        if (evenCnt == 2 && numberIsEven && emptyCnt > 0)
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _evenNumbers, numRange);
                            }
                        }
                        else if (oddCnt == 2 && !numberIsEven & emptyCnt > 0)
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _oddNumbers, numRange);
                            }
                        }
                        break;
                    case "None":
                        if (emptyCnt != 1)
                            break;
                        if (evenCnt == 3 || evenCnt == 1)
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _evenNumbers, numRange);
                            }
                        }
                        else if (oddCnt == 3 || oddCnt == 1)
                        {
                            foreach (Coordinates coordinates in area)
                            {
                                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, _oddNumbers, numRange);
                            }
                        }
                        break;
                }
            }
        }
    }
}