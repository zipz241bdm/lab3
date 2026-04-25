namespace LightHTML
{
    public abstract class LightNode
    {
        public abstract string OuterHTML(int indent = 0);
        public abstract string InnerHTML(int indent = 0);

        public abstract void Accept(Visitor.ILightNodeVisitor visitor);

        protected string Indent(int level) => new string(' ', level * 2);

        protected virtual void OnCreated() { }

        protected virtual void OnInserted(LightNode child) { }

        protected virtual void OnRemoved(LightNode child) { }

        protected virtual void OnStylesApplied(string property, string value) { }

        protected virtual void OnClassListApplied(string cssClass) { }

        protected virtual void OnTextRendered(string text) { }
    }
}