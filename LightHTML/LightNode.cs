namespace LightHTML
{
    public abstract class LightNode
    {
        public abstract string OuterHTML(int indent = 0);
        public abstract string InnerHTML(int indent = 0);

        protected string Indent(int level) => new string(' ', level * 2);
    }
}