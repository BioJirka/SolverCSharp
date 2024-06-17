using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverConsecutiveDotTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverConsecutiveDot _solver = new(new List<Coordinates> {
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

        [TestMethod]
        public void Ban_FirstInBlack2_Ban26789InOthers()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 0].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, true, false, false, false, true, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 5;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, false, false, false, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 5;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, false, false, false, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInBlack23_SetOthersTo145()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 3;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 3;

            _gridMethod.Fields[1, 0].Bans[2] = true;
            _gridMethod.Fields[1, 0].BanCnt++;
            _gridMethod.Fields[1, 1].Bans[2] = true;
            _gridMethod.Fields[1, 1].BanCnt++;
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, true, false, false, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 6;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, true, false, false, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInBlack25_SetOthersTo34()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 5;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 5;

            _gridMethod.Fields[1, 0].Bans[4] = true;
            _gridMethod.Fields[1, 0].BanCnt++;
            _gridMethod.Fields[1, 1].Bans[4] = true;
            _gridMethod.Fields[1, 1].BanCnt++;
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, false, false, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 7;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, false, false, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInBlack235_SetLastTo4()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 3;
            _gridMethod.Fields[1, 0].Number = 5;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 3;
            _gridManual.Fields[1, 0].Number = 5;

            _gridMethod.Fields[1, 1].Bans = new bool[_numRange] { false, false, true, false, true, false, false, false, false };
            _gridMethod.Fields[1, 1].BanCnt += 2;
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInBlack234_SetLastTo15()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 3;
            _gridMethod.Fields[1, 0].Number = 4;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 3;
            _gridManual.Fields[1, 0].Number = 4;

            _gridMethod.Fields[1, 1].Bans = new bool[_numRange] { false, false, true, true, false, false, false, false, false };
            _gridMethod.Fields[1, 1].BanCnt += 2;
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, true, true, false, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthInBlack234_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 3;
            _gridMethod.Fields[1, 0].Number = 4;
            _gridMethod.Fields[1, 1].Number = 5;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 3;
            _gridManual.Fields[1, 0].Number = 4;
            _gridManual.Fields[1, 1].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstInWhite_BanThatNumberAndConsecutiveInOthers()
        {
            _gridMethod.Fields[2, 2].Number = 2;
            _gridManual.Fields[2, 2].Number = 2;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 3;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 3;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[2, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInWhite_BanThatNumberAndConsecutiveInOthers()
        {
            _gridMethod.Fields[1, 2].Number = 2;
            _gridMethod.Fields[2, 1].Number = 4;
            _gridMethod.Fields[2, 2].Number = 7;
            _gridManual.Fields[1, 2].Number = 2;
            _gridManual.Fields[2, 1].Number = 4;
            _gridManual.Fields[2, 2].Number = 7;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 7, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, false };
            _gridManual.Fields[1, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstInNone_DoNothing()
        {
            _gridMethod.Fields[0, 2].Number = 7;
            _gridManual.Fields[0, 2].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 7, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNoneTwoNumbersEquals_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 5;
            _gridMethod.Fields[0, 2].Number = 7;
            _gridMethod.Fields[1, 1].Number = 7;
            _gridManual.Fields[0, 1].Number = 5;
            _gridManual.Fields[0, 2].Number = 7;
            _gridManual.Fields[1, 1].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNoneTwoConsecutiveLastFar_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 3;
            _gridMethod.Fields[0, 2].Number = 6;
            _gridMethod.Fields[1, 1].Number = 7;
            _gridManual.Fields[0, 1].Number = 3;
            _gridManual.Fields[0, 2].Number = 6;
            _gridManual.Fields[1, 1].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNonePotentialWhite135_Ban789InLast()
        {
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[0, 2].Number = 3;
            _gridMethod.Fields[1, 1].Number = 5;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[0, 2].Number = 3;
            _gridManual.Fields[1, 1].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNonePotentialBlack345_Ban26InLast()
        {
            _gridMethod.Fields[0, 1].Number = 3;
            _gridMethod.Fields[0, 2].Number = 4;
            _gridMethod.Fields[1, 1].Number = 5;
            _gridManual.Fields[0, 1].Number = 3;
            _gridManual.Fields[0, 2].Number = 4;
            _gridManual.Fields[1, 1].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 7, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, true, false, false, false, true, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNonePotentialBlack346_Ban5InLast()
        {
            _gridMethod.Fields[0, 1].Number = 3;
            _gridMethod.Fields[0, 2].Number = 4;
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[0, 1].Number = 3;
            _gridManual.Fields[0, 2].Number = 4;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 4, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdInNonePotentialBlack356_Ban4InLast()
        {
            _gridMethod.Fields[0, 1].Number = 3;
            _gridMethod.Fields[0, 2].Number = 5;
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[0, 1].Number = 3;
            _gridManual.Fields[0, 2].Number = 5;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, true, false, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
