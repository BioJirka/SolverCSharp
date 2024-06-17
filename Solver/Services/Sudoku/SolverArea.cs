using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverArea : ISolver
    {
        private readonly List<List<Coordinates>> _areaFields;
        private readonly List<List<Coordinates>>[,] _areaFieldsMapping;
        private const int _priority = (int)Priority.Low;

        public SolverArea(List<List<Coordinates>> areaFields, int rowCnt, int colCnt)
        {
            _areaFields = areaFields;
            _areaFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    _areaFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
                    foreach (List<Coordinates> area in _areaFields)
                    {
                        if (area.Any(x => x.Row == i && x.Col == j))
                            _areaFieldsMapping[i - 1, j - 1].Add(area);
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (List<Coordinates> area in _areaFields)
            {
                foreach (Coordinates coordinates in area)
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _priority;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (List<Coordinates> area in _areaFieldsMapping[row - 1, col - 1])
            {
                foreach (Coordinates coordinates in area)
                {
                    if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                        HelperBan.Ban(grid, coordinates.Row, coordinates.Col, number, numRange);
                }
            }
        }
    }
}