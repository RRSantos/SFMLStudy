using System;
using SFML.Graphics;
using SFMLCanvas.Helpers;

namespace DrawingStudy.Draws
{
    class Mandelbrot
    {
        private readonly uint width;
        private readonly uint height;
        private readonly uint maxIterations;
        private readonly double xScale;
        private readonly double yScale;
        public Mandelbrot(uint width, uint height, uint maxIterations)
        {
            this.width = width;
            this.height = height;
            this.maxIterations = maxIterations;
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

                    double a = x / xScale - 2.5;
                    double b = y / yScale - 1;

                    double next_x = a;
                    double next_y = b;

                    int i = 0;
                    double dist = 0;
                    while (i < maxIterations && (dist <= 16))
                    {
                        double temp_x = Math.Pow(next_x, 2) - Math.Pow(next_y, 2) + a;
                        next_y = 2 * next_y * next_x + b;
                        next_x = temp_x;
                        i++;
                        dist = Math.Pow(next_x, 2) + Math.Pow(next_y, 2);
                    }

                    double saturation = i / (double)maxIterations;
                    
                    image.SetPixel(x, y, ColorHelper.HSV(200, saturation, 0.8));
                    
                }

            }

            return image;
        }
    }
}
