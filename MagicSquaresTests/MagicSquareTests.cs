using MagicSquares;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicSquaresTests
{
    [TestClass]
    public class MagicSquareTests
    {
        [TestMethod]
        public void IsMagicSquareTest()
        {
            {
                int[][] square = new int[][] {
                    new int[] { 2, 4, 9 },
                    new int[] { 6, 8, 1 },
                    new int[] { 7, 3, 5 }
                };

                Assert.IsTrue(square.IsMagicSquare());
            }

            {
                int[][] square = new int[][] {
                    new int[] { 4, 14, 15, 1 },
                    new int[] { 9, 7, 6, 12 },
                    new int[] { 5, 11, 10, 8 },
                    new int[] { 16, 2, 3, 13 }
                };

                Assert.IsTrue(square.IsMagicSquare());
            }
        }

        [TestMethod]
        public void IsNotMagicSquareTest()
        {
            {
                int[][] square = new int[][] {
                    new int[] { 2, 4, 9 },
                    new int[] { 6, 8, 1 },
                    new int[] { 7, 3, 6 }
                };

                Assert.IsFalse(square.IsMagicSquare());
            }

            {
                int[][] square = new int[][] {
                    new int[] { 2, 4, 9 },
                    new int[] { 6, 7, 1, 1 },
                    new int[] { 7, 4, 5 }
                };

                Assert.IsFalse(square.IsMagicSquare());
            }
        }
    }
}
