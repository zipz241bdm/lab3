using Bridge.Shapes;
using Bridge.Renderers;

namespace Bridge
{
    class Program
    {
        static void Main()
        {
            IRenderer vector = new VectorRenderer();
            IRenderer raster = new RasterRenderer();

            Console.WriteLine("Векторний рендерер: ");
            Shape circle = new Circle(vector);
            Shape square = new Square(vector);
            Shape triangle = new Triangle(vector);

            circle.Draw();
            square.Draw();
            triangle.Draw();

            Console.WriteLine("\nРастровий рендерер: ");
            circle = new Circle(raster);
            square = new Square(raster);
            triangle = new Triangle(raster);

            circle.Draw();
            square.Draw();
            triangle.Draw();

            Console.WriteLine("\nЗміна рендерера: ");
            Shape dynamicCircle = new Circle(vector);
            Console.Write("До зміни: "); dynamicCircle.Draw();

            dynamicCircle.SetRenderer(raster);
            Console.Write("Після зміни: "); dynamicCircle.Draw();
        }
    }
}