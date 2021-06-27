using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicSquareCreator
{
	public class MagicSquare
	{

		static readonly int[,] PX = new int[,] { { 0, 2 }, { 3, 1 } };
		static readonly int[,] PY = new int[,] { { 3, 2 }, { 0, 1 } };
		static readonly int[,] PZ = new int[,] { { 0, 1 }, { 2, 3 } };

		public static int[,] CreateFromFirst(int size, in int first)
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
				var fourth = size / 2;

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

				// Swap diagnals...
				for (var c = 0; c < half - 1; ++c)
				{
					var y2 = half - c - 1;
					var y3 = size - y2 - 1;
					var y4 = size - c - 1;

					SwapY(c, c, y3);
					if (c != y2) SwapY(c, y2, y4);
				}

				void SwapY(in int x, in int y1, in int y2)
				{
					var a = grid[x, y1];
					var b = grid[x, y2];
					grid[x, y1] = b;
					grid[x, y2] = a;
				}
			}

			return grid;
		}
	}
}
