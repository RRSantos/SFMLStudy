using SFML.Graphics;
using System;
using SFMLCanvas.Helpers;

namespace DrawingStudy.Draws
{
    class JuliaSet
    {
        private readonly uint width;
        private readonly uint height;
        private readonly uint maxIterations;
        private readonly float a;
        private readonly float b;
        private readonly double xScale;
        private readonly double yScale;
        public JuliaSet(uint width, uint height, uint maxIterations, float a, float b)
        {
            this.width = width;
            this.height = height;
            this.maxIterations = maxIterations;
            this.a = a;
            this.b = b;
            xScale = this.width / 3.5;
            yScale = this.height / 2;
        }

        public Image GetImage()
        {   
            Image image = new Image(width, height);
            
            for (uint x = 0; x < width; x++)
            {
                for (uint y = 0; y < height; y++)
                {

                    double next_x = x / xScale - 2.5;
                    double next_y = y / yScale - 1;

                    int i = 0;
                    double dist = 0;
                    while (i < maxIterations && (dist <= 2))
                    {
                        double temp_x = Math.Pow(next_x, 2) - Math.Pow(next_y, 2) + a;
                        next_y = 2 * next_y * next_x + b;
                        next_x = temp_x;
                        i++;
                        dist = Math.Pow(next_x, 2) + Math.Pow(next_y, 2);
                    }
                    if (i % 360 < 60)
                        i = 240;
                    image.SetPixel(x, y, ColorHelper.HSV(i , .8d, 1-dist / 266));
                }
            }
            

            return image;
        }
    }
}
