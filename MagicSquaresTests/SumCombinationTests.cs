using MagicSquares;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MagicSquaresTests
{
	[TestClass]
	public class SumCombinationTests
	{
		readonly PossibleAddens SC = new();

		[TestMethod]
		public void NoAddendsLessThan2()
		{
			for(uint i = 0;i<2;i++)
				Assert.AreEqual(0, SC.UniqueAddensFor(7, i).Count);
		}

		[TestMethod]
		public void NoAddendsWithLowSum()
		{
			for (uint i = 0; i < 4; i++)
				Assert.AreEqual(0, SC.UniqueAddensFor(2, i).Count);
		}

		[TestMethod]
		public void AddendsFor2()
		{
			{
				var result = SC.UniqueAddensFor(3, 2);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 2 }));
			}
			{
				var result = SC.UniqueAddensFor(4, 2);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 3 }));
			}
			{
				var result = SC.UniqueAddensFor(5, 2);
				Assert.AreEqual(2, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 4 }));
				Assert.IsTrue(result[1].SequenceEqual(new uint[] { 2, 3 }));
			}
			{
				var result = SC.UniqueAddensFor(6, 2);
				Assert.AreEqual(2, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 5 }));
				Assert.IsTrue(result[1].SequenceEqual(new uint[] { 2, 4 }));
			}
			{
				var result = SC.UniqueAddensFor(7, 2);
				Assert.AreEqual(3, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 6 }));
				Assert.IsTrue(result[1].SequenceEqual(new uint[] { 2, 5 }));
				Assert.IsTrue(result[2].SequenceEqual(new uint[] { 3, 4 }));
			}
		}


		[TestMethod]
		public void AddendsFor3()
		{
			{
				for(uint i = 0;i<6;i++)
				{
					var result = SC.UniqueAddensFor(i, 3);
					Assert.AreEqual(0, result.Count);
				}
			}
			{
				var result = SC.UniqueAddensFor(6, 3);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 2, 3 }));
			}
			{
				var result = SC.UniqueAddensFor(7, 3);
				Assert.AreEqual(1, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 2, 4 }));
			}
			{
				var result = SC.UniqueAddensFor(8, 3);
				Assert.AreEqual(2, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 2, 5 }));
				Assert.IsTrue(result[1].SequenceEqual(new uint[] { 1, 3, 4 }));
			}
			{
				var result = SC.UniqueAddensFor(9, 3);
				Assert.AreEqual(3, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 2, 6 }));
				Assert.IsTrue(result[1].SequenceEqual(new uint[] { 1, 3, 5 }));
				Assert.IsTrue(result[2].SequenceEqual(new uint[] { 2, 3, 4 }));
			}
			{
				var result = SC.UniqueAddensFor(10, 3);
				Assert.AreEqual(4, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 2, 7 }));
				Assert.IsTrue(result[1].SequenceEqual(new uint[] { 1, 3, 6 }));
				Assert.IsTrue(result[2].SequenceEqual(new uint[] { 1, 4, 5 }));
				Assert.IsTrue(result[3].SequenceEqual(new uint[] { 2, 3, 5 }));
			}

			{
				var result = SC.UniqueAddensFor(15, 3);
				Assert.AreEqual(12, result.Count);
				Assert.IsTrue(result[0].SequenceEqual(new uint[] { 1, 2, 12 }));
				Assert.IsTrue(result[1].SequenceEqual(new uint[] { 1, 3, 11 }));
				Assert.IsTrue(result[2].SequenceEqual(new uint[] { 1, 4, 10 }));
				Assert.IsTrue(result[3].SequenceEqual(new uint[] { 2, 3, 10 }));
				Assert.IsTrue(result[4].SequenceEqual(new uint[] { 1, 5, 9 }));
				Assert.IsTrue(result[5].SequenceEqual(new uint[] { 2, 4, 9 }));
				Assert.IsTrue(result[6].SequenceEqual(new uint[] { 1, 6, 8 }));
				Assert.IsTrue(result[7].SequenceEqual(new uint[] { 2, 5, 8 }));
				Assert.IsTrue(result[8].SequenceEqual(new uint[] { 3, 4, 8 }));
				Assert.IsTrue(result[9].SequenceEqual(new uint[] { 2, 6, 7 }));
				Assert.IsTrue(result[10].SequenceEqual(new uint[] { 3, 5, 7 }));
				Assert.IsTrue(result[11].SequenceEqual(new uint[] { 4, 5, 6 }));
			}
		}

	}
}
