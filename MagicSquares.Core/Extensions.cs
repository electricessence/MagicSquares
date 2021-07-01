using Open.Disposable;
using System;
using System.Buffers;
using System.Collections.Generic;
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
				square.Rows().Select(c=>c.ToArray()).ToArray(),
				sizeX, 0, false);
		}

		public static IEnumerable<IEnumerable<T>> Columns<T>(this T[,] source)
		{
			var sizeX = source.GetLength(0);
			var sizeY = source.GetLength(1);
			return Enumerable.Range(0, sizeY)
				.Select(y => Enumerable.Range(0, sizeX).Select(x => source[x, y]));
		}

		public static IEnumerable<IEnumerable<T>> Rows<T>(this T[,] source)
		{
			var sizeX = source.GetLength(0);
			var sizeY = source.GetLength(1);
			return Enumerable.Range(0, sizeY)
				.Select(y => Enumerable.Range(0, sizeX).Select(x => source[y, x]));
		}

		public static IEnumerable<T> Row<T>(this T[,] source, int index)
		{
			var cells = source.GetLength(1);
			for (var i = 0; i < cells; ++i) yield return source[index, i];
		}

		public static IEnumerable<T> Column<T>(this T[,] source, int index)
		{
			var rows = source.GetLength(0);
			for (var i = 0; i < rows; ++i) yield return source[i, index];
		}

		public static IEnumerable<string> ToRowStrings(this int[,] xyGrid, int size)
		{
			var table = new List<string[]>();
			var colSum = new int[size];
			for (var y = size - 1; y >= 0; --y)
			{
				var rowSum = 0;
				var row = new string[size + 2];
				for (var x = 0; x < size; ++x)
				{
					var v = xyGrid[x, y];
					rowSum += v;
					colSum[x] += v;
					row[x] = v.ToString();
				}
				row[^2] = "=";
				row[^1] = rowSum.ToString();
				table.Add(row);
			}
			var divider = new string[size];
			var colSumRow = colSum.Select(s => s.ToString()).ToArray();

			for (var c = 0; c < size; ++c)
			{
				var width = colSumRow[c].Length;
				divider[c] = new string('-', width);
				for (var r = 0; r < size; ++r)
				{
					var value = table[r][c];
					var diff = width - value.Length;
					table[r][c] = new string(' ', diff) + value;
				}
			}

			foreach (var row in table)
				yield return string.Join(' ', row);

			table.Clear();
			yield return string.Join('-', divider);

			yield return string.Join(' ', colSumRow);
		}

		public static void OutputToConsole(this int[,] xyGrid, int size)
		{
			var table = xyGrid.ToRowStrings(size);

			foreach (var row in table)
			{
				Console.WriteLine(row);
			}
		}
	}
}
