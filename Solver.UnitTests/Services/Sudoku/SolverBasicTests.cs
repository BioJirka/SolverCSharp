using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverBasicTests
    {
        private const int _rowCnt = 4;
        private const int _colCnt = 4;
        private const int _numRange = 9;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverBasic _solver = new();

        [TestInitialize]
        public void TestInit()
        {
            _gridMethod = new(_rowCnt, _colCnt, _numRange);
            _gridManual = new(_rowCnt, _colCnt, _numRange);
        }

        [TestMethod]
        public void SetPriorities_Void_DoNothing()
        {
            _solver.SetPriorities(_gridMethod, _rowCnt, _colCnt);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_DoNothing()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_RandomField_BanRowAndColumn()
        {
            _solver.Ban(_gridMethod, 2, _rowCnt, 3, _colCnt, 7, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 0].BanCnt = 1;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 1;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 2].BanCnt = 1;
            _gridManual.Fields[1, 3].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 3].BanCnt = 1;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[0, 2].BanCnt = 1;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[2, 2].BanCnt = 1;
            _gridManual.Fields[3, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[3, 2].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}

