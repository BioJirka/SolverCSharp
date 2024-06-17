using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverShapeTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverShape _solver = new(new List<List<Coordinates>> { 
            new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1 }, 
                new Coordinates { Row = 1, Col = 2 }, }, 
            new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1 }, 
                new Coordinates { Row = 2, Col = 1 }, }, 
            new List<Coordinates> { 
                new Coordinates { Row = 3, Col = 1 }, 
                new Coordinates { Row = 3, Col = 2 }, 
                new Coordinates { Row = 3, Col = 3 }, }, 
            new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 3 }, 
                new Coordinates { Row = 2, Col = 3 }, 
                new Coordinates { Row = 3, Col = 3 }, }, }, _rowCnt, _colCnt);

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
            _gridManual.Fields[0, 1].Priority = _priorityMid;
            _gridManual.Fields[0, 2].Priority = _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityMid;
            _gridManual.Fields[1, 2].Priority = _priorityLow;
            _gridManual.Fields[2, 0].Priority = _priorityLow;
            _gridManual.Fields[2, 1].Priority = _priorityLow;
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
        public void Ban_NotAllPositionsCovered_DoNothing()
        {
            _gridManual.Fields[0, 2].Number = 1;
            _gridManual.Fields[1, 2].Number = 2;
            _gridManual.Fields[2, 0].Number = 1;
            _gridManual.Fields[2, 1].Number = 2;
            _gridMethod.Fields[0, 2].Number = 1;
            _gridMethod.Fields[1, 2].Number = 2;
            _gridMethod.Fields[2, 0].Number = 1;
            _gridMethod.Fields[2, 1].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_AllPositionsCoveredNow_BanOtherNumbers()
        {
            _gridManual.Fields[0, 2].Number = 1;
            _gridManual.Fields[1, 2].Number = 2;
            _gridManual.Fields[2, 2].Number = 3;
            _gridMethod.Fields[0, 2].Number = 1;
            _gridMethod.Fields[1, 2].Number = 2;
            _gridMethod.Fields[2, 2].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 1, _numRange);
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 0].BanCnt = 7;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_AllPositionsAlreadyCovered_DoNothing()
        {
            _gridManual.Fields[0, 2].Number = 1;
            _gridManual.Fields[1, 2].Number = 2;
            _gridManual.Fields[2, 0].Number = 1;
            _gridManual.Fields[2, 1].Number = 3;
            _gridMethod.Fields[0, 2].Number = 1;
            _gridMethod.Fields[1, 2].Number = 2;
            _gridMethod.Fields[2, 0].Number = 1;
            _gridMethod.Fields[2, 1].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
