using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverFortress : ISolver
    {
        private readonly List<Coordinates>[,] _fortressFields;
        private const int _priority = (int)Priority.Low;
        
        public SolverFortress(List<Coordinates> fortressFields, int rowCnt, int colCnt)
        {
            _fortressFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _fortressFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (Coordinates coordinates in fortressFields)
            {
                if (coordinates.Row > 1 && !fortressFields.Any(x => x.Row == coordinates.Row - 1 && x.Col == coordinates.Col))
                {
                    _fortressFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row - 1, Col = coordinates.Col, Text = "Lower" });
                    _fortressFields[coordinates.Row - 2, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = "Higher" });
                }
                if (coordinates.Row < rowCnt && !fortressFields.Any(x => x.Row == coordinates.Row + 1 && x.Col == coordinates.Col))
                {
                    _fortressFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Text = "Lower" });
                    _fortressFields[coordinates.Row, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = "Higher" });
                }
                if (coordinates.Col > 1 && !fortressFields.Any(x => x.Row == coordinates.Row && x.Col == coordinates.Col - 1))
                {
                    _fortressFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col - 1, Text = "Lower" });
                    _fortressFields[coordinates.Row - 1, coordinates.Col - 2].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = "Higher" });
                }
                if (coordinates.Col < colCnt && !fortressFields.Any(x => x.Row == coordinates.Row && x.Col == coordinates.Col + 1))
                {
                    _fortressFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Text = "Lower" });
                    _fortressFields[coordinates.Row - 1, coordinates.Col].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = "Higher" });
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priority * _fortressFields[i - 1, j - 1].Count;
            }
        }
        
        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    if (_fortressFields[i - 1, j - 1].Count == 0)
                        continue;
                    switch (_fortressFields[i - 1, j - 1][0].Text)
                    {
                        case "Lower":
                            HelperBan.Ban(grid, i, j, 1, numRange);
                            break;
                        case "Higher":
                            HelperBan.Ban(grid, i, j, numRange, numRange);
                            break;
                    }
                }
            }
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates coordinates in _fortressFields[row - 1, col - 1])
            {
                switch (coordinates.Text)
                {
                    case "Lower":
                        HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, number, numRange, numRange);
                        break;
                    case "Higher":
                        HelperBan.BanRange(grid, coordinates.Row, coordinates.Col, 1, number, numRange);
                        break;
                }
            }
        }
    }
}