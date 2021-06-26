using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicSquareCreator
{
	public class MagicSquare
	{
		public static int[,] CreateFromFirst(int size, int first)
		{
			var bounds = first + size * size;
			var grid = new int[size, size];
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

			return grid;
		}
	}
}
