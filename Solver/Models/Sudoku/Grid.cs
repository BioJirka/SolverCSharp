namespace Solver.Sudoku.Models
{
    public class Grid
    {
        public Field[,] Fields { get; set; }

        public Grid(int rowCnt, int colCnt, int numRange)
        {
            Fields = new Field[rowCnt, colCnt];
            for (int i = 0; i < rowCnt; i++)
            {
                for (int j = 0; j < colCnt; j++)
                    Fields[i, j] = new Field(numRange);
            }
        }
    }
}