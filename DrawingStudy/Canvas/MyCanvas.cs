using SFML.Graphics;
using SFML.System;
using System;
using SFMLCanvas;

namespace DrawingStudy.Canvas
{
    public class MyCanvas : AbstractCanvas
    {
        
        private const uint SCREEN_WIDTH = 800;
        private const uint SCREEN_HEIGHT = 600;
        private const string WINDOW_TITLE = "This is MyCanvas";
        private readonly float rectangleWidth = 5;
        private readonly float rectangleHeigth = 5;
        RectangleShape shape;

        private float xVelocity;
        private float yVelocity;
        public MyCanvas() : base(SCREEN_WIDTH, SCREEN_HEIGHT, WINDOW_TITLE)
        {

        }
        protected override void Draw()
        {                   
            this.window.Draw(shape);
        }

        protected override void Update()
        {
            
            if ((shape.Position.X + rectangleWidth >= SCREEN_WIDTH) || (shape.Position.X <= 0))
            {
                xVelocity = -xVelocity;
            }

            if ((shape.Position.Y + rectangleHeigth >= SCREEN_HEIGHT) || (shape.Position.Y <= 0))
            {
                yVelocity = -yVelocity;
            }
            
            shape.Position = new Vector2f(shape.Position.X + xVelocity, shape.Position.Y + yVelocity);
        }

        protected override void Setup()
        {
            this.ClearWindowBeforeDraw = false;
            this.backgroundColor = new Color(180, 100, 70, 200);

            double baseRandom = new Random(DateTime.Now.Millisecond).NextDouble();
            xVelocity = (float)(baseRandom * 15d) ;
            yVelocity = (float)(baseRandom * 15d) ;

            shape = new RectangleShape(new Vector2f(rectangleWidth, rectangleHeigth))
            {
                FillColor = Color.Yellow,
                Position = new Vector2f(rectangleWidth, rectangleHeigth)
            };
        }
        
    }
}
