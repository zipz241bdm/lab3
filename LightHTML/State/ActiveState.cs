namespace LightHTML.State
{
    public sealed class ActiveState : ElementStateBase
    {
        public override string StateName => "Active";

        public override void AddChild(StatefulElementNode ctx, LightNode child) => ctx.BaseAddChild(child);
        public override void RemoveChild(StatefulElementNode ctx, LightNode child) => ctx.BaseRemoveChild(child);
        public override void AddClass(StatefulElementNode ctx, string cssClass) => ctx.BaseAddClass(cssClass);
        public override void AddStyle(StatefulElementNode ctx, string property, string value) => ctx.BaseAddStyle(property, value);

        public override void DispatchEvent(StatefulElementNode ctx, string eventType)
            => ctx.BaseDispatchEvent(eventType);

        public override void Freeze(StatefulElementNode ctx)
        {
            Console.WriteLine($"Active -> Frozen - <{ctx.TagName}> заморожено (Read-Only).");
            ctx.TransitionTo(new FrozenState());
        }

        public override void Detach(StatefulElementNode ctx)
        {
            Console.WriteLine($"Active -> Detached - <{ctx.TagName}> від'єднано від DOM.");
            ctx.TransitionTo(new DetachedState());
        }
    }
}
