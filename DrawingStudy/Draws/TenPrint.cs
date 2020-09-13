using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace DrawingStudy.Draws
{
    class TenPrint
    {


        private readonly Random random = new Random();
        private readonly float size;
        private readonly float percent;
        private readonly Color color;

        public TenPrint(float size, float percent, Color color)
        {
            
            this.size = size;
            this.percent = percent;
            this.color = color;
        }

        public VertexArray GetRandomShape(float xOffset, float yOffset)
        {
            VertexArray shape = new VertexArray(PrimitiveType.TriangleFan);
            if (random.NextDouble() >= 0.5d)           
            {   
                shape.Append(new Vertex(new Vector2f(xOffset, yOffset + (1 - percent) * size), color));
                shape.Append(new Vertex(new Vector2f(xOffset, yOffset + size), color));
                shape.Append(new Vertex(new Vector2f(xOffset + percent * size, yOffset + size), color));
                shape.Append(new Vertex(new Vector2f(xOffset + size, yOffset + percent * size), color));
                shape.Append(new Vertex(new Vector2f(xOffset + size, yOffset), color));
                shape.Append(new Vertex(new Vector2f(xOffset + (1 - percent) * size, yOffset), color));
                shape.Append(new Vertex(new Vector2f(xOffset, yOffset + (1 - percent) * size), color));
            }
            else
            {
                shape.Append(new Vertex(new Vector2f(xOffset                        , yOffset ), color));
                shape.Append(new Vertex(new Vector2f(xOffset                        , yOffset+ percent * size), color));
                shape.Append(new Vertex(new Vector2f(xOffset + (1-percent) * size   , yOffset + size), color));
                shape.Append(new Vertex(new Vector2f(xOffset + size                 , yOffset + size), color));
                shape.Append(new Vertex(new Vector2f(xOffset + size                 , yOffset + (1- percent)* size), color));
                shape.Append(new Vertex(new Vector2f(xOffset + percent * size       , yOffset), color));
                shape.Append(new Vertex(new Vector2f(xOffset, yOffset),  color));
            }
            
            return shape;
        }



    }
}
