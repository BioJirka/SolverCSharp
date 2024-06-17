using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverKiller : ISolver
    {
        private readonly List<List<Coordinates>> _killerFields;
        private readonly List<List<Coordinates>>[,] _killerFieldsMapping;
        private const int _prioritySmallArea = (int)Priority.Mid;
        private const int _priorityBigArea = (int)Priority.Low;

        public SolverKiller(List<List<Coordinates>> killerFields, int rowCnt, int colCnt)
        {
            _killerFields = killerFields;
            _killerFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _killerFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (List<Coordinates> area in _killerFields)
            {
                foreach (Coordinates coordinates in area)
                    _killerFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(area);
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            int priority;
            foreach (List<Coordinates> area in _killerFields)
            {
                priority = area.Count < 4 ? _prioritySmallArea : _priorityBigArea;
                foreach (Coordinates coordinates in area)
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            foreach (List<Coordinates> area in _killerFields)
            {
                foreach (Coordinates coordinates in area)
                    HelperSolver.BanSum(grid, coordinates.Row, coordinates.Col, numRange, area[0].Number, area.Count);
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            int subSum;
            int emptyCnt;
            foreach (List<Coordinates> area in _killerFieldsMapping[row - 1, col - 1])
            {
                subSum = 0;
                emptyCnt = 0;
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        emptyCnt++;
                    else
                        subSum += grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number;
                }
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        HelperSolver.BanSum(grid, coordinates.Row, coordinates.Col, numRange, area[0].Number - subSum, emptyCnt);
                }
            }
        }
    }
}