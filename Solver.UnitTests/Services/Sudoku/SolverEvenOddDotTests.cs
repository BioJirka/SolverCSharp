using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverEvenOddDotTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverEvenOddDot _solver = new(
            new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Text = "Black" }, 
                new Coordinates { Row = 2, Col = 2, Text = "White" }
            }, true, _rowCnt, _colCnt, _numRange);

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
            _gridManual.Fields[1, 1].Priority = _priorityMid + 3 * _priorityLow;
            _gridManual.Fields[1, 2].Priority = 2 * _priorityLow;
            _gridManual.Fields[2, 0].Priority = _priorityLow;
            _gridManual.Fields[2, 1].Priority = 2 * _priorityLow;
            _gridManual.Fields[2, 2].Priority = _priorityLow;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_DoNothing()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumberOddInBlackCycle_BanEvenNumbersInRestOfCycle()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridManual.Fields[0, 0].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[0, 1].BanCnt = 4;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[1, 0].BanCnt = 4;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[1, 1].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumberEvenInBlackCycle_BanOddNumbersInRestOfCycle()
        {
            _gridMethod.Fields[0, 0].Number = 6;
            _gridManual.Fields[0, 0].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 6, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[0, 1].BanCnt = 5;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 0].BanCnt = 5;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberInBlackCycle_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 6;
            _gridMethod.Fields[0, 1].Number = 1;
            _gridManual.Fields[0, 0].Number = 6;
            _gridManual.Fields[0, 1].Number = 1;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberInBlackCycle_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 6;
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[1, 0].Number = 7;
            _gridManual.Fields[0, 0].Number = 6;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[1, 0].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthNumberInBlackCycle_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 6;
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[1, 0].Number = 7;
            _gridMethod.Fields[1, 1].Number = 3;
            _gridManual.Fields[0, 0].Number = 6;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[1, 0].Number = 7;
            _gridManual.Fields[1, 1].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumberInWhiteCycle_DoNothing()
        {
            _gridMethod.Fields[2, 2].Number = 6;
            _gridManual.Fields[2, 2].Number = 6;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberEvenInWhiteCycleFirstWasEven_BanEvenInRestOfCycle()
        {
            _gridMethod.Fields[2, 1].Number = 8;
            _gridMethod.Fields[2, 2].Number = 6;
            _gridManual.Fields[2, 1].Number = 8;
            _gridManual.Fields[2, 2].Number = 6;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 6, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[1, 1].BanCnt = 4;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[1, 2].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberOddInWhiteCycleFirstWasOdd_BanOddInRestOfCycle()
        {
            _gridMethod.Fields[2, 1].Number = 1;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[2, 1].Number = 1;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 1].BanCnt = 5;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 2].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberOddInWhiteCycleFirstWasEven_DoNothing()
        {
            _gridMethod.Fields[2, 1].Number = 8;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[2, 1].Number = 8;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberEvenInWhiteCyclePreviousTwoOdd_DoNothing()
        {
            _gridMethod.Fields[1, 2].Number = 3;
            _gridMethod.Fields[2, 1].Number = 1;
            _gridMethod.Fields[2, 2].Number = 6;
            _gridManual.Fields[1, 2].Number = 3;
            _gridManual.Fields[2, 1].Number = 1;
            _gridManual.Fields[2, 2].Number = 6;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberOddInWhiteCyclePreviousTwoEven_DoNothing()
        {
            _gridMethod.Fields[1, 2].Number = 8;
            _gridMethod.Fields[2, 1].Number = 6;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[1, 2].Number = 8;
            _gridManual.Fields[2, 1].Number = 6;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberOddInWhiteCyclePreviousTwoEvenAndOdd_BanOddInLastFieldInCycle()
        {
            _gridMethod.Fields[1, 2].Number = 2;
            _gridMethod.Fields[2, 1].Number = 3;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[1, 2].Number = 2;
            _gridManual.Fields[2, 1].Number = 3;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberEvenInWhiteCyclePreviousTwoEvenAndOdd_BanEvenInLastFieldInCycle()
        {
            _gridMethod.Fields[1, 2].Number = 8;
            _gridMethod.Fields[2, 1].Number = 9;
            _gridMethod.Fields[2, 2].Number = 6;
            _gridManual.Fields[1, 2].Number = 8;
            _gridManual.Fields[2, 1].Number = 9;
            _gridManual.Fields[2, 2].Number = 6;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 6, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[1, 1].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthNumberInWhiteCycle_DoNothing()
        {
            _gridMethod.Fields[1, 1].Number = 4;
            _gridMethod.Fields[1, 2].Number = 8;
            _gridMethod.Fields[2, 1].Number = 9;
            _gridMethod.Fields[2, 2].Number = 6;
            _gridManual.Fields[1, 1].Number = 4;
            _gridManual.Fields[1, 2].Number = 8;
            _gridManual.Fields[2, 1].Number = 9;
            _gridManual.Fields[2, 2].Number = 6;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumberInNonCycle_DoNothing()
        {
            _gridMethod.Fields[0, 2].Number = 6;
            _gridManual.Fields[0, 2].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberInNonCycle_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[0, 2].Number = 6;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[0, 2].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberInNonCycleAllEven_BanEvenInLastFieldInCycle()
        {
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[0, 2].Number = 4;
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[0, 2].Number = 4;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 6, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[1, 2].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberInNonCycleAllOdd_BanOddInLastFieldInCycle()
        {
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[0, 2].Number = 3;
            _gridMethod.Fields[1, 1].Number = 5;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[0, 2].Number = 3;
            _gridManual.Fields[1, 1].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 2].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberInNonCycleTwoOddOneEven_BanEvenInLastFieldInCycle()
        {
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[0, 2].Number = 2;
            _gridMethod.Fields[1, 1].Number = 3;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[0, 2].Number = 2;
            _gridManual.Fields[1, 1].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[1, 2].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberInNonCycleTwoEvenOneOdd_BanOddInLastFieldInCycle()
        {
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[0, 2].Number = 3;
            _gridMethod.Fields[1, 1].Number = 4;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[0, 2].Number = 3;
            _gridManual.Fields[1, 1].Number = 4;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 4, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 2].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthNumberInNonCycle_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[0, 2].Number = 6;
            _gridMethod.Fields[1, 1].Number = 4;
            _gridMethod.Fields[1, 2].Number = 7;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[0, 2].Number = 6;
            _gridManual.Fields[1, 1].Number = 4;
            _gridManual.Fields[1, 2].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
