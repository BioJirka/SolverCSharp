using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverAntiKing : ISolver
    {
        private readonly List<Coordinates> _moves;
        
        public SolverAntiKing()
        {
            _moves = new()
            {
                new Coordinates { Row = 1, Col = 1 },
                new Coordinates { Row = 1, Col = 0 },
                new Coordinates { Row = 1, Col = -1 },
                new Coordinates { Row = 0, Col = 1 },
                new Coordinates { Row = 0, Col = -1 },
                new Coordinates { Row = -1, Col = 1 },
                new Coordinates { Row = -1, Col = 0 },
                new Coordinates { Row = -1, Col = -1 }
            };
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates move in _moves)
            {
                if (row + move.Row > 0 && row + move.Row <= rowCnt && col + move.Col > 0 && col + move.Col <= colCnt)
                    HelperBan.Ban(grid, row + move.Row, col + move.Col, number, numRange);
            }
        }
    }
}