using SFML.Graphics;
using System.Collections.Generic;
using DrawingStudy.Draws;
using SFMLCanvas;

namespace DrawingStudy.Canvas
{
    public class CollatzConjectureCanvas : AbstractCanvas
    {   
        private const uint SCREEN_WIDTH = 1600;
        private const uint SCREEN_HEIGHT = 1000;
        private const string WINDOW_TITLE = "Collatz Conjecture";
        
        private List<Shape> shapes;
        private VertexArray vertexArray;


        public CollatzConjectureCanvas() : base(SCREEN_WIDTH, SCREEN_HEIGHT, WINDOW_TITLE)
        {

        }
        protected override void Draw()
        {
            foreach (Shape shape in shapes)
            {   
                this.window.Draw(shape);
            }
            this.window.Draw(vertexArray);

        }

        protected override void Update()
        {   
            
        }

        protected override void Setup()
        {
            this.ClearWindowBeforeDraw = true;
            this.backgroundColor = Color.Black;

            CollatzConjecture collatz = new CollatzConjecture(SCREEN_WIDTH, SCREEN_HEIGHT);

            

            shapes = collatz.GetShapeList(6171);
            vertexArray = new VertexArray(PrimitiveType.LineStrip, (uint)shapes.Count);
            for (int i = 0; i < shapes.Count; i++)
            {
                Vertex line = new Vertex(shapes[i].Position, shapes[i].FillColor);
                vertexArray.Append(line);
                
            }


        }
        
    }
}
