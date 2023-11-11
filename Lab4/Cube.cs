using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using Lab4;
using System.Drawing;
using OpenTK.Input;
using OpenTK.Graphics;

namespace Lab4
{
    class Cube
    {
        // Clasa ColorRandomizer nu este definită aici; presupun că generează o culoare aleatorie
        ColorRandomizer randomColor = new ColorRandomizer();

        // Matricea de culori pentru fețele cubului
        Color[] faceColors = new Color[6];
        Color4 color = new Color4();

        // Vectorii normali pentru fiecare față a cubului
        float[,] n = new float[,]
        {
            {-1.0f, 0.0f, 0.0f},
            {0.0f, 1.0f, 0.0f},
            {1.0f, 0.0f, 0.0f},
            {0.0f, -1.0f, 0.0f},
            {0.0f, 0.0f, 1.0f},
            {0.0f, 0.0f, -1.0f}
        };

        // Indicii vertecșilor pentru fiecare față a cubului
        int[,] faces = new int[,]
        {
            {0, 1, 2, 3},
            {3, 2, 6, 7},
            {7, 6, 5, 4},
            {4, 5, 1, 0},
            {5, 6, 2, 1},
            {7, 4, 0, 3}
        };

        // Lista de vertecși care definesc cubul
        private List<VertexPoint> vertices;

        public Cube(int size)
        {
            // Inițializarea vertecșilor cubului
            vertices = new List<VertexPoint>
            {
                new VertexPoint(-size / 2, -size / 2, -size / 2),
                new VertexPoint(size / 2, -size / 2, -size / 2),
                new VertexPoint(size / 2, size / 2, -size / 2),
                new VertexPoint(-size / 2, size / 2, -size / 2),
                new VertexPoint(-size / 2, -size / 2, size / 2),
                new VertexPoint(size / 2, -size / 2, size / 2),
                new VertexPoint(size / 2, size / 2, size / 2),
                new VertexPoint(-size / 2, size / 2, size / 2)
            };

            // Obținerea unei culori aleatorii pentru cub
            color = randomColor.GetRandomColor();
        }

        // Metoda care verifică tastatura pentru comenzi
        public void KeyBinds()
        {
            KeyboardState keyboard = Keyboard.GetState();

            // Schimbă culoarea primei fețe a cubului dacă este apăsată tasta 'G'
            if (keyboard[Key.G])
            {
                Color faceColor = Color.Blue; // Culoarea dorită pentru față
                int[] face1 = { 5, 6, 2, 1 }; // Indicii vertecșilor primei a 4 fete 

                // Setează culoarea pentru fiecare vertex al feței // NU MERGE!1
                foreach (int index in face1)
                {
                    vertices[index].pointColor = faceColor;
                }
            }
        }

        // Metoda care desenează cubul
        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            for (int i = 0; i < 6; i++)
            {
                GL.Normal3(n[i, 0], n[i, 1], n[i, 2]);

                for (int j = 0; j < 4; j++)
                {
                    int vertexIndex = faces[i, j];

                    if (i == 4) // Dacă este fața a patra, va fi desenată maro
                    {
                        GL.Color4(Color4.Brown);
                    }
                    else // Altfel, se va folosi culoarea aleatorie a cubului
                    {
                        GL.Color4(color);
                    }

                    GL.Vertex3(vertices[vertexIndex].coordX, vertices[vertexIndex].coordY, vertices[vertexIndex].coordZ);
                }
            }
            GL.End();
        }
    }
}
