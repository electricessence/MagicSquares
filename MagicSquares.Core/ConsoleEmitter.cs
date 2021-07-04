using Open.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MagicSquares.Core
{
	public class ConsoleEmitter
	{
		public ConsoleEmitter(Square square, bool parallelProcessing = true)
		{
			Size = square.Size;
			Square = square;
			ParallelProcessing = parallelProcessing;
		}

		public int Size { get; }
		public Square Square { get; }
		public bool ParallelProcessing { get; }


		readonly ConcurrentHashSet<string> _verification = new();

		public int Start(IEnumerable<IReadOnlyList<IReadOnlyList<int>>> combinations, int sum, string summaryHeader = null)
		{
			var count = 0;
			var plausibleCount = 0;
			// As long as diagnal values are not important, once a magic square is found, its rows or columns can be shuffled and all numbers will still add up.
			// Then get all row (set) permutations.
			// The order of the rows doesn't matter yet as long as they add up.

			var sw = Stopwatch.StartNew();
			if (ParallelProcessing)
				Parallel.ForEach(combinations, rows => ProcessRows(rows, sum, ref plausibleCount, ref count));
			else
				foreach (var rows in combinations) ProcessRows(rows, sum, ref plausibleCount, ref count);

			if (plausibleCount != 0) Report(summaryHeader, count, sw);

			return count;
		}

		public int Start(IReadOnlyList<IReadOnlyList<int>> combinations, int sum = 0, string summaryHeader = null)
		{
			var count = 0;
			var plausibleCount = 0;
			// As long as diagnal values are not important, once a magic square is found, its rows or columns can be shuffled and all numbers will still add up.
			// Then get all row (set) permutations.
			// The order of the rows doesn't matter yet as long as they add up.

			var sw = Stopwatch.StartNew();
			if (ParallelProcessing)
				Parallel.ForEach(combinations.Subsets(Size), rows=> ProcessRows(rows, sum, ref plausibleCount, ref count));
			else
				foreach (var rows in combinations.Subsets(Size)) ProcessRows(rows, sum, ref plausibleCount, ref count);

			if (plausibleCount != 0) Report(summaryHeader, count, sw);

			return count;
		}


		void ProcessRows(IReadOnlyList<IReadOnlyList<int>> rows, int sum, ref int plausibleCount, ref int total)
		{
			// Check the sums first if need be.
			if (sum == 0)
			{
				bool first = true;
				foreach (var row in rows)
				{
					if (first)
					{
						sum = row.Sum();
						first = false;
					}
					else if (row.Sum() != sum)
					{
						return;
					}
				}
			}

			Interlocked.Increment(ref plausibleCount);

			if (!rows.SelectMany(e => e).AllDistinct()) return;
			// Discovered a valid set!  We know the rows already add up.
			// Rearange each row (set) to see if we can get a Magic Square.

			// First provide a collection of row permutations.  Each row still adds up to the same, but just rearranged.
			using var c = rows.Select(r => r.Permutations()).Memoize();

			// Next, group each possible configuration of these rows and look for a winner.
			foreach (var magic in c.RowConfigurations().Where(a => a.IsMagicSquare(Size, sum, true)))
			{
				// Ok!  Found one.  Let's expand the set the possible row configurations.
				foreach (var rowPermutation in magic.Take(Size).Permutations())
				{
					// Now reduce the set further by eliminating any flips or rotations.
					var p = Square.GetPermutation(rowPermutation, ignoreOversize: true).Primary; // Get the normalized version of the matrix.
					if (!_verification.Add(p.Hash)) continue;

					var comment = p.Matrix.IsPerfectMagicSquare() ? "(perfect)" : string.Empty;
					lock (Square)
					{
						Console.WriteLine();
						Console.WriteLine("{0}: {1}", ++total, comment);
						p.Matrix.OutputToConsole();
					}
				}
			}
		}

		void Report(string summaryHeader, int count, Stopwatch sw)
		{
			lock (Square)
			{
				Console.WriteLine();
				if (summaryHeader != null) Console.WriteLine(summaryHeader);
				Console.WriteLine("Total groupings found: {0}", count);
				Console.Write("{0} milliseconds", sw.Elapsed.TotalMilliseconds);
				Console.WriteLine();
			}
		}
	}
}
