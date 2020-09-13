using SFML.Graphics;
using System.Collections.Generic;
using DrawingStudy.Draws;
using SFMLCanvas;

namespace DrawingStudy.Canvas
{
    public class TenPrintCanvas : AbstractCanvas
    {
        
        private const uint SCREEN_WIDTH = 800;
        private const uint SCREEN_HEIGHT = 600;
        private const float SHAPE_WIDTH = 10;        
        private const string WINDOW_TITLE = "Ten Print Canvas";
        
        private float actualX;
        private float actualY;

        TenPrint tenPrint;


        List<VertexArray> shapes;


        public TenPrintCanvas() : base(SCREEN_WIDTH, SCREEN_HEIGHT, WINDOW_TITLE)
        {

        }
        protected override void Draw()
        {
            foreach (VertexArray shape in shapes)
            {
                this.window.Draw(shape);
            }
        }

        protected override void Update()
        {
            if (actualY < SCREEN_HEIGHT)
            {
                VertexArray newShape = tenPrint.GetRandomShape(actualX, actualY);
                shapes.Add(newShape);
                actualX += SHAPE_WIDTH;
                if (actualX > SCREEN_WIDTH)
                {
                    actualX = 0;
                    actualY += SHAPE_WIDTH;
                }
            }
            
        }

        protected override void Setup()
        {
            this.backgroundColor = new Color(70, 70, 70);
            this.window.SetFramerateLimit(60);
            
            tenPrint = new TenPrint(SHAPE_WIDTH, 0.05f, new Color(115, 215, 255));
            shapes = new List<VertexArray>();
            actualX = 0;
            actualY = 0;
        }
        
    }
}
