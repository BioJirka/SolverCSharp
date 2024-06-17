using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverConsecutiveEdge : ISolver
    {
        private readonly List<Coordinates>[,] _consecutiveFields;
        private const int _priorityConsecutive = (int)Priority.Mid;
        private const int _priorityNonConsecutive = (int)Priority.Low;

        public SolverConsecutiveEdge(List<Coordinates> consecutiveFields, bool areAllConsecutivesMarked, int rowCnt, int colCnt)
        {
            _consecutiveFields = new List<Coordinates>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _consecutiveFields[i - 1, j - 1] = new List<Coordinates>();
            }
            foreach (Coordinates coordinates in consecutiveFields)
            {
                switch (coordinates.Direction)
                {
                    case "S":
                        _consecutiveFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row + 1, Col = coordinates.Col, Text = coordinates.Text });
                        _consecutiveFields[coordinates.Row, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text });
                        break;
                    case "E":
                        _consecutiveFields[coordinates.Row - 1, coordinates.Col - 1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col + 1, Text = coordinates.Text });
                        _consecutiveFields[coordinates.Row - 1, coordinates.Col].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Text = coordinates.Text });
                        break;
                }
            }
            if (!areAllConsecutivesMarked)
                return;
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    if (!_consecutiveFields[i - 1, j - 1].Any(x => x.Row == i + 1 && x.Col == j) && i < rowCnt)
                    {
                        _consecutiveFields[i - 1, j - 1].Add(new Coordinates { Row = i + 1, Col = j, Text = "O" });
                        _consecutiveFields[i, j - 1].Add(new Coordinates { Row = i, Col = j, Text = "O" });
                    }
                    if (!_consecutiveFields[i - 1, j - 1].Any(x => x.Row == i && x.Col == j + 1) && j < colCnt)
                    {
                        _consecutiveFields[i - 1, j - 1].Add(new Coordinates { Row = i, Col = j + 1, Text = "O" });
                        _consecutiveFields[i - 1, j].Add(new Coordinates { Row = i, Col = j, Text = "O" });
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    grid.Fields[i - 1, j - 1].Priority += _priorityConsecutive * _consecutiveFields[i - 1, j - 1].Where(x => x.Text == "X").Count() + _priorityNonConsecutive * _consecutiveFields[i - 1, j - 1].Where(x => x.Text == "O").Count();
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (Coordinates coordinates in _consecutiveFields[row - 1, col - 1])
            {
                switch (coordinates.Text)
                {
                    case "X":
                        HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, new List<int> { number - 1, number + 1 }, numRange);
                        break;
                    case "O":
                        HelperBan.BanList(grid, coordinates.Row, coordinates.Col, new List<int> { number - 1, number + 1 }, numRange);
                        break;
                }
            }
        }
    }
}