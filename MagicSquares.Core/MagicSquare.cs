using System;
using System.Diagnostics;

namespace MagicSquares.Core
{
	public static class MagicSquare
	{
		public static SquareMatrix<int> CreateFromFirst(int size, in int first)
		{
			if (size < 3) throw new ArgumentOutOfRangeException(nameof(size), size, "Must be at least 3.");

			var length = size * size;
			var bounds = first + length;
			var grid = new int[size, size];

			if (size % 2 == 1)
			{
				// Odd sizes are easy...

				var x = size / 2;
				var y = size - 1;
				//if (size % 2 == 0) --x;

				void Escape()
				{
					x -= 1;
					y -= 2;
				}

				void EnsureTargetCell()
				{
					while (true)
					{
						if (x == size && y == size) Escape();
						else if (x == size) x = 0;
						else if (y == size) y = 0;
						if (grid[x, y] == 0) break;
						else Escape();
						if (x < 0) x = size + x;
						if (y < 0) y = size + y;
					}
				}

				for (var i = first; i < bounds; i++)
				{
					EnsureTargetCell();
					grid[x, y] = i;
					++x;
					++y;
				}

			}
			else if (size % 4 == 0)
			{
				// Doubly even size.
				var fourth1 = size / 4;
				var fourth4 = fourth1 * 3;

				for (var i = 0; i < length; i++)
				{
					var x = i % size;
					var y = i / size;

					var xi = x >= fourth1 && x < fourth4;
					var yi = y >= fourth1 && y < fourth4;
					if (xi && !yi || !xi && yi)
					{
						x = size - x - 1;
						y = size - y - 1;
					}
					grid[x, y] = i + first;
				}
			}
			else
			{
				var half = size / 2;
				var fourth1 = size / 4;
				var fourth4 = size - fourth1 + 1;

				// Setup.
				for (var q = 0; q < 4; ++q)
				{
					var xt = 0;
					var yt = 0;
					switch (q)
					{
						case 2:
							xt = half;
							yt = half;
							break;

						case 1:
							xt = half;
							break;

						case 0:
							yt = half;
							break;
					}
					var msq = CreateFromFirst(half, q * half * half + first);
					for (var y = 0; y < half; ++y)
					{
						for (var x = 0; x < half; ++x)
						{
							grid[xt + x, yt + y] = msq[x, y];
						}
					}
				}

				// Shift all extra columns.
				int c;
				for (var r = 0; r < half; r++)
				{
					for (c = 0; c < fourth1 - 1; ++c)
					{
						SwapY(c, r, r + half);
					}
					for (c = fourth4; c < size; ++c)
					{
						SwapY(c, r, r + half);
					}
				}

				// Shift special.
				for (var r = 0; r < half; r++)
				{
					SwapY(r == fourth1 ? fourth1 : (fourth1 - 1), r, r + half);
				}

				void SwapY(in int x, in int y1, in int y2)
				{
					var a = grid[x, y1];
					var b = grid[x, y2];
					grid[x, y1] = b;
					grid[x, y2] = a;
				}
			}

			var square = SquareMatrix<int>.Create(grid, size);
			Debug.Assert(square.MagicSquareQuality()==MagicSquareQuality.True);
			return square;
		}
	}
}
