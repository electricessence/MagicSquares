using Open.Collections;
using Open.Hierarchy;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquares
{
    public class LazyNode<T> : IParent<LazyNode<T>>
        where T : struct
    {
        public LazyNode(T value, IEnumerable<T> values)
        {
            Value = value;
            var filtered = values
                .Where(v => !v.Equals(value)).Memoize();

            Children = filtered
                .Select(v => new LazyNode<T>(v, filtered)).Memoize();
        }

        public T Value { get; }

        public IReadOnlyList<LazyNode<T>> Children { get; }

        IReadOnlyList<object> IParent.Children => Children;
    }
}
