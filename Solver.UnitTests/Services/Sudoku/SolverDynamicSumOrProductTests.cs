using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverDynamicSumOrProductTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverDynamicSumOrProduct _solver = new(new List<Coordinates> {
            new Coordinates { Row = 1, Col = 1, Direction = "N", Number = 3 }, 
            new Coordinates { Row = 2, Col = 2, Direction = "N", Number = 7 }, 
            new Coordinates { Row = 3, Col = 3, Direction = "N", Number = 15 }, 
            }, _rowCnt, _colCnt, _numRange);

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
            _gridManual.Fields[0, 0].Priority = _priorityMid + _priorityLow;
            _gridManual.Fields[0, 1].Priority = _priorityLow;
            _gridManual.Fields[0, 2].Priority = _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityLow;
            _gridManual.Fields[1, 1].Priority = _priorityMid + _priorityLow;
            _gridManual.Fields[1, 2].Priority = _priorityLow;
            _gridManual.Fields[2, 0].Priority = _priorityLow;
            _gridManual.Fields[2, 1].Priority = _priorityLow;
            _gridManual.Fields[2, 2].Priority = _priorityMid + _priorityLow;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_SetNumbersAsInDescription()
            // (1, 1): 2
            // (2, 1): 1-2
            // (1, 2): 2, 5, 7
            // (2, 2): 1-2
            // (1, 3): 1, 3-9
            // (2, 3): 1, 3-9
            // (3, 3): 2-3
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, false, true, true, false, true, false, true, true };
            _gridManual.Fields[0, 1].BanCnt = 6;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, true, false, false, false, false, false, false, false };
            _gridManual.Fields[0, 2].BanCnt = 1;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 7;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 7;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, true, false, false, false, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 1;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, false, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set2To11_SetNumbersAsInDescription()
            // (2, 1): 1
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 0].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set2To21_SetNumbersAsInDescription()
            // (1, 1): null
        {
            _gridMethod.Fields[1, 0].Number = 2;
            _gridManual.Fields[1, 0].Number = 2;

            _solver.Ban(_gridMethod, 2, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 9;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set5To12_SetNumbersAsInDescription()
            // (2, 2): 2
        {
            _gridMethod.Fields[0, 1].Number = 5;
            _gridManual.Fields[0, 1].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set7To12_SetNumbersAsInDescription()
            // (2, 2): 1
        {
            _gridMethod.Fields[0, 1].Number = 7;
            _gridManual.Fields[0, 1].Number = 7;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 7, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set1To22_SetNumbersAsInDescription()
            // (1, 2): 7
        {
            _gridMethod.Fields[1, 1].Number = 1;
            _gridManual.Fields[1, 1].Number = 1;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 1, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[0, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set2To22_SetNumbersAsInDescription()
            // (1, 2): 5
        {
            _gridMethod.Fields[1, 1].Number = 2;
            _gridManual.Fields[1, 1].Number = 2;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 2, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set1To13_SetNumbersAsInDescription()
            // (2, 3): 3, 5
            // (3, 3): 3
        {
            _gridMethod.Fields[0, 2].Number = 1;
            _gridManual.Fields[0, 2].Number = 1;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 1, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, false, true, false, true, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 7;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set3To13_SetNumbersAsInDescription()
            // (2, 3): 5
            // (3, 3): 2
        {
            _gridMethod.Fields[0, 2].Number = 3;
            _gridManual.Fields[0, 2].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 8;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set6To13_SetNumbersAsInDescription()
            // (2, 3): 9
            // (3, 3): 2
        {
            _gridMethod.Fields[0, 2].Number = 6;
            _gridManual.Fields[0, 2].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 6, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[1, 2].BanCnt = 8;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set8To13_SetNumbersAsInDescription()
            // (2, 3): 3-4, 7
            // (3, 3): 2-3
        {
            _gridMethod.Fields[0, 2].Number = 8;
            _gridManual.Fields[0, 2].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 8, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, false, false, true, true, false, true, true };
            _gridManual.Fields[1, 2].BanCnt = 6;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, false, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set8To13Set2To33_SetNumbersAsInDescription()
            // (2, 3): 7
        {
            _gridMethod.Fields[0, 2].Number = 8;
            _gridMethod.Fields[2, 2].Number = 2;
            _gridManual.Fields[0, 2].Number = 8;
            _gridManual.Fields[2, 2].Number = 2;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[1, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set8To13Set3To33_SetNumbersAsInDescription()
            // (2, 3): 4
        {
            _gridMethod.Fields[0, 2].Number = 8;
            _gridMethod.Fields[2, 2].Number = 3;
            _gridManual.Fields[0, 2].Number = 8;
            _gridManual.Fields[2, 2].Number = 3;

            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, true, false, true, true, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set9To13Set6To23_SetNumbersAsInDescription()
            // (3, 3): 2
        {
            _gridMethod.Fields[0, 2].Number = 9;
            _gridMethod.Fields[1, 2].Number = 6;
            _gridManual.Fields[0, 2].Number = 9;
            _gridManual.Fields[1, 2].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 9, _numRange);
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
