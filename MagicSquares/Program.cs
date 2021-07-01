using Open.Collections;
using Open.Collections.Numeric;
using Open.Disposable;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
			var defaultSquare = MagicSquare.CreateFromFirst(size, 1);
			defaultSquare.OutputToConsole(size);
			Console.WriteLine();

			// REDUCING THE SEARCH SPACE IS THE KEY.
			// Instead of searching all possible configurations limit the search space by the magic square sum.
			// Once a known minimum is established, we know there are more above that sum.
			// For example 4x4 will be rows that add up to 34.
			var firstSum = defaultSquare.Row(0).Sum();
			var square = new Square(size);
			var sums = new PossibleAddends();
			var len = square.Length;

			// Get all possible value combinations.
			var s = sums.UniqueAddendsFor(firstSum, size);
			Console.WriteLine("Possible {0} unique addends combinations for a sum of {1}:", s.Count, firstSum);

			Console.WriteLine();
			Console.WriteLine("Searching for other {0} x {0} ({1} unique) Magic Squares with a sum of {2}...", size, len, firstSum);

			var count = 0;
			var sw = Stopwatch.StartNew();
#if DEBUG
			var verification = new HashSet<string>();
#endif
			// Then get all row permutations.
			Parallel.ForEach(s.Subsets(size), rows =>
			{
				if (!rows.SelectMany(e => e).AllDistinct()) return;

				// Discovered a valid set!  We know the rows already add up.
				// Rearange each row (set) to see if we can get a Magic Square.
				HashSetPool<Square.Permutation>.Shared.Rent(p =>
				{
					// First provide a collection of row permutations.  Each row still adds up to the same, but just rearranged.
					using var c = rows.Select(r => r.Permutations()).Memoize();
					// Next, group each possible configuration of these rows and look for a winner.
					foreach (var config in c.RowConfigurations().Skip(1).Where(a => a.IsMagicSquare(size, firstSum, true)))
					{
						// Ok!  Found one.  Now reduce the set further by eliminating any flips or rotations.
						var ms = square.GetPermutation(config.Take(size).SelectMany(e => e).ToImmutableArray());
						var pCombo = ms.Group.Value[0].Value; // Get the normalized version of the matrix.
						if (!p.Add(pCombo)) continue; // Already reported the normalized version?

#if DEBUG
						bool isNew;
						lock (verification) isNew = verification.Add(pCombo.Hash);
						Debug.Assert(isNew);
						if (!isNew) return;
#endif

						lock (square)
						{
							Console.WriteLine();
							Console.WriteLine("{0}:", ++count);
							pCombo.ToXYGrid().OutputToConsole(size);
						}
					}
				});
			});

			Console.WriteLine();
			Console.WriteLine("Total found: {0}", count);
			Console.Write(sw.Elapsed);
		}

	}
}
