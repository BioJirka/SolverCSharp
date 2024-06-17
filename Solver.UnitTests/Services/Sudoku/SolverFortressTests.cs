using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverFortressTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverFortress _solver = new(
            new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1 },
                new Coordinates { Row = 1, Col = 2 },
                new Coordinates { Row = 2, Col = 1 },
                new Coordinates { Row = 2, Col = 2 }
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
            _gridManual.Fields[0, 1].Priority = _priority;
            _gridManual.Fields[0, 2].Priority = _priority;
            _gridManual.Fields[1, 0].Priority = _priority;
            _gridManual.Fields[1, 1].Priority = 2 * _priority;
            _gridManual.Fields[1, 2].Priority = _priority;
            _gridManual.Fields[2, 0].Priority = _priority;
            _gridManual.Fields[2, 1].Priority = _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_Ban1OnFortressEdgeAndBan9NextToFortress()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, false, false, false, false, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 1;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, false, false, false, false, false, false, false, false };
            _gridManual.Fields[1, 0].BanCnt = 1;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, false, false, false, false, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 1;

            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[0, 2].BanCnt = 1;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[1, 2].BanCnt = 1;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[2, 0].BanCnt = 1;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[2, 1].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_BanFortressEdge_BanHigherOrEqualNumbersNextToThatFieldOutsideFortress()
        {
            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 7, _numRange);
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 3;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, true, true };
            _gridManual.Fields[2, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_BanNextToFortress_BanLowerOrEqualNumbersNextToThatFieldInFortress()
        {
            _solver.Ban(_gridMethod, 3, _rowCnt, 2, _colCnt, 6, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, false, false, false };
            _gridManual.Fields[1, 1].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
