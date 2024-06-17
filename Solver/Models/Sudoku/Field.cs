namespace Solver.Sudoku.Models
{
    public class Field
    {
        public int Number { get; set; }
        public bool[] Bans { get; set; }
        public int BanCnt { get; set; }
        public int Priority { get; set; }

        public Field(int numRange)
        {
            Bans = new bool[numRange];
        }
    }
}