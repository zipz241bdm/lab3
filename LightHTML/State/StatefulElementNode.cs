namespace LightHTML.State
{
    public class StatefulElementNode : LightElementNode
    {
        private IElementState _state;

        public StatefulElementNode(
            string tagName,
            DisplayType display = DisplayType.Block,
            ClosingType closing = ClosingType.WithClosingTag,
            IEnumerable<string>? cssClasses = null)
            : base(tagName, display, closing, cssClasses)
        {
            _state = new DraftState();
        }

        public void TransitionTo(IElementState newState)
        {
            _state = newState;
        }

        public void Mount() => _state.Mount(this);
        public void Freeze() => _state.Freeze(this);
        public void Unfreeze() => _state.Unfreeze(this);
        public void Detach() => _state.Detach(this);
        public void Reattach() => _state.Reattach(this);

        public override LightElementNode AddChild(LightNode node)
        {
            _state.AddChild(this, node);
            return this;
        }

        public override LightElementNode RemoveChild(LightNode node)
        {
            _state.RemoveChild(this, node);
            return this;
        }

        public override LightElementNode AddClass(string cls)
        {
            _state.AddClass(this, cls);
            return this;
        }

        public override LightElementNode AddStyle(string property, string value)
        {
            _state.AddStyle(this, property, value);
            return this;
        }

        public override void DispatchEvent(string eventType)
            => _state.DispatchEvent(this, eventType);

        public override string OuterHTML(int indent = 0)
            => _state.Render(this, indent);

        public void BaseAddChild(LightNode node) => base.AddChild(node);
        public void BaseRemoveChild(LightNode node) => base.RemoveChild(node);
        public void BaseAddClass(string cls) => base.AddClass(cls);
        public void BaseAddStyle(string prop, string val) => base.AddStyle(prop, val);
        public void BaseDispatchEvent(string ev) => base.DispatchEvent(ev);
        public string BaseOuterHTML(int indent) => base.OuterHTML(indent);
    }
}
