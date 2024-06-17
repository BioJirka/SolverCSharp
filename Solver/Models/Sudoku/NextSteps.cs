namespace Solver.Sudoku.Models
{
    public class NextStep
    {
        public bool IsNotPossibleToSolve { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public int MaxPriority { get; set; }
        public List<int> Numbers { get; set; } = new List<int>();
    }
}