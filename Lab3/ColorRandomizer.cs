using System;

using OpenTK.Graphics;

public class ColorRandomizer
{
    private Random random;

    public ColorRandomizer()
    {
        random = new Random();
    }

    public Color4 GetRandomColor()
    {
        float r = (float)random.NextDouble();
        float g = (float)random.NextDouble();
        float b = (float)random.NextDouble();
        return new Color4(r, g, b, 1.0f);
    }
}
