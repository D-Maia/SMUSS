using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SMUSS
{
    public static class GeometryUtils
    {

        public abstract class Shape
        {
            public Vector2 Position { get; set; }
           
            
            public class Triangle : Shape
            {
                public Triangle(Vector2 firstVertexPosition, Vector2 secondVertexPosition, Vector2 thirdVertexPosition)
                {

                    if (firstVertexPosition.Y <= secondVertexPosition.Y &&
                        firstVertexPosition.Y <= thirdVertexPosition.Y)
                        Position = firstVertexPosition;
                    else if (secondVertexPosition.Y <= thirdVertexPosition.Y &&
                             secondVertexPosition.Y < firstVertexPosition.Y)
                        Position = secondVertexPosition;
                    else
                        Position = thirdVertexPosition;

                }
            }
        }

        
    }
}
