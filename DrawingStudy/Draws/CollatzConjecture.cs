using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFMLCanvas.Helpers;

namespace DrawingStudy.Draws
{
    class CollatzConjecture
    {
        
        private const float rectSideSize = 7f;
        private readonly uint screenWidth;
        private readonly uint screenHeight;
        private readonly Vector2f rectSize; 

        public CollatzConjecture(uint screenWidth, uint screenHeight)
        {   
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            rectSize = new Vector2f(rectSideSize, rectSideSize);

        }

        public List<Shape> GetShapeList(int number)
        {

            List<Shape> shapeList = new List<Shape>();

            List<int> collatzResultNumbers = new List<int>();


            int maxNumber = number;
            int newNumber = number;
            do
            {
                collatzResultNumbers.Add(newNumber);

                if (newNumber % 2 == 0)
                {
                    newNumber = newNumber / 2;
                }
                else
                {
                    newNumber = 3 * newNumber + 1;
                }

                if (newNumber > maxNumber)
                {
                    maxNumber = newNumber;
                }
                
            } while (newNumber > 1);

            int maxX = collatzResultNumbers.Count;
            float xFactor = (float)maxX / (float)screenWidth;
            float yFactor = (float)maxNumber / (float)screenHeight;

            for (int i = 0; i < collatzResultNumbers.Count; i++)
            {
                RectangleShape shape = new RectangleShape(rectSize)
                {
                    Position = new Vector2f((i + 1)/xFactor , screenHeight - collatzResultNumbers[i] / yFactor),
                    FillColor = ColorHelper.HSV(collatzResultNumbers[i], 0.8, 0.7)
                };

                shapeList.Add(shape);
         
                
            }




            return shapeList;
        }
    }
}
