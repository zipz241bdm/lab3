namespace Bridge.Renderers
{
    public class VectorRenderer : IRenderer
    {
        public void RenderCircle() => Console.WriteLine("Малювання кола у вигляді векторної графіки");
        public void RenderSquare() => Console.WriteLine("Малювання квадрата у вигляді векторної графіки");
        public void RenderTriangle() => Console.WriteLine("Малювання трикутника у вигляді векторної графіки");
    }
}