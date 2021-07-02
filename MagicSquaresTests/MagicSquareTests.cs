using MagicSquares;
using MagicSquares.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Immutable;

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
                    new int[] { 6, 1, 8 },
                    new int[] { 7, 5, 3 },
                    new int[] { 2, 9, 4 }
                };

                Assert.IsTrue(square.IsMagicSquare());

                var sq = SquareMatrix<int>.Create(square, 3);
                Assert.IsTrue(sq.IsMagicSquare());
                Assert.IsTrue(sq.IsPerfectMagicSquare());
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

            {
                int[][] square = new int[][] {
                    new int[] { 4, 14, 15, 1 },
                    new int[] { 9, 7, 6, 12 },
                    new int[] { 5, 11, 10, 8, 1 },
                    new int[] { 16, 2, 3, 13 }
                };

                Assert.IsTrue(square.IsMagicSquare(true));
            }

            {
                int[][] square = new int[][] {
                    new int[] { 4, 14, 15, 1 },
                    new int[] { 9, 7, 6, 12 },
                    new int[] { 5, 11, 10, 8, 1 },
                    new int[] { 16, 2, 3, 13 },
                    null,
                    null
                };

                Assert.IsTrue(square.IsMagicSquare(4, 34, true));
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

        [TestMethod]
        public void RotationTest()
		{
            {
                int[][] square = new int[][] {
                    new int[] { 6, 1, 8 },
                    new int[] { 7, 5, 3 },
                    new int[] { 2, 9, 4 }
                };

                int[][] expected = new int[][] {
                    new int[] { 2, 7, 6 },
                    new int[] { 9, 5, 1 },
                    new int[] { 4, 3, 8 }
                };

                var sq = SquareMatrix<int>.Create(SquareMatrix<int>.Create(square, 3).GetRotatedCW().ToImmutableArray());
                var ex = SquareMatrix<int>.Create(expected, 3);

                Assert.AreEqual(ex.ToMatrixString(), sq.ToMatrixString());
            }
        }
    }
}
