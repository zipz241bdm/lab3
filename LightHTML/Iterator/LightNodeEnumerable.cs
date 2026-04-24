using System.Collections;

namespace LightHTML.Iterator
{
    public class LightNodeEnumerable : IEnumerable<LightNode>
    {
        private readonly LightNode _root;
        private readonly TraversalType _traversal;

        public LightNodeEnumerable(LightNode root, TraversalType traversal)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _traversal = traversal;
        }

        public IEnumerator<LightNode> GetEnumerator()
        {
            return _traversal switch
            {
                TraversalType.BreadthFirst => new LightBFSIterator(_root),
                TraversalType.DepthFirst => new LightDFSIterator(_root),
                _ => throw new ArgumentOutOfRangeException(nameof(_traversal))
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}