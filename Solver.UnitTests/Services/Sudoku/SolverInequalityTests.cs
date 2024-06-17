using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverInequalityTests
    {
        private const int _rowCnt = 9;
        private const int _colCnt = 9;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverInequality _solver = new(new List<Coordinates> {
            new Coordinates { Row = 1, Col = 2, Direction = "S", Text = "Lower" }, 
            new Coordinates { Row = 2, Col = 2, Direction = "S", Text = "Lower" }, 
            new Coordinates { Row = 3, Col = 2, Direction = "S", Text = "Lower" }, 
            new Coordinates { Row = 6, Col = 5, Direction = "E", Text = "Higher" }, 
            new Coordinates { Row = 6, Col = 6, Direction = "E", Text = "Higher" }, 
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
            _gridManual.Fields[0, 1].Priority = 3 * _priority;
            _gridManual.Fields[1, 1].Priority = 3 * _priority;
            _gridManual.Fields[2, 1].Priority = 3 * _priority;
            _gridManual.Fields[3, 1].Priority = 3 * _priority;
            _gridManual.Fields[5, 4].Priority = 2 * _priority;
            _gridManual.Fields[5, 5].Priority = 2 * _priority;
            _gridManual.Fields[5, 6].Priority = 2 * _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_BanHighOrLoweValuesAtChainesEnds()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 3;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, true };
            _gridManual.Fields[1, 1].BanCnt = 3;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { true, false, false, false, false, false, false, true, true };
            _gridManual.Fields[2, 1].BanCnt = 3;
            _gridManual.Fields[3, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, true, true, true };
            _gridManual.Fields[3, 1].BanCnt = 3;
            _gridManual.Fields[5, 4].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[5, 4].BanCnt = 2;
            _gridManual.Fields[5, 5].Bans = new bool[_numRange] { true, false, false, false, false, false, false, false, true };
            _gridManual.Fields[5, 5].BanCnt = 2;
            _gridManual.Fields[5, 6].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[5, 6].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValuesTo3_BanHigherOrLowerValuesInChains()
        {
            _solver.Ban(_gridMethod, 6, _rowCnt, 5, _colCnt, 3, _numRange);
            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 3;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 1].BanCnt = 7;
            _gridManual.Fields[3, 1].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[3, 1].BanCnt = 8;
            _gridManual.Fields[5, 5].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[5, 5].BanCnt = 3;
            _gridManual.Fields[5, 6].Bans = new bool[_numRange] { true, true, true, true, false, false, false, false, false };
            _gridManual.Fields[5, 6].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
