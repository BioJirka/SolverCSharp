namespace Solver.Sudoku.Models
{
    public class Coordinates
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Number { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
    }
}