namespace LightHTML.Iterator
{
    public class LightDFSIterator : ILightIterator
    {
        private readonly LightNode _root;
        private readonly Stack<LightNode> _stack = new();
        private LightNode? _current;

        public LightDFSIterator(LightNode root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _stack.Push(_root);
        }

        public LightNode Current => _current ?? throw new InvalidOperationException("Перерахування не розпочато або завершено.");

        public bool MoveNext()
        {
            if (_stack.Count == 0)
                return false;

            _current = _stack.Pop();

            if (_current is LightElementNode element)
            {
                for (int i = element.Children.Count - 1; i >= 0; i--)
                {
                    _stack.Push(element.Children[i]);
                }
            }

            return true;
        }

        public void Reset()
        {
            _stack.Clear();
            _stack.Push(_root);
            _current = null;
        }

        public void Dispose()
        {
            _stack.Clear();
        }
    }
}
