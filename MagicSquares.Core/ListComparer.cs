using System;
using System.Collections.Generic;

namespace MagicSquares.Core;

internal class ListComparer<T> : IComparer<IReadOnlyList<T>>
	where T : IComparable<T>
{
	private readonly int _length;

	public ListComparer(int length)
		=> _length = length;

	public int Compare(IReadOnlyList<T>? x, IReadOnlyList<T>? y)
	{
		// This returns 1 when x is greater than y and returns -1 when x is less than y.
		if (x is null) return y is null ? 0 : -1;
		if (y is null) return 1;

		for (var i = 0; i < _length; ++i)
		{
			var a = x[i];
			var b = y[i];
			if (a == null) return b == null ? 0 : -b.CompareTo(a);
			var c = a.CompareTo(b);
			if (c != 0) return c;
		}
		return 0;
	}
}
