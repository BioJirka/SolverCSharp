using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverSumArrow : ISolver
    {
        private readonly List<List<Coordinates>> _sumArrowFields;
        private readonly List<List<Coordinates>>[,] _sumArrowFieldsMapping;
        private const int _prioritySumTarget = (int)Priority.Mid;
        private const int _prioritySum = (int)Priority.Low;

        public SolverSumArrow(List<List<Coordinates>> sumArrowFields, int rowCnt, int colCnt)
        {
            _sumArrowFields = sumArrowFields;
            _sumArrowFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _sumArrowFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (List<Coordinates> area in _sumArrowFields)
            {
                foreach (Coordinates coordinates in area)
                    _sumArrowFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(area);
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (List<Coordinates> area in _sumArrowFields)
            {
                grid.Fields[area[0].Row - 1, area[0].Col - 1].Priority += _prioritySumTarget;
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _prioritySum;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            foreach (List<Coordinates> area in _sumArrowFields)
            {
                HelperBan.BanRange(grid, area[0].Row, area[0].Col, 1, area.Count - 2, numRange);
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, numRange - area.Count + 3, numRange, numRange);
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            int sumTarget;
            int subSum;
            int emptyCnt;
            foreach (List<Coordinates> area in _sumArrowFieldsMapping[row - 1, col - 1])
            {
                sumTarget = grid.Fields[area[0].Row - 1, area[0].Col - 1].Number;
                subSum = 0;
                emptyCnt = 0;
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        emptyCnt++;
                    else
                        subSum += grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number;
                }
                if (sumTarget == 0)
                {
                    if (emptyCnt == 0)
                        HelperBan.BanInverse(grid, area[0].Row, area[0].Col, subSum, numRange);
                    else
                        HelperBan.BanRange(grid, area[0].Row, area[0].Col, 1, subSum + emptyCnt - 1, numRange);
                    foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    {
                        if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                            HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, numRange - subSum - emptyCnt + 2, numRange, numRange);
                    }
                }
                else
                {
                    foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    {
                        if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                            HelperSolver.BanSum(grid, coordinates.Row, coordinates.Col, numRange, sumTarget - subSum, emptyCnt);
                    }
                }
            }
        }
    }
}