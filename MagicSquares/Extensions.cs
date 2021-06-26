using Open.Disposable;
using System;
using System.Buffers;
using System.Collections.Generic;

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
        => HashSetPool<T>.Instance.Rent(d =>
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
                if (ignoreOversized ? row.Count<size : row.Count != size) break;
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

    }
}
