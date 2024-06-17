using Solver.Sudoku.Models;

namespace Solver.Sudoku.Helpers
{
    public static class HelperArea
    {
        public static List<List<Coordinates>> GetAreasBasic(int rowCnt, int colCnt)
        {
            List<List<Coordinates>> areas = new();
            if (rowCnt == 9 && colCnt == 9)
            {
                foreach (int i in new List<int>{1, 4, 7})
                {
                    foreach (int j in new List<int>{1, 4, 7})
                    {
                        areas.Add(new List<Coordinates>()
                            {  
                                new Coordinates { Row = i, Col = j}, new Coordinates { Row = i, Col = j + 1}, new Coordinates { Row = i, Col = j + 2},
                                new Coordinates { Row = i + 1, Col = j}, new Coordinates { Row = i + 1, Col = j + 1}, new Coordinates { Row = i + 1, Col = j + 2},
                                new Coordinates { Row = i + 2, Col = j}, new Coordinates { Row = i + 2, Col = j + 1}, new Coordinates { Row = i + 2, Col = j + 2}
                            });
                    }
                }
            }
            return areas;
        }

        public static List<List<Coordinates>> GetAreasDiagonal(int rowCnt, int colCnt)
        {
            List<List<Coordinates>> areas = new();
            if (rowCnt == colCnt)
            {
                areas.Add(new List<Coordinates>());
                areas.Add(new List<Coordinates>());
                for (int i = 1; i <= rowCnt; i++)
                {
                    areas[0].Add(new Coordinates { Row = i, Col = i});
                    areas[1].Add(new Coordinates { Row = i, Col = rowCnt + 1 - i});
                }
            }
            return areas;
        }

        public static List<List<Coordinates>> GetAreasWindoku(int rowCnt, int colCnt)
        {
            List<List<Coordinates>> areas = new();
            if (rowCnt == 9 && colCnt == 9)
            {
                foreach (int i in new List<int>{2, 6})
                {
                    foreach (int j in new List<int>{2, 6})
                    {
                        areas.Add(new List<Coordinates>()
                            {
                                new Coordinates { Row = i, Col = j}, new Coordinates { Row = i, Col = j + 1}, new Coordinates { Row = i, Col = j + 2},
                                new Coordinates { Row = i + 1, Col = j}, new Coordinates { Row = i + 1, Col = j + 1}, new Coordinates { Row = i + 1, Col = j + 2},
                                new Coordinates { Row = i + 2, Col = j}, new Coordinates { Row = i + 2, Col = j + 1}, new Coordinates { Row = i + 2, Col = j + 2}
                            });
                    }
                }
            }
            return areas;
        }

        public static List<List<Coordinates>> GetAreasAntiWindoku(int rowCnt, int colCnt)
        {
            List<List<Coordinates>> areas = GetAreasWindoku(rowCnt, colCnt);
            if (rowCnt == 9 && colCnt == 9)
            {
                foreach (List<Coordinates> area in areas)
                    area[0].Number = 4;
            }
            return areas;
        }
    }
}