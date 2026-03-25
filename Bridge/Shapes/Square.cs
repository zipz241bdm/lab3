using Bridge.Renderers;

namespace Bridge.Shapes
{
    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) { }

        public override void Draw() => Renderer.RenderSquare();
    }
}