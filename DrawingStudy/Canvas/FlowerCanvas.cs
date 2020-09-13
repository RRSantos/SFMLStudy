using SFML.Graphics;
using System.Collections.Generic;
using DrawingStudy.Draws;
using SFMLCanvas;

namespace DrawingStudy.Canvas
{
    public class FlowerCanvas : AbstractCanvas
    {   
        private const uint SCREEN_WIDTH = 1024;
        private const uint SCREEN_HEIGHT = 768;
        private const string WINDOW_TITLE = "Flowers";
        
        List<Shape> shapes;
        
        public FlowerCanvas() : base(SCREEN_WIDTH, SCREEN_HEIGHT, WINDOW_TITLE)
        {

        }
        protected override void Draw()
        {
            foreach (Shape shape in shapes)
            {   
                this.window.Draw(shape);
            }
            
        }

        protected override void Update()
        {   
            
        }

        protected override void Setup()
        {
            this.ClearWindowBeforeDraw = true;
            this.backgroundColor = Color.Black;

            Phyllotaxis phyllotaxis = new Phyllotaxis(6, 5.5f, SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);

            shapes = phyllotaxis.GetShapeList();
            
        }
        
    }
}
