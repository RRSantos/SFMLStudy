using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace DrawingStudy.Draws
{
    class RainDrop
    {

        private const float DROP_MAX_WIDTH = 1.7f;
        private const float DROP_MAX_HEIGHT = 13;
        private const float UMBRELLA_WIDTH = 100;

        

        private RectangleShape dropShape;
        private readonly uint screenWidth;
        private readonly uint screenHeight;

        


        private readonly float velocity;

        private readonly Random random = new Random();

        private void initializeDrop()
        {
            
            Vector2f linePosStart = new Vector2f((float)(random.NextDouble() * this.screenWidth), (float)(random.NextDouble() * this.screenHeight));

            Vector2f rectSize = new Vector2f((float)(random.NextDouble() * DROP_MAX_WIDTH), (float)(random.NextDouble() * DROP_MAX_HEIGHT));
            dropShape = new RectangleShape(rectSize)
            {
                Position = linePosStart,
                FillColor = new Color(255, 100, 255, Convert.ToByte((rectSize.Y / DROP_MAX_HEIGHT) * 255))
                
            };
        }

        public RainDrop(uint screenWidth, uint screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            velocity = random.Next(10,25);

            initializeDrop();
        }

        public void Update(Vector2f mouseScreenPosition)
        {
            float newYPosition = dropShape.Position.Y + velocity;
            if (newYPosition > screenHeight)
            {
                newYPosition = (float)random.NextDouble() * screenHeight;
            }
            else if (mouseScreenPosition.X > 0 && mouseScreenPosition.Y  > 0)
            {
                double mouseAndDropDistance = mouseScreenPosition.X - dropShape.Position.X;
                if (Math.Abs(mouseAndDropDistance) < UMBRELLA_WIDTH)
                {
                    float yDistance = getUmbrellaDistanceForY(mouseAndDropDistance);
                    if (newYPosition > mouseScreenPosition.Y - yDistance)
                    {
                        newYPosition = -UMBRELLA_WIDTH - dropShape.Position.Y;
                    }
                }
            }

            dropShape.Position = new Vector2f(dropShape.Position.X, newYPosition);
            
        }

        private float getUmbrellaDistanceForY(double mouseAndDropDistance)
        {
            double normalizedDistance = mouseAndDropDistance / (UMBRELLA_WIDTH);
            double angle = Math.Acos(normalizedDistance);
            float distanceForY = (float)((UMBRELLA_WIDTH) * Math.Sin(angle));
            return distanceForY;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(dropShape);
        }

    }
}
