using Open.Disposable;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace MagicSquares.Core;

public readonly struct SquareMatrix<T>
	: IReadOnlyList<T>, IComparable<SquareMatrix<T>>
	where T : IComparable<T>
{
	public SquareMatrix(ReadOnlyMemory<T> vector, int size)
	{
		var len = size * size;
		if (vector.Length != len) throw new ArgumentException($"The size ({size}) does not match the vector length ({vector.Length}).");
		Vector = vector;
		Size = size;
	}

	public SquareMatrix(ReadOnlyMemory<T> vector) : this(vector, GetSquareRoot(vector.Length))
	{
	}

	public SquareMatrix<TResult> Transform<TResult>(Func<T, TResult> transform)
		where TResult : IComparable<TResult>
	{
		var len = Vector.Length;
		var span = Vector.Span;
		var transformed = new TResult[len];
		for(var i = 0; i<len; ++i)
			transformed[i] = transform(span[i]);

		return new(transformed, Size);
	}

	public static SquareMatrix<T> Create(IEnumerable<T> vector)
	{
		ArgumentNullException.ThrowIfNull(vector);
		var a = vector.ToArray();
		var size = GetSquareRoot(a.Length);
		return new(a, size);
	}

	static SquareMatrix<T> CreateCore(T[,] matrix, int size)
	{
		var len = size * size;
		var vector = new T[len];
		for (var y = 0; y < size; ++y)
		{
			for (var x = 0; x < size; ++x)
			{
				vector[y * size + x] = matrix[x, y];
			}
		}

		return new SquareMatrix<T>(vector, size);
	}

	public static SquareMatrix<T> Create(T[,] matrix, int size)
	{
		ArgumentNullException.ThrowIfNull(matrix);
		if (matrix.GetLength(0) != size)
			throw new ArgumentException("Matrix width does not match the size.");
		if (matrix.GetLength(1) != size)
			throw new ArgumentException("Matrix height does not match the size.");

		return CreateCore(matrix, size);
	}

	public static SquareMatrix<T> Create(T[,] matrix)
	{
		ArgumentNullException.ThrowIfNull(matrix);
		var size = matrix.GetLength(0);
		if (size != matrix.GetLength(1)) throw new ArgumentException("Matrix is not square.");

		return CreateCore(matrix, size);
	}

	public static SquareMatrix<T> Create(IEnumerable<IEnumerable<T>> rows, int size, bool ignoreOversize = false)
	{
		var len = size * size;
		var vector = new T[len];

		var y = 0;
		foreach (var row in rows)
		{
			if (y == size)
			{
				if (!ignoreOversize) throw new ArgumentException($"Row count is greater than the size ({size}).");
				break;
			}
			var x = 0;
			foreach (var cell in row)
			{
				if (x == size)
				{
					if (!ignoreOversize) throw new ArgumentException($"Cell count is greater than the size ({size}).");
					break;
				}
				vector[y * size + x] = cell;
				++x;
			}
			++y;
		}
		return new SquareMatrix<T>(vector, size);
	}

	public static SquareMatrix<T> Create(T[] vector, int size)
	{
		ArgumentNullException.ThrowIfNull(vector);
		return new(vector.ToArray() /* make a copy to avoid changes */, size);
	}

	static int GetSquareRoot(int length)
	{
		var sqrt = Math.Sqrt(length);
		if (Math.Floor(sqrt) != sqrt) throw new ArgumentException("Vector is not square.");
		return (int)sqrt;
	}

	public IEnumerator<T> GetEnumerator()
	{
		var len = Vector.Length;
		for(var i = 0; i < len; ++i)
			yield return Vector.Span[i];
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public int CompareToVector(ReadOnlyMemory<T> other)
	{
		var len = Vector.Length;
		if (len != other.Length) throw new ArgumentException("Comparison is incompatible. Lengths are different.");
		var span = Vector.Span;
		var otherSpan = other.Span;
		for (var i = 0; i < len; ++i)
		{
			var a = span[i];
			var b = otherSpan[i];
			if (a == null) return b == null ? 0 : -b.CompareTo(a);
			var c = a.CompareTo(b);
			if (c != 0) return c;
		}
		return 0;
	}

	public int CompareTo(SquareMatrix<T> other) => CompareToVector(other.Vector);

	public ReadOnlyMemory<T> Vector { get; }

	public int Size { get; }

	public int Length => Vector.Length;

	int IReadOnlyCollection<T>.Count => Length;

	public T this[int index] => Vector.Span[index];

	public T this[int x, int y] => Vector.Span[y * Size + x];

	public static implicit operator ReadOnlyMemory<T>(SquareMatrix<T> square) => square.Vector;

	public static implicit operator SquareMatrix<T>(ReadOnlyMemory<T> vector) => new(vector);
}

public static class SquareMatrixExtensions
{
	static IEnumerable<T> RowCore<T>(ReadOnlyMemory<T> vector, int size, int index)
	{
		for (var x = 0; x < size; ++x)
			yield return vector.Span[index * size + x];
	}

	static IEnumerable<T> ColumnCore<T>(ReadOnlyMemory<T> vector, int size, int index)
	{
		for (var y = 0; y < size; ++y)
			yield return vector.Span[y * size + index];
	}

	public static IEnumerable<T> Row<T>(this SquareMatrix<T> square, int index)
		where T : IComparable<T>
	{
		if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), index, "Must be at least zero.");
		var size = square.Size;
		if (index < size) return RowCore(square.Vector, size, index);
		throw new ArgumentOutOfRangeException(nameof(index), index, "Must be less than the size.");
	}

	public static IEnumerable<T> Column<T>(this SquareMatrix<T> square, int index)
		where T : IComparable<T>
	{
		if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), index, "Must be at least zero.");
		var size = square.Size;
		if (index < size) return ColumnCore(square.Vector, size, index);
		throw new ArgumentOutOfRangeException(nameof(index), index, "Must be less than the size.");
	}

	public static IEnumerable<IEnumerable<T>> Rows<T>(this SquareMatrix<T> square)
		where T : IComparable<T>
	{
		var size = square.Size;
		for (var y = 0; y < size; ++y)
			yield return Row(square, y);
	}

	public static IEnumerable<IEnumerable<T>> Columns<T>(this SquareMatrix<T> square)
		where T : IComparable<T>
	{
		var size = square.Size;
		for (var x = 0; x < size; ++x)
			yield return Column(square, x);
	}

	public static string ToMatrixString(this SquareMatrix<int> square)
		=> string.Join('\n', square.Rows().Select(r => string.Join(' ', r)));

	public static IEnumerable<T> GetMirrorX<T>(this SquareMatrix<T> square)
		where T : IComparable<T>
	{
		var size = square.Size;
		for (var y = 0; y < size; ++y)
		{
			for (var x = size - 1; x >= 0; --x)
			{
				yield return square[x, y];
			}
		}
	}

	public static IEnumerable<T> GetRotatedCW<T>(this SquareMatrix<T> square)
		where T : IComparable<T>
	{
		var size = square.Size;
		for (var x = 0; x < size; ++x)
		{
			for (var y = size - 1; y >= 0; --y)
			{
				yield return square[x, y];
			}
		}
	}

	public static IEnumerable<string> ToDisplayRowStrings<T>(this SquareMatrix<T> square)
		where T : IComparable<T>
	{
		var pool = ListPool<string[]>.Shared;
		var table = pool.Take();
		var size = square.Size;
		var colWidth = new int[size];
		for (var y = size - 1; y >= 0; --y)
		{
			var row = new string[size];
			for (var x = 0; x < size; ++x)
			{
				var v = square[x, y]?.ToString();
				Debug.Assert(v != null);
				colWidth[x] = Math.Max(colWidth[x], v!.Length);
				row[x] = v;
			}

			table.Add(row);
		}

		for (var c = 0; c < size; ++c)
		{
			var width = colWidth[c];
			for (var r = 0; r < size; ++r)
			{
				var value = table[r][c];
				var diff = width - value.Length;
				table[r][c] = new string(' ', diff) + value;
			}
		}

		foreach (var row in table)
			yield return string.Join(' ', row);

		pool.Give(table);
	}

	public static IEnumerable<string> ToDisplayRowStringsWithTotals(this SquareMatrix<int> square)
	{
		var pool = ListPool<string[]>.Shared;
		var table = pool.Take();
		var size = square.Size;
		var colSum = new int[size];
		for (var y = size - 1; y >= 0; --y)
		{
			var rowSum = 0;
			var row = new string[size + 2];
			for (var x = 0; x < size; ++x)
			{
				var v = square[x, y];
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

		pool.Give(table);
		yield return string.Join('-', divider);

		yield return string.Join(' ', colSumRow);
	}

	public static void OutputToConsole<T>(this SquareMatrix<T> square)
		where T : IComparable<T>
	{
		foreach (var row in square.ToDisplayRowStrings())
		{
			Console.WriteLine(row);
		}
	}

	public static void OutputToConsoleWithTotals(this SquareMatrix<int> square)
	{
		foreach (var row in square.ToDisplayRowStringsWithTotals())
		{
			Console.WriteLine(row);
		}
	}

	public static MagicSquareQuality MagicSquareQuality(this SquareMatrix<int> square, int sum = 0)
	{
		var pool = ArrayPool<int>.Shared;
		var size = square.Size;
		var columns = pool.Rent(size);
		var right = 0;
		var left = 0;
		try
		{
			for (var y = 0; y < size; ++y)
			{
				var rowSum = 0;
				for (var x = 0; x < size; ++x)
				{
					var cell = square[x, y];
					rowSum += cell;
					if (y == 0) columns[x] = cell;
					else columns[x] += cell;
					if (x == y) right += cell;
					if (size - x - 1 == y) left += cell;
				}

				if (sum == 0) sum = rowSum;
				else if (rowSum != sum) return Core.MagicSquareQuality.Failed;
			}

			for (var i = 0; i < size; i++)
				if (columns[i] != sum) return Core.MagicSquareQuality.Failed;

			if (left != sum || right != sum) return Core.MagicSquareQuality.Semi;

			return Core.MagicSquareQuality.True;
		}
		finally
		{
			pool.Return(columns);
		}
	}
}
