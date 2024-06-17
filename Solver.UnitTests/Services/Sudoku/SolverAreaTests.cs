using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverAreasTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverArea _solver = new(HelperArea.GetAreasDiagonal(_rowCnt, _colCnt), _rowCnt, _colCnt);

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
            _gridManual.Fields[0, 0].Priority = _priority;
            _gridManual.Fields[0, 2].Priority = _priority;
            _gridManual.Fields[1, 1].Priority = 2* _priority;
            _gridManual.Fields[2, 0].Priority = _priority;
            _gridManual.Fields[2, 2].Priority = _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_DoNothing()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Middle_BanBothDiagonals()
        {
            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 7, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[0, 0].BanCnt = 1;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[0, 2].BanCnt = 1;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 1;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[2, 0].BanCnt = 1;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[2, 2].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Corner_BanOneDiagonal()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 7, _numRange);
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[0, 2].BanCnt = 1;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 1;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[2, 0].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_OutOfDiagonals_DoNothing()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 7, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
