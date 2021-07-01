using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Collections.Numeric;
using System.Linq;

namespace MagicSquaresTests
{
	[TestClass]
	public class SumCombinationTests
	{
		readonly PossibleAddends SC = new();

		[TestMethod]
		public void NoAddendsLessThan2()
		{
			for(int i = 0;i<2;i++)
				Assert.AreEqual(0, SC.UniqueAddendsFor(7, i).Count);
		}

		[TestMethod]
		public void NoAddendsWithLowSum()
		{
			for (int i = 0; i < 4; i++)
				Assert.AreEqual(0, SC.UniqueAddendsFor(2, i).Count);
		}

		[TestMethod]
		public void AddendsFor2()
		{
			{
				var result = SC.UniqueAddendsFor(3, 2);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 2 }));
			}
			{
				var result = SC.UniqueAddendsFor(4, 2);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 3 }));
			}
			{
				var result = SC.UniqueAddendsFor(5, 2);
				Assert.AreEqual(2, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 4 }));
				Assert.IsTrue(result[1].SequenceEqual(new int[] { 2, 3 }));
			}
			{
				var result = SC.UniqueAddendsFor(6, 2);
				Assert.AreEqual(2, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 5 }));
				Assert.IsTrue(result[1].SequenceEqual(new int[] { 2, 4 }));
			}
			{
				var result = SC.UniqueAddendsFor(7, 2);
				Assert.AreEqual(3, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 6 }));
				Assert.IsTrue(result[1].SequenceEqual(new int[] { 2, 5 }));
				Assert.IsTrue(result[2].SequenceEqual(new int[] { 3, 4 }));
			}
		}


		[TestMethod]
		public void AddendsFor3()
		{
			{
				for(int i = 0;i<6;i++)
				{
					var result = SC.UniqueAddendsFor(i, 3);
					Assert.AreEqual(0, result.Count);
				}
			}
			{
				var result = SC.UniqueAddendsFor(6, 3);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 2, 3 }));
			}
			{
				var result = SC.UniqueAddendsFor(7, 3);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 2, 4 }));
			}
			{
				var result = SC.UniqueAddendsFor(8, 3);
				Assert.AreEqual(2, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 2, 5 }));
				Assert.IsTrue(result[1].SequenceEqual(new int[] { 1, 3, 4 }));
			}
			{
				var result = SC.UniqueAddendsFor(9, 3);
				Assert.AreEqual(3, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 2, 6 }));
				Assert.IsTrue(result[1].SequenceEqual(new int[] { 1, 3, 5 }));
				Assert.IsTrue(result[2].SequenceEqual(new int[] { 2, 3, 4 }));
			}
			{
				var result = SC.UniqueAddendsFor(10, 3);
				Assert.AreEqual(4, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 2, 7 }));
				Assert.IsTrue(result[1].SequenceEqual(new int[] { 1, 3, 6 }));
				Assert.IsTrue(result[2].SequenceEqual(new int[] { 1, 4, 5 }));
				Assert.IsTrue(result[3].SequenceEqual(new int[] { 2, 3, 5 }));
			}

			{
				var result = SC.UniqueAddendsFor(15, 3);
				Assert.AreEqual(12, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new int[] { 1, 2, 12 }));
				Assert.IsTrue(result[1].SequenceEqual(new int[] { 1, 3, 11 }));
				Assert.IsTrue(result[2].SequenceEqual(new int[] { 1, 4, 10 }));
				Assert.IsTrue(result[3].SequenceEqual(new int[] { 2, 3, 10 }));
				Assert.IsTrue(result[4].SequenceEqual(new int[] { 1, 5, 9 }));
				Assert.IsTrue(result[5].SequenceEqual(new int[] { 2, 4, 9 }));
				Assert.IsTrue(result[6].SequenceEqual(new int[] { 1, 6, 8 }));
				Assert.IsTrue(result[7].SequenceEqual(new int[] { 2, 5, 8 }));
				Assert.IsTrue(result[8].SequenceEqual(new int[] { 3, 4, 8 }));
				Assert.IsTrue(result[9].SequenceEqual(new int[] { 2, 6, 7 }));
				Assert.IsTrue(result[10].SequenceEqual(new int[] { 3, 5, 7 }));
				Assert.IsTrue(result[11].SequenceEqual(new int[] { 4, 5, 6 }));
			}
		}

	}
}
