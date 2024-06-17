using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverEvenOdd : ISolver
    {
        private readonly List<Coordinates> _evenOddFields;
        
        public SolverEvenOdd(List<Coordinates> evenOddFields)
        {
            _evenOddFields = evenOddFields;
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            List<int> evenNumbers = Enumerable.Range(1, numRange).Where(x => x % 2 == 0).ToList();
            List<int> oddNumbers = Enumerable.Range(1, numRange).Where(x => x % 2 != 0).ToList();
            foreach (Coordinates coordinates in _evenOddFields)
            {
                if (coordinates.Text == "Even")
                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, oddNumbers, numRange);
                else if (coordinates.Text == "Odd")
                    HelperBan.BanList(grid, coordinates.Row, coordinates.Col, evenNumbers, numRange);
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
        }
    }
}