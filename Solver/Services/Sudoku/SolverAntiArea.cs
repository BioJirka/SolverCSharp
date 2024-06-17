using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverAntiArea : ISolver
    {
        private readonly List<List<Coordinates>> _antiAreaFields;
        private readonly List<List<Coordinates>>[,] _antiAreaFieldsMapping;
        private const int _priorityBigArea = (int)Priority.Mid;
        private const int _prioritySmallArea = (int)Priority.Low;

        public SolverAntiArea(List<List<Coordinates>> antiAreaFields, int rowCnt, int colCnt)
        {
            _antiAreaFields = antiAreaFields;
            _antiAreaFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    _antiAreaFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
                    foreach (List<Coordinates> area in _antiAreaFields)
                    {
                        if (area.Any(x => x.Row == i && x.Col == j))
                            _antiAreaFieldsMapping[i - 1, j - 1].Add(area);
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            int priority;
            foreach (List<Coordinates> area in _antiAreaFields)
            {
                priority = 2 * area[0].Number <= area.Count ? _priorityBigArea : _prioritySmallArea;
                foreach (Coordinates coordinates in area)
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            List<int> currentNumbers;
            List<int> currentNumbersDistinct;
            foreach (List<Coordinates> area in _antiAreaFieldsMapping[row - 1, col - 1])
            {
                currentNumbers = new List<int>();
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number != 0)
                        currentNumbers.Add(grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number);
                }
                currentNumbersDistinct = currentNumbers.Distinct().ToList();
                if (currentNumbersDistinct.Count == area[0].Number && currentNumbers.Where(x => x == number).ToList().Count == 1)
                {
                    foreach (Coordinates coordinates in area)
                    {
                        if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                            HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, currentNumbersDistinct, numRange);
                    }
                }
            }
        }
    }
}