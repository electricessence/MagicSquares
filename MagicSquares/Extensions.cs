using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static bool IsMagicSquare(this IEnumerable<IReadOnlyList<int>> square)
        {
            if (square is null) throw new ArgumentNullException(nameof(square));

            int[]? columns = null;
            int colCount = 0;
            int rowCount = 0;
            int sum = 0;
            foreach (var row in square)
            {
                ++rowCount;
                if (columns == null)
                {
                    colCount = row.Count;
                    columns = new int[colCount];
                    sum = row.Sum();
                }
                else
                {
                    if (row.Count != colCount || sum != row.Sum()) return false;
                }

                for (var i = 0; i < colCount; i++)
                {
                    columns[i] += row[i];
                }
            }
            
            if (columns == null || rowCount != colCount) return false;
            return columns.All(value=>value==sum);
        }
    }
}
