using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverDifferenceTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Mid;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverDifference _solver = new(new List<Coordinates> {
            new Coordinates { Row = 1, Col = 1, Direction = "S", Number = 2 }, 
            new Coordinates { Row = 1, Col = 1, Direction = "E", Number = 6 }
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
            _gridManual.Fields[0, 0].Priority = 2 * _priority;
            _gridManual.Fields[0, 1].Priority = _priority;
            _gridManual.Fields[1, 0].Priority = _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_Ban456NextToDiff6()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, true, true, true, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 3;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, true, true, true, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValue3InDiffCorner_SetValueNextToDiff2To1Or5AndSetValueNextToDiff6To9()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[0, 1].BanCnt = 8;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, true, true, false, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValueInNonCorner_DoNothing()
        {
            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 3, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
