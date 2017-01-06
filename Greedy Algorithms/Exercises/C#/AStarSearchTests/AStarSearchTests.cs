using System;
using AStarAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AStarSearchTests
{
    [TestClass]
    public class AStarSearchTests
    {
        [TestMethod]
        public void AStarSearchTestFirstMap()
        {
            char[,] map =
            {
                    { '-', '-', '-', '-', '-' },
                    { '-', '-', '*', '-', '-' },
                    { '-', 'W', 'W', 'W', '-' },
                    { '-', '-', '-', '-', '-' },
                    { '-', '-', 'P', '-', '-' },
                    { '-', '-', '-', '-', '-' }
            };

            var aStar = new AStar(map);

            var cells = aStar.FindShortestPath(new int[] { 4, 2 }, new int[] { 1, 2 });

            Assert.AreEqual(cells.Count, 5);

            Assert.AreEqual(cells[0][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[0][1], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][1], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][0], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][1], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][0], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][1], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][0], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][1], 2, "An expected cell in the path did not match!");
        }

        [TestMethod]
        public void AStarSearchTestSecondMap()
        {
            char[,] map =
            {
                { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
                { '-', '-', '-', 'W', '*', '-', '-', '-', '-', '-', '-' },
                { '-', '-', '-', 'W', 'W', 'W', 'W', 'W', '-', '-', '-' },
                { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
                { '-', '-', '-', '-', '-', '-', '-', 'P', '-', '-', '-' },
                { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' }
            };

            var aStar = new AStar(map);

            var cells = aStar.FindShortestPath(new int[] { 4, 7 }, new int[] { 1, 4 });

            Assert.AreEqual(cells.Count, 7);

            Assert.AreEqual(cells[0][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[0][1], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][1], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][1], 6, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][1], 7, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][0], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][1], 8, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[5][0], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[5][1], 7, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[6][0], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[6][1], 7, "An expected cell in the path did not match!");
        }


        [TestMethod]
        public void AStarSearchTestThirdMap()
        {
            char[,] map =
            {
                { '-', '-', '-', '-', 'W', '-', '-', '-', 'W', '*', '-' },
                { '-', 'W', '-', '-', 'W', '-', '-', '-', 'W', '-', '-' },
                { 'P', '-', 'W', '-', 'W', '-', '-', '-', 'W', '-', '-' },
                { '-', 'W', '-', '-', 'W', 'W', 'W', '-', 'W', 'W', '-' },
                { '-', '-', '-', 'W', 'W', '-', '-', '-', '-', 'W', '-' },
                { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' }
            };

            var aStar = new AStar(map);

            var cells = aStar.FindShortestPath(new int[] { 2, 0 }, new int[] { 0, 9 });

            Assert.AreEqual(cells.Count, 16);

            Assert.AreEqual(cells[0][0], 0, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[0][1], 9, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][1], 9, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][0], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][1], 9, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][0], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][1], 10, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][0], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][1], 10, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[5][0], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[5][1], 9, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[6][0], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[6][1], 8, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[7][0], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[7][1], 7, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[8][0], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[8][1], 6, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[9][0], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[9][1], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[10][0], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[10][1], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[11][0], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[11][1], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[12][0], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[12][1], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[13][0], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[13][1], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[14][0], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[14][1], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[15][0], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[15][1], 0, "An expected cell in the path did not match!");
        }

        [TestMethod]
        public void AStarSearchTestFourthMap()
        {
            char[,] map =
            {
                { 'P', 'W', '-', 'W', '-', '-' },
                { '-', '-', 'W', '-', 'W', '-' },
                { '-', 'W', 'W', 'W', 'W', '-' },
                { '-', 'W', 'W', 'W', '-', '*' },
                { '-', '-', '-', '-', '-', '-' }

            };

        var aStar = new AStar(map);

            var cells = aStar.FindShortestPath(new int[] { 0, 0 }, new int[] { 3, 5 });

            Assert.AreEqual(cells.Count, 9);

            Assert.AreEqual(cells[0][0], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[0][1], 5, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][0], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[1][1], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][0], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[2][1], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][0], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[3][1], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][0], 4, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[4][1], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[5][0], 3, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[5][1], 0, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[6][0], 2, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[6][1], 0, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[7][0], 1, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[7][1], 0, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[8][0], 0, "An expected cell in the path did not match!");
            Assert.AreEqual(cells[8][1], 0, "An expected cell in the path did not match!");
        }
    }
}
