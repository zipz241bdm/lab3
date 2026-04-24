namespace LightHTML.State
{
    public sealed class DraftState : ElementStateBase
    {
        public override string StateName => "Draft";

        public override void AddChild(StatefulElementNode ctx, LightNode child)
        {
            ctx.BaseAddChild(child);
            Console.WriteLine($"Draft - Додано дочірній вузол до <{ctx.TagName}>.");
        }

        public override void RemoveChild(StatefulElementNode ctx, LightNode child)
        {
            ctx.BaseRemoveChild(child);
            Console.WriteLine($"Draft - Вилучено дочірній вузол з <{ctx.TagName}>.");
        }

        public override void AddClass(StatefulElementNode ctx, string cssClass)
        {
            ctx.BaseAddClass(cssClass);
            Console.WriteLine($"Draft - Клас \"{cssClass}\" -> <{ctx.TagName}>.");
        }

        public override void AddStyle(StatefulElementNode ctx, string property, string value)
        {
            ctx.BaseAddStyle(property, value);
            Console.WriteLine($"Draft - Стиль \"{property}:{value}\" -> <{ctx.TagName}>.");
        }

        public override string Render(StatefulElementNode ctx, int indent)
        {
            var pad = new string(' ', indent * 2);
            return $"{pad}<!-- DRAFT <{ctx.TagName}> -->\n{ctx.BaseOuterHTML(indent)}";
        }

        public override void Mount(StatefulElementNode ctx)
        {
            Console.WriteLine($"Draft -> Active - <{ctx.TagName}> змонтовано в DOM.");
            ctx.TransitionTo(new ActiveState());
        }
    }
}