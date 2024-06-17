using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverThermometerTests
    {
        private const int _rowCnt = 9;
        private const int _colCnt = 9;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverThermometer _solver = new(new List<List<Coordinates>> {
            new List<Coordinates> { 
                new Coordinates { Row = 6, Col = 5 }, 
                new Coordinates { Row = 6, Col = 6 }, 
                new Coordinates { Row = 7, Col = 6 }, 
                new Coordinates { Row = 7, Col = 7 }
            } }, _rowCnt, _colCnt);

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
            _gridManual.Fields[5, 4].Priority = 3 * _priority;
            _gridManual.Fields[5, 5].Priority = 3 * _priority;
            _gridManual.Fields[6, 5].Priority = 3 * _priority;
            _gridManual.Fields[6, 6].Priority = 3 * _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_BanHighOrLoweValuesAtThermometerEnds()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[5, 4].Bans = new bool[_numRange] { false, false, false, false, false, false, true, true, true };
            _gridManual.Fields[5, 4].BanCnt = 3;
            _gridManual.Fields[5, 5].Bans = new bool[_numRange] { true, false, false, false, false, false, false, true, true };
            _gridManual.Fields[5, 5].BanCnt = 3;
            _gridManual.Fields[6, 5].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, true };
            _gridManual.Fields[6, 5].BanCnt = 3;
            _gridManual.Fields[6, 6].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[6, 6].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SetValueTo3_BanHigherOrLowerValuesInChains()
        {
            _solver.Ban(_gridMethod, 6, _rowCnt, 6, _colCnt, 3, _numRange);
            _gridManual.Fields[5, 4].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[5, 4].BanCnt = 7;
            _gridManual.Fields[6, 5].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[6, 5].BanCnt = 3;
            _gridManual.Fields[6, 6].Bans = new bool[_numRange] { true, true, true, true, false, false, false, false, false };
            _gridManual.Fields[6, 6].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
