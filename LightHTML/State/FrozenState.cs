namespace LightHTML.State
{
    public sealed class FrozenState : ElementStateBase
    {
        public override string StateName => "Frozen";

        public override void DispatchEvent(StatefulElementNode ctx, string eventType) 
            => ctx.BaseDispatchEvent(eventType);

        public override string Render(StatefulElementNode ctx, int indent)
        {
            var pad = new string(' ', indent * 2);
            return $"{pad}\n{ctx.BaseOuterHTML(indent)}";
        }

        public override void Unfreeze(StatefulElementNode ctx)
        {
            Console.WriteLine($"Frozen -> Active - <{ctx.TagName}> розморожено.");
            ctx.TransitionTo(new ActiveState());
        }
    }
}