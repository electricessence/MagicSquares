using Open.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MagicSquares.Core
{
	public class ConsoleEmitter
	{
		public ConsoleEmitter(Square square)
		{
			Size = square.Size;
			Square = square;
		}

		public int Size { get; }
		public Square Square { get; }

		public int Start(IReadOnlyList<IReadOnlyList<int>> combinations, int sum = 0, string summaryHeader = null)
		{
			var count = 0;
			var sw = Stopwatch.StartNew();
			var verification = new ConcurrentHashSet<string>();

			// As long as diagnal values are not important, once a magic square is found, its rows or columns can be shuffled and all numbers will still add up.
			// Then get all row (set) permutations.
			// The order of the rows doesn't matter yet as long as they add up.
			Parallel.ForEach(combinations.Subsets(Size), rows =>
			{
				// Check the sums first if need be.
				var localSum = sum;
				if(localSum == 0)
				{
					bool first = true;
					foreach(var row in rows)
					{
						if (first)
						{
							localSum = row.Sum();
							first = false;
						}
						else if (row.Sum() != localSum)
						{
							return;
						}
					}
				}

				if (!rows.SelectMany(e => e).AllDistinct()) return;
				// Discovered a valid set!  We know the rows already add up.
				// Rearange each row (set) to see if we can get a Magic Square.

				// First provide a collection of row permutations.  Each row still adds up to the same, but just rearranged.
				using var c = rows.Select(r => r.Permutations()).Memoize();

				// Next, group each possible configuration of these rows and look for a winner.
				foreach (var magic in c.RowConfigurations().Where(a => a.IsMagicSquare(Size, localSum, true)))
				{
					// Ok!  Found one.  Let's expand the set the possible row configurations.
					foreach (var rowPermutation in magic.Take(Size).Permutations())
					{
						// Now reduce the set further by eliminating any flips or rotations.
						var p = Square.GetPermutation(rowPermutation, ignoreOversize: true).Primary; // Get the normalized version of the matrix.
						if (!verification.Add(p.Hash)) continue;

						var comment = p.Matrix.IsPerfectMagicSquare() ? "(perfect)" : string.Empty;
						lock (Square)
						{
							Console.WriteLine();
							Console.WriteLine("{0}: {1}", ++count, comment);
							p.Matrix.OutputToConsole();
						}
					}
				}
			});

			lock(Square)
			{
				Console.WriteLine();
				if (summaryHeader != null) Console.WriteLine(summaryHeader);
				Console.WriteLine("Total groupings found: {0}", count);
				Console.Write(sw.Elapsed);
				Console.WriteLine();
			}

			return count;
		}
	}
}
