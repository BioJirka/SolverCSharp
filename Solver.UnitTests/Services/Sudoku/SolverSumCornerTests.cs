using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverSumCornerTests
    {
        private const int _rowCnt = 9;
        private const int _colCnt = 9;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Low;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverSumCorner _solver = new(new List<Coordinates> {
            new Coordinates { Row = 1, Col = 1, Number = 10 }, 
            new Coordinates { Row = 4, Col = 4, Number = 20 }, 
            new Coordinates { Row = 7, Col = 7, Number = 30 } 
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
            _gridManual.Fields[0, 0].Priority = _priority;
            _gridManual.Fields[0, 1].Priority = _priority;
            _gridManual.Fields[1, 0].Priority = _priority;
            _gridManual.Fields[1, 1].Priority = _priority;
            _gridManual.Fields[3, 3].Priority = _priority;
            _gridManual.Fields[3, 4].Priority = _priority;
            _gridManual.Fields[4, 3].Priority = _priority;
            _gridManual.Fields[4, 4].Priority = _priority;
            _gridManual.Fields[6, 6].Priority = _priority;
            _gridManual.Fields[6, 7].Priority = _priority;
            _gridManual.Fields[7, 6].Priority = _priority;
            _gridManual.Fields[7, 7].Priority = _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_BanNumbersIfSumLowOrHigh()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[0, 0].BanCnt = 2;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[0, 1].BanCnt = 2;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 2;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[1, 1].BanCnt = 2;
            _gridManual.Fields[6, 6].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[6, 6].BanCnt = 2;
            _gridManual.Fields[6, 7].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[6, 7].BanCnt = 2;
            _gridManual.Fields[7, 6].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[7, 6].BanCnt = 2;
            _gridManual.Fields[7, 7].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[7, 7].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FirstNumber_BanNumbersInCycleIfRemainingSumLowOrHigh()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridMethod.Fields[3, 3].Number = 5;
            _gridMethod.Fields[6, 6].Number = 8;
            _gridManual.Fields[0, 0].Number = 3;
            _gridManual.Fields[3, 3].Number = 5;
            _gridManual.Fields[6, 6].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);
            _solver.Ban(_gridMethod, 4, _rowCnt, 4, _colCnt, 5, _numRange);
            _solver.Ban(_gridMethod, 7, _rowCnt, 7, _colCnt, 8, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 4;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true  };
            _gridManual.Fields[1, 0].BanCnt = 4;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, true, true, true, true  };
            _gridManual.Fields[1, 1].BanCnt = 4;
            _gridManual.Fields[6, 7].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[6, 7].BanCnt = 3;
            _gridManual.Fields[7, 6].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[7, 6].BanCnt = 3;
            _gridManual.Fields[7, 7].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[7, 7].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_SecondNumber_BanNumbersInCycleIfRemainingSumLowOrHigh()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[0, 1].Number = 1;
            _gridMethod.Fields[3, 3].Number = 5;
            _gridMethod.Fields[3, 4].Number = 5;
            _gridMethod.Fields[6, 6].Number = 9;
            _gridMethod.Fields[6, 7].Number = 9;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 1;
            _gridManual.Fields[3, 3].Number = 5;
            _gridManual.Fields[3, 4].Number = 5;
            _gridManual.Fields[6, 6].Number = 9;
            _gridManual.Fields[6, 7].Number = 9;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 1, _numRange);
            _solver.Ban(_gridMethod, 4, _rowCnt, 4, _colCnt, 5, _numRange);
            _solver.Ban(_gridMethod, 7, _rowCnt, 7, _colCnt, 9, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true  };
            _gridManual.Fields[1, 0].BanCnt = 2;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true  };
            _gridManual.Fields[1, 1].BanCnt = 2;
            _gridManual.Fields[7, 6].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[7, 6].BanCnt = 2;
            _gridManual.Fields[7, 7].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[7, 7].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_ThirdNumber_SetLastNumber()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[1, 0].Number = 3;
            _gridMethod.Fields[3, 3].Number = 4;
            _gridMethod.Fields[3, 4].Number = 5;
            _gridMethod.Fields[4, 3].Number = 6;
            _gridMethod.Fields[6, 6].Number = 7;
            _gridMethod.Fields[6, 7].Number = 8;
            _gridMethod.Fields[7, 6].Number = 9;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[1, 0].Number = 3;
            _gridManual.Fields[3, 3].Number = 4;
            _gridManual.Fields[3, 4].Number = 5;
            _gridManual.Fields[4, 3].Number = 6;
            _gridManual.Fields[6, 6].Number = 7;
            _gridManual.Fields[6, 7].Number = 8;
            _gridManual.Fields[7, 6].Number = 9;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 1, _numRange);
            _solver.Ban(_gridMethod, 4, _rowCnt, 4, _colCnt, 4, _numRange);
            _solver.Ban(_gridMethod, 7, _rowCnt, 7, _colCnt, 7, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, true, true, true, true, true  };
            _gridManual.Fields[1, 1].BanCnt = 8;
            _gridManual.Fields[4, 4].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[4, 4].BanCnt = 8;
            _gridManual.Fields[7, 7].Bans = new bool[_numRange] { true, true, true, true, true, false, true, true, true };
            _gridManual.Fields[7, 7].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_FourthNumber_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 1;
            _gridMethod.Fields[0, 1].Number = 2;
            _gridMethod.Fields[1, 0].Number = 3;
            _gridMethod.Fields[1, 1].Number = 4;
            _gridManual.Fields[0, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 2;
            _gridManual.Fields[1, 0].Number = 3;
            _gridManual.Fields[1, 1].Number = 4;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
