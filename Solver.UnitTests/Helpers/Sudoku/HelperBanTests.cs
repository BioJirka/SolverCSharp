using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;

namespace Solver.UnitTests.Sudoku.Helpers
{
    [TestClass]
    public class HelperBanTests
    {
        private const int _rowCnt = 1;
        private const int _colCnt = 1;
        private const int _numRange = 9;
        private Grid _gridMethod;
        private Grid _gridManual;

        [TestInitialize]
        public void TestInit()
        {
            _gridMethod = new(_rowCnt, _colCnt, _numRange);
            _gridManual = new(_rowCnt, _colCnt, _numRange);
        }

        [TestMethod]
        public void Ban_NumberOutOfRange_DoNothing()
        {
            HelperBan.Ban(_gridMethod, 1, 1, -1, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void Ban_4_Ban4()
        {
            HelperBan.Ban(_gridMethod, 1, 1, 4, _numRange);
            _gridManual.Fields[0, 0].Bans[3] = true;
            _gridManual.Fields[0, 0].BanCnt = 1;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRange_MinNumberGreaterThanMaxNumber_DoNothing()
        {
            HelperBan.BanRange(_gridMethod, 1, 1, 7, 6, _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRange_MinNumberLessThan1_BanFrom1ToMaxNumber()
        {
            HelperBan.BanRange(_gridMethod, 1, 1, -1, 6, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRange_MaxNumberGreaterThanNumRange_BanFromMinNumberToNumRange()
        {
            HelperBan.BanRange(_gridMethod, 1, 1, 3, 11, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 7;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRange_Min3Max6_BanFrom3To6()
        {
            HelperBan.BanRange(_gridMethod, 1, 1, 3, 6, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, true, true, true, true, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 4;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanList_EmptyList_DoNothing()
        {
            HelperBan.BanList(_gridMethod, 1, 1, new List<int>(), _numRange);

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanList_NonEmptyList_BanNumbersInList()
        {
            HelperBan.BanList(_gridMethod, 1, 1, new List<int> { 3, 6, 9 }, _numRange);
            _gridManual.Fields[0, 0].Bans[2] = true;
            _gridManual.Fields[0, 0].Bans[5] = true;
            _gridManual.Fields[0, 0].Bans[8] = true;
            _gridManual.Fields[0, 0].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanList_ListIncludesNumbersOutOfRange_BanNumbersInListIgnoreNumbersOutOfRange()
        {
            HelperBan.BanList(_gridMethod, 1, 1, new List<int> { -1, 3, 6, 9, 12 }, _numRange);
            _gridManual.Fields[0, 0].Bans[2] = true;
            _gridManual.Fields[0, 0].Bans[5] = true;
            _gridManual.Fields[0, 0].Bans[8] = true;
            _gridManual.Fields[0, 0].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanList_ListIncludesDuplicities_BanNumbersInListIgnoreDuplicities()
        {
            HelperBan.BanList(_gridMethod, 1, 1, new List<int> { 3, 6, 9, 3, 6, 3 }, _numRange);
            _gridManual.Fields[0, 0].Bans[2] = true;
            _gridManual.Fields[0, 0].Bans[5] = true;
            _gridManual.Fields[0, 0].Bans[8] = true;
            _gridManual.Fields[0, 0].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInverse_NumberOutOfRange_BanAll()
        {
            HelperBan.BanInverse(_gridMethod, 1, 1, 12, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 9;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanInverse_4_BanAllExcept4()
        {
            HelperBan.BanInverse(_gridMethod, 1, 1, 4, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, false, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 8;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRangeInverse_MinNumberGreaterThanMaxNumber_BanAll()
        {
            HelperBan.BanRangeInverse(_gridMethod, 1, 1, 6, 3, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 9;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRangeInverse_MinNumberLessThan1_BanFromMaxNumberToNumRange()
        {
            HelperBan.BanRangeInverse(_gridMethod, 1, 1, -1, 6, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { false, false, false, false, false, false, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 3;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRangeInverse_MaxNumberGreaterThanNumRange_BanFrom1ToMinNumber()
        {
            HelperBan.BanRangeInverse(_gridMethod, 1, 1, 3, 12, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, false, false, false, false, false, false, false };
            _gridManual.Fields[0, 0].BanCnt = 2;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanRangeInverse_Min3Max6_BanNumbers1279()
        {
            HelperBan.BanRangeInverse(_gridMethod, 1, 1, 3, 6, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, false, false, false, false, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 5;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanListInverse_EmptyList_BanAll()
        {
            HelperBan.BanListInverse(_gridMethod, 1, 1, new List<int>(), _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 9;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanListInverse_NonEmptyList_BanAllExceptNumbersInList()
        {
            HelperBan.BanListInverse(_gridMethod, 1, 1, new List<int> { 3, 6, 9 }, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, false, true, true, false, true, true, false };
            _gridManual.Fields[0, 0].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanListInverse_ListIncludesNumbersOutOfRange_BanAllExceptNumbersInListIgnoreNumbersOutOfRange()
        {
            HelperBan.BanListInverse(_gridMethod, 1, 1, new List<int> { -1, 3, 6, 9, 12 }, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, false, true, true, false, true, true, false };
            _gridManual.Fields[0, 0].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanListInverse_ListIncludesDuplicities_BanAllExceptNumbersInListIgnoreDuplicities()
        {
            HelperBan.BanListInverse(_gridMethod, 1, 1, new List<int> { 3, 6, 9, 3, 6, 3 }, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, false, true, true, false, true, true, false };
            _gridManual.Fields[0, 0].BanCnt = 6;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }

        [TestMethod]
        public void BanAll_Void_BanAllNumbers()
        {
            HelperBan.BanAll(_gridMethod, 1, 1, _numRange);
            _gridManual.Fields[0, 0].Bans = new bool[_numRange] { true, true, true, true, true, true, true, true, true };
            _gridManual.Fields[0, 0].BanCnt = 9;

            Assert.AreEqual(JsonConvert.SerializeObject(_gridManual), JsonConvert.SerializeObject(_gridMethod));
        }
    }
}
