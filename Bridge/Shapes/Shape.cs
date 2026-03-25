using Bridge.Renderers;

namespace Bridge.Shapes
{
    public abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer) => Renderer = renderer;

        public abstract void Draw();

        public void SetRenderer(IRenderer renderer) => Renderer = renderer;
    }
}