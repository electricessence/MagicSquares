using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicSquares.Core
{
	internal class ListComparer<T> : IComparer<IReadOnlyList<T>>
		where T : IComparable<T>
	{
		private readonly int _length;

		public ListComparer(int length)
		{
			_length = length;
		}

		public int Compare(IReadOnlyList<T>? x, IReadOnlyList<T>? y)
		{
			if (x is null) throw new ArgumentNullException(nameof(x));
			if (y is null) throw new ArgumentNullException(nameof(y));

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
}
