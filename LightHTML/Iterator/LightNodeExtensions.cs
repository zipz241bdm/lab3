namespace LightHTML.Iterator
{
    public static class LightNodeExtensions
    {
        public static IEnumerable<LightNode> AsEnumerable(this LightNode root, TraversalType traversal)
        {
            return new LightNodeEnumerable(root, traversal);
        }

        public static void Traverse(this LightNode root, TraversalType traversal, Action<LightNode, int> visitor)
        {
            int index = 0;
            foreach (var node in root.AsEnumerable(traversal))
            {
                visitor(node, index++);
            }
        }
    }
}
