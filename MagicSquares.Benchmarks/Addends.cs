using BenchmarkDotNet.Attributes;
using Open.Numeric;

namespace MagicSquares.Benchmarks
{
	public class Addends
	{
		[Params(6, 81, 256)]
		public int N;

		[Params(3, 4, 7, 13)]
		public int C;

		readonly PossibleAddends Cache = new();
        readonly PossibleAddends TempCache = new();

		[Benchmark]
		public void Uncached() => Cache.UniqueAddendsFor(N, C);

		[Benchmark]
		public void Cached() => TempCache.UniqueAddendsFor(N, C);

		[GlobalCleanup]
		public void GlobalCleanup() => TempCache.Dispose();
	}
}
