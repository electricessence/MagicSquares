using Open.Collections;
using Open.Disposable;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace MagicSquares
{
	/**
     * What is a magic square?
     * All rows and columns add up to the same number.
     * a + b + c = N
     * d + e + f = N
     * g + h + i = N
     * 
     * a + d + g = N
     * b + e + h = N
     * c + f + i = N 
     */

	public static class Extensions
	{
		public static bool AllDistinct<T>(this IEnumerable<T> rows)
		=> HashSetPool<T>.Shared.Rent(d =>
		{
			foreach (var e in rows)
			{
				if (!d.Add(e)) return false;
			}
			return true;
		});

		static bool IsMagicSquareInternal<T>(T square, ref int size, ref int sum, Func<int[], int> validator)
		{
			if (square is null) throw new ArgumentNullException(nameof(square));

			var pool = ArrayPool<int>.Shared;
			var columns = pool.Rent(size);
			try
			{
				var rowCount = validator(columns);
				if (rowCount != size) return false;
				for (var i = 0; i < size; i++)
					if (columns[i] != sum) return false;
				return true;
			}
			finally
			{
				pool.Return(columns);
			}
		}

		public static bool IsMagicSquare(this IEnumerable<IReadOnlyCollection<int>> square, int size, int sum, bool ignoreOversized = false)
		=> IsMagicSquareInternal(square, ref size, ref sum, columns =>
		{
			int rowCount = 0;
			foreach (var row in square)
			{
				if (ignoreOversized ? row.Count < size : row.Count != size) break;
				var rowSum = 0;
				var i = 0;
				foreach (var cell in row)
				{
					if (ignoreOversized && i == size)
						break;

					rowSum += cell;
					if (rowCount == 0)
						columns[i] = cell;
					else
						columns[i] += cell;
					++i;
				}
				if (rowCount == 0 && sum == 0) sum = rowSum;
				else if (sum != rowSum) break;
				++rowCount;

				if (ignoreOversized && rowCount == size) break;
			}
			return rowCount;
		});

		public static bool IsMagicSquare(this IReadOnlyCollection<IReadOnlyCollection<int>> square, int sum, bool ignoreOversized = false)
		=> IsMagicSquare(square, square?.Count ?? 0, sum, ignoreOversized);

		public static bool IsMagicSquare(this IReadOnlyCollection<IReadOnlyCollection<int>> square, bool ignoreOversized = false)
		=> IsMagicSquare(square, square?.Count ?? 0, 0, ignoreOversized);

		public static bool IsMagicSquare(this int[,] square)
		{
			var sizeX = square.GetLength(0);
			var sizeY = square.GetLength(1);
			return IsMagicSquare(
				square.Rows().Select(c => c.ToArray()).ToArray(),
				sizeX, 0, false);
		}

		public static IEnumerable<IReadOnlyList<int>> MagicSquares(
			this IEnumerable<IReadOnlyList<int>> source,
			int size,
			bool checkDistinct = false)
			=> checkDistinct
				? source.AsParallel().Where(e =>
				{
					using var rows = e.RowsBuffered(size).MemoizeUnsafe();
					return e.RowsBuffered(size).IsMagicSquare(size, 0, true) && rows.AllDistinct();
				})
				: source.Where(e => e.RowsBuffered(size).IsMagicSquare(size, 0, true));

		public static IEnumerable<IEnumerable<T>> Rows<T>(this T[,] source)
		{
			var sizeX = source.GetLength(0);
			var sizeY = source.GetLength(1);
			return Enumerable.Range(0, sizeY)
				.Select(y => Enumerable.Range(0, sizeX).Select(x => source[y, x]));
		}

		public static IEnumerable<T> Flat<T>(this T[,] source)
			=> source.Rows().SelectMany(e => e);

		public static IEnumerable<T[]> RowsBuffered<T>(this IEnumerable<T> source, int width)
		{
			var pool = ArrayPool<T>.Shared;
			var buffer = pool.Rent(width);
			var i = 0;
			try
			{
				foreach (var e in source)
				{
					buffer[i] = e;
					if (++i == width)
					{
						yield return buffer;
						i = 0;
					}
				}
				if (i != 0) throw new ArgumentException("The source values did not divide evenly into the width.");
			}
			finally
			{
				pool.Return(buffer);
			}
		}

		public static IEnumerable<T[]> RowConfigurations<T>(this IReadOnlyList<IEnumerable<T>> source)
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
				Debug.Assert(source.Count == count);

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

		public static IEnumerable<T[]> RowConfigurations<T>(this IReadOnlyList<IReadOnlyList<T>> rowPerms, ImmutableArray<ImmutableArray<int>> combinationGrid)
		{
			var pool = ArrayPool<T>.Shared;
			var result = pool.Rent(rowPerms.Count);
			try
			{
				foreach (var combination in combinationGrid)
				{
					for (var r = 0; r < rowPerms.Count; r++)
					{
						result[r] = rowPerms[r][combination[r]];
					}
					yield return result;
				}
			}
			finally
			{
				pool.Return(result);
			}
		}
	}
}
