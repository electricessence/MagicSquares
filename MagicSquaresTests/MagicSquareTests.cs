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
                int[][] square = new int[3][] {
                    new int[3] { 2, 4, 9 },
                    new int[3] { 6, 8, 1 },
                    new int[3] { 7, 3, 5 }
                };

                Assert.IsTrue(square.IsMagicSquare());
            }

            {
                int[][] square = new int[3][] {
                    new int[3] { 2, 4, 9 },
                    new int[3] { 6, 8, 1 },
                    new int[3] { 7, 3, 6 }
                };

                Assert.IsFalse(square.IsMagicSquare());
            }

            {
                int[][] square = new int[3][] {
                    new int[3] { 2, 4, 9 },
                    new int[4] { 6, 7, 1, 1 },
                    new int[3] { 7, 4, 5 }
                };

                Assert.IsFalse(square.IsMagicSquare());
            }
        }
    }
}
