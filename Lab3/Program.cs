using System;

using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

public class MainWindow : GameWindow
{
    private float angleX = 0.0f;
    private float angleY = 0.0f;
    private ColorRandomizer colorRandomizer = new ColorRandomizer();
    private Color4[] colors = new Color4[3];
    public MainWindow(int width, int height) : base(width, height) 
    {

        for (int i = 0; i < 3; i++)
        {
            colors[i] = colorRandomizer.GetRandomColor();
        }

    }

   

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        KeyboardState keyboard = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        //Generez un set nou de culori
        if (keyboard[Key.R])
        {
            for (int i = 0; i < 3; i++)
            {
                colors[i] = colorRandomizer.GetRandomColor();
            }
        }
        //Printez culorile
        if (mouse[MouseButton.Right])
        {
            for (int i=0;i< colors.Length;i++)
            {
                Console.WriteLine($"Color {i}: R={colors[i].R}, G={colors[i].G}, B={colors[i].B}");
               

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
        GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
        GL.Ortho(0, Width, Height, 0, -1, 1);


        // Activăm gestionarea evenimentelor mouse-ului
        MouseMove += (sender, args) =>
        {
            angleX += args.XDelta * 0.01f;
            angleY += args.YDelta * 0.01f;
        };
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.PointSize(5f);

      

        GL.Begin(PrimitiveType.TriangleFan);
        GL.Color4(colors[0]);
        GL.Vertex2(200, 200);
        GL.Color4(colors[1]);
        GL.Vertex2(300, 100);
        GL.Color4(colors[2]);
        GL.Vertex2(400, 200);
        GL.End();

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
