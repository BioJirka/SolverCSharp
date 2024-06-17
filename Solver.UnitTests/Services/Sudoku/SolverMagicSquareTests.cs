using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverMagicSquareTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.High;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverMagicSquare _solver = new(
            new List<Coordinates> { 
                new Coordinates { Row = 1, Col = 1 }, } 
            , _rowCnt, _colCnt);

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
            _gridManual.Fields[0, 1].Priority = _priority;
            _gridManual.Fields[0, 2].Priority = _priority;
            _gridManual.Fields[1, 0].Priority = _priority;
            _gridManual.Fields[1, 1].Priority = _priority;
            _gridManual.Fields[1, 2].Priority = _priority;
            _gridManual.Fields[2, 0].Priority = _priority;
            _gridManual.Fields[2, 1].Priority = _priority;
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
        public void Ban_NoStraightLineYet_DoNothing()
        {
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[0, 2].Number = 2;
            _gridMethod.Fields[1, 0].Number = 3;
            _gridMethod.Fields[1, 2].Number = 4;
            _gridMethod.Fields[2, 0].Number = 5;
            _gridMethod.Fields[2, 1].Number = 6;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[0, 2].Number = 2;
            _gridManual.Fields[1, 0].Number = 3;
            _gridManual.Fields[1, 2].Number = 4;
            _gridManual.Fields[2, 0].Number = 5;
            _gridManual.Fields[2, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_StraightLineExists_BanOtherFields()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[1, 1].Number = 5;
            _gridMethod.Fields[2, 2].Number = 9;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[1, 1].Number = 5;
            _gridManual.Fields[2, 2].Number = 9;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 5, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, false, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 4;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 8;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, false, false, false, false, false };
            _gridManual.Fields[1, 0].BanCnt = 4;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 4;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[2, 0].BanCnt = 8;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true };
            _gridManual.Fields[2, 1].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
