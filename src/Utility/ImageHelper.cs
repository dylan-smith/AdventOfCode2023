using System.Drawing;

namespace AdventOfCode;

public static class ImageHelper
{
    public static void CreateBitmap(int width, int height, string filePath, Func<int, int, Color> getPixel)
    {
        using var img = new Bitmap(width, height);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                img.SetPixel(x, y, getPixel(x, y));
            }
        }

        img.Save(filePath);
    }

    public static void CreateBitmap(Color[,] pixels, string filePath)
    {
        var width = pixels.GetUpperBound(0);
        var height = pixels.GetUpperBound(1);

        using var img = new Bitmap(width, height);

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                img.SetPixel(x, y, pixels[x, y]);
            }
        }

        img.Save(filePath);
    }
}