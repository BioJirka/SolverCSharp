using Solver.Sudoku.Models;

namespace Solver.Sudoku.Helpers
{
    public static class HelperBan
    {
        public static void Ban(Grid grid, int row, int col, int number, int numRange)
        {
            if (NumCheck(number, numRange))
                BanInternal(grid, row, col, number);
        }

        public static void BanRange(Grid grid, int row, int col, int minNumber, int maxNumber, int numRange)
        {
            minNumber = Math.Max(minNumber, 1);
            maxNumber = Math.Min(maxNumber, numRange);
            BanRangeInternal(grid, row, col, minNumber, maxNumber);
        }

        public static void BanList(Grid grid, int row, int col, List<int> numbers, int numRange)
        {
            foreach (int number in numbers)
            {
                if (0 < number && number <= numRange)
                    BanInternal(grid, row, col, number);
            }
        }

        public static void BanInverse(Grid grid, int row, int col, int number, int numRange)
        {
            if (0 < number && number <= numRange)
                BanInverseInternal(grid, row, col, number, numRange);
            else
                BanRangeInternal(grid, row, col, 1, numRange);
        }

        public static void BanRangeInverse(Grid grid, int row, int col, int minNumber, int maxNumber, int numRange)
        {
            minNumber = Math.Min(Math.Max(minNumber, 1), numRange);
            maxNumber = Math.Min(Math.Max(maxNumber, 1), numRange);
            BanRangeInverseInternal(grid, row, col, minNumber, maxNumber, numRange);
        }

        public static void BanListInverse(Grid grid, int row, int col, List<int> numbers, int numRange)
        {
            for (int k = 1; k <= numRange; k++)
            {
                if (!numbers.Contains(k))
                    BanInternal(grid, row, col, k);
            }
        }

        public static void BanAll(Grid grid, int row, int col, int numRange)
        {
            BanRangeInternal(grid, row, col, 1, numRange);
        }

        private static void BanInternal(Grid grid, int row, int col, int number)
        {
            if (!grid.Fields[row - 1, col - 1].Bans[number - 1])
            {
                grid.Fields[row - 1, col - 1].Bans[number - 1] = true;
                grid.Fields[row - 1, col - 1].BanCnt++;
            }
        }

        private static void BanRangeInternal(Grid grid, int row, int col, int minNumber, int maxNumber)
        {
            for (int k = minNumber; k <= maxNumber; k++)
                BanInternal(grid, row, col, k);
        }

        private static void BanRangeInverseInternal(Grid grid, int row, int col, int minNumber, int maxNumber, int numRange)
        {
            BanRangeInternal(grid, row, col, 1, minNumber - 1);
            BanRangeInternal(grid, row, col, maxNumber + 1, numRange);
        }

        private static void BanInverseInternal(Grid grid, int row, int col, int number, int numRange)
        {
            BanRangeInverseInternal(grid, row, col, number, number, numRange);
        }

        private static bool NumCheck(int number, int numRange)
        {
            return 0 < number && number <= numRange;
        }
    }
}