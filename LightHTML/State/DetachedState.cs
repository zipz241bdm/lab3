namespace LightHTML.State
{
    public sealed class DetachedState : ElementStateBase
    {
        public override string StateName => "Detached";

        public override void AddChild(StatefulElementNode ctx, LightNode child) => ctx.BaseAddChild(child);
        public override void RemoveChild(StatefulElementNode ctx, LightNode child) => ctx.BaseRemoveChild(child);
        public override void AddClass(StatefulElementNode ctx, string cssClass) => ctx.BaseAddClass(cssClass);
        public override void AddStyle(StatefulElementNode ctx, string property, string value) => ctx.BaseAddStyle(property, value);

        public override string Render(StatefulElementNode ctx, int indent)
        {
            var pad = new string(' ', indent * 2);
            return $"{pad}\n{ctx.BaseOuterHTML(indent)}";
        }

        public override void Reattach(StatefulElementNode ctx)
        {
            Console.WriteLine($"Detached -> Active - <{ctx.TagName}> приєднано назад до DOM.");
            ctx.TransitionTo(new ActiveState());
        }
    }
}
