using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverSumArrowTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverSumArrow _solver = new(
            new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 1 }, 
                    new Coordinates { Row = 1, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 1 }, 
                    new Coordinates { Row = 2, Col = 2 }, 
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
            _gridManual.Fields[0, 0].Priority = _priorityMid;
            _gridManual.Fields[0, 1].Priority = _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityLow;
            _gridManual.Fields[1, 1].Priority = _priorityLow;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_Ban12InTargetFieldBan89InRestOfArrow()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 2;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[0, 1].BanCnt = 2;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 2;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[1, 1].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TargetSetTo7OtherNumber2_Set1234ToRestInArrow()
        {
            _gridMethod.Fields[0, 0].Number = 7;
            _gridMethod.Fields[1, 0].Number = 2;
            _gridManual.Fields[0, 0].Number = 7;
            _gridManual.Fields[1, 0].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 7, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 5;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TargetNotSetOtherNumber3_SetTargetTo56789Ban6789InRestOfArrow()
        {
            _gridMethod.Fields[1, 0].Number = 3;
            _gridManual.Fields[1, 0].Number = 3;

            _solver.Ban(_gridMethod, 2, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, false, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 4;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 4;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_AllSetExceptForTarget_SetTarget()
        {
            _gridMethod.Fields[0, 1].Number = 4;
            _gridMethod.Fields[1, 0].Number = 2;
            _gridMethod.Fields[1, 1].Number = 1;
            _gridManual.Fields[0, 1].Number = 4;
            _gridManual.Fields[1, 0].Number = 2;
            _gridManual.Fields[1, 1].Number = 1;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 1, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
