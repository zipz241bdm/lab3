namespace LightHTML.State
{
    public abstract class ElementStateBase : IElementState
    {
        public abstract string StateName { get; }

        public virtual void AddChild(StatefulElementNode ctx, LightNode child)
            => Warn($"AddChild заблоковано у стані {StateName}.");

        public virtual void RemoveChild(StatefulElementNode ctx, LightNode child)
            => Warn($"RemoveChild заблоковано у стані {StateName}.");

        public virtual void AddClass(StatefulElementNode ctx, string cssClass)
            => Warn($"AddClass заблоковано у стані {StateName}.");

        public virtual void AddStyle(StatefulElementNode ctx, string property, string value)
            => Warn($"AddStyle заблоковано у стані {StateName}.");

        public virtual void DispatchEvent(StatefulElementNode ctx, string eventType)
            => Warn($"DispatchEvent(\"{eventType}\") ігнорується у стані {StateName}: елемент не в DOM.");

        public virtual string Render(StatefulElementNode ctx, int indent)
            => ctx.BaseOuterHTML(indent);

        public virtual void Mount(StatefulElementNode ctx) => Warn($"Mount()    недоступний у стані {StateName}.");
        public virtual void Freeze(StatefulElementNode ctx) => Warn($"Freeze()   недоступний у стані {StateName}.");
        public virtual void Unfreeze(StatefulElementNode ctx) => Warn($"Unfreeze() недоступний у стані {StateName}.");
        public virtual void Detach(StatefulElementNode ctx) => Warn($"Detach()   недоступний у стані {StateName}.");
        public virtual void Reattach(StatefulElementNode ctx) => Warn($"Reattach() недоступний у стані {StateName}.");

        protected static void Warn(string message)
            => Console.WriteLine($"State - {message}");
    }
}