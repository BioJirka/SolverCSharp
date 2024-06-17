using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverKillerTests
    {
        private const int _rowCnt = 9;
        private const int _colCnt = 9;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverKiller _solver = new(new List<List<Coordinates>> {
            new List<Coordinates> { 
                new Coordinates { Row = 5, Col = 5, Number = 7 } }, 
            new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1, Number = 15 }, 
                new Coordinates { Row = 1, Col = 2 }, 
                new Coordinates { Row = 1, Col = 3 } }, 
            new List<Coordinates> { 
                new Coordinates { Row = 9, Col = 1, Number = 30 }, 
                new Coordinates { Row = 9, Col = 2 }, 
                new Coordinates { Row = 9, Col = 3 }, 
                new Coordinates { Row = 9, Col = 4 }, 
                new Coordinates { Row = 9, Col = 5 }, 
                new Coordinates { Row = 9, Col = 6 } }
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
            _gridManual.Fields[4, 4].Priority = _priorityMid;
            _gridManual.Fields[0, 0].Priority = _priorityMid;
            _gridManual.Fields[0, 1].Priority = _priorityMid;
            _gridManual.Fields[0, 2].Priority = _priorityMid;
            _gridManual.Fields[8, 0].Priority = _priorityLow;
            _gridManual.Fields[8, 1].Priority = _priorityLow;
            _gridManual.Fields[8, 2].Priority = _priorityLow;
            _gridManual.Fields[8, 3].Priority = _priorityLow;
            _gridManual.Fields[8, 4].Priority = _priorityLow;
            _gridManual.Fields[8, 5].Priority = _priorityLow;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_SetNumberInFirstArea()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[4, 4].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[4, 4].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetLowNumbers_BanLowNumbers()
        {
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[8, 1].Number = 1;
            _gridMethod.Fields[8, 3].Number = 2;
            _gridMethod.Fields[8, 5].Number = 3;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[8, 1].Number = 1;
            _gridManual.Fields[8, 3].Number = 2;
            _gridManual.Fields[8, 5].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 1, _numRange);
            _solver.Ban(_gridMethod, 9, _rowCnt, 6, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, false, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 4;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, true, true, false, false, false, false, false };
            _gridManual.Fields[0, 2].BanCnt = 4;
            _gridManual.Fields[8, 0].Bans = new bool[_numRange] { true, true, true, true, true, false, false, false, false };
            _gridManual.Fields[8, 0].BanCnt = 5;
            _gridManual.Fields[8, 2].Bans = new bool[_numRange] { true, true, true, true, true, false, false, false, false };
            _gridManual.Fields[8, 2].BanCnt = 5;
            _gridManual.Fields[8, 4].Bans = new bool[_numRange] { true, true, true, true, true, false, false, false, false };
            _gridManual.Fields[8, 4].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetHighNumbers_BanHighNumbers()
        {
            _gridMethod.Fields[0, 1].Number = 9;
            _gridMethod.Fields[8, 1].Number = 7;
            _gridMethod.Fields[8, 3].Number = 8;
            _gridMethod.Fields[8, 5].Number = 9;
            _gridManual.Fields[0, 1].Number = 9;
            _gridManual.Fields[8, 1].Number = 7;
            _gridManual.Fields[8, 3].Number = 8;
            _gridManual.Fields[8, 5].Number = 9;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 9, _numRange);
            _solver.Ban(_gridMethod, 9, _rowCnt, 6, _colCnt, 9, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 4;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 4;
            _gridManual.Fields[8, 0].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true };
            _gridManual.Fields[8, 0].BanCnt = 5;
            _gridManual.Fields[8, 2].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true };
            _gridManual.Fields[8, 2].BanCnt = 5;
            _gridManual.Fields[8, 4].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true };
            _gridManual.Fields[8, 4].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
