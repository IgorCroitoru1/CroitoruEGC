using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Lab2
{
    class SimpleWindow : GameWindow
    {
        private Vector2 squarePosition = new Vector2(0.0f, 0.0f); // Poziția pătratului
        private float rotationAngle = 0.0f; // Unghiul de rotație al pătratului
        private float rotationSpeed = 2.0f; // Viteza de rotație

        public SimpleWindow() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            KeyDown += Keyboard_KeyDown; // Ascultă evenimentele tastelor apăsate
            MouseMove += Mouse_Move; // Ascultă evenimentele de mișcare a mouse-ului
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit(); // Ieșirea din aplicație la apăsarea tastei Esc

            // Mută pătratul cu tastele săgeată stânga/dreapta
            if (e.Key == Key.Left)
                squarePosition.X -= 0.05f;
            if (e.Key == Key.Right)
                squarePosition.X += 0.05f;
        }

        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            // Roțește pătratul în funcție de mișcarea mouse-ului
            rotationAngle += e.XDelta * rotationSpeed;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.350f, 0.245f, 0.245f, 0); // Setarea culorii de fundal a ferestrei
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height); // Setarea zonei de afișare OpenGL

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0); // Definirea sistemului de coordonate
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // Logica de actualizare (dacă este necesară)
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); // Șterge buffer-ul de culoare

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Aplică transformările asupra pătratului
            GL.Translate(squarePosition.X, squarePosition.Y, 0.0);
            GL.Rotate(rotationAngle, 0.0, 0.0, 1.0);

            // Desenarea pătratului
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.622f, 0.840f, 0.822f); // Setarea culorii pătratului
            GL.Vertex2(-0.1f, -0.1f); // Definirea vârfurilor pătratului
            GL.Vertex2(0.1f, -0.1f);
            GL.Vertex2(0.1f, 0.1f);
            GL.Vertex2(-0.1f, 0.1f);
            GL.End();

            this.SwapBuffers(); // Efectuarea schimbului de buffer pentru afișare
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0); // Rularea aplicației cu un anumit framerate
            }
        }
    }
}
