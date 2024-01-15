using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab9
{
    class Pyramid
    {
        private List<VertexPoint> vertices;

        public Pyramid(int size)
        {
            vertices = new List<VertexPoint>
            {
                // Define the vertices of the pyramid
                new VertexPoint(0, 0, 0, Color.Red),        // Vertex 0 (base)
                new VertexPoint(size, 0, 0, Color.Green),   // Vertex 1 (base)
                new VertexPoint(size, 0, size, Color.Blue),  // Vertex 2 (base)
                new VertexPoint(0, 0, size, Color.Yellow),   // Vertex 3 (base)
                new VertexPoint(size / 2, size, size / 2, Color.Purple) // Apex (top)
            };
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);

            // Draw the base of the pyramid
            DrawTriangle(vertices[0], vertices[1], vertices[2]);
            DrawTriangle(vertices[0], vertices[2], vertices[3]);

            // Draw the sides of the pyramid
            DrawTriangle(vertices[0], vertices[1], vertices[4]);
            DrawTriangle(vertices[1], vertices[2], vertices[4]);
            DrawTriangle(vertices[2], vertices[3], vertices[4]);
            DrawTriangle(vertices[3], vertices[0], vertices[4]);

            GL.End();
        }

        private void DrawTriangle(VertexPoint v1, VertexPoint v2, VertexPoint v3)
        {
            GL.Color3(v1.pointColor);
            GL.Vertex3(v1.coordX, v1.coordY, v1.coordZ);

            GL.Color3(v2.pointColor);
            GL.Vertex3(v2.coordX, v2.coordY, v2.coordZ);

            GL.Color3(v3.pointColor);
            GL.Vertex3(v3.coordX, v3.coordY, v3.coordZ);
        }
    }
}
