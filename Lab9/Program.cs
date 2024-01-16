using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    internal class Program : GameWindow
    {
        private Color[] colorVertices = { Color.White, Color.LawnGreen, Color.WhiteSmoke, Color.Tomato, Color.Turquoise, Color.OldLace, Color.Olive, Color.MidnightBlue, Color.PowderBlue, Color.PeachPuff, Color.LavenderBlush, Color.MediumAquamarine };
        private const int XYZ_SIZE = 75;
        private int selectedFace = -1; // Initially, no face is selected
        private float minRed = 0.0f;
        private float maxRed = 1.0f;
        private float minGreen = 0.0f;
        private float maxGreen = 1.0f;
        private float minBlue = 0.0f;
        private float maxBlue = 1.0f;
        private Cube cube;
        private Pyramid pyramid;


        private ColorRandomizer randomizer;

        private int[] triangle_1 = { 35, 25, 20 };
        private int[] triangle_2 = { 70, 25, 40 };
        private int[] triangle_3 = { 30, 60, 50 };
        private float lightIntensity = 1.0f;
        private int[] textures = new int[2];
        public Program(int width, int height) : base(width, height)
        {
            pyramid = new Pyramid(10);
            cube = new Cube(10);
        }

      
        //Desenează cubul - triunghuri.


       
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            // showCube = true;

        }

        private void SetupLight()
        {
            GL.Enable(EnableCap.Light0);

            float[] lightPosition = { 10.0f, 10.0f, 10.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            //texture

            GL.GenTextures(textures.Length, textures);
            LoadTexture(textures[0], "C:\\Users\\Igor\\source\\repos\\LaboratoareEGC\\Lab9\\brickTexture.jpg");


            SetupLight();
           // Enable lighting
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            //Set light properties
            float[] lightPosition = { 50.0f, 50.0f, 50.0f, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.4f, 0.4f, 0.4f, 1.0f });

            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 6.0f, 6.0f, 6.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });


            // Set material properties
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, new float[] { 0.8f, 0.8f, 0.8f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 100.0f);



        }

       
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
           // GL.BindTexture(TextureTarget.Texture2D, textures[0]);
            DrawAxes();
            cube.Draw(textures[0]);
            //pyramid.Draw();
            SwapBuffers();
        }
      
      
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            cube.KeyBinds();
            KeyboardState keyboard = Keyboard.GetState();

            // Increase light intensity when 'L' is pressed
            if (keyboard[Key.L])
            {
                lightIntensity += 0.1f; // You can adjust the step as needed
                GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { lightIntensity, lightIntensity, lightIntensity, 1.0f });
            }
        }



        private void DrawAxes()
        {
            // Desenează axa Ox (cu roșu).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(XYZ_SIZE, 0, 0);
            GL.End();

            // Desenează axa Oy (cu galben).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, XYZ_SIZE, 0); ;
            GL.End();

            // Desenează axa Oz (cu verde).
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, XYZ_SIZE);
            GL.End();
        }


        private void DrawTriangle()
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Blue);
            GL.Vertex3(triangle_1[0], triangle_1[1], triangle_1[2]);
            GL.Vertex3(triangle_2[0], triangle_2[1], triangle_2[2]);
            GL.Vertex3(triangle_3[0], triangle_3[1], triangle_3[2]);
            GL.End();
        }


        private void LoadTexture(int textureId, string filename)
        {
            Bitmap bmp = new Bitmap(filename);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                          bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                          PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
        }


        static void Main(string[] args)
        {
            using (Program example = new Program(800, 600))
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
