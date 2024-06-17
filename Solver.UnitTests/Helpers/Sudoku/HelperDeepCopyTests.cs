using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;

namespace Solver.UnitTests.Sudoku.Helpers
{
    [TestClass]
    public class HelperDeepCopyTests
    {
        private const int _rowCnt = 3;
        private const int _colCnt = 3;
        private const int _numRange = 3;
        private Grid _grid;

        [TestInitialize]
        public void TestInit()
        {
            _grid = new(_rowCnt, _colCnt, _numRange);
        }

        [TestMethod]
        public void DeepCopy_GridAsArgument_CopyIt()
        {
            Grid gridDeepCopy = HelperDeepCopy.DeepCopy(_grid);

            Assert.AreNotEqual(_grid, gridDeepCopy);
            Assert.AreEqual(JsonConvert.SerializeObject(_grid), JsonConvert.SerializeObject(gridDeepCopy));
        }

        [TestMethod]
        public void CopyGrid_GridWithNoNumbers_CopyEverything()
        {
            _grid.Fields[1, 1].Bans = new bool[_numRange] { true, true, false };
            _grid.Fields[1, 1].BanCnt = 2;
            _grid.Fields[1, 1].Priority = 1_000;

            Grid gridCopy = HelperDeepCopy.CopyGrid(_grid, _rowCnt, _colCnt, _numRange);

            Assert.AreNotEqual(_grid, gridCopy);
            Assert.AreEqual(JsonConvert.SerializeObject(_grid), JsonConvert.SerializeObject(gridCopy));
        }

        [TestMethod]
        public void CopyGrid_GridWithNumbers_ForFieldsWithNumbersCopyOnlyNumber()
        {
            _grid.Fields[1, 1].Bans = new bool[_numRange] { true, true, false };
            _grid.Fields[1, 1].BanCnt = 2;
            _grid.Fields[1, 1].Priority = 1_000;
            _grid.Fields[1, 1].Number = 7;

            Grid gridCopy = HelperDeepCopy.CopyGrid(_grid, _rowCnt, _colCnt, _numRange);

            Assert.AreNotEqual(_grid, gridCopy);
            Assert.AreEqual(_grid.Fields[1, 1].Number, gridCopy.Fields[1, 1].Number);
        }
    }
}
