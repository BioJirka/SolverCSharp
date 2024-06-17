using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverDynamicSumOrProduct : ISolver
    {
        private readonly List<List<Coordinates>> _sumProductFields;
        private readonly List<List<Coordinates>>[,] _sumProductFieldsMapping;
        private const int _priorityTarget = (int)Priority.Mid;
        private const int _prioritySumProduct = (int)Priority.Low;
        private readonly Dictionary<(int, int, int), List<List<int>>> _combinations;

        public SolverDynamicSumOrProduct(List<Coordinates> sumProductFields, int rowCnt, int colCnt, int numRange)
        {
            _sumProductFields = new List<List<Coordinates>>();
            _sumProductFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _sumProductFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (Coordinates coordinates in sumProductFields)
            {
                _sumProductFields.Add(new List<Coordinates> { coordinates });
                switch (coordinates.Direction)
                {
                    case "N":
                        _sumProductFields[^1].Add(new Coordinates { Number = coordinates.Row });
                        for (int i = 1; i <= rowCnt; i++)
                            _sumProductFields[^1].Add(new Coordinates { Row = i, Col = coordinates.Col });
                        break;
                    case "W":
                        _sumProductFields[^1].Add(new Coordinates { Number = coordinates.Col });
                        for (int j = 1; j <= colCnt; j++)
                            _sumProductFields[^1].Add(new Coordinates { Row = coordinates.Row, Col = j });
                        break;
                    case "S":
                        _sumProductFields[^1].Add(new Coordinates { Number = rowCnt - coordinates.Row + 1 });
                        for (int i = rowCnt; i > 0; i--)
                            _sumProductFields[^1].Add(new Coordinates { Row = i, Col = coordinates.Col });
                        break;
                    case "E":
                        _sumProductFields[^1].Add(new Coordinates { Number = colCnt - coordinates.Col + 1 });
                        for (int j = colCnt; j > 0; j--)
                            _sumProductFields[^1].Add(new Coordinates { Row = coordinates.Row, Col = j });
                        break;
                }
            }
            foreach (List<Coordinates> area in _sumProductFields)
            {
                foreach (Coordinates coordinates in area.GetRange(2, area.Count - 2))
                    _sumProductFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(area);
            }
            _combinations = GetCombinations(numRange);
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (List<Coordinates> area in _sumProductFields)
            {
                grid.Fields[area[0].Row - 1, area[0].Col - 1].Priority += _priorityTarget;
                foreach (Coordinates coordinates in area.GetRange(2, 3))
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _prioritySumProduct;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            int target;
            int cntPosition;
            int minCnt;
            List<int> possibleNumbers;
            List<int> possibleNumbersTarget;
            foreach (List<Coordinates> area in _sumProductFields)
            {
                target = area[0].Number;
                cntPosition = area[1].Number;
                minCnt = 0;
                possibleNumbers = new List<int>();
                possibleNumbersTarget = new List<int>();
                for (int k = 1; k <= area.Count - 2; k++)
                {
                    if (cntPosition <= k)
                    {
                        foreach (List<int> combination in _combinations[(target, k, cntPosition)])
                        {
                            if (!combination.Contains(k))
                                continue;
                            if (minCnt == 0)
                                minCnt = k;
                            possibleNumbers.AddRange(combination);
                            possibleNumbersTarget.Add(k);
                        }
                    }
                    else
                    {
                        foreach (List<int> combination in _combinations[(target, k, 0)])
                        {
                            if (minCnt == 0)
                                minCnt = k;
                            possibleNumbers.AddRange(combination);
                            possibleNumbersTarget.Add(k);
                        }
                    }
                }
                possibleNumbers = possibleNumbers.Distinct().ToList();
                possibleNumbersTarget = possibleNumbersTarget.Distinct().ToList();
                HelperBan.BanListInverse(grid, area[0].Row, area[0].Col, possibleNumbersTarget, numRange);
                for (int k = 1; k <= minCnt; k++)
                    HelperBan.BanListInverse(grid, area[k + 1].Row, area[k + 1].Col, possibleNumbers, numRange);
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            int target;
            int cntPosition;
            int cnt;
            int minCnt;
            List<int> possibleNumbers;
            List<int> possibleNumbersTarget;
            List<int> currentNumbers;
            List<List<int>> combinations;
            foreach (List<Coordinates> area in _sumProductFieldsMapping[row - 1, col - 1])
            {
                target = area[0].Number;
                cntPosition = area[1].Number;
                cnt = grid.Fields[area[0].Row - 1, area[0].Col - 1].Number;
                if (cnt != 0)
                {
                    if (cntPosition <= cnt)
                        combinations = _combinations[(target, cnt, cntPosition)];
                    else
                        combinations = _combinations[(target, cnt, 0)];
                    possibleNumbers = new List<int>();
                    currentNumbers = new List<int>();
                    foreach (Coordinates coordinates in area.GetRange(2, cnt))
                    {
                        if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number != 0)
                            currentNumbers.Add(grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number);
                    }
                    foreach (List<int> combination in combinations)
                    {
                        if (currentNumbers.Intersect(combination).Count() == currentNumbers.Count)
                            possibleNumbers.AddRange(combination);
                    }
                    possibleNumbers = possibleNumbers.Distinct().ToList();
                    possibleNumbers.RemoveAll(x => currentNumbers.Contains(x));
                    foreach (Coordinates coordinates in area.GetRange(2, cnt))
                    {
                        if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                            HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, possibleNumbers, numRange);
                    }
                }
                else
                {
                    minCnt = 0;
                    possibleNumbers = new List<int>();
                    possibleNumbersTarget = new List<int>();
                    currentNumbers = new List<int>();
                    for (int k = 1; k <= area.Count - 2; k++)
                    {
                        currentNumbers.Add(grid.Fields[area[k + 1].Row - 1, area[k + 1].Col - 1].Number);
                        if (cntPosition <= k)
                        {
                            foreach (List<int> combination in _combinations[(target, k, cntPosition)])
                            {
                                if (combination.Contains(k) && !currentNumbers.Contains(k) && currentNumbers.GetRange(0, k).Intersect(combination).Count() == currentNumbers.GetRange(0, k).Where(x => x != 0).Count())
                                {
                                    if (minCnt == 0)
                                        minCnt = k;
                                    possibleNumbers.AddRange(combination);
                                    possibleNumbersTarget.Add(k);
                                }
                            }
                        }
                        else
                        {
                            foreach (List<int> combination in _combinations[(target, k, 0)])
                            {
                                if (!currentNumbers.Contains(k) && currentNumbers.GetRange(0, k).Intersect(combination).Count() == currentNumbers.GetRange(0, k).Where(x => x != 0).Count())
                                {
                                    if (minCnt == 0)
                                        minCnt = k;
                                    possibleNumbers.AddRange(combination);
                                    possibleNumbersTarget.Add(k);
                                }
                            }
                        }
                    }
                    possibleNumbers = possibleNumbers.Distinct().ToList();
                    possibleNumbersTarget = possibleNumbersTarget.Distinct().ToList();
                    possibleNumbers.RemoveAll(x => currentNumbers.Contains(x));
                    possibleNumbersTarget.RemoveAll(x => currentNumbers.Contains(x));
                    HelperBan.BanListInverse(grid, area[0].Row, area[0].Col, possibleNumbersTarget, numRange);
                    for (int k = 1; k <= minCnt; k++)
                    {
                        if (grid.Fields[area[k + 1].Row - 1, area[k + 1].Col - 1].Number == 0)
                            HelperBan.BanListInverse(grid, area[k + 1].Row, area[k + 1].Col, possibleNumbers, numRange);
                    }
                }
            }
        }

        private Dictionary<(int, int, int), List<List<int>>> GetCombinations(int numRange)
        {
            List<int> targets = new();
            int maxTarget;
            Dictionary<int, List<List<int>>> sumCombinations = new();
            Dictionary<int, List<List<int>>> productCombinations = new();
            Dictionary<(int, int, int), List<List<int>>> combinations = new();
            List<int> duplicitiesIndex;
            sumCombinations[1] = new List<List<int>>();
            productCombinations[1] = new List<List<int>>();

            foreach (List<Coordinates> area in _sumProductFields)
                targets.Add(area[0].Number);
            targets = targets.Select(x => x).Distinct().ToList();
            maxTarget = targets.Max();
            for (int k = 1; k <= numRange; k++)
            {
                if (k <= maxTarget)
                {
                    sumCombinations[1].Add(new List<int> { k, k });
                    productCombinations[1].Add(new List<int> { k, k });
                }
            }
            for (int kk = 2; kk <= numRange; kk++)
            {
                sumCombinations[kk] = new List<List<int>>();
                productCombinations[kk] = new List<List<int>>();
                foreach (List<int> combination in sumCombinations[kk - 1])
                {
                    for (int k = combination[^1] + 1; k <= numRange; k++)
                    {
                        if (combination[0] + k <= maxTarget)
                        {
                            sumCombinations[kk].Add(combination.ToList());
                            sumCombinations[kk][^1].Add(k);
                            sumCombinations[kk][^1][0] += k;
                        }
                    }
                }
                foreach (List<int> combination in productCombinations[kk - 1])
                {
                    for (int k = combination[^1] + 1; k <= numRange; k++)
                    {
                        if (combination[0] * k <= maxTarget)
                        {
                            productCombinations[kk].Add(combination.ToList());
                            productCombinations[kk][^1].Add(k);
                            productCombinations[kk][^1][0] *= k;
                        }
                    }
                }
            }
            for (int kk = 1; kk <= numRange; kk++)
            {
                foreach (List<int> combination in sumCombinations[kk])
                {
                    if (!targets.Contains(combination[0]))
                        continue;
                    if (combinations.ContainsKey((combination[0], kk, 0)))
                    {
                        combinations[(combination[0], kk, 0)].Add(combination.ToList());
                        combinations[(combination[0], kk, 0)][^1].RemoveAt(0);
                    }
                    else
                    {
                        combinations[(combination[0], kk, 0)] = new List<List<int>> { combination.ToList() };
                        combinations[(combination[0], kk, 0)][^1].RemoveAt(0);
                    }
                }
                foreach (List<int> combination in productCombinations[kk])
                {
                    if (!targets.Contains(combination[0]))
                        continue;
                    if (combinations.ContainsKey((combination[0], kk, 0)))
                    {
                        combinations[(combination[0], kk, 0)].Add(combination.ToList());
                        combinations[(combination[0], kk, 0)][^1].RemoveAt(0);
                    }
                    else
                    {
                        combinations[(combination[0], kk, 0)] = new List<List<int>> { combination.ToList() };
                        combinations[(combination[0], kk, 0)][^1].RemoveAt(0);
                    }
                }
            }
            for (int kk = 1; kk <= numRange; kk++)
            {
                foreach (int target in targets)
                {
                    if (!combinations.ContainsKey((target, kk, 0)))
                        combinations[(target, kk, 0)] = new List<List<int>>();
                }
            }
            for (int kk = 1; kk <= numRange; kk++)
            {
                foreach (int target in targets)
                {
                    duplicitiesIndex = new List<int>();
                    for (int indexA = 0; indexA < combinations[(target, kk, 0)].Count; indexA++)
                    {
                        for (int indexB = indexA + 1; indexB < combinations[(target, kk, 0)].Count; indexB++)
                        {
                            if (combinations[(target, kk, 0)][indexA].Intersect(combinations[(target, kk, 0)][indexB]).Count() == combinations[(target, kk, 0)][indexA].Count)
                                duplicitiesIndex.Add(indexB);
                        }
                    }
                    duplicitiesIndex = duplicitiesIndex.Distinct().ToList();
                    duplicitiesIndex.Sort();
                    for (int index = duplicitiesIndex.Count - 1; index >= 0; index--)
                        combinations[(target, kk, 0)].RemoveAt(index);
                }
            }
            foreach (List<Coordinates> area in _sumProductFields)
            {
                for (int k = 1; k <= numRange; k++)
                {
                    if (combinations.ContainsKey((area[0].Number, k, area[1].Number)))
                        continue;
                    combinations[(area[0].Number, k, area[1].Number)] = new List<List<int>>();
                    foreach (List<int> combination in combinations[(area[0].Number, k, 0)])
                    {
                        if (area[1].Number <= k || combination.Contains(area[1].Number))
                            combinations[(area[0].Number, k, area[1].Number)].Add(combination.ToList());
                    }
                }
            }
            return combinations;
        }
    }
}