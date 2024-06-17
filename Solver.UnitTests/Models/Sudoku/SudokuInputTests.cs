using Solver.Sudoku.Models;

namespace Solver.UnitTests.Sudoku.Models
{
    [TestClass]
    public class SudokuInputTests
    {
        private SudokuInput _sudokuInput;

        [TestInitialize]
        public void TestInit()
        {
            _sudokuInput = new SudokuInput { 
                RowCnt = 9, 
                ColCnt = 9, 
                NumRange = 9, 
                };
        }

        [TestMethod]
        public void SudokuInputCheck_EmptyClass_ReturnEmptyList()
        {
            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(0, messages.Count);
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectBasicNumbers_ReturnErrorMessages()
        {
            _sudokuInput.RowCnt = 0;
            _sudokuInput.ColCnt = 0;
            _sudokuInput.NumRange = 0;

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(3, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, number")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectInputNumbers_ReturnErrorMessages()
        {
            _sudokuInput.InputNumbers = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Number = 0 }, 
                new Coordinates { Row = 5, Col = 5, Number = 5 }, 
                new Coordinates { Row = 11, Col = 11, Number = 11 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(6, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, number")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectAreas_ReturnErrorMessages()
        {
            _sudokuInput.AreaFields = new List<List<Coordinates>> {
                new List<Coordinates> { 
                    new Coordinates { Row = 0, Col = 0 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 11, Col = 11 }, 
                }};

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectAntiAreas_ReturnErrorMessage()
        {
            _sudokuInput.AntiAreaFields = new List<List<Coordinates>> {
                new List<Coordinates> {
                    new Coordinates { Row = 0, Col = 0 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 11, Col = 11 }, 
                }};
            
            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(5, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, number")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectClone_ReturnErrorMessages()
        {
            _sudokuInput.CloneFields = new List<List<List<Coordinates>>> { 
                new List<List<Coordinates>>(), 
                new List<List<Coordinates>> { 
                    new List<Coordinates> { 
                        new Coordinates { Row = 0, Col = 0 }, 
                        new Coordinates { Row = 11, Col = 11 }, }, 
                    new List<Coordinates> { 
                        new Coordinates { Row = 2, Col = 2 }, }, 
                    new List<Coordinates> { 
                        new Coordinates { Row = 3, Col = 3 }, }, 
                 }, 
                new List<List<Coordinates>> { 
                    new List<Coordinates> { 
                        new Coordinates { Row = 4, Col = 4 }, 
                        new Coordinates { Row = 5, Col = 5 }, }, 
                    new List<Coordinates> { 
                        new Coordinates { Row = 6, Col = 6 }, 
                        new Coordinates { Row = 7, Col = 7 }, }, 
                    new List<Coordinates> { 
                        new Coordinates { Row = 8, Col = 8 }, 
                        new Coordinates { Row = 9, Col = 9 }, }, } };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(6, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, lengths")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectShape_ReturnErrorMessages()
        {
            _sudokuInput.ShapeFields = new List<List<Coordinates>> {
                new List<Coordinates> { 
                    new Coordinates { Row = 0, Col = 0 }, 
                    new Coordinates { Row = 11, Col = 11 }, 
                    new Coordinates { Row = 5, Col = 5 }, }, };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectEvenOdd_ReturnErrorMessages()
        {
            _sudokuInput.EvenOddFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Text = "Even" }, 
                new Coordinates { Row = 5, Col = 5, Text = "Odd" }, 
                new Coordinates { Row = 11, Col = 11, Text = "Zero" }, };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(5, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectEvenOddArea_ReturnErrorMessages()
        {
            _sudokuInput.EvenOddAreaFields = new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 0, Col = 0 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 11, Col = 11 }, }, };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectLowMidHigh_ReturnErrorMessages()
        {
            _sudokuInput.LowMidHighFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Text = "Low" }, 
                new Coordinates { Row = 5, Col = 5, Text = "Mid" }, 
                new Coordinates { Row = 6, Col = 6, Text = "High" }, 
                new Coordinates { Row = 11, Col = 11, Text = "Zero" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(5, messages.Count, 5);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectFortress_ReturnErrorMessages()
        {
            _sudokuInput.FortressFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0 }, 
                new Coordinates { Row = 5, Col = 5 }, 
                new Coordinates { Row = 11, Col = 11 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectConsecutiveEdge_ReturnErrorMessages()
        {
            _sudokuInput.ConsecutiveEdgeFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Direction = "N", Text = "O" }, 
                new Coordinates { Row = 5, Col = 9, Direction = "E", Text = "O" }, 
                new Coordinates { Row = 9, Col = 5, Direction = "S", Text = "X" },  
                new Coordinates { Row = 11, Col = 11, Direction = "X", Text = "A" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(9, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, direction")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectRoman_ReturnErrorMessages()
        {
            _sudokuInput.RomanFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Direction = "N", Text = "O" }, 
                new Coordinates { Row = 5, Col = 9, Direction = "E", Text = "V" }, 
                new Coordinates { Row = 9, Col = 5, Direction = "S", Text = "X" },  
                new Coordinates { Row = 11, Col = 11, Direction = "X", Text = "A" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(9, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, direction")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectDifference_ReturnErrorMessages()
        {
            _sudokuInput.DifferenceFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Direction = "N", Number = 0 }, 
                new Coordinates { Row = 5, Col = 9, Direction = "E", Number = 1234 }, 
                new Coordinates { Row = 9, Col = 5, Direction = "S", Number = -1 },  
                new Coordinates { Row = 11, Col = 11, Direction = "X", Number = 6 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(9, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, number")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, direction")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectMinDifference_ReturnErrorMessages()
        {
            _sudokuInput.MinDifferenceFields = new List<List<Coordinates>> { 
                new List<Coordinates> {
                    new Coordinates { Row = 0, Col = 0, Number = 0 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 5, Col = 9, Number = 1234 }, 
                    new Coordinates { Row = 9, Col = 5 },  
                    new Coordinates { Row = 11, Col = 11 }, }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(5, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, number")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectSumEdge_ReturnErrorMessages()
        {
            _sudokuInput.SumEdgeFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Direction = "N", Number = 0 }, 
                new Coordinates { Row = 5, Col = 9, Direction = "E", Number = 1234 }, 
                new Coordinates { Row = 9, Col = 5, Direction = "S", Number = 5 },  
                new Coordinates { Row = 11, Col = 11, Direction = "X", Number = 6 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(9, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, number")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, direction")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectInequality_ReturnErrorMessages()
        {
            _sudokuInput.InequalityFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Direction = "N", Text = "Higher" }, 
                new Coordinates { Row = 5, Col = 9, Direction = "E", Text = "Higher" }, 
                new Coordinates { Row = 9, Col = 5, Direction = "S", Text = "Lower" },  
                new Coordinates { Row = 11, Col = 11, Direction = "X", Text = "X" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(9, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, direction")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectThermometer_ReturnErrorMessages()
        {
            _sudokuInput.ThermometerFields = new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 0, Col = 0 }, 
                    new Coordinates { Row = 5, Col = 5 }, 
                    new Coordinates { Row = 11, Col = 11 }, 
                } };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectWhereIsX_ReturnErrorMessages()
        {
            _sudokuInput.WhereIsXFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Direction = "N", Number = 0 }, 
                new Coordinates { Row = 2, Col = 2, Direction = "W", Number = 2 }, 
                new Coordinates { Row = 3, Col = 3, Direction = "S", Number = 3 }, 
                new Coordinates { Row = 4, Col = 4, Direction = "E", Number = 4 }, 
                new Coordinates { Row = 5, Col = 5, Direction = "NW", Number = 5 }, 
                new Coordinates { Row = 6, Col = 6, Direction = "NE", Number = 6 }, 
                new Coordinates { Row = 7, Col = 7, Direction = "SW", Number = 7 }, 
                new Coordinates { Row = 8, Col = 8, Direction = "SE", Number = 8 }, 
                new Coordinates { Row = 11, Col = 11, Direction = "X", Number = 11 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(7, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, number")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, direction")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_DynamicSumOrProduct_ReturnErrorMessage()
        {
            _sudokuInput.DynamicSumOrProductFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Direction = "N", Number = 0}, 
                new Coordinates { Row = 3, Col = 3, Direction = "W", Number = 1}, 
                new Coordinates { Row = 5, Col = 5, Direction = "S", Number = 5}, 
                new Coordinates { Row = 7, Col = 7, Direction = "E", Number = 11}, 
                new Coordinates { Row = 11, Col = 11, Direction = "X", Number = 111}, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(6, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, number")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, direction")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectKiller_ReturnErrorMessages()
        {
            _sudokuInput.KillerFields = new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 0, Col = 0, Number = 1234 }, 
                    new Coordinates { Row = 5, Col = 5 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 11, Col = 11, Number = 0 }, 
                } };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(5, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, number")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectSumWeighted_ReturnErrorMessages()
        {
            _sudokuInput.SumWeightedFields = new List<List<Coordinates>> {
                new List<Coordinates> { 
                    new Coordinates { Number = -1 }, }, 
                new List<Coordinates> {
                    new Coordinates { Number = 100 }, 
                    new Coordinates { Row = 0, Col = 0, Number = 101 }, 
                    new Coordinates { Row = 5, Col = 5, Number = 0 }, 
                    new Coordinates { Row = 11, Col = 11, Number = 5 }, }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(7, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, number")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectSumCorner_ReturnErrorMessages()
        {
            _sudokuInput.SumCornerFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Number = 0 }, 
                new Coordinates { Row = 5, Col = 9, Number = 1234 }, 
                new Coordinates { Row = 9, Col = 5, Number = 5 }, 
                new Coordinates { Row = 11, Col = 11, Number = 1 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(7, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, number")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectEvenOddDot_ReturnErrorMessages()
        {
            _sudokuInput.EvenOddDotFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Text = "Black" }, 
                new Coordinates { Row = 5, Col = 9, Text = "White" }, 
                new Coordinates { Row = 9, Col = 5, Text = "None" }, 
                new Coordinates { Row = 11, Col = 11, Text = "X" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(7, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectSumDot_ReturnErrorMessages()
        {
            _sudokuInput.SumDotFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Text = "Black" }, 
                new Coordinates { Row = 5, Col = 9, Text = "White" }, 
                new Coordinates { Row = 9, Col = 5, Text = "None" }, 
                new Coordinates { Row = 11, Col = 11, Text = "X" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(8, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectSumArrow_ReturnErrorMessages()
        {
            _sudokuInput.SumArrowFields = new List<List<Coordinates>> {
                new List<Coordinates> {
                    new Coordinates { Row = 5, Col = 5 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 0, Col = 0 },  
                    new Coordinates { Row = 11, Col = 11 },  
                    new Coordinates { Row = 5, Col = 5 }, }, 
            };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(5, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, length ")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectConsecutiveDot_ReturnErrorMessages()
        {
            _sudokuInput.ConsecutiveDotFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Text = "Black" }, 
                new Coordinates { Row = 5, Col = 9, Text = "White" }, 
                new Coordinates { Row = 9, Col = 5, Text = "None" }, 
                new Coordinates { Row = 11, Col = 11, Text = "X" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(7, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectClock_ReturnErrorMessages()
        {
            _sudokuInput.ClockFields = new List<Coordinates> { 
                new Coordinates { Row = 0, Col = 0, Text = "Black" }, 
                new Coordinates { Row = 5, Col = 9, Text = "White" }, 
                new Coordinates { Row = 9, Col = 5, Text = "None" }, 
                new Coordinates { Row = 11, Col = 11, Text = "X" }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(7, messages.Count);
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(3, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, text")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectChildSum_ReturnErrorMessages()
        {
            _sudokuInput.ChildSumFields = new List<List<Coordinates>> {
                new List<Coordinates> {
                    new Coordinates { Number = 8, Text = "Max sum" }, 
                    new Coordinates { Number = 0, Text = "Sum" }, 
                    new Coordinates { Number = 9, Text = "X" }, 
                    new Coordinates { Row = 4, Col = 4, Text = "Coordinates" }, 
                    new Coordinates { Number = 11, Text = "Sum" }, 
                    new Coordinates { Row = 5, Col = 5, Text = "Coordinates" }, 
                    new Coordinates { Row = 6, Col = 6, Text = "Coordinates" }, 
                    new Coordinates { Row = 11, Col = 11, Text = "Coordinates" }, }, 
                new List<Coordinates> {
                    new Coordinates { Number = 0, Text = "Max sum" }, 
                    new Coordinates { Number = 5, Text = "Sum"}, 
                    new Coordinates { Row = 5, Col = 5, Text = "Coordinates" }, } ,
                new List<Coordinates> {
                    new Coordinates { Text = "Coordinates" }, }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(11, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
            Assert.AreEqual(4, messages.Where(x => x.StartsWith("Error, number")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, text")).Count());
            Assert.AreEqual(1, messages.Where(x => x.StartsWith("Error, no numbers or no coordinates")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectMagicSquare_ReturnErrorMessages()
        {
            _sudokuInput.MagicSquareFields = new List<Coordinates> {
                new Coordinates { Row = 0, Col = 0 }, 
                new Coordinates { Row = 2, Col = 2 }, 
                new Coordinates { Row = 8, Col = 8 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
        }

        [TestMethod]
        public void SudokuInputCheck_IncorrectProductResult_ReturnErrorMessages()
        {
            _sudokuInput.ProductResultFields = new List<Coordinates> {
                new Coordinates { Row = 0, Col = 0 }, 
                new Coordinates { Row = 5, Col = 5 }, 
                new Coordinates { Row = 9, Col = 9 }, 
                };

            List<string> messages = _sudokuInput.SudokuInputCheck();

            Assert.AreEqual(4, messages.Count);
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, row")).Count());
            Assert.AreEqual(2, messages.Where(x => x.StartsWith("Error, column")).Count());
        }
    }
}