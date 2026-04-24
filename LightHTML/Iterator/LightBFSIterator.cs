namespace LightHTML.Iterator
{
    public class LightBFSIterator : ILightIterator
    {
        private readonly LightNode _root;
        private readonly Queue<LightNode> _queue = new();
        private LightNode? _current;

        public LightBFSIterator(LightNode root)
        {
            _root = root ?? throw new ArgumentNullException(nameof(root));
            _queue.Enqueue(_root);
        }

        public LightNode Current => _current ?? throw new InvalidOperationException("Перерахування не розпочато або завершено.");

        public bool MoveNext()
        {
            if (_queue.Count == 0)
                return false;

            _current = _queue.Dequeue();

            if (_current is LightElementNode element)
            {
                foreach (var child in element.Children)
                {
                    _queue.Enqueue(child);
                }
            }

            return true;
        }

        public void Reset()
        {
            _queue.Clear();
            _queue.Enqueue(_root);
            _current = null;
        }

        public void Dispose()
        {
            _queue.Clear();
        }
    }
}