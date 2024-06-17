using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverAntiAreasTests
    {
        private const int _rowCnt = 9;
        private const int _colCnt = 9;
        private const int _numRange = 9;
        private const int _priorityMid = (int)Priority.Mid;
        private const int _priorityLow = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverAntiArea _solver = new(HelperArea.GetAreasAntiWindoku(_rowCnt, _colCnt), _rowCnt, _colCnt);

        [TestInitialize]
        public void TestInit()
        {
            _gridMethod = new(_rowCnt, _colCnt, _numRange);
            _gridManual = new(_rowCnt, _colCnt, _numRange);
        }

        [TestMethod]
        public void SetPriorities_Void_SetPriorities()
        {
            SolverAntiArea solver = new(new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 1, Number = 3 }, 
                    new Coordinates { Row = 1, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 1 }, 
                    new Coordinates { Row = 2, Col = 2 }, }, 
                new List<Coordinates> {
                    new Coordinates { Row = 2, Col = 2, Number = 2 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 3, Col = 3 }, }, 
                }, _rowCnt, _colCnt);
            solver.SetPriorities(_gridMethod, _rowCnt, _colCnt);
            _gridManual.Fields[0, 0].Priority = _priorityLow;
            _gridManual.Fields[0, 1].Priority = _priorityLow;
            _gridManual.Fields[1, 0].Priority = _priorityLow;
            _gridManual.Fields[1, 1].Priority = _priorityLow + _priorityMid;
            _gridManual.Fields[1, 2].Priority = _priorityMid;
            _gridManual.Fields[2, 1].Priority = _priorityMid;
            _gridManual.Fields[2, 2].Priority = _priorityMid;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_DoNothing()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_LessDistinctNumbersThanMax_DoNothing()
        {
            _gridMethod.Fields[1, 1].Number = 4;
            _gridMethod.Fields[1, 2].Number = 4;
            _gridMethod.Fields[1, 3].Number = 4;
            _gridMethod.Fields[2, 1].Number = 5;
            _gridMethod.Fields[2, 2].Number = 5;
            _gridMethod.Fields[2, 3].Number = 6;
            _gridManual.Fields[1, 1].Number = 4;
            _gridManual.Fields[1, 2].Number = 4;
            _gridManual.Fields[1, 3].Number = 4;
            _gridManual.Fields[2, 1].Number = 5;
            _gridManual.Fields[2, 2].Number = 5;
            _gridManual.Fields[2, 3].Number = 6;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 4, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_LastDistinctNumberAdded_BanOtherNumbersThanAlreadyExistInAreaInEmptyFields()
        {
            _gridMethod.Fields[1, 1].Number = 7;
            _gridMethod.Fields[1, 2].Number = 6;
            _gridMethod.Fields[1, 3].Number = 5;
            _gridMethod.Fields[2, 1].Number = 5;
            _gridMethod.Fields[2, 2].Number = 4;
            _gridMethod.Fields[2, 3].Number = 4;
            _gridMethod.Fields[3, 1].Number = 4;
            _gridManual.Fields[1, 1].Number = 7;
            _gridManual.Fields[1, 2].Number = 6;
            _gridManual.Fields[1, 3].Number = 5;
            _gridManual.Fields[2, 1].Number = 5;
            _gridManual.Fields[2, 2].Number = 4;
            _gridManual.Fields[2, 3].Number = 4;
            _gridManual.Fields[3, 1].Number = 4;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 7, _numRange);
            _gridManual.Fields[3, 2].Bans = new bool[_numRange] { true, true, true, false, false, false, false, true, true };
            _gridManual.Fields[3, 2].BanCnt = 5;
            _gridManual.Fields[3, 3].Bans = new bool[_numRange] { true, true, true, false, false, false, false, true, true };
            _gridManual.Fields[3, 3].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_AdditionalNumberWhenMaxAlreadyReached_DoNothing()
        {
            _gridMethod.Fields[1, 1].Number = 4;
            _gridMethod.Fields[1, 2].Number = 4;
            _gridMethod.Fields[1, 3].Number = 5;
            _gridMethod.Fields[2, 1].Number = 6;
            _gridMethod.Fields[2, 2].Number = 7;
            _gridManual.Fields[1, 1].Number = 4;
            _gridManual.Fields[1, 2].Number = 4;
            _gridManual.Fields[1, 3].Number = 5;
            _gridManual.Fields[2, 1].Number = 6;
            _gridManual.Fields[2, 2].Number = 7;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 4, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
