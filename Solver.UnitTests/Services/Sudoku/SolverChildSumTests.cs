using Newtonsoft.Json;
using Solver.Sudoku.Models;
using Solver.Sudoku.Services;

namespace Solver.UnitTests.Sudoku.Services
{
    [TestClass]
    public class SolverChildSumTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 9;
        private const int _priority = (int)Priority.Mid;
        private Grid _gridMethod;
        private Grid _gridManual;
        private SolverChildSum _solver = new(new List<List<Coordinates>> {
            new List<Coordinates> { 
                new Coordinates { Number = 9, Text = "Max sum" }, 
                new Coordinates { Number = 1, Text = "Sum" }, 
                new Coordinates { Number = 9, Text = "Sum" }, 
                new Coordinates { Number = 3, Text = "Sum" }, 
                new Coordinates { Number = 7, Text = "Sum" }, 
                new Coordinates { Number = 6, Text = "Sum" }, 
                new Coordinates { Number = 9, Text = "Sum" }, 
                new Coordinates { Number = 8, Text = "Sum" }, 
                new Coordinates { Number = 2, Text = "Sum" }, 
                new Coordinates { Row = 1, Col = 1, Text = "Coordinates" }, 
                new Coordinates { Row = 1, Col = 2, Text = "Coordinates" }, 
                new Coordinates { Row = 1, Col = 3, Text = "Coordinates" }, 
                new Coordinates { Row = 2, Col = 1, Text = "Coordinates" }, 
                new Coordinates { Row = 2, Col = 2, Text = "Coordinates" }, 
                new Coordinates { Row = 2, Col = 3, Text = "Coordinates" }, 
                new Coordinates { Row = 3, Col = 1, Text = "Coordinates" }, 
                new Coordinates { Row = 3, Col = 2, Text = "Coordinates" }, 
                new Coordinates { Row = 3, Col = 3, Text = "Coordinates" }, }, 
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
            SolverChildSum solver = new(
                new List<List<Coordinates>> { 
                    new List<Coordinates> { 
                        new Coordinates { Number = 9, Text = "Max sum" }, 
                        new Coordinates { Number = 9, Text = "Sum" }, 
                        new Coordinates { Row = 1, Col = 1, Text = "Coordinates" }, }}
                , _rowCnt, _colCnt, _numRange);
            solver.SetPriorities(_gridMethod, _rowCnt, _colCnt);
            _gridManual.Fields[0, 0].Priority = _priority;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInitial_Void_SetNumbersAsInDescription()
            // (1, 1): 1
            // (1, 2): 9
            // (1, 3): 1-3
            // (2, 1): 1-2, 7
            // (2, 2): 3-7
            // (2, 3): 1-9
            // (3, 1): 1-7, 9
            // (3, 2): 1-8
            // (3, 3): 2
        {
            _solver.BanInitial(_gridMethod, _rowCnt, _colCnt, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[0, 1].BanCnt = 8;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, false, true, true, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 6;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 6;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, false, false, false, false, false, true, true };
            _gridManual.Fields[1, 1].BanCnt = 4;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 0;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, false };
            _gridManual.Fields[2, 0].BanCnt = 1;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[2, 1].BanCnt = 1;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set3InPosition13_SetNumbersAsInDescription()
            // (1, 1): 1
            // (1, 2): 9
            // (1, 3): 
            // (2, 1): 7
            // (2, 2): 3-6
            // (2, 3): 1-9
            // (3, 1): 1-7, 9
            // (3, 2): 1-8
            // (3, 3): 2
        {
            _gridMethod.Fields[0, 2].Number = 3;
            _gridManual.Fields[0, 2].Number = 3;

            _solver.Ban(_gridMethod, 1, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[0, 1].BanCnt = 8;
            // _gridManual.Fields[0, 2].Bans = new bool[_numRange] { false, false, false, true, true, true, true, true, true };
            // _gridManual.Fields[0, 2].BanCnt = 6;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, false, false, false, false, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 5;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, false };
            _gridManual.Fields[1, 2].BanCnt = 0;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, false };
            _gridManual.Fields[2, 0].BanCnt = 1;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, true };
            _gridManual.Fields[2, 1].BanCnt = 1;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set3InPosition22_SetNumbersAsInDescription()
            // (1, 1): 1
            // (1, 2): 9
            // (1, 3): 3
            // (2, 1): 7
            // (2, 2): 
            // (2, 3): 3
            // (3, 1): 9
            // (3, 2): 8
            // (3, 3): 2
        {
            _gridMethod.Fields[1, 1].Number = 3;
            _gridManual.Fields[1, 1].Number = 3;

            _solver.Ban(_gridMethod, 2, _rowCnt, 2, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[0, 1].BanCnt = 8;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 8;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;
            // _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, false, false, false, false, false, true, true };
            // _gridManual.Fields[1, 1].BanCnt = 4;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[1, 2].BanCnt = 8;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[2, 0].BanCnt = 8;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, false, true };
            _gridManual.Fields[2, 1].BanCnt = 8;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set3InPosition23_SetNumbersAsInDescription()
            // (1, 1): 1
            // (1, 2): 9
            // (1, 3): 3
            // (2, 1): 7
            // (2, 2): 3
            // (2, 3): 
            // (3, 1): 9
            // (3, 2): 8
            // (3, 3): 2
        {
            _gridMethod.Fields[1, 2].Number = 3;
            _gridManual.Fields[1, 2].Number = 3;

            _solver.Ban(_gridMethod, 2, _rowCnt, 3, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[0, 1].BanCnt = 8;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 8;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;
            // _gridManual.Fields[1, 2].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, false };
            // _gridManual.Fields[1, 2].BanCnt = 0;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[2, 0].BanCnt = 8;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, false, true };
            _gridManual.Fields[2, 1].BanCnt = 8;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set3InPosition31_SetNumbersAsInDescription()
            // (1, 1): 1
            // (1, 2): 9
            // (1, 3): 3
            // (2, 1): 7
            // (2, 2): 6
            // (2, 3): 6, 9
            // (3, 1): 
            // (3, 2): 5, 8
            // (3, 3): 2
        {
            _gridMethod.Fields[2, 0].Number = 3;
            _gridManual.Fields[2, 0].Number = 3;

            _solver.Ban(_gridMethod, 3, _rowCnt, 1, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[0, 1].BanCnt = 8;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 8;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, true, true, false, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, true, true, true, false, true, true, false };
            _gridManual.Fields[1, 2].BanCnt = 7;
            // _gridManual.Fields[2, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, false, true, false };
            // _gridManual.Fields[2, 0].BanCnt = 1;
            _gridManual.Fields[2, 1].Bans = new bool[_numRange] { true, true, true, true, false, true, true, false, true };
            _gridManual.Fields[2, 1].BanCnt = 7;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_Set3InPosition32_SetNumbersAsInDescription()
            // (1, 1): 1
            // (1, 2): 9
            // (1, 3): 3
            // (2, 1): 7
            // (2, 2): 6
            // (2, 3): 9
            // (3, 1): 5
            // (3, 2): 
            // (3, 3): 2
        {
            _gridMethod.Fields[2, 1].Number = 3;
            _gridManual.Fields[2, 1].Number = 3;

            _solver.Ban(_gridMethod, 3, _rowCnt, 2, _colCnt, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;
            _gridManual.Fields[0, 1].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[0, 1].BanCnt = 8;
            _gridManual.Fields[0, 2].Bans = new bool[_numRange] { true, true, false, true, true, true, true, true, true };
            _gridManual.Fields[0, 2].BanCnt = 8;
            _gridManual.Fields[1, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, true, true };
            _gridManual.Fields[1, 0].BanCnt = 8;
            _gridManual.Fields[1, 1].Bans = new bool[_numRange] { true, true, true, true, true, false, true, true, true };
            _gridManual.Fields[1, 1].BanCnt = 8;
            _gridManual.Fields[1, 2].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, false };
            _gridManual.Fields[1, 2].BanCnt = 8;
            _gridManual.Fields[2, 0].Bans = new bool[_numRange] { true, true, true, true, false, true, true, true, true };
            _gridManual.Fields[2, 0].BanCnt = 8;
            // _gridManual.Fields[2, 1].Bans = new bool[_numRange] { false, false, false, false, false, false, false, false, true };
            // _gridManual.Fields[2, 1].BanCnt = 1;
            _gridManual.Fields[2, 2].Bans = new bool[_numRange] { true, false, true, true, true, true, true, true, true };
            _gridManual.Fields[2, 2].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
