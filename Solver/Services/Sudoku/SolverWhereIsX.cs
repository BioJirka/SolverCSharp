using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverWhereIsX : ISolver
    {
        private readonly List<List<Coordinates>> _whereIsXFields;
        private readonly List<List<Coordinates>>[,] _whereIsXMapping;
        private const int _priorityArrow = (int)Priority.Mid;
        private const int _priorityTarget = (int)Priority.Low;

        public SolverWhereIsX(List<Coordinates> whereIsXFields, int rowCnt, int colCnt)
        {
            _whereIsXFields = new List<List<Coordinates>>();
            _whereIsXMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                    _whereIsXMapping[i - 1, j - 1] = new List<List<Coordinates>>();
            }
            int ij;
            foreach (Coordinates coordinates in whereIsXFields)
            {
                _whereIsXFields.Add(new List<Coordinates> { new Coordinates { Row = coordinates.Row, Col = coordinates.Col, Number = coordinates.Number } });
                switch (coordinates.Direction)
                {
                    case "N":
                        for (int i = coordinates.Row - 1; i > 0; i--)
                            _whereIsXFields.Last().Add(new Coordinates { Row = i, Col = coordinates.Col });
                        break;
                    case "W":
                        for (int j = coordinates.Col - 1; j > 0; j--)
                            _whereIsXFields.Last().Add(new Coordinates { Row = coordinates.Row, Col = j });
                        break;
                    case "S":
                        for (int i = coordinates.Row + 1; i <= rowCnt; i++)
                            _whereIsXFields.Last().Add(new Coordinates { Row = i, Col = coordinates.Col });
                        break;
                    case "E":
                        for (int j = coordinates.Col + 1; j <= colCnt; j++)
                            _whereIsXFields.Last().Add(new Coordinates { Row = coordinates.Row, Col = j });
                        break;
                    case "NW":
                        ij = 1;
                        while (coordinates.Row - ij > 0 && coordinates.Col - ij > 0)
                        {
                            _whereIsXFields.Last().Add(new Coordinates { Row = coordinates.Row - ij, Col = coordinates.Col - ij });
                            ij++;
                        }
                        break;
                    case "NE":
                        ij = 1;
                        while (coordinates.Row - ij > 0 && coordinates.Col + ij <= colCnt)
                        {
                            _whereIsXFields.Last().Add(new Coordinates { Row = coordinates.Row - ij, Col = coordinates.Col + ij });
                            ij++;
                        }
                        break;
                    case "SW":
                        ij = 1;
                        while (coordinates.Row + ij <= rowCnt && coordinates.Col - ij > 0)
                        {
                            _whereIsXFields.Last().Add(new Coordinates { Row = coordinates.Row + ij, Col = coordinates.Col - ij });
                            ij++;
                        }
                        break;
                    case "SE":
                        ij = 1;
                        while (coordinates.Row + ij <= rowCnt && coordinates.Col + ij <= colCnt)
                        {
                            _whereIsXFields.Last().Add(new Coordinates { Row = coordinates.Row + ij, Col = coordinates.Col + ij });
                            ij++;
                        }
                        break;
                }
            }
            foreach (List<Coordinates> area in _whereIsXFields)
            {
                foreach (Coordinates coordinates in area)
                    _whereIsXMapping[coordinates.Row - 1, coordinates.Col - 1].Add(area);
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (List<Coordinates> area in _whereIsXFields)
            {
                grid.Fields[area[0].Row - 1, area[0].Col - 1].Priority += _priorityArrow;
                foreach (Coordinates coordinates in area.GetRange(1, area.Count - 1))
                    grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _priorityTarget;
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            foreach (List<Coordinates> area in _whereIsXFields)
                HelperBan.BanRange(grid, area[0].Row, area[0].Col, area.Count, numRange, numRange);
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            foreach (List<Coordinates> area in _whereIsXMapping[row - 1, col - 1])
            {
                if (area[0].Row == row && area[0].Col == col)
                    HelperBan.BanInverse(grid, area[number].Row, area[number].Col, area[0].Number, numRange);
                else
                {
                    if (number != area[0].Number)
                        HelperBan.Ban(grid, area[0].Row, area[0].Col, Math.Max(Math.Abs(row - area[0].Row), Math.Abs(col - area[0].Col)), numRange);
                }
            }
        }
    }
}