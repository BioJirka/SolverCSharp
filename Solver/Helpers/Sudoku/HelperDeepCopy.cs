using Newtonsoft.Json;
using Solver.Sudoku.Models;

namespace Solver.Sudoku.Helpers
{
    public static class HelperDeepCopy
    {
        // do not use, takes too much time (at least in our case where we need to call it thousands of times)
        public static T DeepCopy<T>(T myObject)
        {
            JsonSerializerSettings deserializeSettings = new() { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(myObject), deserializeSettings);
        }

        public static Grid CopyGrid(Grid grid, int rowCnt, int colCnt, int numRange)
        {
            Grid newGrid = new(rowCnt, colCnt, numRange);
            for (int i = 0; i < rowCnt; i++)
            {
                for (int j = 0; j < colCnt; j++)
                {
                    if (grid.Fields[i, j].Number != 0)
                        newGrid.Fields[i, j].Number = grid.Fields[i, j].Number;
                    else
                    {
                        newGrid.Fields[i, j].Priority = grid.Fields[i, j].Priority;
                        newGrid.Fields[i, j].Bans = grid.Fields[i, j].Bans.ToArray();
                        newGrid.Fields[i, j].BanCnt = grid.Fields[i, j].BanCnt;
                    }
                }
            }
            return newGrid;
        }
    }
}