﻿using System;

using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using System.IO;

public class MainWindow : GameWindow
{
    private float angleX = 0.0f;
    private float angleY = 0.0f;
    private ColorRandomizer colorRandomizer = new ColorRandomizer();
    private Color4[] vertexColors = new Color4[3];
    private Color4 culoareTriunghi = Color4.White;
    private Color4 culoareBack = Color4.Red;

    private int lineX_1 = 35;
    private int lineY_1 = 25;
    private int lineZ_1 = 20;
    private int lineX_2 = 70;
    private int lineY_2 = 25;
    private int lineZ_2 = 40;


    float[] triangleVertices = new float[9]; // Trei coordonate pentru fiecare vârf al triunghiului
    public MainWindow(int width, int height) : base(1152, 600)
    {

        for (int i = 0; i < 3; i++)
        {
            vertexColors[i] = colorRandomizer.GetRandomColor();
        }

    }

   

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        KeyboardState keyboard = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        if (keyboard[Key.W])
        {
            triangleVertices[1]++;
            triangleVertices[3]++;
            triangleVertices[5]++;

        }
        if (keyboard[Key.S])
        {
            triangleVertices[1]--;
            triangleVertices[3]--;
            triangleVertices[5]--;

        }

        if (keyboard[Key.H])
        {
            culoareBack = colorRandomizer.GetRandomColor();
        }


        //Generez un set nou de culori
        if (keyboard[Key.R])
        {
            // Amplificarea canalului roșu
            vertexColors[0].R *= 2.0f;
            vertexColors[1].R *= 2.0f;
            vertexColors[2].R *= 2.0f;
        }
        else if (keyboard[Key.G])
        {
            // Amplificarea canalului verde
            vertexColors[0].G *= 2.0f;
            vertexColors[1].G *= 2.0f;
            vertexColors[2].G *= 2.0f;
        }
        else if (keyboard[Key.B])
        {
            // Amplificarea canalului albastru
            vertexColors[0].B *= 2.0f;
            vertexColors[1].B *= 2.0f;
            vertexColors[2].B *= 2.0f;
        }

        else if (keyboard[Key.X])
        {

            // Afișarea valorilor RGB în consolă
            Console.WriteLine($"Vertex 0 - R: {vertexColors[0].R}, G: {vertexColors[0].G}, B: {vertexColors[0].B}");
            Console.WriteLine($"Vertex 1 - R: {vertexColors[1].R}, G: {vertexColors[1].G}, B: {vertexColors[1].B}");
            Console.WriteLine($"Vertex 2 - R: {vertexColors[2].R}, G: {vertexColors[2].G}, B: {vertexColors[2].B}");
        }
        //Printez culorile
        if (mouse[MouseButton.Right])
        {
            for (int i=0;i< vertexColors.Length;i++)
            {
                Console.WriteLine($"Color {i}: R={vertexColors[i].R}, G={vertexColors[i].G}, B={vertexColors[i].B}");
               

            }
        }



        // Se utilizeaza mecanismul de control input oferit de OpenTK (include perifcerice multiple, inclusiv
        // pentru gaminig - gamepads, joysticks, etc.).
        if (keyboard[Key.Escape])
        {
            Exit();
            return;
        }

    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        GL.ClearColor(culoareBack);
        GL.Ortho(0, Width, Height, 0, -1, 1);

        // Citirea coordonatelor triunghiului din fișier
        string[] lines = File.ReadAllLines("C:\\Users\\Igor\\source\\repos\\LaboratoareEGC\\Lab3\\coord.txt");

        if (lines.Length >= 3)
        {

            for (int i = 0; i < 3; i++)
            {
                string[] coords = lines[i].Split(' ');
                for (int j = 0; j < 2; j++) // Citim doar două coordonate (x și y)
                {
                    triangleVertices[i * 2 + j] = float.Parse(coords[j]);
                }
            }

        }
        else
        {
            Console.WriteLine("Nu s-au găsit suficiente coordonate pentru a desena un triunghi.");
        }

    }

    private void DrawLine()
    {
        GL.Begin(PrimitiveType.Lines);
        GL.Color3(Color.Blue);
        GL.Vertex3(lineX_1, lineY_1, lineZ_1);
        GL.Vertex3(lineX_2, lineY_2, lineZ_2);
        GL.End();
    }

    private void Triangle()
    {
        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color4(vertexColors[0]);
        GL.Vertex2(triangleVertices[0], triangleVertices[1]);

        // GL.Color4(vertexColors[1]);
        GL.Vertex2(triangleVertices[2], triangleVertices[3]);

        // GL.Color4(vertexColors[2]);
        GL.Vertex2(triangleVertices[4], triangleVertices[5]);

        GL.End();
    }

    private void Triangle1()
    {
        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color4(Color.Blue);
        GL.Vertex2(triangleVertices[0], triangleVertices[1]);

        // GL.Color4(vertexColors[1]);
        GL.Vertex2(triangleVertices[2]+100, triangleVertices[3]+130);

        // GL.Color4(vertexColors[2]);
        GL.Vertex2(triangleVertices[4], triangleVertices[5]);

        GL.End();
    }



    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.ClearColor(culoareBack);

        GL.PointSize(5f);


        DrawLine();
        Triangle();
        Triangle1();

        SwapBuffers();
    }

    [STAThread]
    public static void Main()
    {
        using (var game = new MainWindow(800, 600))
        {
            game.Title = "OpenTK Primitives Example";
            game.Run(60);
        }
    }
}
