using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverSumDotTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverSumDot _solver = new(
            new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Text = "Black" } 
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
        public void Ban_FirstNumberLessThan4InSum_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridManual.Fields[0, 0].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumber4InSum_Ban4InRestOfCycle()
        {
            _gridMethod.Fields[0, 0].Number = 4;
            _gridManual.Fields[0, 0].Number = 4;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 4, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 1;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[1, 0].BanCnt = 1;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumberGreaterThan4InSum_BanFromNumberMinus1ToNumberPlus1InRestOfCycle()
        {
            _gridMethod.Fields[0, 0].Number = 5;
            _gridManual.Fields[0, 0].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 5, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, true, true, true, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 3;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, true, true, true, false, false, false };
            _gridManual.Fields[1, 0].BanCnt = 3;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, true, true, true, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        /*
            Testing this feature for second number in the sum cycle is quite complex,
                so we describe the logic here and then pickup one example for each scenario
            Notation:
                x ... higher of 2 numbers in cycle
                y ... lower of 2 numbers in cycle
                N ... numRange
            Scenarios:
                1) 2(x + y) <= N - 1
                    no bans
                2) 2x <= N + 1 and N - 1 < 2(x + y) and x + y <= N - 1
                    allow numbers from 1 to N - (x + y) and from x + y + 1 to N
                3) N + 1 <= 2x and x + y <= N - 1
                    allow numbers from 1 to x - y - 1 and from x + y + 1 to N
                4) N + 1 <= 2x and N < x + y + 1
                    allow numbers from 1 to x - y - 1
        */

        [TestMethod]
        public void Ban_SecondNumberInSum1And3Scenario1_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[0, 1].Number = 3;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberInSum3And4Scenario2_BanFrom3To7InRestOfCycle()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridMethod.Fields[0, 1].Number = 4;
            _gridManual.Fields[0, 0].Number = 3;
            _gridManual.Fields[0, 1].Number = 4;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, false, false };
            _gridManual.Fields[1, 0].BanCnt = 5;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, true, true, true, true, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberInSum2And6Scenario3_BanFrom4To8InRestOfCycle()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 6;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, true, true, true, true, true, false };
            _gridManual.Fields[1, 0].BanCnt = 5;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, true, true, true, true, true, false };
            _gridManual.Fields[1, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberInSum5And9Scenario4_BanFrom4To9InRestOfCycle()
        {
            _gridMethod.Fields[0, 0].Number = 5;
            _gridMethod.Fields[0, 1].Number = 9;
            _gridManual.Fields[0, 0].Number = 5;
            _gridManual.Fields[0, 1].Number = 9;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, true, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 6;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, true, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberInSum_SetLastAsSumOrAsMaxNumberMinusOthersTwo()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[1, 0].Number = 5;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[1, 0].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 1, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, true, true, true, true, true, false, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthNumberInSum_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[1, 0].Number = 3;
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[1, 0].Number = 3;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumberOutsideSum_DoNothing()
        {
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumberOutsideSum_DoNothing()
        {
            _gridMethod.Fields[2, 1].Number = 3;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[2, 1].Number = 3;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumberOutsideSum_BanLastAsSumOrAsMaxNumberMinusOthersTwo()
        {
            _gridMethod.Fields[1, 2].Number = 1;
            _gridMethod.Fields[2, 1].Number = 2;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[1, 2].Number = 1;
            _gridManual.Fields[2, 1].Number = 2;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, false, false, false, false, false, true, false };
            _gridManual.Fields[1, 1].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthNumberOutsideSum_DoNothing()
        {
            _gridMethod.Fields[1, 1].Number = 1;
            _gridMethod.Fields[1, 2].Number = 2;
            _gridMethod.Fields[2, 1].Number = 3;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridManual.Fields[1, 1].Number = 1;
            _gridManual.Fields[1, 2].Number = 2;
            _gridManual.Fields[2, 1].Number = 3;
            _gridManual.Fields[2, 2].Number = 5;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
