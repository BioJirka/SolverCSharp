using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverEvenOddAreaTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverEvenOddArea _solver = new(
            new List<List<Coordinates>> {
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 1 }, 
                    new Coordinates { Row = 1, Col = 2 }, 
                    new Coordinates { Row = 1, Col = 3 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 2, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, }, 
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
            _gridManual.Fields[0, 0].Priority = _priorityLow;
            _gridManual.Fields[0, 1].Priority = _priorityLow;
            _gridManual.Fields[0, 2].Priority = _priorityLow;
            _gridManual.Fields[1, 1].Priority = _priorityMid;
            _gridManual.Fields[1, 2].Priority = _priorityMid;
            _gridManual.Fields[2, 1].Priority = _priorityMid;
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
        public void Ban_FirstInArea_SetThatParityInRestOfArea()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridManual.Fields[0, 0].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[0, 1].BanCnt = 4;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[0, 2].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInArea_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 6;
            _gridMethod.Fields[0, 1].Number = 4;
            _gridManual.Fields[0, 0].Number = 6;
            _gridManual.Fields[0, 1].Number = 4;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

    }
}
