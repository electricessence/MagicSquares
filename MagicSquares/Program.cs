using Open.Collections;
using Open.Collections.Numeric;
using Open.Disposable;
using System;
using System.Buffers;
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

			var firstSum = defaultSquare.Row(0).Sum();
			var square = new Square(size);
			var sums = new PossibleAddends();
			var len = square.Length;

			var s = sums.UniqueAddendsFor(firstSum, size);
			Console.WriteLine("Possible {0} unique addends combinations for a sum of {1}:", s.Count, firstSum);

			Console.WriteLine();
			Console.WriteLine("Searching for other {0} x {0} ({1} unique) Magic Squares with a sum of {2}...", size, len, firstSum);

			var sw = Stopwatch.StartNew();
			var verification = new HashSet<string>();
			// Get N rows and test them.
			Parallel.ForEach(s.Subsets(size), rows =>
			{
				if (!rows.SelectMany(e => e).AllDistinct()) return;
				HashSetPool<Square.Permutation>.Shared.Rent(p =>
				{
					var c = rows.Select(r => r.Permutations()).Memoize();
					foreach (var combo in Arrangments(c).Skip(1).Where(a => a.IsMagicSquare(size, firstSum, true)))
					{
						var ms = square.GetPermutation(combo.Take(size).SelectMany(e => e).ToImmutableArray());
						var pCombo = ms.Group.Value[0].Value; // Get the normalized version of the matrix.
						if (!p.Add(pCombo)) continue;

						lock (square)
						{
							if (!verification.Add(pCombo.Hash)) return;
							Console.WriteLine();
							Console.WriteLine("{0}:", verification.Count);
							pCombo.ToXYGrid().OutputToConsole(size);
						}
					}
				});
			});

			Console.WriteLine();
			if (verification.Count < 2)
			{
				Console.WriteLine("No additional found for {0}.", firstSum);
			}
			else
			{
				Console.WriteLine("Total found: {0}", verification.Count);
			}
			Console.Write(sw.Elapsed);
		}

		static IEnumerable<T[]> Arrangments<T>(IReadOnlyList<IEnumerable<T>> source)
		{
			var listPool = ListPool<IEnumerator<T>>.Shared;
			List<IEnumerator<T>> enumerators = listPool.Take();
			try
			{
				foreach (var e in source.Select(e => e.GetEnumerator()))
				{
					if (!e.MoveNext())
					{
						yield break;
					}
					enumerators.Add(e);
				}

				var count = enumerators.Count;

				bool GetNext() => ListPool<int>.Shared.Rent(reset =>
				{
					for (var i = 0; i < count; i++)
					{
						var e = enumerators[i];
						if (e.MoveNext())
						{
							foreach (var r in reset)
							{
								enumerators[r] = e = source[r].GetEnumerator();
								e.MoveNext();
							}
							return true;
						}
						e.Dispose();
						if (i == count - 1) break;
						reset.Add(i);
					}

					return false;
				});


				var arrayPool = ArrayPool<T>.Shared;
				var buffer = arrayPool.Rent(count);
				try
				{
					do
					{
						for (var i = 0; i < count; i++)
						{
							buffer[i] = enumerators[i].Current ?? throw new NullReferenceException();
						}
						yield return buffer;
					}
					while (GetNext());
				}
				finally
				{
					arrayPool.Return(buffer);
				}

			}
			finally
			{
				listPool.Give(enumerators);
			}
		}
	}
}
