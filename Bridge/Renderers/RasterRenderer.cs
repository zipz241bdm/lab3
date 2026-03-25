namespace Bridge.Renderers
{
    public class RasterRenderer : IRenderer
    {
        public void RenderCircle() => Console.WriteLine("Малювання кола у вигляді пікселів");
        public void RenderSquare() => Console.WriteLine("Малювання квадрата у вигляді пікселів");
        public void RenderTriangle() => Console.WriteLine("Малювання трикутника у вигляді пікселів");
    }
}