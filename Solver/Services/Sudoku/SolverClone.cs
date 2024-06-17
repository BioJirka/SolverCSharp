using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverClone : ISolver
    {
        private readonly List<Coordinates>[,] _cloneFields;
        private const int _priority = (int)Priority.Mid;

        public SolverClone(List<List<List<Coordinates>>> cloneFields, int rowCnt, int colCnt)
        {
            _cloneFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _cloneFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (List<List<Coordinates>> areas in cloneFields)
            {
                for (int a = 0; a < areas.Count; a++)
                {
                    for (int b = a + 1; b < areas.Count; b++)
                    {
                        for (int c = 0; c < areas[0].Count; c++)
                        {
                            _cloneFields[areas[a][c].Row - 1, areas[a][c].Col - 1].Add(new Coordinates { Row = areas[b][c].Row, Col = areas[b][c].Col });
                            _cloneFields[areas[b][c].Row - 1, areas[b][c].Col - 1].Add(new Coordinates { Row = areas[a][c].Row, Col = areas[a][c].Col });
                        }
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priority * _cloneFields[i - 1, j - 1].Count;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            bool allEmpty = true;
            foreach (Coordinates coordinates in _cloneFields[row - 1, col - 1])
            {
                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number != 0)
                {
                    allEmpty = false;
                    break;
                }
            }
            if (!allEmpty)
                return;
            foreach (Coordinates coordinates in _cloneFields[row - 1, col - 1])
            {
                if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                    HelperBan.BanInverse(grid, coordinates.Row, coordinates.Col, number, numRange);
            }
        }
    }
}