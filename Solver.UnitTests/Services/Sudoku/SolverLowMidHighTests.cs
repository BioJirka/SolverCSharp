using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverLowMidHighTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverLowMidHigh _solver = new(
            new List<Coordinates> {
                new Coordinates { Row = 1, Col = 1, Text = "Low" }, 
                new Coordinates { Row = 2, Col = 2, Text = "Mid" },
                new Coordinates { Row = 3, Col = 3, Text = "High" } }
        );

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
        public void BanInitial_Void_BanLowMidHighFields()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 6;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, false, false, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 6;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, true, true, true, true, true, false, false, false };
            _gridManual.Fields[2, 2].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_NumberInLowArea_DoNothing()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_NumberInMidArea_DoNothing()
        {
            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_NumberInHighArea_DoNothing()
        {
            _solver.Ban(_gridMethod, 3, _rowCnt, 3, _colCnt, 9, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_OutOfAreas_DoNothing()
        {
            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
