using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverProductResultTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Mid;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverProductResult _solver = new(new List<Coordinates> {
            new Coordinates { Row = 1, Col = 1 }
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
            _gridManual.Fields[0, 0].Priority = _priority;
            _gridManual.Fields[0, 1].Priority = _priority;
            _gridManual.Fields[1, 0].Priority = _priority;
            _gridManual.Fields[1, 1].Priority = _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_Ban1InTopPositionsBan9InBottomLeft()
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans[0] = true;
            _gridManual.Fields[0, 0].BanCnt = 1;
            _gridManual.Fields[0, 1].Bans[0] = true;
            _gridManual.Fields[0, 1].BanCnt = 1;
            _gridManual.Fields[1, 0].Bans[8] = true;
            _gridManual.Fields[1, 0].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopLeft8_Ban1InTopRightBan89InBottomLeftSetBottomRightEven()
        {
            _gridMethod.Fields[0, 0].Number = 8;
            _gridManual.Fields[0, 0].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 8, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, false, false, false, false, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 1;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 2;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, true, false, true, false, true, false, true };
            _gridManual.Fields[1, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopRight3_Ban123InTopLeftSetBottomLeft12()
        {
            _gridMethod.Fields[0, 1].Number = 3;
            _gridManual.Fields[0, 1].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, false, false, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 3;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_BottomLeft5_SetTopPositions6789()
        {
            _gridMethod.Fields[1, 0].Number = 5;
            _gridManual.Fields[1, 0].Number = 5;

            _solver.Ban(_gridMethod, 2, _rowCnt, 1, _colCnt, 5, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 5;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_BottomRight5_SetTopPositionsOdd()
        {
            _gridMethod.Fields[1, 1].Number = 5;
            _gridManual.Fields[1, 1].Number = 5;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 5, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[0, 0].BanCnt = 4;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, true, false, true, false, true, false, true, false };
            _gridManual.Fields[0, 1].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_BottomRight6_Ban5InTopPositions()
        {
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 6, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 1;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { false, false, false, false, true, false, false, false, false };
            _gridManual.Fields[0, 1].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopLeft9TopRight6_SetBottomLeft5SetBottomRight4()
        {
            _gridMethod.Fields[0, 0].Number = 9;
            _gridMethod.Fields[0, 1].Number = 6;
            _gridManual.Fields[0, 0].Number = 9;
            _gridManual.Fields[0, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 9, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopLeft4BottomLeft2_SetTopRight67SetBottomRight48()
        {
            _gridMethod.Fields[0, 0].Number = 4;
            _gridMethod.Fields[1, 0].Number = 2;
            _gridManual.Fields[0, 0].Number = 4;
            _gridManual.Fields[1, 0].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 4, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, false, false, true, true };
            _gridManual.Fields[0, 1].BanCnt = 7;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, false, true, true, true, false, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopLeft8BottomRight6_SetTopRight27SetBottomLeft15()
        {
            _gridMethod.Fields[0, 0].Number = 8;
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[0, 0].Number = 8;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 8, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, false, true, true, true, true, false, true, true };
            _gridManual.Fields[0, 1].BanCnt = 7;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, true, true, false, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopRight4BottomLeft1_SetTopLeft34SetBottomRight26()
        {
            _gridMethod.Fields[0, 1].Number = 4;
            _gridMethod.Fields[1, 0].Number = 1;
            _gridManual.Fields[0, 1].Number = 4;
            _gridManual.Fields[1, 0].Number = 1;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 4, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, false, false, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 7;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, false, true, true, true, false, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopRight9BottomRight6_SetTopLeft4SetBottomLeft3()
        {
            _gridMethod.Fields[0, 1].Number = 9;
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[0, 1].Number = 9;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 9, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, false, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_BottomLeft1BottomRight2_SetTopLeft2346SetTopRight2346()
        {
            _gridMethod.Fields[1, 0].Number = 1;
            _gridMethod.Fields[1, 1].Number = 2;
            _gridManual.Fields[1, 0].Number = 1;
            _gridManual.Fields[1, 1].Number = 2;

            _solver.Ban(_gridMethod, 2, _rowCnt, 1, _colCnt, 1, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, false, false, false, true, false, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 5;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, false, false, false, true, false, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopLeft5TopRight7BottomLeft3_SetBottomRight5()
        {
            _gridMethod.Fields[0, 0].Number = 5;
            _gridMethod.Fields[0, 1].Number = 7;
            _gridMethod.Fields[1, 0].Number = 3;
            _gridManual.Fields[0, 0].Number = 5;
            _gridManual.Fields[0, 1].Number = 7;
            _gridManual.Fields[1, 0].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 5, _numRange);
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopLeft2TopRight9BottomRight8_SetBottomLeft1()
        {
            _gridMethod.Fields[0, 0].Number = 2;
            _gridMethod.Fields[0, 1].Number = 9;
            _gridMethod.Fields[1, 1].Number = 8;
            _gridManual.Fields[0, 0].Number = 2;
            _gridManual.Fields[0, 1].Number = 9;
            _gridManual.Fields[1, 1].Number = 8;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 2, _numRange);
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopLeft7BottomLeft4BottomRight2_SetTopRight6()
        {
            _gridMethod.Fields[0, 0].Number = 7;
            _gridMethod.Fields[1, 0].Number = 4;
            _gridMethod.Fields[1, 1].Number = 2;
            _gridManual.Fields[0, 0].Number = 7;
            _gridManual.Fields[1, 0].Number = 4;
            _gridManual.Fields[1, 1].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 7, _numRange);
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, false, true, true, true };
            _gridManual.Fields[0, 1].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_TopRight8BottomLeft5BottomRight6_SetTopLeft7()
        {
            _gridMethod.Fields[0, 1].Number = 8;
            _gridMethod.Fields[1, 0].Number = 5;
            _gridMethod.Fields[1, 1].Number = 6;
            _gridManual.Fields[0, 1].Number = 8;
            _gridManual.Fields[1, 0].Number = 5;
            _gridManual.Fields[1, 1].Number = 6;

            _solver.Ban(_gridMethod, 1, _rowCnt, 2, _colCnt, 8, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_AllNumbersSet_DoNothing()
        {
            _gridMethod.Fields[0, 0].Number = 3;
            _gridMethod.Fields[0, 1].Number = 4;
            _gridMethod.Fields[1, 0].Number = 1;
            _gridMethod.Fields[1, 1].Number = 2;
            _gridManual.Fields[0, 0].Number = 3;
            _gridManual.Fields[0, 1].Number = 4;
            _gridManual.Fields[1, 0].Number = 1;
            _gridManual.Fields[1, 1].Number = 2;

            _solver.Ban(_gridMethod, 1, _rowCnt, 1, _colCnt, 3, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
