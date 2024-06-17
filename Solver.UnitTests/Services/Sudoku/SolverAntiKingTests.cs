using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverAntiKingTests
    {
        private const int _rowCnt = 9;
        private const int _colCnt = 9;
        private const int _numRange = 9;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverAntiKing _solver = new();

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
        public void Ban_MiddleField_Ban8Fields()
        {
            _solver.Ban(_gridMethod, 6, _rowCnt, 5, _colCnt, 7, _numRange);
            _gridManual.Fields[4, 3].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[4, 3].BanCnt = 1;
            _gridManual.Fields[4, 4].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[4, 4].BanCnt = 1;
            _gridManual.Fields[4, 5].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[4, 5].BanCnt = 1;
            _gridManual.Fields[5, 3].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[5, 3].BanCnt = 1;
            _gridManual.Fields[5, 5].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[5, 5].BanCnt = 1;
            _gridManual.Fields[6, 3].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[6, 3].BanCnt = 1;
            _gridManual.Fields[6, 4].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[6, 4].BanCnt = 1;
            _gridManual.Fields[6, 5].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[6, 5].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FieldInCorner_Ban3Fields()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 7, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[0, 1].BanCnt = 1;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 0].BanCnt = 1;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, false, false };
            _gridManual.Fields[1, 1].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
