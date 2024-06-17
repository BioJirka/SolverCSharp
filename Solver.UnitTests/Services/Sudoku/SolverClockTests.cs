using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverClockTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverClock _solver = new(new List<Coordinates> {
            new Coordinates { Row = 1, Col = 1, Text = "Black" }, 
            new Coordinates { Row = 2, Col = 2, Text = "White" } 
            }, true, _rowCnt, _colCnt);


        [TestInitialize]
        public void TestInit()
        {
            _gridMethod = new(_rowCnt, _colCnt, _numRange);
            _gridManual = new(_rowCnt, _colCnt, _numRange);
        }

        [TestMethod]
        public void SetPriorities_Void_SetPriorities()
        {
            _solver.SetPriorities(_gridMethod, _rowCnt, _colCnt);
            _gridManual.Fields[0, 0].Priority = _priorityMid;
            _gridManual.Fields[0, 1].Priority = _priorityMid + _priorityLow;
            _gridManual.Fields[0, 2].Priority = _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityMid + _priorityLow;
            _gridManual.Fields[1, 1].Priority = 2 * _priorityMid + 2 * _priorityLow;
            _gridManual.Fields[1, 2].Priority = _priorityMid + _priorityLow;
            _gridManual.Fields[2, 0].Priority = _priorityLow;
            _gridManual.Fields[2, 1].Priority = _priorityMid + _priorityLow;
            _gridManual.Fields[2, 2].Priority = _priorityMid;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_DoNothing()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        /*
            We will write only 1 test for counterclockwise cycle to see if the order is right
                (counterclockwise cycles works exactly the same as clockwise, just order of fields is different)
            Notation - example for cycle [1,1], [1,2], [2,2], [2,1]
                for field [1,1]
                    field [1,2] is next
                    field [2,2] is opposite
                    field [2,1] is previous
        */

        [TestMethod]
        public void Ban_FirstInClock2_Ban129InNext123InOpposite234InPrevious()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 0].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, true };
            _gridManual.Fields[0, 1].BanCnt = 3;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, true, true, false, false, false, false, false };
            _gridManual.Fields[1, 0].BanCnt = 3;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstInClock6_Ban456InNext567InOpposite678InPrevious()
        {
            _gridMethod.Fields[0, 0].Number = 6;
            _gridManual.Fields[0, 0].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 6, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, true, true, true, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 3;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, false };
            _gridManual.Fields[1, 0].BanCnt = 3;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, true, true, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstInClock9_Ban789InNext189InOpposite129InPrevious()
        {
            _gridMethod.Fields[0, 0].Number = 9;
            _gridManual.Fields[0, 0].Number = 9;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 9, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 3;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, true };
            _gridManual.Fields[1, 0].BanCnt = 3;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, false, false, false, false, false, true, true };
            _gridManual.Fields[1, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInClock3Next7_SetOppositeTo189SetPreviousTo129()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridMethod.Fields[0, 1].Number = 7;
            _gridManual.Fields[0, 0].Number = 3;
            _gridManual.Fields[0, 1].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, false };
            _gridManual.Fields[1, 0].BanCnt = 6;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, true, true, true, true, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInClock3Opposite7_SetNextTo456SetPreviousTo1289()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridMethod.Fields[1, 1].Number = 7;
            _gridManual.Fields[0, 0].Number = 3;
            _gridManual.Fields[1, 1].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 6;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, false, false };
            _gridManual.Fields[1, 0].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInClock3Previous7_SetNextTo45SetOpprositeTo56()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridMethod.Fields[1, 0].Number = 7;
            _gridManual.Fields[0, 0].Number = 3;
            _gridManual.Fields[1, 0].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, false, false, true, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 7;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, true, false, false, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInClock258_SetLastTo19()
        {
            _gridMethod.Fields[0, 0].Number = 5;
            _gridMethod.Fields[0, 1].Number = 8;
            _gridMethod.Fields[1, 0].Number = 2;
            _gridManual.Fields[0, 0].Number = 5;
            _gridManual.Fields[0, 1].Number = 8;
            _gridManual.Fields[1, 0].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, false };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInClock582_SetLastTo34()
        {
            _gridMethod.Fields[0, 0].Number = 8;
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[1, 0].Number = 5;
            _gridManual.Fields[0, 0].Number = 8;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[1, 0].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 8, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, false, false, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInClock825_SetLastTo67()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 5;
            _gridMethod.Fields[1, 0].Number = 8;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 5;
            _gridManual.Fields[1, 0].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, true, true, false, false, true, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthInClock_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[0, 1].Number = 5;
            _gridMethod.Fields[1, 0].Number = 6;
            _gridMethod.Fields[1, 1].Number = 8;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 5;
            _gridManual.Fields[1, 0].Number = 6;
            _gridManual.Fields[1, 1].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstInCounterclockwiseClock2_Ban129InPrevious123InOpposite234InNext()
        {
            _gridMethod.Fields[2, 2].Number = 2;
            _gridManual.Fields[2, 2].Number = 2;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 3;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, true };
            _gridManual.Fields[1, 2].BanCnt = 3;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, true, true, true, false, false, false, false, false };
            _gridManual.Fields[2, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstInNoClock_DoNothing()
        {
            _gridMethod.Fields[0, 2].Number = 5;
            _gridManual.Fields[0, 2].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInNoClock_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 4;
            _gridMethod.Fields[0, 2].Number = 7;
            _gridManual.Fields[0, 1].Number = 4;
            _gridManual.Fields[0, 2].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 7, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNoClockTwoNumbersEqual_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[0, 2].Number = 2;
            _gridMethod.Fields[1, 2].Number = 4;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[0, 2].Number = 2;
            _gridManual.Fields[1, 2].Number = 4;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 2, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNoClock258_Ban19InLast()
        {
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[0, 2].Number = 5;
            _gridMethod.Fields[1, 2].Number = 8;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[0, 2].Number = 5;
            _gridManual.Fields[1, 2].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, false, false, false, false, false, false, true };
            _gridManual.Fields[1, 1].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNoClock285_Ban34InLast()
        {
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[0, 2].Number = 8;
            _gridMethod.Fields[1, 2].Number = 5;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[0, 2].Number = 8;
            _gridManual.Fields[1, 2].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 8, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, true, true, false, false, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNoClock528_Ban67InLast()
        {
            _gridMethod.Fields[0, 1].Number = 5;
            _gridMethod.Fields[0, 2].Number = 2;
            _gridMethod.Fields[1, 2].Number = 8;
            _gridManual.Fields[0, 1].Number = 5;
            _gridManual.Fields[0, 2].Number = 2;
            _gridManual.Fields[1, 2].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, true, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthInNoClock_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 4;
            _gridMethod.Fields[0, 2].Number = 7;
            _gridMethod.Fields[1, 1].Number = 1;
            _gridMethod.Fields[1, 2].Number = 9;
            _gridManual.Fields[0, 1].Number = 4;
            _gridManual.Fields[0, 2].Number = 7;
            _gridManual.Fields[1, 1].Number = 1;
            _gridManual.Fields[1, 2].Number = 9;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 7, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
