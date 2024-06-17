using Solver.Sudoku.Helpers;
using Solver.Sudoku.Services;
using System;
using System.Text.Json;

// To Do:
// add possibility to have blank fields (sudokuro)

namespace Solver.Sudoku.Models
{
    public class SudokuClass
    {
        public string Status = "New"; // { New ; Error in input ; Solved ; Does not have a solution }
        public List<string> Messages = new();
        public string SudokuInputJson = string.Empty;
        public SudokuInput SudokuInput = new();
        public List<List<List<int>>> OutputNumbers = new();
        public int SolutionCnt = new();
        public List<List<Grid>> Grids = new();
        public List<ISolver> Solvers = new();

        public SudokuClass(string sudokuInputString)
        {
            if (string.IsNullOrWhiteSpace(sudokuInputString))
            {
                Status = "Error in input";
                Messages.Add("Empty input");
                return;
            }
            SudokuInputJson = sudokuInputString;
            try
            {
                SudokuInput = JsonSerializer.Deserialize<SudokuInput>(sudokuInputString) ?? new();
            }
            catch
            {
                Status = "Error in input";
                Messages.Add("Creating sudoku object failed");
                return;
            }
            SudokuBuilderLastStep();
        }

        public SudokuClass(SudokuInput sudokuInputBuilder)
        {
            SudokuInputJson = Newtonsoft.Json.JsonConvert.SerializeObject(sudokuInputBuilder);
            SudokuInput = sudokuInputBuilder;
            SudokuBuilderLastStep();
        }

        private void SudokuBuilderLastStep()
        {
            List<string> inputErrorMessages = SudokuInput.SudokuInputCheck();
            if (inputErrorMessages.Count > 0)
            {
                Status = "Error in input";
                Messages.AddRange(inputErrorMessages);
            }
            if (Status == "New")
                AddSolvers();
        }

