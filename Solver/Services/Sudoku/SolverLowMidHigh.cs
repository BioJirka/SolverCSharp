using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverLowMidHigh : ISolver
    {
        private readonly List<Coordinates> _lowMidHighFields;
        
        public SolverLowMidHigh(List<Coordinates> lowMidHighFields)
        {
            _lowMidHighFields = lowMidHighFields;
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            int breakPointLowMid = numRange / 3;
            int breakPointMidHigh = 2 * numRange / 3;
            List<int> lowNumbers = Enumerable.Range(1, breakPointLowMid).ToList();
            List<int> midNumbers = Enumerable.Range(breakPointLowMid + 1, breakPointMidHigh - breakPointLowMid).ToList();
            List<int> highNumbers = Enumerable.Range(breakPointMidHigh + 1, numRange - breakPointMidHigh).ToList();
            foreach (Coordinates coordinates in _lowMidHighFields)
            {
                if (coordinates.Text == "Low")
                    HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, lowNumbers, numRange);
                else if (coordinates.Text == "Mid")
                    HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, midNumbers, numRange);
                else if (coordinates.Text == "High")
                    HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, highNumbers, numRange);
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
        }
    }
}