using MagicSquares.Core;
using Open.Collections;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace MagicSquares.OfSquares
{
	class Program
	{
		static void Main()
		{
			Console.Write("Enter the size of the desired Magic Square: ");
			var size = byte.Parse(Console.ReadLine());
			if (size < 3)
			{
				Console.WriteLine("Invalid size.  Must be at least 3.");
				return;
			}

			var square = new Square(size);
			var len = square.Length;

			Console.Write("Enter the first number in the search range: ");
			var first = byte.Parse(Console.ReadLine());
			Console.Write("Enter the last number in the search range: ");
			var last = byte.Parse(Console.ReadLine());
			if (last < first + len - 1)
			{
				Console.WriteLine("Last must be at least the value of the tail of the possible set.");
				return;
			}

			using var tester = new Tester(square);
			using var _ = tester.Subscribe(found =>
			{
				var (familyId, square, perfect) = found;
				lock (tester)
				{
					Console.WriteLine();
					var vector = square.Vector.Select(i => Math.Sqrt(i)).OrderBy(i => i);
					if (perfect) Console.WriteLine("TRUE MAGIC SQUARE OF SQUARES!");
					Console.Write("{0}: [{1}]", familyId, string.Join(' ', vector));
					var rows = square.ToDisplayRowStrings().ToArray();
					var rowsSq = square.Transform(i => Math.Sqrt(i) + "²").ToDisplayRowStrings().ToArray();
					for (var i = 0; i < rows.Length; i++)
					{
						Console.WriteLine();
						Console.Write(rowsSq[i]);
						Console.Write(" | ");
						Console.Write(rows[i]);
					}
					Console.WriteLine(" = {0}", square.Vector.Take(size).Sum());
				}
			});

			Console.WriteLine();
			Console.WriteLine("Searching for {0} x {0} Magic Squares of squares...", size);

			var squares = Enumerable.Range(1, last - first + 1).Select(v => v * v).ToImmutableArray();

			var (_, _, rowConfigurations, _) = PossibleSearch(len, size);
			var (_, _, uniqueRows, _) = PossibleSearch(squares.Length, size);
			var (_, lenFact, possibleSubsets, searchSpace) = PossibleSearch(squares.Length, len);
			Console.WriteLine("Possible square configurations: {0:n0}", lenFact);
			Console.WriteLine("Possible row configurations: {0:n0}", rowConfigurations);
			Console.WriteLine("Possible unique rows: {0:n0}", uniqueRows);
			Console.WriteLine("Possible square subsets: {0:n0}", possibleSubsets);
			Console.WriteLine("Search space: {0:n0}", searchSpace);


			var sw = Stopwatch.StartNew();
			tester.TestSumCombinationSubsets(squares);
			sw.Stop();

			Console.WriteLine();
			Console.WriteLine("Unique Magic Squares: {0}", tester.TrueCount);
			Console.WriteLine("Total Families: {0}", tester.FamilyCount);
			Console.WriteLine("Plausible Reviewed: {0}", tester.PlausibleCount);
			Console.WriteLine("Elasped Milliseconds: {0}", sw.Elapsed.TotalMilliseconds);
		}

		static BigInteger Factorial(in BigInteger of)
		{
			BigInteger result = 1;
			for (var i = 2; i <= of; ++i) result *= i;
			return result;
		}

		static (BigInteger fFull, BigInteger fSub, BigInteger pSub, BigInteger all) PossibleSearch(in BigInteger fullSet, in BigInteger subSet)
		{
			var f = Factorial(fullSet);
			var s = Factorial(subSet);
			var searchSpace = f / Factorial(fullSet - subSet);
			var possibleSubsets = searchSpace / s;
			return (f, s, possibleSubsets, searchSpace);
		}

	}
}
