using SFML.Graphics;
using SFML.System;
using System;
using SFMLCanvas;
using DrawingStudy.Draws;

namespace DrawingStudy.Canvas
{
    public class FractalCanvas : AbstractCanvas
    {
        private const uint SCREEN_WIDTH = 1920;
        private const uint SCREEN_HEIGHT = 1080;
        private const string WINDOW_TITLE = "Fractals";
        
        Sprite fractal;
        
        public FractalCanvas() : base(SCREEN_WIDTH, SCREEN_HEIGHT, WINDOW_TITLE)
        {

        }
        protected override void Draw()
        {   
            this.window.Draw(fractal);
        }

        protected override void Update()
        {   
            
        }

        protected override void Setup()
        {
            this.ClearWindowBeforeDraw = true;
            this.backgroundColor = Color.Black;

            Mandelbrot mandelbrot = new Mandelbrot(SCREEN_WIDTH, SCREEN_HEIGHT, 400);
            Image fractalImage = mandelbrot.GetImage();
            //JuliaSet juliaSet = new JuliaSet(SCREEN_WIDTH, SCREEN_HEIGHT, 2000, -0.4f, 0.6f);
            //Image fractalImage = juliaSet.GetImage();
            Texture tex = new Texture(fractalImage);
            fractal = new Sprite(tex);
            window.MouseWheelScrolled += Window_MouseWheelScrolled;
            
        }

        private void Window_MouseWheelScrolled(object sender, SFML.Window.MouseWheelScrollEventArgs e)
        {
            float zoomFactor = e.Delta * 0.1f;           
            
            View view = window.GetView();

            Vector2f mousePos = window.MapPixelToCoords(new Vector2i(e.X, e.Y));
            Vector2f diffVector = mousePos - view.Center;

            view.Zoom(1 - zoomFactor);
            Vector2f newPosition = new Vector2f(diffVector.X * zoomFactor, diffVector.Y * zoomFactor);            
            view.Move(newPosition);
            window.SetView(view);
            Console.WriteLine($"Zoom: {zoomFactor}; newPosition.X: {newPosition.X}; newPosition.Y:{newPosition.Y}");
            
        }
    }
}
