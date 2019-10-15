using System;
using System.Drawing;
using System.Windows.Forms;

public class Program
{
    static void Main(string[] args)
    {
        //Bitmap bmp = new Bitmap(500,500);
        //using (Graphics graph = Graphics.FromImage(bmp))
        //{
        //    Rectangle ImageSize = new Rectangle(0, 0, x, y);
        //    graph.FillRectangle(Brushes.White, ImageSize);
        //}
        

        Bitmap image1;
        image1 = new Bitmap(500, 500);


        // Retrieve the image.
        //image1 = new Bitmap(@"C:\Documents and Settings\All Users\"
        //    + @"Documents\My Music\music.bmp", true);

        int x, y;

        // Loop through the images pixels to reset color.
        for (x = 0; x < image1.Width; x++)
        {
            for (y = 0; y < image1.Height; y++)
            {
                Color pixelColor = image1.GetPixel(x, y);
                Color newColor = Color.FromArgb(pixelColor.R, 0, 0);
                image1.SetPixel(x, y, newColor);
            }
        }

        image1.Save(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\Shapes075.jpg");
    }
}