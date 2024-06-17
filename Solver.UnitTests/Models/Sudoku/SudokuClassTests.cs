using Newtonsoft.Json;
using Solver.Sudoku.Models;

namespace Solver.UnitTests.Sudoku.Models
{
    [TestClass]
    public class SudokuClassTests
    {
        private SudokuInput _sudokuInput;
        private SudokuClass _sudoku;

        [TestInitialize]
        public void TestInit()
        {
            _sudokuInput = new SudokuInput { 
                RowCnt = 9, 
                ColCnt = 9, 
                NumRange = 9, 
                Types = new List<string> { "Basic" }, 
                InputNumbers = new List<Coordinates>(), 
                };
        }

        // for each sudoku type check if
            // solution was found
            // there is only 1 solution
            // values in the middle area are correct

        [TestMethod]
        public void Solve_Basic1_SolveSudoku()
        {
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Number = 5 }, 
                new Coordinates { Row = 1, Col = 5, Number = 7 }, 
                new Coordinates { Row = 1, Col = 8, Number = 8 }, 
                new Coordinates { Row = 2, Col = 1, Number = 4 }, 
                new Coordinates { Row = 2, Col = 2, Number = 1 }, 
                new Coordinates { Row = 2, Col = 4, Number = 9 }, 
                new Coordinates { Row = 2, Col = 5, Number = 3 }, 
                new Coordinates { Row = 2, Col = 9, Number = 7 }, 
                new Coordinates { Row = 3, Col = 1, Number = 9 }, 
                new Coordinates { Row = 3, Col = 3, Number = 2 }, 
                new Coordinates { Row = 3, Col = 5, Number = 1 }, 
                new Coordinates { Row = 3, Col = 8, Number = 4 }, 
                new Coordinates { Row = 4, Col = 1, Number = 2 }, 
                new Coordinates { Row = 4, Col = 5, Number = 4 }, 
                new Coordinates { Row = 4, Col = 9, Number = 5 }, 
                new Coordinates { Row = 5, Col = 1, Number = 6 }, 
                new Coordinates { Row = 5, Col = 5, Number = 5 }, 
                new Coordinates { Row = 6, Col = 7, Number = 6 }, 
                new Coordinates { Row = 7, Col = 6, Number = 8 }, 
                new Coordinates { Row = 7, Col = 8, Number = 9 }, 
                new Coordinates { Row = 8, Col = 1, Number = 1 }, 
                new Coordinates { Row = 8, Col = 3, Number = 3 }, 
                new Coordinates { Row = 8, Col = 7, Number = 8 }, 
                new Coordinates { Row = 8, Col = 9, Number = 6 }, 
                new Coordinates { Row = 9, Col = 2, Number = 2 }, 
                new Coordinates { Row = 9, Col = 4, Number = 4 }, 
                new Coordinates { Row = 9, Col = 8, Number = 5 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 4, 1, 3, 5, 2, 7, 8, 9 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Basic2_SolveSudoku()
        {
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 2, Number = 7 }, 
                new Coordinates { Row = 1, Col = 3, Number = 2 }, 
                new Coordinates { Row = 1, Col = 4, Number = 1 }, 
                new Coordinates { Row = 1, Col = 8, Number = 8 }, 
                new Coordinates { Row = 2, Col = 1, Number = 5 }, 
                new Coordinates { Row = 2, Col = 5, Number = 9 }, 
                new Coordinates { Row = 2, Col = 9, Number = 6 }, 
                new Coordinates { Row = 3, Col = 1, Number = 6 }, 
                new Coordinates { Row = 3, Col = 5, Number = 2 }, 
                new Coordinates { Row = 3, Col = 8, Number = 7 }, 
                new Coordinates { Row = 4, Col = 1, Number = 2 }, 
                new Coordinates { Row = 4, Col = 2, Number = 3 }, 
                new Coordinates { Row = 4, Col = 3, Number = 4 }, 
                new Coordinates { Row = 4, Col = 4, Number = 5 }, 
                new Coordinates { Row = 4, Col = 5, Number = 6 }, 
                new Coordinates { Row = 4, Col = 9, Number = 7 }, 
                new Coordinates { Row = 5, Col = 1, Number = 1 }, 
                new Coordinates { Row = 5, Col = 5, Number = 7 }, 
                new Coordinates { Row = 6, Col = 7, Number = 3 }, 
                new Coordinates { Row = 7, Col = 6, Number = 9 }, 
                new Coordinates { Row = 7, Col = 8, Number = 5 }, 
                new Coordinates { Row = 8, Col = 1, Number = 9 }, 
                new Coordinates { Row = 8, Col = 3, Number = 5 }, 
                new Coordinates { Row = 8, Col = 7, Number = 6 }, 
                new Coordinates { Row = 8, Col = 9, Number = 2 }, 
                new Coordinates { Row = 9, Col = 2, Number = 1 }, 
                new Coordinates { Row = 9, Col = 4, Number = 6 }, 
                new Coordinates { Row = 9, Col = 8, Number = 3 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 6, 1, 9, 7, 3, 2, 4, 8 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Basic3_SolveSudoku()
        {
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 2, Number = 4 }, 
                new Coordinates { Row = 1, Col = 3, Number = 3 }, 
                new Coordinates { Row = 1, Col = 4, Number = 2 }, 
                new Coordinates { Row = 1, Col = 8, Number = 1 }, 
                new Coordinates { Row = 2, Col = 1, Number = 5 }, 
                new Coordinates { Row = 2, Col = 5, Number = 1 }, 
                new Coordinates { Row = 2, Col = 9, Number = 2 }, 
                new Coordinates { Row = 3, Col = 1, Number = 6 }, 
                new Coordinates { Row = 3, Col = 8, Number = 9 }, 
                new Coordinates { Row = 4, Col = 1, Number = 7 }, 
                new Coordinates { Row = 4, Col = 5, Number = 2 }, 
                new Coordinates { Row = 4, Col = 9, Number = 9 }, 
                new Coordinates { Row = 5, Col = 2, Number = 3 }, 
                new Coordinates { Row = 5, Col = 3, Number = 9 }, 
                new Coordinates { Row = 5, Col = 4, Number = 6 }, 
                new Coordinates { Row = 6, Col = 7, Number = 8 }, 
                new Coordinates { Row = 7, Col = 6, Number = 5 }, 
                new Coordinates { Row = 7, Col = 8, Number = 4 }, 
                new Coordinates { Row = 8, Col = 1, Number = 9 }, 
                new Coordinates { Row = 8, Col = 3, Number = 4 }, 
                new Coordinates { Row = 8, Col = 7, Number = 6 }, 
                new Coordinates { Row = 8, Col = 9, Number = 8 }, 
                new Coordinates { Row = 9, Col = 2, Number = 8 }, 
                new Coordinates { Row = 9, Col = 4, Number = 3 }, 
                new Coordinates { Row = 9, Col = 8, Number = 7 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 2, 3, 6, 4, 8, 7, 9, 1 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Basic4_SolveSudoku()
        {
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 5 }, 
                new Coordinates { Row = 1, Col = 2, Number = 6 }, 
                new Coordinates { Row = 1, Col = 3, Number = 7 }, 
                new Coordinates { Row = 1, Col = 4, Number = 1 }, 
                new Coordinates { Row = 1, Col = 8, Number = 3 }, 
                new Coordinates { Row = 2, Col = 1, Number = 4 }, 
                new Coordinates { Row = 2, Col = 5, Number = 7 }, 
                new Coordinates { Row = 2, Col = 9, Number = 2 }, 
                new Coordinates { Row = 3, Col = 1, Number = 3 }, 
                new Coordinates { Row = 3, Col = 5, Number = 8 }, 
                new Coordinates { Row = 3, Col = 8, Number = 4 }, 
                new Coordinates { Row = 4, Col = 1, Number = 2 }, 
                new Coordinates { Row = 4, Col = 2, Number = 8 }, 
                new Coordinates { Row = 4, Col = 3, Number = 6 }, 
                new Coordinates { Row = 4, Col = 4, Number = 5 }, 
                new Coordinates { Row = 4, Col = 9, Number = 1 }, 
                new Coordinates { Row = 5, Col = 1, Number = 1 }, 
                new Coordinates { Row = 5, Col = 5, Number = 4 }, 
                new Coordinates { Row = 6, Col = 7, Number = 8 }, 
                new Coordinates { Row = 7, Col = 6, Number = 6 }, 
                new Coordinates { Row = 7, Col = 8, Number = 9 }, 
                new Coordinates { Row = 8, Col = 1, Number = 6 }, 
                new Coordinates { Row = 8, Col = 3, Number = 2 }, 
                new Coordinates { Row = 8, Col = 7, Number = 5 }, 
                new Coordinates { Row = 8, Col = 9, Number = 3 }, 
                new Coordinates { Row = 9, Col = 2, Number = 1 }, 
                new Coordinates { Row = 9, Col = 4, Number = 3 }, 
                new Coordinates { Row = 9, Col = 8, Number = 8 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 3, 9, 2, 4, 8, 7, 6, 1 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Diagonal_SolveSudoku()
        {
            _sudokuInput.Types.Add("Diagonal");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 2, Number =  9}, 
                new Coordinates { Row = 1, Col = 5, Number =  2}, 
                new Coordinates { Row = 1, Col = 8, Number =  5}, 
                new Coordinates { Row = 2, Col = 1, Number =  2}, 
                new Coordinates { Row = 2, Col = 7, Number =  8}, 
                new Coordinates { Row = 2, Col = 9, Number =  3}, 
                new Coordinates { Row = 3, Col = 4, Number =  7}, 
                new Coordinates { Row = 3, Col = 5, Number =  9}, 
                new Coordinates { Row = 3, Col = 6, Number =  5}, 
                new Coordinates { Row = 4, Col = 3, Number =  7}, 
                new Coordinates { Row = 4, Col = 7, Number =  9}, 
                new Coordinates { Row = 4, Col = 9, Number =  6}, 
                new Coordinates { Row = 5, Col = 1, Number =  1}, 
                new Coordinates { Row = 5, Col = 3, Number =  6}, 
                new Coordinates { Row = 5, Col = 6, Number =  7}, 
                new Coordinates { Row = 6, Col = 3, Number =  9}, 
                new Coordinates { Row = 6, Col = 5, Number =  5}, 
                new Coordinates { Row = 6, Col = 9, Number =  8}, 
                new Coordinates { Row = 7, Col = 2, Number =  6}, 
                new Coordinates { Row = 7, Col = 4, Number =  1}, 
                new Coordinates { Row = 8, Col = 1, Number =  4}, 
                new Coordinates { Row = 8, Col = 9, Number =  7}, 
                new Coordinates { Row = 9, Col = 2, Number =  1}, 
                new Coordinates { Row = 9, Col = 4, Number =  5}, 
                new Coordinates { Row = 9, Col = 6, Number =  8}, 
                new Coordinates { Row = 9, Col = 8, Number =  3}, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 3, 4, 1, 9, 8, 7, 6, 5, 2 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Windoku_SolveSudoku()
        {
            _sudokuInput.Types.Add("Windoku");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 1 }, 
                new Coordinates { Row = 1, Col = 4, Number = 5 }, 
                new Coordinates { Row = 1, Col = 6, Number = 3 }, 
                new Coordinates { Row = 2, Col = 7, Number = 5 }, 
                new Coordinates { Row = 2, Col = 9, Number = 3 }, 
                new Coordinates { Row = 3, Col = 6, Number = 9 }, 
                new Coordinates { Row = 3, Col = 8, Number = 6 }, 
                new Coordinates { Row = 4, Col = 1, Number = 9 }, 
                new Coordinates { Row = 4, Col = 5, Number = 8 }, 
                new Coordinates { Row = 4, Col = 9, Number = 6 }, 
                new Coordinates { Row = 5, Col = 4, Number = 9 }, 
                new Coordinates { Row = 6, Col = 1, Number = 6 }, 
                new Coordinates { Row = 6, Col = 3, Number = 2 }, 
                new Coordinates { Row = 6, Col = 8, Number = 9 }, 
                new Coordinates { Row = 6, Col = 9, Number = 5 }, 
                new Coordinates { Row = 7, Col = 2, Number = 5 }, 
                new Coordinates { Row = 7, Col = 8, Number = 7 }, 
                new Coordinates { Row = 8, Col = 3, Number = 4 }, 
                new Coordinates { Row = 8, Col = 6, Number = 5 }, 
                new Coordinates { Row = 8, Col = 7, Number = 6 }, 
                new Coordinates { Row = 9, Col = 2, Number = 3 }, 
                new Coordinates { Row = 9, Col = 4, Number = 6 }, 
                new Coordinates { Row = 9, Col = 6, Number = 4 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 2, 8, 7, 9, 5, 6, 3, 4, 1 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Area_SolveSudoku()
        {
            _sudokuInput.Types.Add("Area");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 5, Number = 3 }, 
                new Coordinates { Row = 1, Col = 6, Number = 9 }, 
                new Coordinates { Row = 2, Col = 7, Number = 7 }, 
                new Coordinates { Row = 2, Col = 8, Number = 5 }, 
                new Coordinates { Row = 3, Col = 6, Number = 6 }, 
                new Coordinates { Row = 3, Col = 8, Number = 4 }, 
                new Coordinates { Row = 4, Col = 5, Number = 1 }, 
                new Coordinates { Row = 4, Col = 7, Number = 9 }, 
                new Coordinates { Row = 4, Col = 9, Number = 4 }, 
                new Coordinates { Row = 5, Col = 1, Number = 6 }, 
                new Coordinates { Row = 5, Col = 4, Number = 9 }, 
                new Coordinates { Row = 5, Col = 5, Number = 8 }, 
                new Coordinates { Row = 6, Col = 1, Number = 2 }, 
                new Coordinates { Row = 6, Col = 3, Number = 4 }, 
                new Coordinates { Row = 6, Col = 9, Number = 5 }, 
                new Coordinates { Row = 7, Col = 2, Number = 7 }, 
                new Coordinates { Row = 7, Col = 4, Number = 4 }, 
                new Coordinates { Row = 8, Col = 2, Number = 8 }, 
                new Coordinates { Row = 8, Col = 3, Number = 2 }, 
                new Coordinates { Row = 9, Col = 4, Number = 7 }, 
                new Coordinates { Row = 9, Col = 6, Number = 8 }, 
                new Coordinates { Row = 9, Col = 9, Number = 3 }, 
                });
            _sudokuInput.AreaFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 2, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 5, Col = 2 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 5, Col = 6 }, 
                    new Coordinates { Row = 5, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 5 }, 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 8, Col = 5 }, 
                }});
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 2, 1, 5, 9, 8, 4, 3, 6, 7 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_AntiWindoku_SolveSudoku()
        {
            _sudokuInput.Types.Add("AntiWindoku");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 3, Number = 5 }, 
                new Coordinates { Row = 1, Col = 8, Number = 8 }, 
                new Coordinates { Row = 1, Col = 9, Number = 3 }, 
                new Coordinates { Row = 2, Col = 5, Number = 1 }, 
                new Coordinates { Row = 2, Col = 7, Number = 7 }, 
                new Coordinates { Row = 2, Col = 8, Number = 5 }, 
                new Coordinates { Row = 2, Col = 9, Number = 9 }, 
                new Coordinates { Row = 3, Col = 5, Number = 5 }, 
                new Coordinates { Row = 4, Col = 2, Number = 3 }, 
                new Coordinates { Row = 5, Col = 3, Number = 1 }, 
                new Coordinates { Row = 5, Col = 4, Number = 4 }, 
                new Coordinates { Row = 5, Col = 7, Number = 8 }, 
                new Coordinates { Row = 5, Col = 8, Number = 6 }, 
                new Coordinates { Row = 6, Col = 5, Number = 8 }, 
                new Coordinates { Row = 7, Col = 5, Number = 6 }, 
                new Coordinates { Row = 7, Col = 9, Number = 4 }, 
                new Coordinates { Row = 8, Col = 2, Number = 4 }, 
                new Coordinates { Row = 8, Col = 6, Number = 3 }, 

                new Coordinates { Row = 1, Col = 1, Number = 4 }, 
                new Coordinates { Row = 4, Col = 1, Number = 5 }, 
                new Coordinates { Row = 7, Col = 1, Number = 8 }, 
                new Coordinates { Row = 9, Col = 3, Number = 9 }, 
                new Coordinates { Row = 9, Col = 6, Number = 8 }, 
                new Coordinates { Row = 9, Col = 9, Number = 7 }, 

                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 9, 2, 4, 3, 5, 7, 8, 1 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Clone1_SolveSudoku()
        {
            _sudokuInput.Types.Add("Clone");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 2, Number = 9 }, 
                new Coordinates { Row = 1, Col = 3, Number = 2 }, 
                new Coordinates { Row = 2, Col = 1, Number = 4 }, 
                new Coordinates { Row = 2, Col = 2, Number = 5 }, 
                new Coordinates { Row = 2, Col = 6, Number = 6 }, 
                new Coordinates { Row = 2, Col = 7, Number = 9 }, 
                new Coordinates { Row = 3, Col = 1, Number = 3 }, 
                new Coordinates { Row = 3, Col = 7, Number = 4 }, 
                new Coordinates { Row = 3, Col = 8, Number = 7 }, 
                new Coordinates { Row = 4, Col = 4, Number = 1 }, 
                new Coordinates { Row = 4, Col = 8, Number = 8 }, 
                new Coordinates { Row = 6, Col = 2, Number = 6 }, 
                new Coordinates { Row = 6, Col = 6, Number = 2 }, 
                new Coordinates { Row = 7, Col = 2, Number = 7 }, 
                new Coordinates { Row = 7, Col = 3, Number = 1 }, 
                new Coordinates { Row = 7, Col = 9, Number = 4 }, 
                new Coordinates { Row = 8, Col = 3, Number = 5 }, 
                new Coordinates { Row = 8, Col = 4, Number = 7 }, 
                new Coordinates { Row = 8, Col = 8, Number = 9 }, 
                new Coordinates { Row = 8, Col = 9, Number = 3 }, 
                new Coordinates { Row = 9, Col = 7, Number = 1 }, 
                new Coordinates { Row = 9, Col = 8, Number = 2 }, 
                });
            _sudokuInput.CloneFields.Add(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 4, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 5 }, 
                    new Coordinates { Row = 4, Col = 6 }, 
                    new Coordinates { Row = 5, Col = 4 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 6, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 6, Col = 8 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 7, Col = 7 }, 
                    new Coordinates { Row = 8, Col = 6 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 1, 7, 3, 8, 6, 5, 4, 9, 2 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Clone2_SolveSudoku()
        {
            _sudokuInput.Types.Add("Clone");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 2, Number = 3 }, 
                new Coordinates { Row = 2, Col = 1, Number = 2 }, 
                new Coordinates { Row = 2, Col = 6, Number = 4 }, 
                new Coordinates { Row = 2, Col = 7, Number = 1 }, 
                new Coordinates { Row = 2, Col = 8, Number = 8 }, 
                new Coordinates { Row = 3, Col = 7, Number = 2 }, 
                new Coordinates { Row = 3, Col = 8, Number = 9 }, 
                new Coordinates { Row = 3, Col = 9, Number = 5 }, 
                new Coordinates { Row = 4, Col = 8, Number = 7 }, 
                new Coordinates { Row = 4, Col = 9, Number = 2 }, 
                new Coordinates { Row = 5, Col = 9, Number = 1 }, 
                new Coordinates { Row = 6, Col = 2, Number = 5 }, 
                new Coordinates { Row = 7, Col = 2, Number = 7 }, 
                new Coordinates { Row = 7, Col = 3, Number = 2 }, 
                new Coordinates { Row = 8, Col = 2, Number = 9 }, 
                new Coordinates { Row = 8, Col = 3, Number = 4 }, 
                new Coordinates { Row = 8, Col = 4, Number = 6 }, 
                new Coordinates { Row = 8, Col = 9, Number = 8 }, 
                new Coordinates { Row = 9, Col = 3, Number = 6 }, 
                new Coordinates { Row = 9, Col = 4, Number = 7 }, 
                new Coordinates { Row = 9, Col = 5, Number = 3 }, 
                new Coordinates { Row = 9, Col = 8, Number = 2 }, 
                });
            _sudokuInput.CloneFields.Add(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 2, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 5, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 6 }, 
                    new Coordinates { Row = 5, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 5 }, 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 8, Col = 5 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 9, 3, 4, 6, 7, 2, 1, 8 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Shape_SolveSudoku()
        {
            _sudokuInput.Types.Add("Shape");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 2, Col = 2, Number = 7 }, 
                new Coordinates { Row = 2, Col = 5, Number = 8 }, 
                new Coordinates { Row = 2, Col = 8, Number = 3 }, 
                new Coordinates { Row = 3, Col = 4, Number = 1 }, 
                new Coordinates { Row = 3, Col = 9, Number = 5 }, 
                new Coordinates { Row = 4, Col = 3, Number = 8 }, 
                new Coordinates { Row = 4, Col = 5, Number = 2 }, 
                new Coordinates { Row = 4, Col = 7, Number = 9 }, 
                new Coordinates { Row = 5, Col = 2, Number = 4 }, 
                new Coordinates { Row = 5, Col = 4, Number = 6 }, 
                new Coordinates { Row = 5, Col = 6, Number = 8 }, 
                new Coordinates { Row = 5, Col = 8, Number = 2 }, 
                new Coordinates { Row = 6, Col = 5, Number = 4 }, 
                new Coordinates { Row = 6, Col = 9, Number = 1 }, 
                new Coordinates { Row = 7, Col = 4, Number = 5 }, 
                new Coordinates { Row = 8, Col = 2, Number = 5 }, 
                new Coordinates { Row = 8, Col = 5, Number = 1 }, 
                new Coordinates { Row = 9, Col = 3, Number = 9 }, 
                new Coordinates { Row = 9, Col = 6, Number = 7 }, 
                new Coordinates { Row = 9, Col = 9, Number = 4 }, 
                });
            _sudokuInput.ShapeFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 7 }, 
                    new Coordinates { Row = 1, Col = 8 }, 
                    new Coordinates { Row = 2, Col = 6 }, 
                    new Coordinates { Row = 2, Col = 7 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 9 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 4, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 6 }, 
                    new Coordinates { Row = 4, Col = 6 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 9 }, 
                    new Coordinates { Row = 5, Col = 9 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 3 }, 
                    new Coordinates { Row = 6, Col = 3 }, 
                    new Coordinates { Row = 6, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 2 }, 
                    new Coordinates { Row = 7, Col = 1 }, 
                    new Coordinates { Row = 7, Col = 2 }, 
                    new Coordinates { Row = 8, Col = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 6, Col = 8 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 7, Col = 7 }, 
                    new Coordinates { Row = 8, Col = 6 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 8 }, 
                    new Coordinates { Row = 8, Col = 9 }, 
                    new Coordinates { Row = 9, Col = 8 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 9, Col = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 9, Col = 4 }, 
                    new Coordinates { Row = 9, Col = 5 }, }, });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 7, 2, 1, 6, 5, 8, 9, 4, 3 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_AntiKnight_SolveSudoku()
        {
            _sudokuInput.Types.Add("AntiKnight");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 2, Number = 4 }, 
                new Coordinates { Row = 1, Col = 4, Number = 7 }, 
                new Coordinates { Row = 1, Col = 8, Number = 6 }, 
                new Coordinates { Row = 2, Col = 1, Number = 9 }, 
                new Coordinates { Row = 2, Col = 9, Number = 3 }, 
                new Coordinates { Row = 3, Col = 4, Number = 5 }, 
                new Coordinates { Row = 3, Col = 6, Number = 4 }, 
                new Coordinates { Row = 4, Col = 1, Number = 8 }, 
                new Coordinates { Row = 4, Col = 3, Number = 5 }, 
                new Coordinates { Row = 4, Col = 9, Number = 7 }, 
                new Coordinates { Row = 5, Col = 6, Number = 3 }, 
                new Coordinates { Row = 6, Col = 3, Number = 1 }, 
                new Coordinates { Row = 6, Col = 5, Number = 5 }, 
                new Coordinates { Row = 6, Col = 7, Number = 9 }, 
                new Coordinates { Row = 7, Col = 6, Number = 9 }, 
                new Coordinates { Row = 7, Col = 8, Number = 2 }, 
                new Coordinates { Row = 8, Col = 1, Number = 1 }, 
                new Coordinates { Row = 8, Col = 7, Number = 5 }, 
                new Coordinates { Row = 9, Col = 2, Number = 3 }, 
                new Coordinates { Row = 9, Col = 4, Number = 4 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 9, 4, 2, 1, 7, 3, 8, 5, 6 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_AntiKing_SolveSudoku()
        {
            _sudokuInput.Types.Add("AntiKing");
            _sudokuInput.Types.Add("AntiKnight");
            _sudokuInput.Types.Add("ConsecutiveEdge");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 5, Col = 3, Number = 1 }, 
                new Coordinates { Row = 6, Col = 7, Number = 2 }, 
                });
            _sudokuInput.ConsecutiveEdgeFields.AddRange(new List<Coordinates>());
            _sudokuInput.AreAllConsecutiveEdgesMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 2, 6, 1, 5, 9, 4, 8, 3, 7 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_EvenOdd1_SolveSudoku()
        {
            _sudokuInput.Types.Add("EvenOdd");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 7 }, 
                new Coordinates { Row = 1, Col = 3, Number = 5 }, 
                new Coordinates { Row = 1, Col = 8, Number = 1 }, 
                new Coordinates { Row = 2, Col = 5, Number = 4 }, 
                new Coordinates { Row = 2, Col = 6, Number = 5 }, 
                new Coordinates { Row = 3, Col = 1, Number = 6 }, 
                new Coordinates { Row = 3, Col = 8, Number = 5 }, 
                new Coordinates { Row = 4, Col = 6, Number = 4 }, 
                new Coordinates { Row = 5, Col = 2, Number = 5 }, 
                new Coordinates { Row = 5, Col = 7, Number = 9 }, 
                new Coordinates { Row = 5, Col = 8, Number = 4 }, 
                new Coordinates { Row = 5, Col = 9, Number = 6 }, 
                new Coordinates { Row = 6, Col = 2, Number = 7 }, 
                new Coordinates { Row = 6, Col = 4, Number = 1 }, 
                new Coordinates { Row = 7, Col = 5, Number = 6 }, 
                new Coordinates { Row = 7, Col = 9, Number = 1 }, 
                new Coordinates { Row = 8, Col = 1, Number = 8 }, 
                new Coordinates { Row = 8, Col = 3, Number = 9 }, 
                new Coordinates { Row = 8, Col = 5, Number = 7 }, 
                new Coordinates { Row = 9, Col = 5, Number = 3 }, 
                new Coordinates { Row = 9, Col = 7, Number = 2 }, 
                new Coordinates { Row = 9, Col = 9, Number = 5 }, 
                });
            _sudokuInput.EvenOddFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 5, Text = "Even" }, 
                new Coordinates { Row = 2, Col = 2, Text = "Even" }, 
                new Coordinates { Row = 2, Col = 4, Text = "Even" }, 
                new Coordinates { Row = 2, Col = 8, Text = "Even" }, 
                new Coordinates { Row = 3, Col = 3, Text = "Even" }, 
                new Coordinates { Row = 3, Col = 4, Text = "Even" }, 
                new Coordinates { Row = 3, Col = 9, Text = "Even" }, 
                new Coordinates { Row = 4, Col = 2, Text = "Even" }, 
                new Coordinates { Row = 4, Col = 3, Text = "Even" }, 
                new Coordinates { Row = 4, Col = 4, Text = "Even" }, 
                new Coordinates { Row = 5, Col = 1, Text = "Even" }, 
                new Coordinates { Row = 5, Col = 5, Text = "Even" }, 
                new Coordinates { Row = 6, Col = 6, Text = "Even" }, 
                new Coordinates { Row = 6, Col = 9, Text = "Even" }, 
                new Coordinates { Row = 7, Col = 7, Text = "Even" }, 
                new Coordinates { Row = 8, Col = 2, Text = "Even" }, 
                new Coordinates { Row = 8, Col = 8, Text = "Even" }, 
                new Coordinates { Row = 9, Col = 3, Text = "Even" }, 
                new Coordinates { Row = 9, Col = 6, Text = "Even" }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 2, 5, 4, 7, 8, 3, 1, 9, 6 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_EvenOdd2_SolveSudoku()
        {
            _sudokuInput.Types.Add("EvenOdd");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 2, Number = 1 }, 
                new Coordinates { Row = 1, Col = 8, Number = 6 }, 
                new Coordinates { Row = 1, Col = 9, Number = 5 }, 
                new Coordinates { Row = 2, Col = 1, Number = 4 }, 
                new Coordinates { Row = 2, Col = 4, Number = 3 }, 
                new Coordinates { Row = 2, Col = 5, Number = 6 }, 
                new Coordinates { Row = 3, Col = 5, Number = 5 }, 
                new Coordinates { Row = 3, Col = 9, Number = 8 }, 
                new Coordinates { Row = 4, Col = 2, Number = 9 }, 
                new Coordinates { Row = 4, Col = 6, Number = 6 }, 
                new Coordinates { Row = 5, Col = 2, Number = 4 }, 
                new Coordinates { Row = 5, Col = 3, Number = 7 }, 
                new Coordinates { Row = 5, Col = 8, Number = 5 }, 
                new Coordinates { Row = 6, Col = 4, Number = 4 }, 
                new Coordinates { Row = 6, Col = 9, Number = 2 }, 
                new Coordinates { Row = 7, Col = 9, Number = 9 }, 
                new Coordinates { Row = 8, Col = 1, Number = 7 }, 
                new Coordinates { Row = 8, Col = 5, Number = 1 }, 
                new Coordinates { Row = 9, Col = 1, Number = 3 }, 
                new Coordinates { Row = 9, Col = 3, Number = 9 }, 
                new Coordinates { Row = 9, Col = 6, Number = 5 }, 
                new Coordinates { Row = 9, Col = 7, Number = 4 }, 
                });
            _sudokuInput.EvenOddFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Text = "Odd" }, 
                new Coordinates { Row = 1, Col = 6, Text = "Odd" }, 
                new Coordinates { Row = 2, Col = 3, Text = "Odd" }, 
                new Coordinates { Row = 2, Col = 6, Text = "Odd" }, 
                new Coordinates { Row = 3, Col = 2, Text = "Odd" }, 
                new Coordinates { Row = 3, Col = 3, Text = "Odd" }, 
                new Coordinates { Row = 3, Col = 7, Text = "Odd" }, 
                new Coordinates { Row = 4, Col = 4, Text = "Odd" }, 
                new Coordinates { Row = 4, Col = 8, Text = "Odd" }, 
                new Coordinates { Row = 5, Col = 5, Text = "Odd" }, 
                new Coordinates { Row = 6, Col = 1, Text = "Odd" }, 
                new Coordinates { Row = 6, Col = 2, Text = "Odd" }, 
                new Coordinates { Row = 6, Col = 6, Text = "Odd" }, 
                new Coordinates { Row = 7, Col = 3, Text = "Odd" }, 
                new Coordinates { Row = 7, Col = 7, Text = "Odd" }, 
                new Coordinates { Row = 7, Col = 8, Text = "Odd" }, 
                new Coordinates { Row = 8, Col = 4, Text = "Odd" }, 
                new Coordinates { Row = 8, Col = 7, Text = "Odd" }, 
                new Coordinates { Row = 9, Col = 9, Text = "Odd" }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 3, 6, 2, 9, 8, 4, 7, 1 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_EvenOddArea_SolveSudoku()
        {
            _sudokuInput.Types.Add("EvenOddArea");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 7 }, 
                new Coordinates { Row = 1, Col = 2, Number = 3 }, 
                new Coordinates { Row = 1, Col = 4, Number = 9 }, 
                new Coordinates { Row = 1, Col = 5, Number = 6 }, 
                new Coordinates { Row = 1, Col = 7, Number = 5 }, 
                new Coordinates { Row = 1, Col = 8, Number = 8 }, 
                new Coordinates { Row = 2, Col = 1, Number = 9 }, 
                new Coordinates { Row = 2, Col = 2, Number = 2 }, 
                new Coordinates { Row = 2, Col = 6, Number = 7 }, 
                new Coordinates { Row = 3, Col = 5, Number = 1 }, 
                new Coordinates { Row = 3, Col = 9, Number = 2 }, 
                new Coordinates { Row = 4, Col = 1, Number = 8 }, 
                new Coordinates { Row = 4, Col = 5, Number = 7 }, 
                new Coordinates { Row = 5, Col = 1, Number = 6 }, 
                new Coordinates { Row = 5, Col = 3, Number = 5 }, 
                new Coordinates { Row = 5, Col = 4, Number = 2 }, 
                new Coordinates { Row = 5, Col = 7, Number = 4 }, 
                new Coordinates { Row = 5, Col = 8, Number = 7 }, 
                new Coordinates { Row = 6, Col = 2, Number = 9 }, 
                new Coordinates { Row = 7, Col = 1, Number = 2 }, 
                new Coordinates { Row = 7, Col = 5, Number = 9 }, 
                new Coordinates { Row = 8, Col = 1, Number = 1 }, 
                new Coordinates { Row = 8, Col = 5, Number = 4 }, 
                new Coordinates { Row = 8, Col = 7, Number = 2 }, 
                new Coordinates { Row = 8, Col = 8, Number = 6 }, 
                new Coordinates { Row = 8, Col = 9, Number = 7 }, 
                new Coordinates { Row = 9, Col = 3, Number = 8 }, 
                new Coordinates { Row = 9, Col = 8, Number = 5 }, 
                new Coordinates { Row = 9, Col = 9, Number = 9 }, 
                });
            _sudokuInput.EvenOddAreaFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 4, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 6 }, 
                    new Coordinates { Row = 2, Col = 7 }, 
                    new Coordinates { Row = 2, Col = 8 }, 
                    new Coordinates { Row = 3, Col = 6 }, 
                    new Coordinates { Row = 3, Col = 7 }, 
                    new Coordinates { Row = 3, Col = 8 }, 
                    new Coordinates { Row = 4, Col = 6 }, 
                    new Coordinates { Row = 4, Col = 7 }, 
                    new Coordinates { Row = 4, Col = 8 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 2 }, 
                    new Coordinates { Row = 6, Col = 3 }, 
                    new Coordinates { Row = 6, Col = 4 }, 
                    new Coordinates { Row = 7, Col = 2 }, 
                    new Coordinates { Row = 7, Col = 3 }, 
                    new Coordinates { Row = 7, Col = 4 }, 
                    new Coordinates { Row = 8, Col = 2 }, 
                    new Coordinates { Row = 8, Col = 3 }, 
                    new Coordinates { Row = 8, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 6, Col = 8 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 7, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 8 }, 
                    new Coordinates { Row = 8, Col = 6 }, 
                    new Coordinates { Row = 8, Col = 7 }, 
                    new Coordinates { Row = 8, Col = 8 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 7, 5, 2, 3, 9, 1, 8, 4 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_LowMidHigh_SolveSudoku()
        {
            _sudokuInput.Types = new List<string>() { "Area", "LowMidHigh" };
            _sudokuInput.AreaFields = new List<List<Coordinates>> {
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 1 }, 
                    new Coordinates { Row = 1, Col = 2 }, 
                    new Coordinates { Row = 1, Col = 3 }, 
                    new Coordinates { Row = 1, Col = 4 }, 
                    new Coordinates { Row = 1, Col = 5 }, 
                    new Coordinates { Row = 2, Col = 1 }, 
                    new Coordinates { Row = 3, Col = 1 }, 
                    new Coordinates { Row = 4, Col = 1 }, 
                    new Coordinates { Row = 5, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 2, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 4, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 5 }, 
                    new Coordinates { Row = 5, Col = 4 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 3, Col = 7 }, 
                    new Coordinates { Row = 4, Col = 6 }, 
                    new Coordinates { Row = 4, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 5, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 4 }, 
                    new Coordinates { Row = 6, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 3 }, 
                    new Coordinates { Row = 7, Col = 4 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 4, Col = 8 }, 
                    new Coordinates { Row = 5, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 8, Col = 4 }, 
                    new Coordinates { Row = 8, Col = 5 }, }, 
                };
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 6 }, 
                new Coordinates { Row = 1, Col = 9, Number = 3 }, 
                new Coordinates { Row = 2, Col = 4, Number = 3 }, 
                new Coordinates { Row = 2, Col = 8, Number = 9 }, 
                new Coordinates { Row = 3, Col = 3, Number = 1 }, 
                new Coordinates { Row = 4, Col = 2, Number = 2 }, 
                new Coordinates { Row = 4, Col = 4, Number = 4 }, 
                new Coordinates { Row = 5, Col = 5, Number = 7 }, 
                new Coordinates { Row = 5, Col = 9, Number = 8 }, 
                new Coordinates { Row = 6, Col = 6, Number = 9 }, 
                new Coordinates { Row = 6, Col = 7, Number = 6 }, 
                new Coordinates { Row = 7, Col = 6, Number = 7 }, 
                new Coordinates { Row = 8, Col = 2, Number = 6 }, 
                new Coordinates { Row = 8, Col = 8, Number = 7 }, 
                new Coordinates { Row = 9, Col = 1, Number = 5 }, 
                new Coordinates { Row = 9, Col = 5, Number = 8 }, 
                });
            _sudokuInput.LowMidHighFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 3, Text = "Low" }, 
                new Coordinates { Row = 1, Col = 8, Text = "Low" }, 
                new Coordinates { Row = 1, Col = 9, Text = "Low" }, 
                new Coordinates { Row = 2, Col = 4, Text = "Low" }, 
                new Coordinates { Row = 2, Col = 5, Text = "Low" }, 
                new Coordinates { Row = 2, Col = 6, Text = "Low" }, 
                new Coordinates { Row = 3, Col = 2, Text = "Low" }, 
                new Coordinates { Row = 3, Col = 3, Text = "Low" }, 
                new Coordinates { Row = 3, Col = 7, Text = "Low" }, 

                new Coordinates { Row = 4, Col = 1, Text = "Low" }, 
                new Coordinates { Row = 4, Col = 2, Text = "Low" }, 
                new Coordinates { Row = 4, Col = 7, Text = "Low" }, 
                new Coordinates { Row = 5, Col = 1, Text = "Low" }, 
                new Coordinates { Row = 5, Col = 4, Text = "Low" }, 
                new Coordinates { Row = 5, Col = 7, Text = "Low" }, 
                new Coordinates { Row = 6, Col = 2, Text = "Low" }, 
                new Coordinates { Row = 6, Col = 3, Text = "Low" }, 
                new Coordinates { Row = 6, Col = 8, Text = "Low" }, 

                new Coordinates { Row = 7, Col = 1, Text = "Low" }, 
                new Coordinates { Row = 7, Col = 4, Text = "Low" }, 
                new Coordinates { Row = 7, Col = 5, Text = "Low" }, 
                new Coordinates { Row = 8, Col = 5, Text = "Low" }, 
                new Coordinates { Row = 8, Col = 6, Text = "Low" }, 
                new Coordinates { Row = 8, Col = 9, Text = "Low" }, 
                new Coordinates { Row = 9, Col = 6, Text = "Low" }, 
                new Coordinates { Row = 9, Col = 8, Text = "Low" }, 
                new Coordinates { Row = 9, Col = 9, Text = "Low" }, 

                new Coordinates { Row = 1, Col = 1, Text = "Mid" }, 
                new Coordinates { Row = 1, Col = 5, Text = "Mid" }, 
                new Coordinates { Row = 1, Col = 7, Text = "Mid" }, 
                new Coordinates { Row = 2, Col = 1, Text = "Mid" }, 
                new Coordinates { Row = 2, Col = 2, Text = "Mid" }, 
                new Coordinates { Row = 2, Col = 9, Text = "Mid" }, 
                new Coordinates { Row = 3, Col = 5, Text = "Mid" }, 
                new Coordinates { Row = 3, Col = 6, Text = "Mid" }, 
                new Coordinates { Row = 3, Col = 8, Text = "Mid" }, 

                new Coordinates { Row = 4, Col = 3, Text = "Mid" }, 
                new Coordinates { Row = 4, Col = 4, Text = "Mid" }, 
                new Coordinates { Row = 4, Col = 6, Text = "Mid" }, 
                new Coordinates { Row = 5, Col = 3, Text = "Mid" }, 
                new Coordinates { Row = 5, Col = 6, Text = "Mid" }, 
                new Coordinates { Row = 5, Col = 8, Text = "Mid" }, 
                new Coordinates { Row = 6, Col = 5, Text = "Mid" }, 
                new Coordinates { Row = 6, Col = 7, Text = "Mid" }, 
                new Coordinates { Row = 6, Col = 9, Text = "Mid" }, 

                new Coordinates { Row = 7, Col = 7, Text = "Mid" }, 
                new Coordinates { Row = 7, Col = 8, Text = "Mid" }, 
                new Coordinates { Row = 7, Col = 9, Text = "Mid" }, 
                new Coordinates { Row = 8, Col = 2, Text = "Mid" }, 
                new Coordinates { Row = 8, Col = 3, Text = "Mid" }, 
                new Coordinates { Row = 8, Col = 4, Text = "Mid" }, 
                new Coordinates { Row = 9, Col = 1, Text = "Mid" }, 
                new Coordinates { Row = 9, Col = 2, Text = "Mid" }, 
                new Coordinates { Row = 9, Col = 4, Text = "Mid" }, 

                new Coordinates { Row = 1, Col = 2, Text = "High" }, 
                new Coordinates { Row = 1, Col = 4, Text = "High" }, 
                new Coordinates { Row = 1, Col = 6, Text = "High" }, 
                new Coordinates { Row = 2, Col = 3, Text = "High" }, 
                new Coordinates { Row = 2, Col = 7, Text = "High" }, 
                new Coordinates { Row = 2, Col = 8, Text = "High" }, 
                new Coordinates { Row = 3, Col = 1, Text = "High" }, 
                new Coordinates { Row = 3, Col = 4, Text = "High" }, 
                new Coordinates { Row = 3, Col = 9, Text = "High" }, 

                new Coordinates { Row = 4, Col = 5, Text = "High" }, 
                new Coordinates { Row = 4, Col = 8, Text = "High" }, 
                new Coordinates { Row = 4, Col = 9, Text = "High" }, 
                new Coordinates { Row = 5, Col = 2, Text = "High" }, 
                new Coordinates { Row = 5, Col = 5, Text = "High" }, 
                new Coordinates { Row = 5, Col = 9, Text = "High" }, 
                new Coordinates { Row = 6, Col = 1, Text = "High" }, 
                new Coordinates { Row = 6, Col = 4, Text = "High" }, 
                new Coordinates { Row = 6, Col = 6, Text = "High" }, 

                new Coordinates { Row = 7, Col = 2, Text = "High" }, 
                new Coordinates { Row = 7, Col = 3, Text = "High" }, 
                new Coordinates { Row = 7, Col = 6, Text = "High" }, 
                new Coordinates { Row = 8, Col = 1, Text = "High" }, 
                new Coordinates { Row = 8, Col = 7, Text = "High" }, 
                new Coordinates { Row = 8, Col = 8, Text = "High" }, 
                new Coordinates { Row = 9, Col = 3, Text = "High" }, 
                new Coordinates { Row = 9, Col = 5, Text = "High" }, 
                new Coordinates { Row = 9, Col = 7, Text = "High" }, 

                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 4, 9, 5, 2, 7, 6, 8, 4, 9 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Fortress_SolveSudoku()
        {
            _sudokuInput.Types.Add("Fortress");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 5 }, 
                new Coordinates { Row = 1, Col = 5, Number = 8 }, 
                new Coordinates { Row = 1, Col = 7, Number = 4 }, 
                new Coordinates { Row = 2, Col = 6, Number = 2 }, 
                new Coordinates { Row = 2, Col = 8, Number = 1 }, 
                new Coordinates { Row = 3, Col = 7, Number = 3 }, 
                new Coordinates { Row = 3, Col = 9, Number = 6 }, 
                new Coordinates { Row = 4, Col = 5, Number = 1 }, 
                new Coordinates { Row = 4, Col = 8, Number = 7 }, 
                new Coordinates { Row = 5, Col = 1, Number = 9 }, 
                new Coordinates { Row = 5, Col = 4, Number = 8 }, 
                new Coordinates { Row = 5, Col = 9, Number = 5 }, 
                new Coordinates { Row = 6, Col = 2, Number = 4 }, 
                new Coordinates { Row = 7, Col = 1, Number = 2 }, 
                new Coordinates { Row = 7, Col = 3, Number = 1 }, 
                new Coordinates { Row = 7, Col = 9, Number = 4 }, 
                new Coordinates { Row = 8, Col = 2, Number = 8 }, 
                new Coordinates { Row = 8, Col = 4, Number = 2 }, 
                new Coordinates { Row = 8, Col = 9, Number = 3 }, 
                new Coordinates { Row = 9, Col = 3, Number = 3 }, 
                new Coordinates { Row = 9, Col = 5, Number = 6 }, 
                new Coordinates { Row = 9, Col = 7, Number = 5 }, 
                new Coordinates { Row = 9, Col = 8, Number = 8 }, 
                });
            _sudokuInput.FortressFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 2, Col = 4 }, 
                new Coordinates { Row = 3, Col = 3 }, 
                new Coordinates { Row = 3, Col = 4 }, 
                new Coordinates { Row = 4, Col = 2 }, 
                new Coordinates { Row = 4, Col = 3 }, 
                new Coordinates { Row = 5, Col = 7 }, 
                new Coordinates { Row = 6, Col = 6 }, 
                new Coordinates { Row = 6, Col = 7 }, 
                new Coordinates { Row = 7, Col = 5 }, 
                new Coordinates { Row = 7, Col = 6 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 4, 1, 9, 8, 7, 3, 6, 2, 5 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Consecutive1_SolveSudoku()
        {
            _sudokuInput.Types.Add("ConsecutiveEdge");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 8, Number = 1 }, 
                new Coordinates { Row = 2, Col = 4, Number = 9 }, 
                new Coordinates { Row = 4, Col = 2, Number = 8 }, 
                new Coordinates { Row = 4, Col = 4, Number = 6 }, 
                new Coordinates { Row = 6, Col = 7, Number = 4 }, 
                new Coordinates { Row = 6, Col = 9, Number = 7 }, 
                new Coordinates { Row = 7, Col = 6, Number = 2 }, 
                new Coordinates { Row = 8, Col = 1, Number = 1 }, 
                new Coordinates { Row = 8, Col = 9, Number = 2 }, 
                new Coordinates { Row = 9, Col = 6, Number = 5 }, 
                new Coordinates { Row = 9, Col = 8, Number = 3 }, 
                });
            _sudokuInput.ConsecutiveEdgeFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 3, Col = 6, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 3, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 5, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 6, Col = 1, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 6, Col = 8, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 6, Col = 9, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 7, Col = 2, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 7, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 1, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 3, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 4, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 6, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 1, Col = 3, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 1, Col = 4, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 2, Col = 2, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 3, Col = 3, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 3, Col = 5, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 4, Col = 2, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 4, Col = 7, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 4, Col = 8, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 5, Col = 3, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 5, Col = 7, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 6, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 6, Col = 3, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 6, Col = 8, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 7, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 7, Col = 7, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 7, Col = 8, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 8, Col = 3, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 8, Col = 5, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 9, Col = 3, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 9, Col = 5, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 9, Col = 8, Direction = "E", Text = "X" }, 
                });
            _sudokuInput.AreAllConsecutiveEdgesMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 4, 7, 2, 8, 3, 5, 1, 9 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Consecutive2_SolveSudoku()
        {
            _sudokuInput.Types.Add("ConsecutiveEdge");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 2, Col = 6, Number = 4 }, 
                new Coordinates { Row = 3, Col = 4, Number = 3 }, 
                new Coordinates { Row = 4, Col = 3, Number = 2 }, 
                new Coordinates { Row = 4, Col = 7, Number = 6 }, 
                new Coordinates { Row = 5, Col = 6, Number = 8 }, 
                new Coordinates { Row = 6, Col = 2, Number = 1 }, 
                new Coordinates { Row = 6, Col = 5, Number = 7 }, 
                new Coordinates { Row = 7, Col = 4, Number = 9 }, 
                new Coordinates { Row = 7, Col = 7, Number = 5 }, 
                });
            _sudokuInput.ConsecutiveEdgeFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 2, Col = 5, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 2, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 2, Col = 9, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 3, Col = 9, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 4, Col = 8, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 5, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 5, Col = 9, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 6, Col = 1, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 6, Col = 5, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 4, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 1, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 1, Col = 2, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 1, Col = 4, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 2, Col = 2, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 3, Col = 2, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 4, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 4, Col = 8, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 5, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 5, Col = 4, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 6, Col = 5, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 7, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 7, Col = 4, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 8, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 8, Col = 2, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 9, Col = 2, Direction = "E", Text = "X" }, 
                });
            _sudokuInput.AreAllConsecutiveEdgesMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 9, 1, 2, 3, 8, 4, 7, 6 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Roman_SolveSudoku()
        {
            _sudokuInput.Types.Add("Roman");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 7, Number = 1 }, 
                new Coordinates { Row = 3, Col = 6, Number = 9 }, 
                new Coordinates { Row = 5, Col = 8, Number = 3 }, 
                new Coordinates { Row = 6, Col = 3, Number = 9 }, 
                new Coordinates { Row = 7, Col = 1, Number = 5 }, 
                new Coordinates { Row = 8, Col = 5, Number = 7 }, 
                });
            _sudokuInput.RomanFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 8, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 2, Col = 5, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 3, Col = 1, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 4, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 6, Col = 4, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 6, Col = 7, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 2, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 8, Col = 9, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 1, Col = 4, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 2, Col = 1, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 3, Col = 2, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 3, Col = 8, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 5, Col = 4, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 7, Col = 3, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 9, Col = 7, Direction = "E", Text = "X" }, 
                new Coordinates { Row = 3, Col = 2, Direction = "S", Text = "V" }, 
                new Coordinates { Row = 4, Col = 5, Direction = "S", Text = "V" }, 
                new Coordinates { Row = 6, Col = 8, Direction = "S", Text = "V" }, 
                new Coordinates { Row = 2, Col = 5, Direction = "E", Text = "V" }, 
                new Coordinates { Row = 6, Col = 4, Direction = "E", Text = "V" }, 
                new Coordinates { Row = 6, Col = 8, Direction = "E", Text = "V" }, 
                new Coordinates { Row = 9, Col = 6, Direction = "E", Text = "V" }, 
                });
            _sudokuInput.AreAllRomansMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 4, 7, 9, 1, 6, 2, 3, 8 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Difference_SolveSudoku()
        {
            _sudokuInput.Types.Add("Difference");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 2 }, 
                new Coordinates { Row = 1, Col = 5, Number = 4 }, 
                new Coordinates { Row = 1, Col = 8, Number = 7 }, 
                new Coordinates { Row = 2, Col = 4, Number = 7 }, 
                new Coordinates { Row = 2, Col = 6, Number = 8 }, 
                new Coordinates { Row = 3, Col = 3, Number = 7 }, 
                new Coordinates { Row = 3, Col = 7, Number = 4 }, 
                new Coordinates { Row = 4, Col = 2, Number = 2 }, 
                new Coordinates { Row = 4, Col = 8, Number = 4 }, 
                new Coordinates { Row = 5, Col = 1, Number = 1 }, 
                new Coordinates { Row = 6, Col = 2, Number = 5 }, 
                new Coordinates { Row = 6, Col = 8, Number = 3 }, 
                new Coordinates { Row = 7, Col = 3, Number = 3 }, 
                new Coordinates { Row = 7, Col = 9, Number = 4 }, 
                new Coordinates { Row = 8, Col = 1, Number = 9 }, 
                new Coordinates { Row = 8, Col = 4, Number = 2 }, 
                new Coordinates { Row = 8, Col = 6, Number = 1 }, 
                new Coordinates { Row = 9, Col = 7, Number = 6 }, 
                });
            _sudokuInput.DifferenceFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 7, Number = 2, Direction = "S" }, 
                new Coordinates { Row = 2, Col = 5, Number = 8, Direction = "S" }, 
                new Coordinates { Row = 5, Col = 4, Number = 1, Direction = "S" }, 
                new Coordinates { Row = 5, Col = 9, Number = 6, Direction = "S" }, 
                new Coordinates { Row = 6, Col = 5, Number = 3, Direction = "S" }, 
                new Coordinates { Row = 8, Col = 3, Number = 3, Direction = "S" }, 
                new Coordinates { Row = 3, Col = 8, Number = 1, Direction = "E" }, 
                new Coordinates { Row = 4, Col = 5, Number = 2, Direction = "E" }, 
                new Coordinates { Row = 5, Col = 2, Number = 4, Direction = "E" }, 
                new Coordinates { Row = 5, Col = 6, Number = 4, Direction = "E" }, 
                new Coordinates { Row = 7, Col = 1, Number = 5, Direction = "E" }, 
                new Coordinates { Row = 9, Col = 5, Number = 1, Direction = "E" }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 1, 7, 5, 3, 2, 9, 4, 8, 6 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_MinDifference_SolveSudoku()
        {
            _sudokuInput.Types.Add("Area");
            _sudokuInput.Types.Add("MinDifference");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 9, Col = 1, Number = 1 }, 
                new Coordinates { Row = 9, Col = 3, Number = 2 }, });
            _sudokuInput.AreaFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 4 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 5 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 4, Col = 2 }, 
                    new Coordinates { Row = 5, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 6 }, 
                    new Coordinates { Row = 2, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 5, Col = 2 }, 
                    new Coordinates { Row = 6, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 7 }, 
                    new Coordinates { Row = 2, Col = 6 }, 
                    new Coordinates { Row = 3, Col = 5 }, 
                    new Coordinates { Row = 4, Col = 4 }, 
                    new Coordinates { Row = 5, Col = 3 }, 
                    new Coordinates { Row = 6, Col = 2 }, 
                    new Coordinates { Row = 7, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 8 }, 
                    new Coordinates { Row = 2, Col = 7 }, 
                    new Coordinates { Row = 3, Col = 6 }, 
                    new Coordinates { Row = 4, Col = 5 }, 
                    new Coordinates { Row = 5, Col = 4 }, 
                    new Coordinates { Row = 6, Col = 3 }, 
                    new Coordinates { Row = 7, Col = 2 }, 
                    new Coordinates { Row = 8, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 9 }, 
                    new Coordinates { Row = 2, Col = 8 }, 
                    new Coordinates { Row = 3, Col = 7 }, 
                    new Coordinates { Row = 4, Col = 6 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 6, Col = 4 }, 
                    new Coordinates { Row = 7, Col = 3 }, 
                    new Coordinates { Row = 8, Col = 2 }, 
                    new Coordinates { Row = 9, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 2, Col = 9 }, 
                    new Coordinates { Row = 3, Col = 8 }, 
                    new Coordinates { Row = 4, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 4 }, 
                    new Coordinates { Row = 8, Col = 3 }, 
                    new Coordinates { Row = 9, Col = 2 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 3, Col = 9 }, 
                    new Coordinates { Row = 4, Col = 8 }, 
                    new Coordinates { Row = 5, Col = 7 }, 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 7, Col = 5 }, 
                    new Coordinates { Row = 8, Col = 4 }, 
                    new Coordinates { Row = 9, Col = 3 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 4, Col = 9 }, 
                    new Coordinates { Row = 5, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 8, Col = 5 }, 
                    new Coordinates { Row = 9, Col = 4 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 5, Col = 9 }, 
                    new Coordinates { Row = 6, Col = 8 }, 
                    new Coordinates { Row = 7, Col = 7 }, 
                    new Coordinates { Row = 8, Col = 6 }, 
                    new Coordinates { Row = 9, Col = 5 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 6, Col = 9 }, 
                    new Coordinates { Row = 7, Col = 8 }, 
                    new Coordinates { Row = 8, Col = 7 }, 
                    new Coordinates { Row = 9, Col = 6 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 7, Col = 9 }, 
                    new Coordinates { Row = 8, Col = 8 }, 
                    new Coordinates { Row = 9, Col = 7 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 8, Col = 9 }, 
                    new Coordinates { Row = 9, Col = 8 }, }, });
            _sudokuInput.MinDifferenceFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 2, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 3, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 4, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 5, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 3, Col = 3 }, 
                    new Coordinates { Row = 4, Col = 2 }, 
                    new Coordinates { Row = 5, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 6, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 5, Col = 2 }, 
                    new Coordinates { Row = 6, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 7, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 6 }, 
                    new Coordinates { Row = 3, Col = 5 }, 
                    new Coordinates { Row = 4, Col = 4 }, 
                    new Coordinates { Row = 5, Col = 3 }, 
                    new Coordinates { Row = 6, Col = 2 }, 
                    new Coordinates { Row = 7, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 8, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 7 }, 
                    new Coordinates { Row = 3, Col = 6 }, 
                    new Coordinates { Row = 4, Col = 5 }, 
                    new Coordinates { Row = 5, Col = 4 }, 
                    new Coordinates { Row = 6, Col = 3 }, 
                    new Coordinates { Row = 7, Col = 2 }, 
                    new Coordinates { Row = 8, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 1, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 2, Col = 8 }, 
                    new Coordinates { Row = 3, Col = 7 }, 
                    new Coordinates { Row = 4, Col = 6 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 6, Col = 4 }, 
                    new Coordinates { Row = 7, Col = 3 }, 
                    new Coordinates { Row = 8, Col = 2 }, 
                    new Coordinates { Row = 9, Col = 1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 2, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 3, Col = 8 }, 
                    new Coordinates { Row = 4, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 4 }, 
                    new Coordinates { Row = 8, Col = 3 }, 
                    new Coordinates { Row = 9, Col = 2 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 3, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 4, Col = 8 }, 
                    new Coordinates { Row = 5, Col = 7 }, 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 7, Col = 5 }, 
                    new Coordinates { Row = 8, Col = 4 }, 
                    new Coordinates { Row = 9, Col = 3 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 4, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 5, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 8, Col = 5 }, 
                    new Coordinates { Row = 9, Col = 4 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 5, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 6, Col = 8 }, 
                    new Coordinates { Row = 7, Col = 7 }, 
                    new Coordinates { Row = 8, Col = 6 }, 
                    new Coordinates { Row = 9, Col = 5 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 6, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 7, Col = 8 }, 
                    new Coordinates { Row = 8, Col = 7 }, 
                    new Coordinates { Row = 9, Col = 6 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 7, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 8, Col = 8 }, 
                    new Coordinates { Row = 9, Col = 7 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 8, Col = 9, Number = 4 }, 
                    new Coordinates { Row = 9, Col = 8 }, }, });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 9, 2, 3, 7, 8, 1, 4, 6, 5 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_SumEdge_SolveSudoku()
        {
            _sudokuInput.Types.Add("SumEdge");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 1 }, 
                new Coordinates { Row = 1, Col = 3, Number = 5 }, 
                new Coordinates { Row = 1, Col = 5, Number = 9 }, 
                new Coordinates { Row = 2, Col = 6, Number = 8 }, 
                new Coordinates { Row = 3, Col = 1, Number = 3 }, 
                new Coordinates { Row = 3, Col = 7, Number = 9 }, 
                new Coordinates { Row = 4, Col = 5, Number = 8 }, 
                new Coordinates { Row = 4, Col = 9, Number = 5 }, 
                new Coordinates { Row = 5, Col = 1, Number = 2 }, 
                new Coordinates { Row = 5, Col = 4, Number = 6 }, 
                new Coordinates { Row = 5, Col = 6, Number = 7 }, 
                new Coordinates { Row = 6, Col = 2, Number = 4 }, 
                new Coordinates { Row = 6, Col = 5, Number = 5 }, 
                new Coordinates { Row = 7, Col = 3, Number = 7 }, 
                new Coordinates { Row = 7, Col = 7, Number = 2 }, 
                new Coordinates { Row = 7, Col = 9, Number = 4 }, 
                new Coordinates { Row = 9, Col = 4, Number = 1 }, 
                new Coordinates { Row = 9, Col = 7, Number = 7 }, 
                });
            _sudokuInput.SumEdgeFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 2, Col = 5, Number = 5, Direction = "S" }, 
                new Coordinates { Row = 2, Col = 8, Number = 13, Direction = "S" }, 
                new Coordinates { Row = 3, Col = 2, Number = 17, Direction = "S" }, 
                new Coordinates { Row = 6, Col = 4, Number = 14, Direction = "S" }, 
                new Coordinates { Row = 6, Col = 8, Number = 11, Direction = "S" }, 
                new Coordinates { Row = 2, Col = 3, Number = 5, Direction = "E" }, 
                new Coordinates { Row = 4, Col = 6, Number = 7, Direction = "E" }, 
                new Coordinates { Row = 5, Col = 2, Number = 13, Direction = "E" }, 
                new Coordinates { Row = 8, Col = 2, Number = 11, Direction = "E" }, 
                new Coordinates { Row = 8, Col = 6, Number = 4, Direction = "E" }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 4, 8, 1, 6, 3, 7, 9, 5, 2 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Inequality_SolveSudoku()
        {
            _sudokuInput.Types.Add("Inequality");
            _sudokuInput.InequalityFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 2, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 1, Col = 3, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 4, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 5, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 1, Col = 6, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 1, Col = 7, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 1, Col = 8, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 9, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 2, Col = 1, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 2, Col = 2, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 3, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 2, Col = 4, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 2, Col = 5, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 6, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 7, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 8, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 2, Col = 9, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 1, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 2, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 3, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 4, Col = 4, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 5, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 6, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 7, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 8, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 9, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 5, Col = 1, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 5, Col = 2, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 5, Col = 3, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 5, Col = 4, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 5, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 6, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 7, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 8, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 5, Col = 9, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 1, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 2, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 7, Col = 3, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 4, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 5, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 6, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 7, Col = 7, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 8, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 7, Col = 9, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 1, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 2, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 3, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 8, Col = 4, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 5, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 8, Col = 6, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 8, Col = 7, Direction = "S", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 8, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 8, Col = 9, Direction = "S", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 1, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 2, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 4, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 1, Col = 5, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 1, Col = 7, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 1, Col = 8, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 1, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 2, Col = 2, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 4, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 2, Col = 5, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 7, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 2, Col = 8, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 3, Col = 1, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 3, Col = 2, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 3, Col = 4, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 3, Col = 5, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 3, Col = 7, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 3, Col = 8, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 1, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 2, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 4, Col = 4, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 5, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 4, Col = 7, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 4, Col = 8, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 1, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 2, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 5, Col = 4, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 5, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 5, Col = 7, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 8, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 6, Col = 1, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 6, Col = 2, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 6, Col = 4, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 6, Col = 5, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 6, Col = 7, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 6, Col = 8, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 7, Col = 1, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 2, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 7, Col = 4, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 7, Col = 5, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 7, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 7, Col = 8, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 8, Col = 1, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 2, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 4, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 5, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 7, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 8, Col = 8, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 9, Col = 1, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 9, Col = 2, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 9, Col = 4, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 9, Col = 5, Direction = "E", Text = "Lower" }, 
                new Coordinates { Row = 9, Col = 7, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 9, Col = 8, Direction = "E", Text = "Lower" }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 1, 5, 3, 2, 6, 4, 9, 8, 7 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Thermometer1_SolveSudoku()
        {
            _sudokuInput.Types.Add("Thermometer");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 3, Number = 7 }, 
                new Coordinates { Row = 2, Col = 2, Number = 1 }, 
                new Coordinates { Row = 2, Col = 3, Number = 8 }, 
                new Coordinates { Row = 3, Col = 1, Number = 2 }, 
                new Coordinates { Row = 3, Col = 2, Number = 3 }, 
                new Coordinates { Row = 5, Col = 9, Number = 8 }, 
                new Coordinates { Row = 6, Col = 8, Number = 9 }, 
                new Coordinates { Row = 6, Col = 9, Number = 1 }, 
                new Coordinates { Row = 7, Col = 7, Number = 8 }, 
                new Coordinates { Row = 7, Col = 8, Number = 5 }, 
                new Coordinates { Row = 8, Col = 6, Number = 6 }, 
                new Coordinates { Row = 8, Col = 7, Number = 2 }, 
                new Coordinates { Row = 9, Col = 5, Number = 2 }, 
                new Coordinates { Row = 9, Col = 6, Number = 9 }, 
                });
            _sudokuInput.ThermometerFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 4 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 3, Col = 5 }, 
                    new Coordinates { Row = 2, Col = 5 }, 
                    new Coordinates { Row = 2, Col = 6 }, 
                    new Coordinates { Row = 1, Col = 6 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 5, Col = 3 }, 
                    new Coordinates { Row = 5, Col = 2 }, 
                    new Coordinates { Row = 6, Col = 2 }, 
                    new Coordinates { Row = 6, Col = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 5, Col = 6 }, 
                    new Coordinates { Row = 5, Col = 7 }, 
                    new Coordinates { Row = 4, Col = 7 }, 
                    new Coordinates { Row = 4, Col = 8 }, 
                    new Coordinates { Row = 3, Col = 8 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 4 }, 
                    new Coordinates { Row = 8, Col = 4 }, 
                    new Coordinates { Row = 8, Col = 3 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 1, 8, 5, 9, 7, 4, 6, 3, 2 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Thermometer2_SolveSudoku()
        {
            _sudokuInput.Types.Add("Thermometer");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 5, Number = 7 }, 
                new Coordinates { Row = 1, Col = 6, Number = 1 }, 
                new Coordinates { Row = 2, Col = 6, Number = 6 }, 
                new Coordinates { Row = 2, Col = 7, Number = 9 }, 
                new Coordinates { Row = 3, Col = 7, Number = 5 }, 
                new Coordinates { Row = 3, Col = 8, Number = 8 }, 
                new Coordinates { Row = 4, Col = 8, Number = 4 }, 
                new Coordinates { Row = 4, Col = 9, Number = 9 }, 
                new Coordinates { Row = 5, Col = 1, Number = 9 }, 
                new Coordinates { Row = 5, Col = 9, Number = 3 }, 
                new Coordinates { Row = 6, Col = 1, Number = 4 }, 
                new Coordinates { Row = 6, Col = 2, Number = 8 }, 
                new Coordinates { Row = 7, Col = 2, Number = 5 }, 
                new Coordinates { Row = 7, Col = 3, Number = 2 }, 
                new Coordinates { Row = 8, Col = 3, Number = 9 }, 
                new Coordinates { Row = 8, Col = 4, Number = 1 }, 
                new Coordinates { Row = 9, Col = 4, Number = 3 }, 
                new Coordinates { Row = 9, Col = 5, Number = 5 }, 
                });
            _sudokuInput.ThermometerFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 4 }, 
                    new Coordinates { Row = 1, Col = 3 }, 
                    new Coordinates { Row = 1, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, 
                    new Coordinates { Row = 2, Col = 5 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 2 }, 
                    new Coordinates { Row = 5, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 6 }, 
                    new Coordinates { Row = 3, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 1 }, 
                    new Coordinates { Row = 3, Col = 1 }, 
                    new Coordinates { Row = 2, Col = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 5 }, 
                    new Coordinates { Row = 4, Col = 6 }, 
                    new Coordinates { Row = 4, Col = 7 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 4 }, 
                    new Coordinates { Row = 6, Col = 4 }, 
                    new Coordinates { Row = 7, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 8 }, 
                    new Coordinates { Row = 5, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 6 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 3 }, 
                    new Coordinates { Row = 5, Col = 3 }, 
                    new Coordinates { Row = 4, Col = 3 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 6, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 9 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 8, Col = 6 }, 
                    new Coordinates { Row = 9, Col = 6 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 5 }, 
                    new Coordinates { Row = 7, Col = 5 }, 
                    new Coordinates { Row = 6, Col = 5 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 3, 5, 4, 1, 8, 7, 9, 2 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_WhereIsX_SolveSudoku()
        {
            _sudokuInput.Types.Add("WhereIsX");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 9, Number = 2 }, 
                new Coordinates { Row = 2, Col = 2, Number = 7 }, 
                new Coordinates { Row = 3, Col = 5, Number = 3 }, 
                new Coordinates { Row = 4, Col = 6, Number = 6 }, 
                new Coordinates { Row = 5, Col = 3, Number = 5 }, 
                new Coordinates { Row = 6, Col = 4, Number = 8 }, 
                new Coordinates { Row = 9, Col = 1, Number = 4 }, 
                });
            _sudokuInput.WhereIsXFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 2, Number = 9, Direction = "S" }, 
                new Coordinates { Row = 1, Col = 5, Number = 9, Direction = "S" }, 
                new Coordinates { Row = 1, Col = 8, Number = 9, Direction = "W" }, 
                new Coordinates { Row = 2, Col = 1, Number = 9, Direction = "E" }, 
                new Coordinates { Row = 2, Col = 6, Number = 9, Direction = "S" }, 
                new Coordinates { Row = 2, Col = 7, Number = 9, Direction = "S" }, 
                new Coordinates { Row = 2, Col = 8, Number = 9, Direction = "S" }, 
                new Coordinates { Row = 3, Col = 4, Number = 9, Direction = "N" }, 
                new Coordinates { Row = 4, Col = 3, Number = 9, Direction = "W" }, 
                new Coordinates { Row = 4, Col = 5, Number = 9, Direction = "S" }, 
                new Coordinates { Row = 5, Col = 1, Number = 9, Direction = "E" }, 
                new Coordinates { Row = 5, Col = 4, Number = 9, Direction = "E" }, 
                new Coordinates { Row = 6, Col = 2, Number = 9, Direction = "E" }, 
                new Coordinates { Row = 6, Col = 7, Number = 9, Direction = "W" }, 
                new Coordinates { Row = 7, Col = 2, Number = 9, Direction = "E" }, 
                new Coordinates { Row = 7, Col = 6, Number = 9, Direction = "N" }, 
                new Coordinates { Row = 7, Col = 7, Number = 9, Direction = "N" }, 
                new Coordinates { Row = 8, Col = 1, Number = 9, Direction = "N" }, 
                new Coordinates { Row = 8, Col = 2, Number = 9, Direction = "E" }, 
                new Coordinates { Row = 8, Col = 9, Number = 9, Direction = "N" }, 
                new Coordinates { Row = 9, Col = 8, Number = 9, Direction = "W" }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 4, 5, 6, 3, 1, 2, 8, 7, 9 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_DynamicSumOrProduct_SolveSudoku()
        {
            _sudokuInput.Types.Add("DynamicSumOrProduct");
            _sudokuInput.DynamicSumOrProductFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Number = 28, Direction = "W" }, 
                new Coordinates { Row = 1, Col = 1, Number = 30, Direction = "E" }, 
                new Coordinates { Row = 2, Col = 2, Number = 15, Direction = "N" }, 
                new Coordinates { Row = 2, Col = 2, Number = 18, Direction = "S" }, 
                new Coordinates { Row = 3, Col = 3, Number = 14, Direction = "W" }, 
                new Coordinates { Row = 3, Col = 3, Number = 24, Direction = "E" }, 
                new Coordinates { Row = 4, Col = 4, Number = 14, Direction = "N" }, 
                new Coordinates { Row = 4, Col = 4, Number = 12, Direction = "S" }, 
                new Coordinates { Row = 5, Col = 5, Number = 24, Direction = "W" }, 
                new Coordinates { Row = 5, Col = 5, Number = 8, Direction = "E" }, 
                new Coordinates { Row = 6, Col = 6, Number = 20, Direction = "N" }, 
                new Coordinates { Row = 6, Col = 6, Number = 20, Direction = "S" }, 
                new Coordinates { Row = 7, Col = 7, Number = 12, Direction = "W" }, 
                new Coordinates { Row = 7, Col = 7, Number = 15, Direction = "E" }, 
                new Coordinates { Row = 8, Col = 8, Number = 16, Direction = "N" }, 
                new Coordinates { Row = 8, Col = 8, Number = 28, Direction = "S" }, 
                new Coordinates { Row = 9, Col = 9, Number = 12, Direction = "W" }, 
                new Coordinates { Row = 9, Col = 9, Number = 18, Direction = "E" }, 
                });
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 4, Col = 1, Number = 5}, 
                new Coordinates { Row = 6, Col = 9, Number = 8}, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 2, 8, 7, 6, 3, 5, 1, 9, 4 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Killer_SolveSudoku()
        {
            _sudokuInput.Types.Add("Killer");
            _sudokuInput.KillerFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 1, Number = 16 }, 
                    new Coordinates { Row = 1, Col = 2,  }, 
                    new Coordinates { Row = 2, Col = 1,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 3, Number = 3 }, 
                    new Coordinates { Row = 1, Col = 4,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 5, Number = 15 }, 
                    new Coordinates { Row = 1, Col = 6,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 7, Number = 9 }, 
                    new Coordinates { Row = 1, Col = 8,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 9, Number = 25 }, 
                    new Coordinates { Row = 2, Col = 8,  }, 
                    new Coordinates { Row = 2, Col = 9,  }, 
                    new Coordinates { Row = 3, Col = 9,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 2, Number = 19 }, 
                    new Coordinates { Row = 2, Col = 3,  }, 
                    new Coordinates { Row = 3, Col = 2,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 4, Number = 8 }, 
                    new Coordinates { Row = 2, Col = 5,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 6, Number = 3 }, 
                    new Coordinates { Row = 2, Col = 7,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 1, Number = 10 }, 
                    new Coordinates { Row = 4, Col = 1,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 3, Number = 16 }, 
                    new Coordinates { Row = 3, Col = 4,  }, 
                    new Coordinates { Row = 4, Col = 3,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 5, Number = 15 }, 
                    new Coordinates { Row = 3, Col = 6,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 7, Number = 10 }, 
                    new Coordinates { Row = 3, Col = 8,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 2, Number = 9 }, 
                    new Coordinates { Row = 5, Col = 2,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 4, Number = 15 }, 
                    new Coordinates { Row = 4, Col = 5,  }, 
                    new Coordinates { Row = 5, Col = 4,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 6, Number = 5 }, 
                    new Coordinates { Row = 4, Col = 7,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 8, Number = 13 }, 
                    new Coordinates { Row = 4, Col = 9,  }, 
                    new Coordinates { Row = 5, Col = 9,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 1, Number = 14 }, 
                    new Coordinates { Row = 6, Col = 1,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 3, Number = 7 }, 
                    new Coordinates { Row = 6, Col = 3,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 5, Number = 21 }, 
                    new Coordinates { Row = 5, Col = 6,  }, 
                    new Coordinates { Row = 6, Col = 5,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 7, Number = 10 }, 
                    new Coordinates { Row = 5, Col = 8,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 2, Number = 9 }, 
                    new Coordinates { Row = 7, Col = 2,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 4, Number = 11 }, 
                    new Coordinates { Row = 7, Col = 4,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 6, Number = 11 }, 
                    new Coordinates { Row = 6, Col = 7,  }, 
                    new Coordinates { Row = 7, Col = 6,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 8, Number = 16 }, 
                    new Coordinates { Row = 6, Col = 9,  }, 
                    new Coordinates { Row = 7, Col = 9,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 7, Col = 1, Number = 9 }, 
                    new Coordinates { Row = 8, Col = 1,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 7, Col = 3, Number = 7 }, 
                    new Coordinates { Row = 8, Col = 3,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 7, Col = 5, Number = 9 }, 
                    new Coordinates { Row = 8, Col = 5,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 7, Col = 7, Number = 17 }, 
                    new Coordinates { Row = 7, Col = 8,  }, 
                    new Coordinates { Row = 8, Col = 7,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 2, Number = 21 }, 
                    new Coordinates { Row = 9, Col = 1,  }, 
                    new Coordinates { Row = 9, Col = 2,  }, 
                    new Coordinates { Row = 9, Col = 3,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 4, Number = 12 }, 
                    new Coordinates { Row = 9, Col = 4,  }, 
                    new Coordinates { Row = 9, Col = 5,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 6, Number = 18 }, 
                    new Coordinates { Row = 9, Col = 6,  }, 
                    new Coordinates { Row = 9, Col = 7,  }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 8, Number = 22 }, 
                    new Coordinates { Row = 8, Col = 9,  }, 
                    new Coordinates { Row = 9, Col = 8,  }, 
                    new Coordinates { Row = 9, Col = 9,  }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 1, 3, 8, 9, 5, 2, 7, 4 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_SumWeighted_SolveSudoku()
        {
            _sudokuInput.Types.Add("SumWeighted");
            _sudokuInput.Types.Add("Diagonal");
            _sudokuInput.SumWeightedFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Number = 6894 }, 
                    new Coordinates { Row = 1, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 1, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 1, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 1, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 1, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 1, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 1, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 1, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 1, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 2997 }, 
                    new Coordinates { Row = 2, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 2, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 2, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 2, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 2, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 2, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 2, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 2, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 2, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 2196 }, 
                    new Coordinates { Row = 3, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 3, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 3, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 3, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 3, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 3, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 3, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 3, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 3, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 4716 }, 
                    new Coordinates { Row = 4, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 4, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 4, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 4, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 4, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 4, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 4, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 4, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 4, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 5886 }, 
                    new Coordinates { Row = 5, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 5, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 5, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 5, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 5, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 5, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 5, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 5, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 5, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 8253 }, 
                    new Coordinates { Row = 6, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 6, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 6, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 6, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 6, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 6, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 6, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 6, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 6, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 10620 }, 
                    new Coordinates { Row = 7, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 7, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 7, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 7, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 7, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 7, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 7, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 7, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 7, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 5463 }, 
                    new Coordinates { Row = 8, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 8, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 8, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 8, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 8, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 8, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 8, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 8, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 8, Col = 9, Number = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 8460 }, 
                    new Coordinates { Row = 9, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 9, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 9, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 9, Col = 4, Number = 1 }, 
                    new Coordinates { Row = 9, Col = 5, Number = 100 }, 
                    new Coordinates { Row = 9, Col = 6, Number = 10 }, 
                    new Coordinates { Row = 9, Col = 7, Number = 1 }, 
                    new Coordinates { Row = 9, Col = 8, Number = 10 }, 
                    new Coordinates { Row = 9, Col = 9, Number = 1 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 2, 7, 4, 1, 9, 6, 3, 5, 8 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_SumCorner1_SolveSudoku()
        {
            _sudokuInput.Types.Add("SumCorner");
            _sudokuInput.SumCornerFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Number = 22 }, 
                new Coordinates { Row = 1, Col = 2, Number = 25 }, 
                new Coordinates { Row = 1, Col = 4, Number = 17 }, 
                new Coordinates { Row = 1, Col = 5, Number = 20 }, 
                new Coordinates { Row = 1, Col = 7, Number = 17 }, 
                new Coordinates { Row = 1, Col = 8, Number = 26 }, 
                new Coordinates { Row = 2, Col = 1, Number = 16 }, 
                new Coordinates { Row = 2, Col = 2, Number = 26 }, 
                new Coordinates { Row = 2, Col = 4, Number = 18 }, 
                new Coordinates { Row = 2, Col = 5, Number = 20 }, 
                new Coordinates { Row = 2, Col = 7, Number = 18 }, 
                new Coordinates { Row = 2, Col = 8, Number = 21 }, 
                new Coordinates { Row = 4, Col = 1, Number = 24 }, 
                new Coordinates { Row = 4, Col = 2, Number = 18 }, 
                new Coordinates { Row = 4, Col = 4, Number = 24 }, 
                new Coordinates { Row = 4, Col = 5, Number = 18 }, 
                new Coordinates { Row = 4, Col = 7, Number = 20 }, 
                new Coordinates { Row = 4, Col = 8, Number = 16 }, 
                new Coordinates { Row = 5, Col = 1, Number = 17 }, 
                new Coordinates { Row = 5, Col = 2, Number = 10 }, 
                new Coordinates { Row = 5, Col = 4, Number = 26 }, 
                new Coordinates { Row = 5, Col = 5, Number = 25 }, 
                new Coordinates { Row = 5, Col = 7, Number = 25 }, 
                new Coordinates { Row = 5, Col = 8, Number = 18 }, 
                new Coordinates { Row = 7, Col = 1, Number = 21 }, 
                new Coordinates { Row = 7, Col = 2, Number = 19 }, 
                new Coordinates { Row = 7, Col = 4, Number = 21 }, 
                new Coordinates { Row = 7, Col = 5, Number = 18 }, 
                new Coordinates { Row = 7, Col = 7, Number = 23 }, 
                new Coordinates { Row = 7, Col = 8, Number = 20 }, 
                new Coordinates { Row = 8, Col = 1, Number = 14 }, 
                new Coordinates { Row = 8, Col = 2, Number = 17 }, 
                new Coordinates { Row = 8, Col = 4, Number = 17 }, 
                new Coordinates { Row = 8, Col = 5, Number = 21 }, 
                new Coordinates { Row = 8, Col = 7, Number = 20 }, 
                new Coordinates { Row = 8, Col = 8, Number = 22 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 1, 3, 8, 9, 5, 2, 7, 4 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_SumCorner2_SolveSudoku()
        {
            _sudokuInput.Types.Add("SumEdge");
            _sudokuInput.Types.Add("SumCorner");
            _sudokuInput.SumEdgeFields.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 9, Number = 15, Direction = "S" }, 
                new Coordinates { Row = 3, Col = 1, Number = 9, Direction = "S" }, 
                new Coordinates { Row = 3, Col = 2, Number = 8, Direction = "S" }, 
                new Coordinates { Row = 3, Col = 5, Number = 8, Direction = "S" }, 
                new Coordinates { Row = 6, Col = 5, Number = 14, Direction = "S" }, 
                new Coordinates { Row = 6, Col = 8, Number = 8, Direction = "S" }, 
                new Coordinates { Row = 6, Col = 9, Number = 10, Direction = "S" }, 
                new Coordinates { Row = 8, Col = 1, Number = 14, Direction = "S" }, 

                new Coordinates { Row = 1, Col = 3, Number = 10, Direction = "E" }, 
                new Coordinates { Row = 1, Col = 7, Number = 7, Direction = "E" }, 
                new Coordinates { Row = 2, Col = 7, Number = 9, Direction = "E" }, 
                new Coordinates { Row = 3, Col = 6, Number = 16, Direction = "E" }, 
                new Coordinates { Row = 4, Col = 3, Number = 5, Direction = "E" }, 
                new Coordinates { Row = 5, Col = 1, Number = 13, Direction = "E" }, 
                new Coordinates { Row = 5, Col = 8, Number = 12, Direction = "E" }, 
                new Coordinates { Row = 6, Col = 6, Number = 7, Direction = "E" }, 
                new Coordinates { Row = 7, Col = 3, Number = 11, Direction = "E" }, 
                new Coordinates { Row = 8, Col = 2, Number = 10, Direction = "E" }, 
                new Coordinates { Row = 9, Col = 2, Number = 10, Direction = "E" }, 
                new Coordinates { Row = 9, Col = 6, Number = 12, Direction = "E" }, 
                });
            _sudokuInput.SumCornerFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Number = 15 }, 
                new Coordinates { Row = 1, Col = 5, Number = 18 }, 
                new Coordinates { Row = 2, Col = 3, Number = 30 }, 
                new Coordinates { Row = 3, Col = 8, Number = 20 }, 
                new Coordinates { Row = 4, Col = 6, Number = 22 }, 
                new Coordinates { Row = 5, Col = 3, Number = 10 }, 
                new Coordinates { Row = 6, Col = 1, Number = 23 }, 
                new Coordinates { Row = 7, Col = 6, Number = 17 }, 
                new Coordinates { Row = 8, Col = 4, Number = 23 }, 
                new Coordinates { Row = 8, Col = 8, Number = 14 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 4, 7, 8, 1, 5, 9, 3, 6, 2 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_EvenOddDot_SolveSudoku()
        {
            _sudokuInput.Types.Add("EvenOddDot");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 3 }, 
                new Coordinates { Row = 1, Col = 2, Number = 7 }, 
                new Coordinates { Row = 1, Col = 3, Number = 5 }, 
                new Coordinates { Row = 1, Col = 4, Number = 6 }, 
                new Coordinates { Row = 1, Col = 5, Number = 8 }, 
                new Coordinates { Row = 1, Col = 6, Number = 2 }, 
                new Coordinates { Row = 1, Col = 7, Number = 4 }, 
                new Coordinates { Row = 1, Col = 8, Number = 1 }, 
                new Coordinates { Row = 1, Col = 9, Number = 9 }, 
                new Coordinates { Row = 2, Col = 1, Number = 4 }, 
                new Coordinates { Row = 2, Col = 5, Number = 1 }, 
                new Coordinates { Row = 3, Col = 1, Number = 8 }, 
                new Coordinates { Row = 3, Col = 3, Number = 1 }, 
                new Coordinates { Row = 4, Col = 1, Number = 1 }, 
                new Coordinates { Row = 4, Col = 6, Number = 8 }, 
                new Coordinates { Row = 5, Col = 1, Number = 9 }, 
                new Coordinates { Row = 5, Col = 5, Number = 6 }, 
                new Coordinates { Row = 5, Col = 9, Number = 7 }, 
                new Coordinates { Row = 6, Col = 1, Number = 2 }, 
                new Coordinates { Row = 6, Col = 4, Number = 1 }, 
                new Coordinates { Row = 6, Col = 7, Number = 5 }, 
                new Coordinates { Row = 7, Col = 1, Number = 6 }, 
                new Coordinates { Row = 8, Col = 1, Number = 5 }, 
                new Coordinates { Row = 8, Col = 8, Number = 4 }, 
                new Coordinates { Row = 9, Col = 1, Number = 7 }, 
                new Coordinates { Row = 9, Col = 9, Number = 5 }, 
                });
            _sudokuInput.EvenOddDotFields.AddRange(new List<Coordinates> {
                new Coordinates { Row = 2, Col = 1, Text = "Black" }, 
                new Coordinates { Row = 2, Col = 3, Text = "Black" }, 
                new Coordinates { Row = 4, Col = 1, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 1, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 5, Text = "Black" }, 
                new Coordinates { Row = 7, Col = 5, Text = "Black" }, 
                new Coordinates { Row = 8, Col = 1, Text = "Black" }, 
                new Coordinates { Row = 1, Col = 1, Text = "White" }, 
                new Coordinates { Row = 1, Col = 4, Text = "White" }, 
                new Coordinates { Row = 1, Col = 5, Text = "White" }, 
                new Coordinates { Row = 1, Col = 6, Text = "White" }, 
                new Coordinates { Row = 1, Col = 7, Text = "White" }, 
                new Coordinates { Row = 1, Col = 8, Text = "White" }, 
                new Coordinates { Row = 2, Col = 2, Text = "White" }, 
                new Coordinates { Row = 2, Col = 7, Text = "White" }, 
                new Coordinates { Row = 2, Col = 8, Text = "White" }, 
                new Coordinates { Row = 3, Col = 1, Text = "White" }, 
                new Coordinates { Row = 3, Col = 2, Text = "White" }, 
                new Coordinates { Row = 3, Col = 5, Text = "White" }, 
                new Coordinates { Row = 3, Col = 6, Text = "White" }, 
                new Coordinates { Row = 3, Col = 7, Text = "White" }, 
                new Coordinates { Row = 3, Col = 8, Text = "White" }, 
                new Coordinates { Row = 4, Col = 2, Text = "White" }, 
                new Coordinates { Row = 4, Col = 4, Text = "White" }, 
                new Coordinates { Row = 4, Col = 6, Text = "White" }, 
                new Coordinates { Row = 4, Col = 8, Text = "White" }, 
                new Coordinates { Row = 5, Col = 1, Text = "White" }, 
                new Coordinates { Row = 5, Col = 2, Text = "White" }, 
                new Coordinates { Row = 5, Col = 3, Text = "White" }, 
                new Coordinates { Row = 5, Col = 4, Text = "White" }, 
                new Coordinates { Row = 5, Col = 5, Text = "White" }, 
                new Coordinates { Row = 5, Col = 8, Text = "White" }, 
                new Coordinates { Row = 6, Col = 2, Text = "White" }, 
                new Coordinates { Row = 6, Col = 7, Text = "White" }, 
                new Coordinates { Row = 6, Col = 8, Text = "White" }, 
                new Coordinates { Row = 7, Col = 1, Text = "White" }, 
                new Coordinates { Row = 7, Col = 2, Text = "White" }, 
                new Coordinates { Row = 7, Col = 4, Text = "White" }, 
                new Coordinates { Row = 7, Col = 7, Text = "White" }, 
                new Coordinates { Row = 7, Col = 8, Text = "White" }, 
                new Coordinates { Row = 8, Col = 2, Text = "White" }, 
                new Coordinates { Row = 8, Col = 4, Text = "White" }, 
                new Coordinates { Row = 8, Col = 5, Text = "White" }, 
                new Coordinates { Row = 8, Col = 6, Text = "White" }, 
                new Coordinates { Row = 8, Col = 7, Text = "White" }, 
                new Coordinates { Row = 8, Col = 8, Text = "White" }, 
                });
            _sudokuInput.AreAllEvenOddDotsMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 7, 5, 8, 2, 6, 4, 1, 3, 9 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_SumDot_SolveSudoku()
        {
            _sudokuInput.Types.Add("SumDot");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 4 }, 
                new Coordinates { Row = 1, Col = 2, Number = 1 }, 
                new Coordinates { Row = 1, Col = 3, Number = 7 }, 
                new Coordinates { Row = 1, Col = 4, Number = 8 }, 
                new Coordinates { Row = 1, Col = 5, Number = 3 }, 
                new Coordinates { Row = 1, Col = 6, Number = 2 }, 
                new Coordinates { Row = 1, Col = 7, Number = 5 }, 
                new Coordinates { Row = 1, Col = 8, Number = 9 }, 
                new Coordinates { Row = 1, Col = 9, Number = 6 }, 
                new Coordinates { Row = 2, Col = 5, Number = 4 }, 
                new Coordinates { Row = 2, Col = 9, Number = 8 }, 
                new Coordinates { Row = 3, Col = 7, Number = 7 }, 
                new Coordinates { Row = 3, Col = 9, Number = 3 }, 
                new Coordinates { Row = 4, Col = 4, Number = 4 }, 
                new Coordinates { Row = 4, Col = 9, Number = 2 }, 
                new Coordinates { Row = 5, Col = 1, Number = 2 }, 
                new Coordinates { Row = 5, Col = 5, Number = 8 }, 
                new Coordinates { Row = 5, Col = 9, Number = 1 }, 
                new Coordinates { Row = 6, Col = 3, Number = 3 }, 
                new Coordinates { Row = 6, Col = 6, Number = 9 }, 
                new Coordinates { Row = 6, Col = 9, Number = 5 }, 
                new Coordinates { Row = 7, Col = 9, Number = 7 }, 
                new Coordinates { Row = 8, Col = 9, Number = 4 }, 
                new Coordinates { Row = 8, Col = 2, Number = 7 }, 
                new Coordinates { Row = 9, Col = 1, Number = 3 }, 
                new Coordinates { Row = 9, Col = 9, Number = 9 }, 
                });
            _sudokuInput.SumDotFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 2, Col = 7, Text = "Black" }, 
                new Coordinates { Row = 2, Col = 8, Text = "Black" }, 
                new Coordinates { Row = 3, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 3, Col = 3, Text = "Black" }, 
                new Coordinates { Row = 4, Col = 8, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 3, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 4, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 6, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 7, Text = "Black" }, 
                new Coordinates { Row = 7, Col = 6, Text = "Black" }, 
                });
            _sudokuInput.AreAllSumDotsMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 4, 5, 3, 6, 8, 7, 2, 1, 9 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_SumArrow1_SolveSudoku()
        {
            _sudokuInput.Types.Add("SumArrow");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 8, Number = 8 }, 
                new Coordinates { Row = 2, Col = 5, Number = 8 }, 
                new Coordinates { Row = 2, Col = 6, Number = 3 }, 
                new Coordinates { Row = 2, Col = 9, Number = 9 }, 
                new Coordinates { Row = 3, Col = 6, Number = 7 }, 
                new Coordinates { Row = 4, Col = 8, Number = 2 }, 
                new Coordinates { Row = 5, Col = 2, Number = 9 }, 
                new Coordinates { Row = 5, Col = 8, Number = 3 }, 
                new Coordinates { Row = 5, Col = 9, Number = 4 }, 
                new Coordinates { Row = 6, Col = 2, Number = 5 }, 
                new Coordinates { Row = 6, Col = 3, Number = 7 }, 
                new Coordinates { Row = 8, Col = 1, Number = 7 }, 
                new Coordinates { Row = 8, Col = 4, Number = 3 }, 
                new Coordinates { Row = 8, Col = 5, Number = 1 }, 
                new Coordinates { Row = 9, Col = 2, Number = 6 }, 
                new Coordinates { Row = 9, Col = 5, Number = 2 }, 
                });
            _sudokuInput.SumArrowFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 1 }, 
                    new Coordinates { Row = 1, Col = 2 }, 
                    new Coordinates { Row = 1, Col = 3 }, 
                    new Coordinates { Row = 1, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 1 }, 
                    new Coordinates { Row = 2, Col = 1 }, 
                    new Coordinates { Row = 3, Col = 1 }, 
                    new Coordinates { Row = 4, Col = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 4, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 4 }, 
                    new Coordinates { Row = 2, Col = 3 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 7, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 7 }, 
                    new Coordinates { Row = 5, Col = 6 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 6 }, 
                    new Coordinates { Row = 6, Col = 5 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 8 }, 
                    new Coordinates { Row = 7, Col = 7 }, 
                    new Coordinates { Row = 6, Col = 6 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 9, Col = 9 }, 
                    new Coordinates { Row = 8, Col = 9 }, 
                    new Coordinates { Row = 7, Col = 9 }, 
                    new Coordinates { Row = 6, Col = 9 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 9, Col = 9 }, 
                    new Coordinates { Row = 9, Col = 8 }, 
                    new Coordinates { Row = 9, Col = 7 }, 
                    new Coordinates { Row = 9, Col = 6 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 8, 6, 9, 5, 7, 1, 4, 3, 2 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_SumArrow2_SolveSudoku()
        {
            _sudokuInput.Types.Add("SumArrow");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 2 }, 
                new Coordinates { Row = 1, Col = 2, Number = 1 }, 
                new Coordinates { Row = 2, Col = 1, Number = 6 }, 
                new Coordinates { Row = 2, Col = 6, Number = 8 }, 
                new Coordinates { Row = 3, Col = 7, Number = 5 }, 
                new Coordinates { Row = 4, Col = 8, Number = 6 }, 
                new Coordinates { Row = 5, Col = 5, Number = 7 }, 
                new Coordinates { Row = 5, Col = 9, Number = 3 }, 
                new Coordinates { Row = 6, Col = 2, Number = 5 }, 
                new Coordinates { Row = 6, Col = 6, Number = 3 }, 
                new Coordinates { Row = 7, Col = 3, Number = 9 }, 
                new Coordinates { Row = 7, Col = 7, Number = 4 }, 
                new Coordinates { Row = 8, Col = 4, Number = 4 }, 
                new Coordinates { Row = 8, Col = 8, Number = 1 }, 
                new Coordinates { Row = 9, Col = 5, Number = 2 }, 
                });
            _sudokuInput.SumArrowFields.AddRange(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 3 }, 
                    new Coordinates { Row = 1, Col = 4 }, 
                    new Coordinates { Row = 1, Col = 5 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 2, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 1 }, 
                    new Coordinates { Row = 4, Col = 1 }, 
                    new Coordinates { Row = 5, Col = 1 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 6 }, 
                    new Coordinates { Row = 4, Col = 5 }, 
                    new Coordinates { Row = 3, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 6, Col = 5 }, 
                    new Coordinates { Row = 5, Col = 4 }, 
                    new Coordinates { Row = 4, Col = 3 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 8, Col = 9 }, 
                    new Coordinates { Row = 7, Col = 8 }, 
                    new Coordinates { Row = 6, Col = 7 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 9, Col = 8 }, 
                    new Coordinates { Row = 8, Col = 7 }, 
                    new Coordinates { Row = 7, Col = 6 }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 1, 4, 6, 7, 2, 9, 8, 3 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_ConsecutiveDot_SolveSudoku()
        {
            _sudokuInput.Types.Add("ConsecutiveDot");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 6 }, 
                new Coordinates { Row = 2, Col = 2, Number = 8 }, 
                new Coordinates { Row = 4, Col = 3, Number = 8 }, 
                new Coordinates { Row = 4, Col = 6, Number = 9 }, 
                new Coordinates { Row = 5, Col = 1, Number = 4 }, 
                new Coordinates { Row = 5, Col = 5, Number = 3 }, 
                new Coordinates { Row = 6, Col = 4, Number = 1 }, 
                new Coordinates { Row = 7, Col = 7, Number = 1 }, 
                new Coordinates { Row = 8, Col = 5, Number = 7 }, 
                });
            _sudokuInput.ConsecutiveDotFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 4, Text = "Black" }, 
                new Coordinates { Row = 4, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 5, Col = 5, Text = "Black" }, 
                new Coordinates { Row = 7, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 8, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 8, Col = 4, Text = "Black" }, 
                new Coordinates { Row = 8, Col = 7, Text = "Black" }, 

                new Coordinates { Row = 1, Col = 1, Text = "White" }, 
                new Coordinates { Row = 1, Col = 8, Text = "White" }, 
                new Coordinates { Row = 2, Col = 5, Text = "White" }, 
                new Coordinates { Row = 2, Col = 8, Text = "White" }, 
                new Coordinates { Row = 3, Col = 2, Text = "White" }, 
                new Coordinates { Row = 3, Col = 8, Text = "White" }, 
                new Coordinates { Row = 4, Col = 6, Text = "White" }, 
                new Coordinates { Row = 4, Col = 8, Text = "White" }, 
                new Coordinates { Row = 5, Col = 1, Text = "White" }, 
                new Coordinates { Row = 5, Col = 2, Text = "White" }, 
                new Coordinates { Row = 5, Col = 3, Text = "White" }, 
                new Coordinates { Row = 5, Col = 4, Text = "White" }, 
                new Coordinates { Row = 5, Col = 6, Text = "White" }, 
                new Coordinates { Row = 6, Col = 1, Text = "White" }, 
                new Coordinates { Row = 6, Col = 3, Text = "White" }, 
                new Coordinates { Row = 6, Col = 7, Text = "White" }, 
                new Coordinates { Row = 7, Col = 1, Text = "White" }, 
                new Coordinates { Row = 7, Col = 7, Text = "White" }, 
                new Coordinates { Row = 8, Col = 6, Text = "White" }, 
                });
            _sudokuInput.AreAllConsecutiveDotsMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 7, 2, 9, 8, 3, 6, 1, 5, 4 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_Clock_SolveSudoku()
        {
            _sudokuInput.Types.Add("Clock");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Number = 7 }, 
                new Coordinates { Row = 1, Col = 9, Number = 5 }, 
                new Coordinates { Row = 2, Col = 1, Number = 8 }, 
                new Coordinates { Row = 2, Col = 8, Number = 3 }, 
                new Coordinates { Row = 3, Col = 1, Number = 3 }, 
                new Coordinates { Row = 4, Col = 1, Number = 9 }, 
                new Coordinates { Row = 4, Col = 4, Number = 5 }, 
                new Coordinates { Row = 4, Col = 7, Number = 2 }, 
                new Coordinates { Row = 5, Col = 1, Number = 5 }, 
                new Coordinates { Row = 5, Col = 5, Number = 4 }, 
                new Coordinates { Row = 5, Col = 9, Number = 7 }, 
                new Coordinates { Row = 6, Col = 1, Number = 6 }, 
                new Coordinates { Row = 6, Col = 6, Number = 7 }, 
                new Coordinates { Row = 7, Col = 1, Number = 2 }, 
                new Coordinates { Row = 7, Col = 3, Number = 5 }, 
                new Coordinates { Row = 8, Col = 1, Number = 1 }, 
                new Coordinates { Row = 8, Col = 5, Number = 5 }, 
                new Coordinates { Row = 9, Col = 1, Number = 4 }, 
                new Coordinates { Row = 9, Col = 2, Number = 8 }, 
                new Coordinates { Row = 9, Col = 3, Number = 6 }, 
                new Coordinates { Row = 9, Col = 4, Number = 7 }, 
                new Coordinates { Row = 9, Col = 5, Number = 9 }, 
                new Coordinates { Row = 9, Col = 6, Number = 3 }, 
                new Coordinates { Row = 9, Col = 7, Number = 5 }, 
                new Coordinates { Row = 9, Col = 8, Number = 2 }, 
                new Coordinates { Row = 9, Col = 9, Number = 1 }, 
                });
            _sudokuInput.ClockFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 2, Col = 4, Text = "Black" }, 
                new Coordinates { Row = 2, Col = 5, Text = "Black" }, 
                new Coordinates { Row = 3, Col = 7, Text = "Black" }, 
                new Coordinates { Row = 4, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 4, Col = 5, Text = "Black" }, 
                new Coordinates { Row = 5, Col = 3, Text = "Black" }, 
                new Coordinates { Row = 5, Col = 8, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 6, Text = "Black" }, 
                new Coordinates { Row = 6, Col = 8, Text = "Black" }, 
                new Coordinates { Row = 7, Col = 1, Text = "Black" }, 
                new Coordinates { Row = 7, Col = 3, Text = "Black" }, 
                new Coordinates { Row = 7, Col = 5, Text = "Black" }, 
                new Coordinates { Row = 8, Col = 2, Text = "Black" }, 
                new Coordinates { Row = 8, Col = 7, Text = "Black" }, 
                new Coordinates { Row = 8, Col = 8, Text = "Black" }, 
                new Coordinates { Row = 1, Col = 1, Text = "White" }, 
                new Coordinates { Row = 1, Col = 4, Text = "White" }, 
                new Coordinates { Row = 1, Col = 6, Text = "White" }, 
                new Coordinates { Row = 2, Col = 2, Text = "White" }, 
                new Coordinates { Row = 3, Col = 4, Text = "White" }, 
                new Coordinates { Row = 3, Col = 6, Text = "White" }, 
                new Coordinates { Row = 4, Col = 3, Text = "White" }, 
                new Coordinates { Row = 4, Col = 7, Text = "White" }, 
                new Coordinates { Row = 5, Col = 1, Text = "White" }, 
                new Coordinates { Row = 6, Col = 3, Text = "White" }, 
                new Coordinates { Row = 6, Col = 4, Text = "White" }, 
                new Coordinates { Row = 6, Col = 7, Text = "White" }, 
                new Coordinates { Row = 7, Col = 2, Text = "White" }, 
                new Coordinates { Row = 7, Col = 6, Text = "White" }, 
                new Coordinates { Row = 7, Col = 8, Text = "White" }, 
                new Coordinates { Row = 8, Col = 1, Text = "White" }, 
                new Coordinates { Row = 8, Col = 3, Text = "White" }, 
                new Coordinates { Row = 8, Col = 5, Text = "White" }, 

                });
            _sudokuInput.AreAllClocksMarked = true;
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 5, 6, 8, 1, 4, 9, 3, 2, 7 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_ChildSum_SolveSudoku()
        {
            _sudokuInput.Types.Add("ChildSum");
            _sudokuInput.ChildSumFields.AddRange(new List<List<Coordinates>> {
                new List<Coordinates> { 
                    new Coordinates { Number = 9, Text = "Max sum" }, 
                    new Coordinates { Number = 7, Text = "Sum" }, 
                    new Coordinates { Number = 3, Text = "Sum" }, 
                    new Coordinates { Number = 7, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Number = 5, Text = "Sum" }, 
                    new Coordinates { Number = 6, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Row = 1, Col = 1, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 2, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 3, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 4, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 5, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 6, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 7, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 8, Text = "Coordinates" }, 
                    new Coordinates { Row = 1, Col = 9, Text = "Coordinates" }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 9, Text = "Max sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 4, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Number = 5, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Number = 2, Text = "Sum" }, 
                    new Coordinates { Row = 3, Col = 1, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 2, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 3, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 4, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 5, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 6, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 7, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 8, Text = "Coordinates" }, 
                    new Coordinates { Row = 3, Col = 9, Text = "Coordinates" }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 9, Text = "Max sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 1, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Row = 4, Col = 1, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 2, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 3, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 4, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 5, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 6, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 7, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 8, Text = "Coordinates" }, 
                    new Coordinates { Row = 4, Col = 9, Text = "Coordinates" }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 9, Text = "Max sum" }, 
                    new Coordinates { Number = 2, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Number = 5, Text = "Sum" }, 
                    new Coordinates { Number = 6, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Number = 4, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 3, Text = "Sum" }, 
                    new Coordinates { Row = 6, Col = 1, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 2, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 3, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 4, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 5, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 6, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 7, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 8, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 9, Text = "Coordinates" }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 9, Text = "Max sum" }, 
                    new Coordinates { Number = 4, Text = "Sum" }, 
                    new Coordinates { Number = 7, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Number = 7, Text = "Sum" }, 
                    new Coordinates { Number = 3, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 7, Text = "Sum" }, 
                    new Coordinates { Row = 7, Col = 1, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 2, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 3, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 4, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 5, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 6, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 7, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 8, Text = "Coordinates" }, 
                    new Coordinates { Row = 7, Col = 9, Text = "Coordinates" }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 9, Text = "Max sum" }, 
                    new Coordinates { Number = 6, Text = "Sum" }, 
                    new Coordinates { Number = 4, Text = "Sum" }, 
                    new Coordinates { Number = 6, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "Sum" }, 
                    new Coordinates { Number = 5, Text = "Sum" }, 
                    new Coordinates { Number = 7, Text = "Sum" }, 
                    new Coordinates { Number = 8, Text = "Sum" }, 
                    new Coordinates { Row = 9, Col = 1, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 2, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 3, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 4, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 5, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 6, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 7, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 8, Text = "Coordinates" }, 
                    new Coordinates { Row = 9, Col = 9, Text = "Coordinates" }, }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 9, 4, 5, 2, 3, 8, 6, 7, 1 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_MagicSquare_SolveSudoku()
        {
            _sudokuInput.Types.Add("MagicSquare");
            _sudokuInput.Types.Add("Diagonal");
            _sudokuInput.Types.Add("AntiKnight");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 4, Col = 1, Number = 3 }, 
                new Coordinates { Row = 4, Col = 2, Number = 8 }, 
                new Coordinates { Row = 4, Col = 3, Number = 4 }, 
                new Coordinates { Row = 9, Col = 9, Number = 2 }, 
                });
            _sudokuInput.MagicSquareFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 4, Col = 4 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 6, 7, 2, 1, 5, 9, 8, 3, 4 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        [TestMethod]
        public void Solve_ProductResult_SolveSudoku()
        {
            _sudokuInput.Types.Add("ProductResult");
            _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
                new Coordinates { Row = 1, Col = 4, Number = 6 }, 
                new Coordinates { Row = 2, Col = 5, Number = 1 }, 
                new Coordinates { Row = 3, Col = 6, Number = 7 }, 
                new Coordinates { Row = 4, Col = 1, Number = 9 }, 
                new Coordinates { Row = 4, Col = 7, Number = 6 }, 
                new Coordinates { Row = 5, Col = 2, Number = 3 }, 
                new Coordinates { Row = 5, Col = 8, Number = 2 }, 
                new Coordinates { Row = 6, Col = 3, Number = 2 }, 
                new Coordinates { Row = 6, Col = 9, Number = 1 }, 
                new Coordinates { Row = 7, Col = 4, Number = 4 }, 
                new Coordinates { Row = 8, Col = 5, Number = 7 }, 
                new Coordinates { Row = 9, Col = 6, Number = 8 }, 
                });
            _sudokuInput.ProductResultFields.AddRange(new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1 }, 
                new Coordinates { Row = 2, Col = 2 }, 
                new Coordinates { Row = 3, Col = 3 }, 
                new Coordinates { Row = 4, Col = 4 }, 
                new Coordinates { Row = 5, Col = 5 }, 
                new Coordinates { Row = 6, Col = 6 }, 
                new Coordinates { Row = 7, Col = 7 }, 
                new Coordinates { Row = 8, Col = 8 }, 
                });
            _sudoku = new SudokuClass(_sudokuInput);

            _sudoku.Solve();

            Assert.AreEqual("Solved", _sudoku.Status);
            Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
            List<int> middleAreaMethod = new();
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
            middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
            List<int> middleAreaExpected = new() { 8, 2, 3, 1, 6, 9, 7, 5, 4 };
            Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        }

        // [TestMethod]
        // public void Solve__SolveSudoku()
        // {
        //     _sudokuInput.Types.Add("");
        //     _sudokuInput.InputNumbers.AddRange(new List<Coordinates> {
        //         new Coordinates { Row = , Col = , Number =  }, 
        //         });
        //     _sudokuInput.InputNumbers.AddRange(new List<Coordinates> { 
        //         new Coordinates { Row = , Col = , Number = , Direction = "", Text = "" }, 
        //         });
        //     _sudoku = new SudokuClass(_sudokuInput);

        //     _sudoku.Solve();

        //     Assert.AreEqual("Solved", _sudoku.Status);
        //     Assert.AreEqual(1, _sudoku.OutputNumbers.Count);
        //     List<int> middleAreaMethod = new();
        //     middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][3].GetRange(3, 3));
        //     middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][4].GetRange(3, 3));
        //     middleAreaMethod.AddRange(_sudoku.OutputNumbers[0][5].GetRange(3, 3));
        //     List<int> middleAreaExpected = new() {  };
        //     Assert.AreEqual(JsonConvert.SerializeObject(middleAreaExpected), JsonConvert.SerializeObject(middleAreaMethod));
        // }
    }
}
