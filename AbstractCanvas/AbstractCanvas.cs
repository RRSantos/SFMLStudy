using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Xml.Serialization;

namespace SFMLCanvas
{
    public abstract class AbstractCanvas
    {
        protected readonly RenderWindow window;
        protected Color backgroundColor;
        protected AbstractCanvas(uint width, uint height, string title)
        {
            VideoMode video = new VideoMode(width, height);            
            window = new RenderWindow(video, title);
            window.SetFramerateLimit(60);
            window.Closed += (_, __) => window.Close();
            window.KeyReleased += Window_KeyReleased;
            ClearWindowBeforeDraw = true;

            this.backgroundColor = Color.Black;
            window.Clear(backgroundColor);

        }

        protected bool ClearWindowBeforeDraw { get; set; }

        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }

        public void Run()
        {
            Setup();

            while (window.IsOpen)
            {
                
                window.DispatchEvents();
                Update();
                if (ClearWindowBeforeDraw)
                {
                    window.Clear(backgroundColor);
                }
                Draw();
                window.Display();
            }
        }
        protected abstract void Setup();
        protected abstract void Update();
        protected abstract void Draw();
    }
}
