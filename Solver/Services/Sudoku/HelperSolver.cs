using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    internal static class HelperSolver
    {
        internal static void BanSum(Grid grid, int row, int col, int numRange, int sum, int emptyCnt)
        {
            HelperBan.BanRangeInverse(grid, row, col, sum - numRange * (emptyCnt - 1), sum - (emptyCnt - 1), numRange);
        }

        internal static List<int> BanSum(int numRange, int sum, int emptyCnt)
        {
            List<int> possibleNumbers = new();
            for (int k = sum - numRange * (emptyCnt - 1); k <= sum - (emptyCnt - 1); k++)
                possibleNumbers.Add(k);
            return possibleNumbers;
        }

        internal static void BanSumWeighted(Grid grid, int row, int col, int numRange, int sum, int weight, int weightOther)
        {
            HelperBan.BanRangeInverse(grid, row, col, (sum - numRange * weightOther - 1) / weight + 1, (sum - weightOther) / weight, numRange);
        }
    }
}