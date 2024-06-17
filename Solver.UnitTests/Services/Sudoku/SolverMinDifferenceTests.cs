using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverMinDifferenceTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverMinDifference _solver = new(new List<List<Coordinates>> {
            new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Number = 3 }, 
                new Coordinates { Row = 1, Col = 2}, 
                new Coordinates { Row = 1, Col = 3}, }, 
            new List<Coordinates> { 
                new Coordinates { Row = 2, Col = 1, Number = 5 }, 
                new Coordinates { Row = 2, Col = 2}, 
                new Coordinates { Row = 2, Col = 3}, }, 
            }, _rowCnt, _colCnt);

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
            _gridManual.Fields[0, 0].Priority = _priorityLow;
            _gridManual.Fields[0, 1].Priority = 2 * _priorityLow;
            _gridManual.Fields[0, 2].Priority = _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityMid;
            _gridManual.Fields[1, 1].Priority = 2 * _priorityMid;
            _gridManual.Fields[1, 2].Priority = _priorityMid;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_Ban5InSecondRow()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[1, 0].BanCnt = 1;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 1;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set12To3_Set11And13To6789()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 5;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, true, true, true, false, false, false, false };
            _gridManual.Fields[0, 2].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set12To5_Set11And13To1289()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 5, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, false, false };
            _gridManual.Fields[0, 0].BanCnt = 5;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, true, true, true, true, true, false, false };
            _gridManual.Fields[0, 2].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
