using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Open.Collections;

namespace MagicSquares.Core
{
	public class Square
	{
		public Square(byte size)
		{
			Size = size;
            var sizeInt = (int)size;
			Length = (ushort)(sizeInt * sizeInt);

			var values = Enumerable.Range(0, Length).ToImmutableArray();
			Permutations = values
				.PermutationsBuffered()
				.Select(p=>p.Take(Length).ToImmutableArray())
				.Select(p=>GetPermutation(p))
				.Memoize();

			UniquePermutations = Permutations.Where(c => c.IsPrimary).Memoize();
		}

		public byte Size { get; }

		public ushort Length { get; }

		public IReadOnlyList<Permutation> Permutations { get; }

		public IReadOnlyList<Permutation> UniquePermutations { get; }

		public class Permutation
		{
			internal Permutation(Square square, SquareMatrix<int> matrix)
			{
				Square = square ?? throw new ArgumentNullException(nameof(square));
				if (matrix.Length != Square.Length) throw new ArgumentException($"Length of values ({matrix.Length}) does not match expected ({Square.Length}).", nameof(matrix));

				Matrix = matrix;
				Hash = Matrix.ToMatrixString();
				var variations = new Lazy<SquareMatrix<int>[]>(() =>
				{
					var variations = GetVariations().ToArray();
					Array.Sort(variations);
					return variations;
				});

				_isPrimary = new Lazy<bool>(() => variations.Value[0].Equals(Matrix));

				Group = new Lazy<IReadOnlyList<Lazy<Permutation>>>(() =>
				{
					if (_isPrimary.Value)
					{
						return variations.Value.Select(v => new Lazy<Permutation>(() => Square.GetPermutation(v))).Memoize();
					}
					else
					{
						return Square.GetPermutation(variations.Value[0]).Group.Value;
					}
				});
			}

			public Square Square { get; }

			public SquareMatrix<int> Matrix { get; }

			public Lazy<IReadOnlyList<Lazy<Permutation>>> Group { get; }

			public Permutation Primary => Group.Value[0].Value;

			public string Hash { get; }

			readonly Lazy<bool> _isPrimary;
			public bool IsPrimary => _isPrimary.Value;

			public override string ToString() => Hash;

			public IEnumerable<SquareMatrix<int>> GetVariations()
			{
				var values = Matrix;
				yield return values;
				SquareMatrix<int> mirror = values.GetMirrorX().ToImmutableArray();
				yield return mirror;
				for (var i = 0; i < 3; i++)
				{
					values = values.GetRotatedCW().ToImmutableArray();
					yield return values;
					mirror = mirror.GetRotatedCW().ToImmutableArray();
					yield return mirror;
				}
			}
		}


		readonly ConcurrentDictionary<string, Lazy<Permutation>> Registry = new();

        public Permutation GetPermutation(SquareMatrix<int> values)
			=> Registry.GetOrAdd(values.ToMatrixString(), key => new Lazy<Permutation>(() => new Permutation(this, values))).Value;

		public Permutation GetPermutation(IEnumerable<IEnumerable<int>> values, bool ignoreOversize = false)
			=> GetPermutation(SquareMatrix<int>.Create(values, Size, ignoreOversize));
	}
}
