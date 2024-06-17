using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverRomanTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 10;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverRoman _solver = new(new List<Coordinates> {
            new Coordinates { Row = 1, Col = 1, Direction = "S", Text = "X" }, 
            new Coordinates { Row = 1, Col = 1, Direction = "E", Text = "X" }, 
            new Coordinates { Row = 2, Col = 3, Direction = "S", Text = "V" }, 
            new Coordinates { Row = 3, Col = 2, Direction = "E", Text = "V" } 
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
            _gridManual.Fields[0, 0].Priority = 2 * _priorityMid;
            _gridManual.Fields[0, 1].Priority = _priorityMid + 2 * _priorityLow;
            _gridManual.Fields[0, 2].Priority = 2 * _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityMid + 2 * _priorityLow;
            _gridManual.Fields[1, 1].Priority = 4 * _priorityLow;
            _gridManual.Fields[1, 2].Priority = _priorityMid + 2 * _priorityLow;
            _gridManual.Fields[2, 0].Priority = 2 * _priorityLow;
            _gridManual.Fields[2, 1].Priority = _priorityMid + 2 * _priorityLow;
            _gridManual.Fields[2, 2].Priority = 2 * _priorityMid;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_BanValuesHigherThan4NextToVAndHigherThan9NextToX()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[0, 0].BanCnt = 1;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[0, 1].BanCnt = 1;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[1, 0].BanCnt = 1;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 6;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true, true };
            _gridManual.Fields[2, 1].BanCnt = 6;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValue3InXCorner_SetAdjacentFieldsTo7()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 9;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 9;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValue3InVCorner_SetAdjacentFieldsTo2()
        {
            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 9;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 1].BanCnt = 9;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValue3InNonCorner_Ban2And7InAdjacentFields()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, true, false, false, false, false, true, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 2;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, true, false, false, false, false, true, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
