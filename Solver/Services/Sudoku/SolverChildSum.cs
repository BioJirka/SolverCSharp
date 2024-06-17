using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverChildSum : ISolver
    {
        private readonly List<ChildSumClass> _childSumFields;
        private readonly List<ChildSumClass>[,] _childSumFieldsMapping;
        private const int _priority = (int)Priority.Mid;

        public SolverChildSum(List<List<Coordinates>> childSumFields, int rowCnt, int colCnt, int numRange)
        {
            int index;
            _childSumFields = new List<ChildSumClass>();
            _childSumFieldsMapping = new List<ChildSumClass>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _childSumFieldsMapping[i - 1, j - 1] = new List<ChildSumClass>();
            }
            foreach (List<Coordinates> area in childSumFields)
            {
                index = area.TakeWhile(x => x.Text != "Coordinates").Count();
                _childSumFields.Add(new ChildSumClass(area[0].Number, area.GetRange(1, index - 1).Select(x => x.Number).ToList(), area.GetRange(index, area.Count - index)));
                foreach (Coordinates coordinates in _childSumFields[^1].Area)
                    _childSumFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(_childSumFields[^1]);
            }
            SetPossibleSolutions(numRange);
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (ChildSumClass area in _childSumFields)
            {
                foreach (Coordinates coordinates in area.Area)
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            List<int> currentSolution;
            List<List<int>> possibleSolutions;
            List<int> possibleNumbers;
            foreach (ChildSumClass area in _childSumFields)
            {
                currentSolution = new List<int> (new int[area.Area.Count]);
                possibleSolutions = area.GetPossibleSolutions(currentSolution);
                for (int index = 0; index < area.Area.Count; index ++)
                {
                    possibleNumbers = possibleSolutions.Select(x => x[index]).Distinct().ToList();
                    HelperBan.BanListInverse(grid, area.Area[index].Row, area.Area[index].Col, possibleNumbers, numRange);
                }
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            List<int> currentSolution;
            List<List<int>> possibleSolutions;
            List<int> possibleNumbers;
            foreach (ChildSumClass area in _childSumFieldsMapping[row - 1, col - 1])
            {
                currentSolution = new();
                foreach (Coordinates coordinates in area.Area)
                    currentSolution.Add(grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number);
                possibleSolutions = area.GetPossibleSolutions(currentSolution);
                for (int index = 0; index < area.Area.Count; index ++)
                {
                    if (grid.Fields[area.Area[index].Row - 1, area.Area[index].Col - 1].Number != 0)
                        continue;
                    possibleNumbers = possibleSolutions.Select(x => x[index]).Distinct().ToList();
                    HelperBan.BanListInverse(grid, area.Area[index].Row, area.Area[index].Col, possibleNumbers, numRange);
                }
            }
        }

        private void SetPossibleSolutions(int numRange)
        {
            int maxCntGenerate = 0;
            int maxSumToGenerate = 0;
            int maxBlockCnt = 0;
            int maxCntToBlock = 0;
            int hash;
            int cnt;
            foreach (ChildSumClass area in _childSumFields)
            {
                maxCntGenerate = Math.Max(maxCntGenerate, area.Area.Count - area.SumNumbers.Count + 1);
                maxSumToGenerate = Math.Max(maxSumToGenerate, area.MaxSumNumber);
                maxBlockCnt = Math.Max(maxBlockCnt, area.SumNumbers.Count);
                maxCntToBlock = Math.Max(maxCntToBlock, area.Area.Count);
            }
            Dictionary<(int, int), List<List<int>>> sumsGenerated = new();
            for (int s = 1; s <= maxSumToGenerate; s++)
            {
                sumsGenerated[(s, 1)] = new List<List<int>>();
                if (s <= numRange)
                    sumsGenerated[(s, 1)].Add(new List<int> { s });
            }
            for (int n = 2; n <= maxCntGenerate; n++)
            {
                for (int s = 1; s <= maxSumToGenerate; s++)
                {
                    sumsGenerated[(s, n)] = new List<List<int>>();
                    for (int k = 1; k < s; k++)
                    {
                        if (k > numRange)
                            continue;
                        foreach (List<int> numbers in sumsGenerated[(s - k, n - 1)])
                        {
                            sumsGenerated[(s, n)].Add(numbers.ToList());
                            sumsGenerated[(s, n)][^1].Add(k);
                        }
                    }
                }
            }
            Dictionary<(int, int), List<List<int>>> blocksGenerated = new();
            for (int ns = 1; ns <= Math.Min(maxBlockCnt, maxCntToBlock); ns++)
                blocksGenerated[(ns, 1)] = new List<List<int>> { new List<int> { ns } };
            for (int n = 2; n <= maxBlockCnt; n++)
            {
                blocksGenerated[(n, n)] = new List<List<int>>();
                blocksGenerated[(n, n)].Add(blocksGenerated[(n - 1, n - 1)][0].ToList());
                blocksGenerated[(n, n)][0].Add(1);
                for (int s = n + 1; s <= maxCntToBlock; s++)
                {
                    blocksGenerated[(s, n)] = new List<List<int>>();
                    foreach (List<int> numbers in blocksGenerated[(s - 1, n)])
                    {
                        for (int index = 0; index < numbers.Count; index ++)
                        {
                            blocksGenerated[(s, n)].Add(numbers.ToList());
                            blocksGenerated[(s, n)][^1][index]++;
                            cnt = blocksGenerated[(s, n)].Count;
                            hash = GetListHash(blocksGenerated[(s, n)][^1], maxSumToGenerate);
                            if (blocksGenerated[(s, n)].TakeWhile(x => GetListHash(x, maxSumToGenerate) != hash).Count() != cnt - 1)
                                blocksGenerated[(s, n)].RemoveAt(cnt - 1);
                        }
                    }
                }
            }
            List<List<List<int>>> possibleSolutions;
            foreach(ChildSumClass area in _childSumFields)
            {
                foreach (List<int> blocks in blocksGenerated[(area.Area.Count, area.SumNumbers.Count)])
                {
                    possibleSolutions = new List<List<List<int>>>();
                    for (int index = 0; index < area.SumNumbers.Count; index++)
                    {
                        possibleSolutions.Add(new List<List<int>>());
                        if (index == 0)
                        {
                            foreach (List<int> sums in sumsGenerated[(area.SumNumbers[index], blocks[index])])
                                possibleSolutions[^1].Add(sums.ToList());
                        }
                        else
                        {
                            foreach (List<int> sums in sumsGenerated[(area.SumNumbers[index], blocks[index])])
                            {
                                if (sums[0] <= area.MaxSumNumber - area.SumNumbers[index - 1])
                                    continue;
                                foreach (List<int> solutions in possibleSolutions[^2])
                                {
                                    possibleSolutions[^1].Add(solutions.ToList());
                                    possibleSolutions[^1][^1].AddRange(sums.ToList());
                                }
                            }
                        }
                    }
                    foreach (List<int> solution in possibleSolutions[^1])
                        area.PossibleSolutions.Add(solution.ToList());
                }
            }
        }

        private static int GetListHash(List<int> list, int maxNumber)
        {
            int result = 0;
            foreach (int k in list)
                result = result * (maxNumber + 1) + k;
            return result;
        }
    }

    internal class ChildSumClass
    {
        public int MaxSumNumber;
        public List<int> SumNumbers = new();
        public List<Coordinates> Area = new();
        public List<List<int>> PossibleSolutions = new();

        public ChildSumClass(int maxSumNumber, List<int> sumNumbers, List<Coordinates> area)
        {
            MaxSumNumber = maxSumNumber;
            SumNumbers = sumNumbers;
            Area = area;
        }

        public List<List<int>> GetPossibleSolutions(List<int> currentSolution)
        {
            List<List<int>> possibleSolutions = PossibleSolutions;
            for (int index = 0; index < currentSolution.Count; index++)
            {
                if (currentSolution[index] != 0)
                    possibleSolutions = possibleSolutions.Where(x => x[index] == currentSolution[index]).ToList();
            }
            return possibleSolutions;
        }
    }
}