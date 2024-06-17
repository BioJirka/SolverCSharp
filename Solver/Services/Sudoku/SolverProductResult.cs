using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverProductResult : ISolver
    {
        private readonly List<List<Coordinates>> _productResultFields;
        private readonly List<List<Coordinates>>[,] _productResultFieldsMapping;
        private readonly List<List<int>> _lastDigitMappingUp;
        private readonly List<List<int>> _lastDigitMappingDown;
        private readonly List<List<int>> _lastDigitMappingLevel;
        private const int _priority = (int)Priority.Mid;

        public SolverProductResult(List<Coordinates> productResultFields, int rowCnt, int colCnt, int numRange)
        {
            int number;
            List<List<int>> lastDigitMappingUp = new() { new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, new List<int> { 1, 3, 7, 9 }, new List<int> { 1, 2, 3, 4, 6, 7, 8, 9 }, new List<int> { 1, 3, 7, 9 }, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new List<int> { 1, 3, 5, 7, 9 }, new List<int> { 1, 2, 3, 4, 6, 7, 8, 9 }, new List<int> { 1, 3, 7, 9 }, new List<int> { 1, 2, 3, 4, 6, 7, 8, 9 }, new List<int> { 1, 3, 7, 9 } };
            List<List<int>> lastDigitMappingDown = new() { new List<int> { 0 }, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, new List<int> { 2, 4, 6, 8, 0 }, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, new List<int> { 2, 4, 6, 8, 0 }, new List<int> { 5, 0 }, new List<int> { 2, 4, 6, 8, 0 }, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, new List<int> { 2, 4, 6, 8, 0 }, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 } };
            _productResultFields = new List<List<Coordinates>>();
            _productResultFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            _lastDigitMappingUp = new List<List<int>>();
            _lastDigitMappingDown = new List<List<int>>();
            _lastDigitMappingLevel = new List<List<int>>();
            foreach (List<int> digits in lastDigitMappingUp)
            {
                _lastDigitMappingUp.Add(new List<int>());
                foreach (int digit in digits)
                {
                    number = digit;
                    while (number <= numRange)
                    {
                        if (number > 0)
                            _lastDigitMappingUp[^1].Add(number);
                        number += 10;
                    }
                }
            }
            foreach (List<int> digits in lastDigitMappingDown)
            {
                _lastDigitMappingDown.Add(new List<int>());
                foreach (int digit in digits)
                {
                    number = digit;
                    while (number <= numRange)
                    {
                        if (number > 0)
                            _lastDigitMappingDown[^1].Add(number);
                        number += 10;
                    }
                }
            }
            for (int k = 0; k < 10; k++)
            {
                _lastDigitMappingLevel.Add(new List<int>());
                number = k;
                while (number <= numRange)
                {
                    if (number > 0)
                        _lastDigitMappingLevel[^1].Add(number);
                    number += 10;
                }
            }
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _productResultFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in productResultFields)
            {
                _productResultFields.Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1 }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col }, new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col + 1 } });
                _productResultFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(_productResultFields[^1]);
                _productResultFieldsMapping[coordinates.Row - 1, coordinates.Col].Add(_productResultFields[^1]);
                _productResultFieldsMapping[coordinates.Row, coordinates.Col - 1].Add(_productResultFields[^1]);
                _productResultFieldsMapping[coordinates.Row, coordinates.Col].Add(_productResultFields[^1]);
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (List<Coordinates> area in _productResultFields)
            {
                foreach (Coordinates coordinates in area)
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            foreach (List<Coordinates> area in _productResultFields)
            {
                HelperBan.BanRange(grid, area[0].Row, area[0].Col, 1, 11 / numRange, numRange);
                HelperBan.BanRange(grid, area[1].Row, area[1].Col, 1, 11 / numRange, numRange);
                HelperBan.BanRangeInverse(grid, area[2].Row, area[2].Col, 1, (numRange * numRange - 1) / 10, numRange);
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            List<bool> emptyCells;
            List<int> numbers;
            List<List<int>> possibleNumbers;
            foreach (List<Coordinates> area in _productResultFieldsMapping[row - 1, col - 1])
            {
                numbers = new List<int> { grid.Fields[area[0].Row - 1, area[0].Col - 1].Number, grid.Fields[area[1].Row - 1, area[1].Col - 1].Number, grid.Fields[area[2].Row - 1, area[2].Col - 1].Number, grid.Fields[area[3].Row - 1, area[3].Col - 1].Number };
                emptyCells = new List<bool> { numbers[0] == 0, numbers[1] == 0, numbers[2] == 0, numbers[3] == 0 };
                if (emptyCells[0] && emptyCells[1] && emptyCells [2] && !emptyCells[3])
                {
                    HelperBan.BanListInverse(grid, area[0].Row, area[0].Col, _lastDigitMappingUp[numbers[3] % 10], numRange);
                    HelperBan.BanListInverse(grid, area[1].Row, area[1].Col, _lastDigitMappingUp[numbers[3] % 10], numRange);
                }
                else if (emptyCells[0] && emptyCells[1] && !emptyCells [2] && emptyCells[3])
                {
                    HelperBan.BanRange(grid, area[0].Row, area[0].Col, 1, 10 * numbers[2] / numRange, numRange);
                    HelperBan.BanRange(grid, area[1].Row, area[1].Col, 1, 10 * numbers[2] / numRange, numRange);
                }
                else if (emptyCells[0] && !emptyCells[1] && emptyCells [2] && emptyCells[3])
                {
                    HelperBan.BanRange(grid, area[0].Row, area[0].Col, 1, 10 / numbers[1], numRange);
                    HelperBan.BanRangeInverse(grid, area[2].Row, area[2].Col, (numbers[1] - 1) / 10, (numbers[1] * numRange - 1) / 10, numRange);
                    HelperBan.BanListInverse(grid, area[3].Row, area[3].Col, _lastDigitMappingDown[numbers[1] % 10], numRange);
                }
                else if (!emptyCells[0] && emptyCells[1] && emptyCells [2] && emptyCells[3])
                {
                    HelperBan.BanRange(grid, area[1].Row, area[1].Col, 1, 10 / numbers[0], numRange);
                    HelperBan.BanRangeInverse(grid, area[2].Row, area[2].Col, (numbers[0] - 1) / 10, (numbers[0] * numRange - 1) / 10, numRange);
                    HelperBan.BanListInverse(grid, area[3].Row, area[3].Col, _lastDigitMappingDown[numbers[0] % 10], numRange);
                }
                else if (emptyCells[0] && emptyCells[1] && !emptyCells [2] && !emptyCells[3])
                {
                    possibleNumbers = new List<List<int>> { new List<int>() };
                    for (int k = 1; k <= numRange; k++)
                    {
                        if ((10 * numbers[2] + numbers[3]) % k == 0 && (10 * numbers[2] + numbers[3]) / k <= numRange)
                            possibleNumbers[0].Add(k);
                    }
                    HelperBan.BanListInverse(grid, area[0].Row, area[0].Col, possibleNumbers[0], numRange);
                    HelperBan.BanListInverse(grid, area[1].Row, area[1].Col, possibleNumbers[0], numRange);
                }
                else if (emptyCells[0] && !emptyCells[1] && emptyCells [2] && !emptyCells[3])
                {
                    possibleNumbers = new List<List<int>> { new List<int>(), new List<int>() };
                    foreach (int digit in _lastDigitMappingUp[numbers[3] % 10])
                    {
                        if ((digit * numbers[1] % 10) == (numbers[3] % 10))
                        {
                            possibleNumbers[0].Add(digit);
                            possibleNumbers[1].Add((digit * numbers[1] - numbers[3]) / 10);
                        }
                    }
                    HelperBan.BanListInverse(grid, area[0].Row, area[0].Col, possibleNumbers[0], numRange);
                    HelperBan.BanListInverse(grid, area[2].Row, area[2].Col, possibleNumbers[1], numRange);
                }
                else if (!emptyCells[0] && emptyCells[1] && emptyCells [2] && !emptyCells[3])
                {
                    possibleNumbers = new List<List<int>> { new List<int>(), new List<int>() };
                    foreach (int digit in _lastDigitMappingUp[numbers[3] % 10])
                    {
                        if ((digit * numbers[0] % 10) == (numbers[3] % 10))
                        {
                            possibleNumbers[1].Add(digit);
                            possibleNumbers[0].Add((digit * numbers[0] - numbers[3]) / 10);
                        }
                    }
                    HelperBan.BanListInverse(grid, area[1].Row, area[1].Col, possibleNumbers[1], numRange);
                    HelperBan.BanListInverse(grid, area[2].Row, area[2].Col, possibleNumbers[0], numRange);
                }
                else if (emptyCells[0] && !emptyCells[1] && !emptyCells [2] && emptyCells[3])
                {
                    possibleNumbers = new List<List<int>> { new List<int>(), new List<int>() };
                    foreach (int digit in _lastDigitMappingDown[numbers[1] % 10])
                    {
                        if ((10 * numbers[2] + digit) % numbers[1] == 0)
                        {
                            possibleNumbers[0].Add(digit);
                            possibleNumbers[1].Add((10 * numbers[2] + digit) / numbers[1]);
                        }
                    }
                    HelperBan.BanListInverse(grid, area[0].Row, area[0].Col, possibleNumbers[1], numRange);
                    HelperBan.BanListInverse(grid, area[3].Row, area[3].Col, possibleNumbers[0], numRange);
                }
                else if (!emptyCells[0] && emptyCells[1] && !emptyCells [2] && emptyCells[3])
                {
                    possibleNumbers = new List<List<int>> { new List<int>(), new List<int>() };
                    foreach (int digit in _lastDigitMappingDown[numbers[0] % 10])
                    {
                        if ((10 * numbers[2] + digit) % numbers[0] == 0)
                        {
                            possibleNumbers[0].Add(digit);
                            possibleNumbers[1].Add((10 * numbers[2] + digit) / numbers[0]);
                        }
                    }
                    HelperBan.BanListInverse(grid, area[1].Row, area[1].Col, possibleNumbers[1], numRange);
                    HelperBan.BanListInverse(grid, area[3].Row, area[3].Col, possibleNumbers[0], numRange);
                }
                else if (!emptyCells[0] && !emptyCells[1] && emptyCells [2] && emptyCells[3])
                {
                    possibleNumbers = new List<List<int>> { new List<int>(), new List<int>() };
                    foreach (int digit in _lastDigitMappingLevel[numbers[0] * numbers[1] % 10])
                    {
                        possibleNumbers[0].Add(digit);
                        possibleNumbers[1].Add((numbers[0] * numbers[1] - digit) / 10);
                    }
                    HelperBan.BanListInverse(grid, area[2].Row, area[2].Col, possibleNumbers[1], numRange);
                    HelperBan.BanListInverse(grid, area[3].Row, area[3].Col, possibleNumbers[0], numRange);
                }
                else if (emptyCells[0] && !emptyCells[1] && !emptyCells [2] && !emptyCells[3])
                {
                    if ((10 * numbers[2] + numbers[3]) % numbers[1] == 0)
                        HelperBan.BanInverse(grid, area[0].Row, area[0].Col, (10 * numbers[2] + numbers[3]) / numbers[1], numRange);
                    else
                        HelperBan.BanAll(grid, area[0].Row, area[0].Col, numRange);
                }
                else if (!emptyCells[0] && emptyCells[1] && !emptyCells [2] && !emptyCells[3])
                {
                    if ((10 * numbers[2] + numbers[3]) % numbers[0] == 0)
                        HelperBan.BanInverse(grid, area[1].Row, area[1].Col, (10 * numbers[2] + numbers[3]) / numbers[0], numRange);
                    else
                        HelperBan.BanAll(grid, area[1].Row, area[1].Col, numRange);
                }
                else if (!emptyCells[0] && !emptyCells[1] && emptyCells [2] && !emptyCells[3])
                {
                    if ((numbers[0] * numbers[1] - numbers[3]) % 10 == 0)
                        HelperBan.BanInverse(grid, area[2].Row, area[2].Col, (numbers[0] * numbers[1] - numbers[3]) / 10, numRange);
                    else
                        HelperBan.BanAll(grid, area[2].Row, area[2].Col, numRange);
                }
                else if (!emptyCells[0] && !emptyCells[1] && !emptyCells [2] && emptyCells[3])
                {
                    HelperBan.BanInverse(grid, area[3].Row, area[3].Col, numbers[0] * numbers[1] - 10 * numbers[2], numRange);
                }
            }
        }
    }
}
