using MagicSquares.Core;
using Open.Collections;
using Open.Collections.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquares
{
	class Program
	{
		static void Main()
		{
			Console.Write("Enter the size of the desired Magic Square: ");
			var size = byte.Parse(Console.ReadLine() ?? string.Empty);
			if (size < 3)
			{
				Console.WriteLine("Invalid size.  Must be at least 3.");
				return;
			}

			Console.WriteLine();
			Console.WriteLine("First possible square (starting with 1):");
			var square = new Square(size);
			var tester = new Tester(square);
			var emitter = new ConsoleEmitter(tester);
			var defaultSquare = MagicSquare.CreateFromFirst(size, 1);
			square.GetPermutation(defaultSquare).Primary.Matrix.OutputToConsole();
			Console.WriteLine();

			// REDUCING THE SEARCH SPACE IS THE KEY.
			// Instead of searching all possible configurations limit the search space by the magic square sum.
			// Once a known minimum is established, we know there are more above that sum.
			// For example 4x4 will be rows that add up to 34.
			var firstSum = defaultSquare.Row(0).Sum();
			using var sums = new PossibleAddends();
			var len = square.Length;

			// Get all possible value combinations.
			var s = sums.UniqueAddendsFor(firstSum, size);
			Console.WriteLine("Possible {0} unique addends combinations for a sum of {1}.", s.Count, firstSum);

			if (size == 3)
			{
				foreach (var pa in s)
					Console.WriteLine(string.Join(' ', pa));

				Console.WriteLine();
				Console.WriteLine("Possible subsets:");
				var possibleSubsets = s.Subsets(size).Where(e => e.SelectMany(e => e).AllDistinct());
				foreach (var subset in possibleSubsets)
				{
					Console.WriteLine();
					Console.WriteLine(string.Join('\n',
						subset.Select(u => $"{PermuString(u)} => {string.Join(' ', u.Permutations().Select(u => PermuString(u)))}")));
				}

				static string PermuString(IReadOnlyList<int> u) => $"[{string.Join(' ', u)}]";
			}

			Console.WriteLine();
			Console.WriteLine("Searching for other {0} x {0} ({1} unique) Magic Squares with a sum of {2}...", size, len, firstSum);

			tester.TestSumCombinationSubsets(s, firstSum);
		}

	}
}
