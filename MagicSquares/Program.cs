using MagicSquares.Core;
using Open.Collections;
using Open.Collections.Numeric;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
			}

			Console.WriteLine();
			Console.WriteLine("First possible square (starting with 1):");
			var square = new Square(size);
			var defaultSquare = MagicSquare.CreateFromFirst(size, 1);
			square.GetPermutation(defaultSquare).Primary.Matrix.OutputToConsole();
			Console.WriteLine();

			// REDUCING THE SEARCH SPACE IS THE KEY.
			// Instead of searching all possible configurations limit the search space by the magic square sum.
			// Once a known minimum is established, we know there are more above that sum.
			// For example 4x4 will be rows that add up to 34.
			var firstSum = defaultSquare.Row(0).Sum();
			var sums = new PossibleAddends();
			var len = square.Length;

			// Get all possible value combinations.
			var s = sums.UniqueAddendsFor(firstSum, size);
			Console.WriteLine("Possible {0} unique addends combinations for a sum of {1}:", s.Count, firstSum);
			foreach (var pa in s)
				Console.WriteLine(string.Join(' ', pa));

			if(size==3)
			{
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

			var count = 0;
			var sw = Stopwatch.StartNew();
			var verification = new ConcurrentHashSet<string>();

			// As long as diagnal values are not important, once a magic square is found, its rows or columns can be shuffled and all numbers will still add up.
			// Then get all row (set) permutations.
			// The order of the rows doesn't matter yet as long as they add up.
			Parallel.ForEach(s.Subsets(size), rows =>
			{
				if (!rows.SelectMany(e => e).AllDistinct()) return;
				// Discovered a valid set!  We know the rows already add up.
				// Rearange each row (set) to see if we can get a Magic Square.

				// First provide a collection of row permutations.  Each row still adds up to the same, but just rearranged.
				using var c = rows.Select(r => r.Permutations()).Memoize();

				// Next, group each possible configuration of these rows and look for a winner.
				foreach (var magic in c.RowConfigurations().Where(a => a.IsMagicSquare(size, firstSum, true)))
				{
					// Ok!  Found one.  Let's expand the set the possible row configurations.
					foreach(var rowPermutation in magic.Take(size).Permutations())
					{
						// Now reduce the set further by eliminating any flips or rotations.
						var p = square.GetPermutation(rowPermutation, ignoreOversize: true).Primary; // Get the normalized version of the matrix.
						if (!verification.Add(p.Hash)) continue;

						var comment = p.Matrix.IsPerfectMagicSquare() ? "(perfect)" : string.Empty;
						lock (square)
						{
							Console.WriteLine();
							Console.WriteLine("{0}: {1}", ++count, comment);
							p.Matrix.OutputToConsole();
						}
					}
				}
			});

			Console.WriteLine();
			Console.WriteLine("Total groupings found: {0}", count);
			Console.Write(sw.Elapsed);
			Console.WriteLine();

			//sw.Reset();
			//verification.Clear();
			//var values = Enumerable.Range(1, len).ToArray();
			//sw.Start();
			//foreach (var e in values.Permutations().MagicSquares(size))
			//{
			//	var p = square.GetPermutation(e).Primary;
			//	if (!verification.Add(p.Hash)) continue;
			//	Console.WriteLine();
			//	Console.WriteLine("{0}:", verification.Count);
			//	p.ToXYGrid().OutputToConsole(size);
			//}

			//Console.WriteLine();
			//Console.WriteLine("Total found: {0}", verification.Count);
			//Console.Write(sw.Elapsed);
			//Console.WriteLine();
		}

	}
}
