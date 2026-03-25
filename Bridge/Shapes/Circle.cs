using Bridge.Renderers;

namespace Bridge.Shapes
{
    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer) { }

        public override void Draw() => Renderer.RenderCircle();
    }
}