using Solver.Sudoku.Models;

namespace Solver.Sudoku.Services
{
    public class SolverSumWeighted : ISolver
    {
        private readonly List<List<Coordinates>> _sumWeightedFields;
        private readonly List<List<Coordinates>>[,] _sumWeightedFieldsMapping;
        private const int _prioritySmallArea = (int)Priority.Mid;
        private const int _priorityBigArea = (int)Priority.Low;

        public SolverSumWeighted(List<List<Coordinates>> sumWeightedFields, int rowCnt, int colCnt)
        {
            _sumWeightedFields = sumWeightedFields;
            _sumWeightedFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _sumWeightedFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            foreach (List<Coordinates> area in _sumWeightedFields)
            {
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    _sumWeightedFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(area);
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            int priority;
            foreach (List<Coordinates> area in _sumWeightedFields)
            {
                priority = area.Count <= 4 ? _prioritySmallArea : _priorityBigArea;
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            int weights;
            foreach (List<Coordinates> area in _sumWeightedFields)
            {
                weights = 0;
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    weights += coordinates.Number;
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                {
                    if (coordinates.Number == 0)
                        continue;
                    HelperSolver.BanSumWeighted(grid, coordinates.Row, coordinates.Col, numRange, area[0].Number, coordinates.Number, weights - coordinates.Number);
                }                
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            int subSum;
            int weights;
            foreach (List<Coordinates> area in _sumWeightedFieldsMapping[row - 1, col - 1])
            {
                subSum = 0;
                weights = 0;
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        weights += coordinates.Number;
                    else
                        subSum += coordinates.Number * grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number;
                }
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        HelperSolver.BanSumWeighted(grid, coordinates.Row, coordinates.Col, numRange, area[0].Number - subSum, coordinates.Number, weights - coordinates.Number);
                }
            }
        }
    }
}