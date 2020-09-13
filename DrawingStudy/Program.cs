using SFMLCanvas;
using DrawingStudy.Canvas;

namespace DrawingStudy
{
    class Program
    {
        static void Main()
        {
            AbstractCanvas canvas = new FlowerCanvas();
            canvas.Run();
        }
    }
}
