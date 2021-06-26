using Open.Collections;
using Open.Disposable;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace MagicSquares
{
    class Program
    {
        static void Main()
        {

            const byte size = 6;
            const int first = 34;
            const int last = 500;
            var combinations = new Combinations();
            var square = new Square(size, combinations);
            var sums = new PossibleAddens();
            var len = square.Length;
            Console.WriteLine("Searching for {0} x {0} ({1} unique) Magic Square...", size, len);


            Parallel.For(first, last + 1, new ParallelOptions
            {
                MaxDegreeOfParallelism = 3 // Don't bloat memory unneccesarily...
            }, n =>
            //for (var n = first; n < last + 1; ++n)
            {
                lock (square) Console.WriteLine("Testing possible sum {0}...", n);
                var s = sums.UniqueAddensFor(n, size);
                if (s.Count < square.Length) return;

                var count = 0;
                // Get N rows and test them.
                Parallel.ForEach(s.GetSubsetsImmutable(size), (rows, loopState) =>
                {
                    if (!rows.SelectMany(e => e).AllDistinct()) return;
                    HashSetPool<Square.Combination>.Instance.Rent(p =>
                    {
                        var c = rows.Select(r => combinations.GetMemoizedCombinations(r)).Memoize();
                        foreach (var combo in Arrangments(c).Skip(1).Where(a => a.IsMagicSquare(size, n, true)))
                        {
                            var ms = square.GetCombination(combo.Take(size).SelectMany(e => e).ToImmutableArray());
                            var pCombo = ms.Group.Value[0].Value; // Get the normalized version of the matrix.
                            if (!p.Add(pCombo)) continue;

                            lock (square)
                            {
                                if (count == 0) loopState.Break();
                                Console.WriteLine();
                                Console.Write("{0} ({1})", n, ++count);
                                for (var i = 0; i < len; i++)
                                {
                                    if (i % size == 0) Console.WriteLine();
                                    else Console.Write(' ');
                                    Console.Write(pCombo.Values[i]);
                                }
                                Console.WriteLine();
                            }
                        }
                    });
                });

                lock (square)
                {
                    if (count == 0)
                    {
                        Console.WriteLine("None found for {0}.",n);
                    }
                    else
                    {
                        Console.WriteLine("[Press the enter key to continue]");
                        Console.ReadLine();
                    }

                }

            }
            );

            Console.WriteLine("Done.");
        }

        static IEnumerable<T[]> Arrangments<T>(IReadOnlyList<IEnumerable<T>> source)
        {
            var listPool = ListPool<IEnumerator<T>>.Instance;
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

                bool GetNext() => ListPool<int>.Instance.Rent(reset =>
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
    }
}
