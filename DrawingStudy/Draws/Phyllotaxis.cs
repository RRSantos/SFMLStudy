using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFMLCanvas.Helpers;

namespace DrawingStudy.Draws
{
    class Phyllotaxis
    {
        private readonly int cycles;
        private readonly float pointRadius;
        private readonly uint screenCenterX;
        private readonly uint screenCenterY;

        public Phyllotaxis(int cycles, float pointRadius, uint screenCenterX, uint screenCenterY)
        {
            this.cycles = cycles;
            this.pointRadius = pointRadius;
            this.screenCenterX = screenCenterX;
            this.screenCenterY = screenCenterY;
        }

        public List<Shape> GetShapeList()
        {

            List<Shape> shapeList = new List<Shape>();
            float xOffset = screenCenterX;
            float yOffset = screenCenterY;

            for (int n = 0; n < 360 * cycles; n++)
            {
                double theta = (Math.PI / 180) * (n * 137.5d);
                double radius = 8 * Math.Sqrt(n);

                float x = (float)(radius * Math.Cos(theta)) + xOffset;
                float y = (float)(radius * Math.Sin(theta)) + yOffset;

                CircleShape point = new CircleShape(pointRadius)
                {
                    FillColor = ColorHelper.HSV(n, 0.75f, 0.8d),
                    Position = new Vector2f(x, y)
                };
                shapeList.Add(point);
            }

            return shapeList;
        }
    }
}
