using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverSumWeightedTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverSumWeighted _solver = new(new List<List<Coordinates>> {
            new List<Coordinates> { 
                new Coordinates { Number = 6894 }, 
                new Coordinates { Row = 1, Col = 1, Number = 1000 }, 
                new Coordinates { Row = 1, Col = 2, Number = 100 }, 
                new Coordinates { Row = 1, Col = 3, Number = 10 }, 
                new Coordinates { Row = 2, Col = 1, Number = 1 }, 
                new Coordinates { Row = 2, Col = 2, Number = 100 }, 
                new Coordinates { Row = 2, Col = 3, Number = 10 }, 
                new Coordinates { Row = 3, Col = 1, Number = 1 }, 
                new Coordinates { Row = 3, Col = 2, Number = 10 }, 
                new Coordinates { Row = 3, Col = 3, Number = 1 }, }, 
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
            SolverSumWeighted solver = new(new List<List<Coordinates>> {
                new List<Coordinates> { 
                    new Coordinates { Number = 6894 }, 
                    new Coordinates { Row = 1, Col = 1, Number = 1000 }, 
                    new Coordinates { Row = 1, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 1, Col = 3, Number = 10 }, 
                    new Coordinates { Row = 2, Col = 1, Number = 1 }, 
                    new Coordinates { Row = 2, Col = 2, Number = 100 }, 
                    new Coordinates { Row = 2, Col = 3, Number = 10 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Number = 25 }, 
                    new Coordinates { Row = 3, Col = 1, Number = 1 }, 
                    new Coordinates { Row = 3, Col = 2, Number = 10 }, 
                    new Coordinates { Row = 3, Col = 3, Number = 1 }, }, 
                }, _rowCnt, _colCnt);
            solver.SetPriorities(_gridMethod, _rowCnt, _colCnt);
            _gridManual.Fields[0, 0].Priority = _priorityLow;
            _gridManual.Fields[0, 1].Priority = _priorityLow;
            _gridManual.Fields[0, 2].Priority = _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityLow;
            _gridManual.Fields[1, 1].Priority = _priorityLow;
            _gridManual.Fields[1, 2].Priority = _priorityLow;
            _gridManual.Fields[2, 0].Priority = _priorityMid;
            _gridManual.Fields[2, 1].Priority = _priorityMid;
            _gridManual.Fields[2, 2].Priority = _priorityMid;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_Set56ToFirstField()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, false, false, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set5ToFirstFields_Set789ToPositions12And22()
        {
            _gridMethod.Fields[0, 0].Number = 5;
            _gridManual.Fields[0, 0].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 5, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 6;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
