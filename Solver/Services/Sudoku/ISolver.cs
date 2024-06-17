using Solver.Sudoku.Models;

namespace Solver.Sudoku.Services
{
    public interface ISolver
    {
        void SetPriorities(Grid grid, int rowCnt, int colCnt);
        void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange);
        void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange);
    }
}