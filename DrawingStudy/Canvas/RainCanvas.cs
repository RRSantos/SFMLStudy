using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using DrawingStudy.Draws;
using SFMLCanvas;

namespace DrawingStudy.Canvas
{
    public class RainCanvas : AbstractCanvas
    {
        
        private const uint SCREEN_WIDTH = 800;
        private const uint SCREEN_HEIGHT = 600;
        private const string WINDOW_TITLE = "Rain Canvas";

        private const uint DROP_COUNT = SCREEN_WIDTH * SCREEN_HEIGHT / 100;

        List<RainDrop> rainDrops;

        
        public RainCanvas() : base(SCREEN_WIDTH, SCREEN_HEIGHT, WINDOW_TITLE)
        {

        }
        protected override void Draw()
        {
            foreach (RainDrop drop in rainDrops)
            {
                drop.Draw(window);
            }
            
        }

        protected override void Update()
        {
            var diffWindowMouse = Mouse.GetPosition() - window.Position;
            var mouseScreenPosition = window.MapPixelToCoords(diffWindowMouse);

            foreach (RainDrop drop in rainDrops)
            {
                drop.Update(mouseScreenPosition);
            }
            
        }

        protected override void Setup()
        {
            this.backgroundColor = new Color(80, 0, 80);

            rainDrops = new List<RainDrop>();
            for (int i = 0; i < DROP_COUNT; i++)
            {
                rainDrops.Add(new RainDrop(SCREEN_WIDTH, SCREEN_HEIGHT));
            }

        }
        
    }
}
