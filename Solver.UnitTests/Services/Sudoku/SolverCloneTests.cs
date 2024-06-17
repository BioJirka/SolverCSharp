using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverCloneTests
    {
        private const int _rowCnt = 4;
        private const int _colCnt = 4;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Mid;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverClone _solver = new(new List<List<List<Coordinates>>> { 
            new List<List<Coordinates>> { 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 1 }, 
                    new Coordinates { Row = 1, Col = 2 }, 
                    new Coordinates { Row = 2, Col = 1 }, 
                    new Coordinates { Row = 2, Col = 2 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 1, Col = 3 }, 
                    new Coordinates { Row = 1, Col = 4 }, 
                    new Coordinates { Row = 2, Col = 3 }, 
                    new Coordinates { Row = 2, Col = 4 }, }, 
                new List<Coordinates> { 
                    new Coordinates { Row = 3, Col = 1 }, 
                    new Coordinates { Row = 3, Col = 2 }, 
                    new Coordinates { Row = 4, Col = 1 }, 
                    new Coordinates { Row = 4, Col = 2 }, }, } 
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
            _gridManual.Fields[0, 1].Priority = 2 * _priority;
            _gridManual.Fields[0, 2].Priority = 2 * _priority;
            _gridManual.Fields[0, 3].Priority = 2 * _priority;
            _gridManual.Fields[1, 0].Priority = 2 * _priority;
            _gridManual.Fields[1, 1].Priority = 2 * _priority;
            _gridManual.Fields[1, 2].Priority = 2 * _priority;
            _gridManual.Fields[1, 3].Priority = 2 * _priority;
            _gridManual.Fields[2, 0].Priority = 2 * _priority;
            _gridManual.Fields[2, 1].Priority = 2 * _priority;
            _gridManual.Fields[3, 0].Priority = 2 * _priority;
            _gridManual.Fields[3, 1].Priority = 2 * _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_DoNothing()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstInPosition_SetOtherPositions()
        {
            _gridMethod.Fields[0, 0].Number = 5;
            _gridManual.Fields[0, 0].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 5, _numRange);
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 8;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[2, 0].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondInPosition_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 5;
            _gridMethod.Fields[0, 2].Number = 5;
            _gridManual.Fields[0, 0].Number = 5;
            _gridManual.Fields[0, 2].Number = 5;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 5, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
