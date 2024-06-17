using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverConsecutiveTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverConsecutiveEdge _solver = new(
            new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Direction = "S", Text = "X" }, 
                new Coordinates { Row = 1, Col = 1, Direction = "E", Text = "X" }
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
            _gridManual.Fields[0, 1].Priority = _priorityMid + 2* _priorityLow;
            _gridManual.Fields[0, 2].Priority = 2 * _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityMid + 2* _priorityLow;
            _gridManual.Fields[1, 1].Priority = 4 * _priorityLow;
            _gridManual.Fields[1, 2].Priority = 3 * _priorityLow;
            _gridManual.Fields[2, 0].Priority = 2 * _priorityLow;
            _gridManual.Fields[2, 1].Priority = 3 * _priorityLow;
            _gridManual.Fields[2, 2].Priority = 2 * _priorityLow;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_DoNothing()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValueInConsecutiveCornerTo3_SetAdjacentFieldsTo2Or4()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, false, true, false, true, true, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 7;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, false, true, false, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValueInNonConsecutiveCornerTo3_Ban2And4InAdjacentFields()
        {
            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, true, false, true, false, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 2;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, true, false, true, false, false, false, false, false };
            _gridManual.Fields[2, 1].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
