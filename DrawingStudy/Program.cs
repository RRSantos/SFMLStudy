using SFMLCanvas;
using DrawingStudy.Canvas;

namespace DrawingStudy
{
    class Program
    {
        static void Main()
        {
            //Teste
            AbstractCanvas canvas = new FlowerCanvas();
            canvas.Run();
        }
    }
}