        private void AddSolvers()
        {
            Solvers.Add(new SolverBasic());
            if (SudokuInput.Types.Contains("Basic"))
                Solvers.Add(new SolverArea(HelperArea.GetAreasBasic(SudokuInput.RowCnt, SudokuInput.ColCnt), SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Diagonal"))
                Solvers.Add(new SolverArea(HelperArea.GetAreasDiagonal(SudokuInput.RowCnt, SudokuInput.ColCnt), SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Windoku"))
                Solvers.Add(new SolverArea(HelperArea.GetAreasWindoku(SudokuInput.RowCnt, SudokuInput.ColCnt), SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Area"))
                Solvers.Add(new SolverArea(SudokuInput.AreaFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("AntiWindoku"))
                Solvers.Add(new SolverAntiArea(HelperArea.GetAreasAntiWindoku(SudokuInput.RowCnt, SudokuInput.ColCnt), SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Clone"))
                Solvers.Add(new SolverClone(SudokuInput.CloneFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Shape"))
                Solvers.Add(new SolverShape(SudokuInput.ShapeFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("AntiKing"))
                Solvers.Add(new SolverAntiKing());
            if (SudokuInput.Types.Contains("AntiKnight"))
                Solvers.Add(new SolverAntiKnight());
            if (SudokuInput.Types.Contains("EvenOdd"))
                Solvers.Add(new SolverEvenOdd(SudokuInput.EvenOddFields));
            if (SudokuInput.Types.Contains("EvenOddArea"))
                Solvers.Add(new SolverEvenOddArea(SudokuInput.EvenOddAreaFields, SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange));
            if (SudokuInput.Types.Contains("LowMidHigh"))
                Solvers.Add(new SolverLowMidHigh(SudokuInput.LowMidHighFields));
            if (SudokuInput.Types.Contains("Fortress"))
                Solvers.Add(new SolverFortress(SudokuInput.FortressFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("ConsecutiveEdge"))
                Solvers.Add(new SolverConsecutiveEdge(SudokuInput.ConsecutiveEdgeFields, SudokuInput.AreAllConsecutiveEdgesMarked, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Roman"))
                Solvers.Add(new SolverRoman(SudokuInput.RomanFields, SudokuInput.AreAllRomansMarked, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Difference"))
                Solvers.Add(new SolverDifference(SudokuInput.DifferenceFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("MinDifference"))
                Solvers.Add(new SolverMinDifference(SudokuInput.MinDifferenceFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("SumEdge"))
                Solvers.Add(new SolverSumEdge(SudokuInput.SumEdgeFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Inequality"))
                Solvers.Add(new SolverInequality(SudokuInput.InequalityFields, SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange));
            if (SudokuInput.Types.Contains("Thermometer"))
                Solvers.Add(new SolverThermometer(SudokuInput.ThermometerFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("WhereIsX"))
                Solvers.Add(new SolverWhereIsX(SudokuInput.WhereIsXFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("DynamicSumOrProduct"))
                Solvers.Add(new SolverDynamicSumOrProduct(SudokuInput.DynamicSumOrProductFields, SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange));
            if (SudokuInput.Types.Contains("Killer"))
                Solvers.Add(new SolverKiller(SudokuInput.KillerFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("SumWeighted"))
                Solvers.Add(new SolverSumWeighted(SudokuInput.SumWeightedFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("SumCorner"))
                Solvers.Add(new SolverSumCorner(SudokuInput.SumCornerFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("EvenOddDot"))
                Solvers.Add(new SolverEvenOddDot(SudokuInput.EvenOddDotFields, SudokuInput.AreAllEvenOddDotsMarked, SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange));
            if (SudokuInput.Types.Contains("SumDot"))
                Solvers.Add(new SolverSumDot(SudokuInput.SumDotFields, SudokuInput.AreAllSumDotsMarked, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("SumArrow"))
                Solvers.Add(new SolverSumArrow(SudokuInput.SumArrowFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("ConsecutiveDot"))
                Solvers.Add(new SolverConsecutiveDot(SudokuInput.ConsecutiveDotFields, SudokuInput.AreAllConsecutiveDotsMarked, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("Clock"))
                Solvers.Add(new SolverClock(SudokuInput.ClockFields, SudokuInput.AreAllClocksMarked, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("ChildSum"))
                Solvers.Add(new SolverChildSum(SudokuInput.ChildSumFields, SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange));
            if (SudokuInput.Types.Contains("MagicSquare"))
                Solvers.Add(new SolverMagicSquare(SudokuInput.MagicSquareFields, SudokuInput.RowCnt, SudokuInput.ColCnt));
            if (SudokuInput.Types.Contains("ProductResult"))
                Solvers.Add(new SolverProductResult(SudokuInput.ProductResultFields, SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange));
        }

        public void Solve()
        {
            if (Status != "New")
            {
                switch (Status)
                {
                    case "Error in input":
                        Messages.Add("Sudoku with incorrect input values cannot be solved.");
                        break;
                    case "Solved":
                        Messages.Add("Sudoku has already been solved.");
                        break;
                    case "Does not have a solution":
                        Messages.Add("We have already tried to solve the sudoku, it does not have a solution.");
                        break;
                }
                return;
            }
            Grids.Add(new List<Grid> { new Grid(SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange) });
            foreach (ISolver solver in Solvers)
            {
                solver.SetPriorities(Grids[0][0], SudokuInput.RowCnt, SudokuInput.ColCnt);
                solver.BanInitial(Grids[0][0], SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange);
            }
            WriteInInputNumbers(Grids[0][0]);
            NextStep nextStep;
            while (Status == "New")
            {
                if (Grids[^1].Count == 0)
                {
                    //foreach (Grid grid in Grids[^2])
                    //    WriteOutputToConsole(grid);
                    //Console.ReadLine();
                    Status = "Does not have a solution";
                }
                else
                {
                    // every step adds exactly 1 number so if any grid is a solution, all grids in the same are solutions as well
                    if (IsSolved(Grids[^1][0]))
                    {
                        Status = "Solved";
                        SolutionCnt = Grids[^1].Count;
                        foreach (Grid grid in Grids[^1])
                        {
                            OutputNumbers.Add(new List<List<int>>());
                            for (int i = 1; i <= SudokuInput.RowCnt; i++ )
                            {
                                OutputNumbers[^1].Add(new List<int>());
                                for (int j = 1; j <= SudokuInput.ColCnt; j++)
                                    OutputNumbers[^1][^1].Add(grid.Fields[i - 1, j - 1].Number);
                            }
                        }
                    }
                    else
                    {
                        Grids.Add(new List<Grid>());
                        foreach (Grid grid in Grids[^2])
                        {
                            nextStep = SetNextStep(grid);
                            if (nextStep.IsNotPossibleToSolve)
                                continue;
                            foreach (int k in nextStep.Numbers)
                            {
                                Grids[^1].Add(HelperDeepCopy.CopyGrid(grid, SudokuInput.RowCnt, SudokuInput.ColCnt, SudokuInput.NumRange));
                                Grids[^1][^1].Fields[nextStep.Row - 1, nextStep.Col - 1].Number = k;
                                foreach (ISolver solver in Solvers)
                                    solver.Ban(Grids[^1][^1], nextStep.Row, SudokuInput.RowCnt, nextStep.Col, SudokuInput.ColCnt, k, SudokuInput.NumRange);
                            }
                        }
                    }
                }
            }
        }

        private NextStep SetNextStep(Grid grid)
        {
            NextStep nextStep = new() { Row = 1, Col = 1 };
            int priority;
            for (int i = 1; i <= SudokuInput.RowCnt; i++)
            {
                for (int j = 1; j <= SudokuInput.ColCnt; j++)
                {
                    if (grid.Fields[i - 1, j - 1].Number == 0)
                    {
                        if (grid.Fields[i - 1, j - 1].BanCnt == SudokuInput.NumRange)
                        {
                            nextStep.IsNotPossibleToSolve = true;
                            nextStep.Row = i;
                            nextStep.Col = j;
                            nextStep.MaxPriority = 1_000_000;
                            nextStep.Numbers = new List<int>();
                        }
                        else
                        {
                            if (grid.Fields[i - 1, j - 1].BanCnt == SudokuInput.NumRange - 1)
                                priority = 1_000_000;
                            else
                                priority = 1_000 * (grid.Fields[i - 1, j - 1].BanCnt + 1) + grid.Fields[i - 1, j - 1].Priority;
                            
                            if (priority > nextStep.MaxPriority)
                            {
                                nextStep.MaxPriority = priority;
                                nextStep.Row = i;
                                nextStep.Col = j;
                                nextStep.Numbers = new List<int>();
                                for (int k = 1; k <= SudokuInput.NumRange; k++)
                                {
                                    if (!grid.Fields[i - 1, j - 1].Bans[k - 1])
                                        nextStep.Numbers.Add(k);
                                }
                            }
                        }
                    }
                }
            }
            return nextStep;
        }

        private bool IsSolved(Grid grid)
        {
            for (int i = 1; i <= SudokuInput.RowCnt; i++)
            {
                for (int j = 1; j <= SudokuInput.ColCnt; j++)
                {
                    if (grid.Fields[i - 1, j - 1].Number == 0)
                        return false;
                }
            }
            return true;
        }

        private void WriteInInputNumbers(Grid grid)
        {
            foreach (Coordinates inputNumber in SudokuInput.InputNumbers)
            {
                if (grid.Fields[inputNumber.Row - 1, inputNumber.Col - 1].Number != 0)
                {
                    Status = "Error in input";
                    Messages.Add($"Two numbers for row {inputNumber.Row} and column {inputNumber.Col}");
                }
                else if (grid.Fields[inputNumber.Row - 1, inputNumber.Col - 1].Bans[inputNumber.Number - 1])
                {
                    Status = "Error in input";
                    Messages.Add($"Number {inputNumber.Number} cannot be in row {inputNumber.Row} and column {inputNumber.Col}");
                }
                else
                {
                    grid.Fields[inputNumber.Row - 1, inputNumber.Col - 1].Number = inputNumber.Number;
                    foreach (ISolver solver in Solvers)
                        solver.Ban(grid, inputNumber.Row, SudokuInput.RowCnt, inputNumber.Col, SudokuInput.ColCnt, inputNumber.Number, SudokuInput.NumRange);
                }
            }
        }

        private void WriteOutputToConsole(Grid grid)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 1; i <= SudokuInput.RowCnt; i++)
            {
                Console.Write(" ");
                for (int j = 1; j <= SudokuInput.ColCnt; j++)
                {
                    Console.Write(grid.Fields[i - 1, j - 1].Number.ToString());
                    if (j % 3 == 0)
                        Console.Write(" ");
                }
                Console.WriteLine();
                if (i % 3 == 0)
                    Console.WriteLine();
            }
        }
    }
}