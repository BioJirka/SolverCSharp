using Newtonsoft.Json;
using Solver.Sudoku.Helpers;
using Solver.Sudoku.Models;

namespace Solver.UnitTests.Sudoku.Helpers
{
    [TestClass]
    public class HelperAreaTests
    {
        [TestMethod]
        public void GetAreasBasic_Grid9x9_ReturnBasicAreas()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasBasic(9, 9);
            List<List<Coordinates>> areasManual = new()
            {
                new List<Coordinates>
                {
                    new Coordinates { Row = 1, Col = 4 },
                    new Coordinates { Row = 1, Col = 5 },
                    new Coordinates { Row = 1, Col = 6 },
                    new Coordinates { Row = 2, Col = 4 },
                    new Coordinates { Row = 2, Col = 5 },
                    new Coordinates { Row = 2, Col = 6 },
                    new Coordinates { Row = 3, Col = 4 },
                    new Coordinates { Row = 3, Col = 5 },
                    new Coordinates { Row = 3, Col = 6 }
                },
                new List<Coordinates>
                {
                    new Coordinates { Row = 4, Col = 7 },
                    new Coordinates { Row = 4, Col = 8 },
                    new Coordinates { Row = 4, Col = 9 },
                    new Coordinates { Row = 5, Col = 7 },
                    new Coordinates { Row = 5, Col = 8 },
                    new Coordinates { Row = 5, Col = 9 },
                    new Coordinates { Row = 6, Col = 7 },
                    new Coordinates { Row = 6, Col = 8 },
                    new Coordinates { Row = 6, Col = 9 }
                }
            };

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual[0]), JsonConvert.SerializeObject(areasMethod[1]));
            Assert.AreEqual(JsonConvert.SerializeObject(areasManual[1]), JsonConvert.SerializeObject(areasMethod[5]));
        }

        [TestMethod]
        public void GetAreaBasic_GridNot9x9_ReturnEmptyAreas()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasBasic(9, 8);
            List<List<Coordinates>> areasManual = new();

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual), JsonConvert.SerializeObject(areasMethod));
        }



        [TestMethod]
        public void GetAreasDiagonal_GridIsSquare_ReturnAreas()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasDiagonal(7, 7);
            List<List<Coordinates>> areasManual = new()
            {
                new List<Coordinates>
                {
                    new Coordinates { Row = 1, Col = 1 },
                    new Coordinates { Row = 2, Col = 2 },
                    new Coordinates { Row = 3, Col = 3 },
                    new Coordinates { Row = 4, Col = 4 },
                    new Coordinates { Row = 5, Col = 5 },
                    new Coordinates { Row = 6, Col = 6 },
                    new Coordinates { Row = 7, Col = 7 }
                },
                new List<Coordinates>
                {
                    new Coordinates { Row = 1, Col = 7 },
                    new Coordinates { Row = 2, Col = 6 },
                    new Coordinates { Row = 3, Col = 5 },
                    new Coordinates { Row = 4, Col = 4 },
                    new Coordinates { Row = 5, Col = 3 },
                    new Coordinates { Row = 6, Col = 2 },
                    new Coordinates { Row = 7, Col = 1 }
                }
            };

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual), JsonConvert.SerializeObject(areasMethod));
        }

        [TestMethod]
        public void GetAreasDiagonal_GridIsNotSquare_ReturnEmptyAreas()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasDiagonal(7, 8);
            List<List<Coordinates>> areasManual = new();

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual), JsonConvert.SerializeObject(areasMethod));
        }



        [TestMethod]
        public void GetAreasWindoku_Grid9x9_ReturnWindokuAreas()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasWindoku(9, 9);
            List<List<Coordinates>> areasManual = new()
            {
                new List<Coordinates>
                {
                    new Coordinates { Row = 2, Col = 6 },
                    new Coordinates { Row = 2, Col = 7 },
                    new Coordinates { Row = 2, Col = 8 },
                    new Coordinates { Row = 3, Col = 6 },
                    new Coordinates { Row = 3, Col = 7 },
                    new Coordinates { Row = 3, Col = 8 },
                    new Coordinates { Row = 4, Col = 6 },
                    new Coordinates { Row = 4, Col = 7 },
                    new Coordinates { Row = 4, Col = 8 }
                },
                new List<Coordinates>
                {
                    new Coordinates { Row = 6, Col = 2 },
                    new Coordinates { Row = 6, Col = 3 },
                    new Coordinates { Row = 6, Col = 4 },
                    new Coordinates { Row = 7, Col = 2 },
                    new Coordinates { Row = 7, Col = 3 },
                    new Coordinates { Row = 7, Col = 4 },
                    new Coordinates { Row = 8, Col = 2 },
                    new Coordinates { Row = 8, Col = 3 },
                    new Coordinates { Row = 8, Col = 4 }
                }
            };

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual[0]), JsonConvert.SerializeObject(areasMethod[1]));
            Assert.AreEqual(JsonConvert.SerializeObject(areasManual[1]), JsonConvert.SerializeObject(areasMethod[2]));
        }

        [TestMethod]
        public void GetAreasWindoku_GridNot9x9_ReturnEmptyAreas()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasWindoku(8, 9);
            List<List<Coordinates>> areasManual = new();

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual), JsonConvert.SerializeObject(areasMethod));
        }

        [TestMethod]
        public void GetAreasAntiWindoku_Grid9x9_ReturnWindokuAreasWithNumber4()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasAntiWindoku(9, 9);
            List<List<Coordinates>> areasManual = new()
            {
                new List<Coordinates>
                {
                    new Coordinates { Row = 2, Col = 6, Number = 4 },
                    new Coordinates { Row = 2, Col = 7 },
                    new Coordinates { Row = 2, Col = 8 },
                    new Coordinates { Row = 3, Col = 6 },
                    new Coordinates { Row = 3, Col = 7 },
                    new Coordinates { Row = 3, Col = 8 },
                    new Coordinates { Row = 4, Col = 6 },
                    new Coordinates { Row = 4, Col = 7 },
                    new Coordinates { Row = 4, Col = 8 }
                },
                new List<Coordinates>
                {
                    new Coordinates { Row = 6, Col = 2, Number = 4 },
                    new Coordinates { Row = 6, Col = 3 },
                    new Coordinates { Row = 6, Col = 4 },
                    new Coordinates { Row = 7, Col = 2 },
                    new Coordinates { Row = 7, Col = 3 },
                    new Coordinates { Row = 7, Col = 4 },
                    new Coordinates { Row = 8, Col = 2 },
                    new Coordinates { Row = 8, Col = 3 },
                    new Coordinates { Row = 8, Col = 4 }
                }
            };

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual[0]), JsonConvert.SerializeObject(areasMethod[1]));
            Assert.AreEqual(JsonConvert.SerializeObject(areasManual[1]), JsonConvert.SerializeObject(areasMethod[2]));        }

        [TestMethod]
        public void GetAreasAntiWindoku_GridNot9x9_ReturnEmptyAreas()
        {
            List<List<Coordinates>> areasMethod = HelperArea.GetAreasAntiWindoku(9, 8);
            List<List<Coordinates>> areasManual = new();

            Assert.AreEqual(JsonConvert.SerializeObject(areasManual), JsonConvert.SerializeObject(areasMethod));
        }
    }
}
