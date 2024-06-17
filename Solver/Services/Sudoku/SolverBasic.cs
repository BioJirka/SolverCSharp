using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverBasic : ISolver
    {
        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
        }
        
        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            for (int j = 1; j <= colCnt; j++)
            {
                if (grid.Fields[row - 1, j - 1].Number == 0)
                    HelperBan.Ban(grid, row, j, number, numRange);
            }
            for (int i = 1; i <= rowCnt; i++)
            {
                if (grid.Fields[i - 1, col - 1].Number == 0)
                    HelperBan.Ban(grid, i, col, number, numRange);
            }
        }
    }
}