namespace LightHTML.State
{
    public interface IElementState
    {
        string StateName { get; }

        void AddChild(StatefulElementNode ctx, LightNode child);
        void RemoveChild(StatefulElementNode ctx, LightNode child);
        void AddClass(StatefulElementNode ctx, string cssClass);
        void AddStyle(StatefulElementNode ctx, string property, string value);

        void DispatchEvent(StatefulElementNode ctx, string eventType);

        string Render(StatefulElementNode ctx, int indent);

        void Mount(StatefulElementNode ctx);
        void Freeze(StatefulElementNode ctx);
        void Unfreeze(StatefulElementNode ctx);
        void Detach(StatefulElementNode ctx);
        void Reattach(StatefulElementNode ctx);
    }
}