using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class Program
{
    static void Main(string[] args)
    {
        //Prepare main image 
        Bitmap mainImage = new Bitmap(100, 100);
        Graphics newGraphics = Graphics.FromImage(mainImage);
        newGraphics.Clear(Color.White); //Set background color to white
       
        


        //create hash
        string nameOfUser = "Jordan";
        string hash = "";
        using (MD5 md5 = MD5.Create())
        {
            hash = GetMd5Hash(md5, nameOfUser);
        }

        List<int> hashedValues = new List<int>();

        for (int i = 6; i < 30; i++)
        {
            if (hashedValues.Count == 16) break;
            hashedValues.Add(int.Parse(hash.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
        }
        

        //Create Main colour for pixels
        var color1 = int.Parse(hash.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        var color2 = int.Parse(hash.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        var color3 = int.Parse(hash.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        Color newColor = Color.FromArgb(color1, color2, color3);

        // Get Pixel
        Image imageFile = Image.FromFile(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\forIdenticon\1.png");
        Graphics newGraphics1 = Graphics.FromImage(imageFile);
        newGraphics1.Clear(newColor); //Set background color to white


        using (Graphics g = Graphics.FromImage(mainImage))
        {
            int vertical = 0;
            int horizontal = 0;

            hashedValues.ForEach(a =>
            {
                if(vertical == 60)
                    Console.WriteLine("yos");
                
                if(a % 2 == 0 && horizontal < 60)
                {
                    g.DrawImage(imageFile, horizontal, vertical);
                    if(horizontal != 60)
                        g.DrawImage(imageFile, 80 - horizontal, vertical);
                    mainImage.Save(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\yos.png");
                }

                if (horizontal >= 60)
                {
                    vertical += 20;
                    horizontal = 0;
                }
                else
                    horizontal += 20; 

            });
        }

        K:
        Console.WriteLine("das");

        //mainImage.Save(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\yos.png");

        //// Loop through the images pixels to reset color.



        //for (int x = 0; x < image1.Width; x++)
        //{
        //    for (int y = 0; y < image1.Height; y++)
        //    {
        //        image1.SetPixel(x, y, newColor);
        //    }
        //}

        //Random random = new Random();


        //Bitmap bmp = new Bitmap(500,500);
        //using (Graphics graph = Graphics.FromImage(bmp))
        //{
        //    Rectangle ImageSize = new Rectangle(0, 0, x, y);
        //    graph.FillRectangle(Brushes.White, ImageSize);
        //}

        //string nameOfUser = "Boogdan";
        //string hash = "";
        //using (MD5 md5 = MD5.Create())
        //{
        //    hash = GetMd5Hash(md5, nameOfUser);
        //}

        //string color = hash.Substring(0, 6);
        //bool[,] pixels = new bool[5, 5];
        //string[,] pixelstemp = new string[400, 400];


        //for (int i = 0; i < 5; i++)
        //{
        //    for (int y1 = 0; y1 < 5; y1++)
        //    {
        //        pixels[i,y1] = int.Parse(hash.Substring((i * 5) + y1 + 6, 1), System.Globalization.NumberStyles.HexNumber) % 2 == 0;
        //    }
        //}


        //Bitmap image1;
        //image1 = new Bitmap(400, 400);
        //Random random = new Random();

        

        //var color1 = int.Parse(hash.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        //var color2 = int.Parse(hash.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        //var color3 = int.Parse(hash.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        //Color newColor = Color.FromArgb(color1, color2, color3);
        //Color bgColor = Color.FromArgb(238, 238, 238);

        //int temp = 0;

        ////Trzeba będzie utworzyć 16 pixeli zapisać do bitmapy następnie 
        ////while (temp <= 5)
        ////{
        ////    for (int i = 0; i <= 20; i++)
        ////    {
        ////        for (int y = 0; y < 20; y++)
        ////        {
        ////            image1.SetPixel(c, m, pixelColor);
        ////        }
        ////    }
        ////}
        //using (var graphics = Graphics.FromImage(image1))
        //{
        //    //graphics.dra
        //    //graphics.FillRectangle(Brushes.Aqua,new Rectangle())
        //    graphics.DrawRectangle(Pens.Black, 50, 50, 50, 50);
        //    graphics.FillRectangle(Brushes.White, 50, 50, 50, 50);
        //        //graphics.DrawLine(blackPen, x1, y1, x2, y2);
        //}

        //for (int c = 150; c < 350; c++)
        //{
        //    for (int m = c; m < 350; m++)
        //    {
        //        //Loop through the images pixels to reset color.
        //        for (x = 0; x < pixels.GetLength(0); x++)
        //        {
        //            for (y = 0; y < pixels.GetLength(1); y++)
        //            {
        //                var pixelColor = bgColor;

        //                if (pixels[x, y])
        //                    pixelColor = newColor;

        //                image1.SetPixel(c, m, pixelColor);
        //                goto Found;  
        //            }
        //        }

        //        Found:
        //    }
        //}

        ////Loop through the images pixels to reset color.
        //for (x = 0; x < pixels.GetLength(0); x++)
        //{
        //    for (y = 0; y < pixels.GetLength(1); y++)
        //    {
        //        var pixelColor = Color.Black;

        //        if (pixels[x,y])
        //            pixelColor = newColor;

        //        image1.SetPixel(x * 80, y * 80, pixelColor);

        //        //var color1 = int.Parse(hash.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        //        //var color2 = int.Parse(hash.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        //        //var color3 = int.Parse(hash.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        //        //Color newColor = Color.FromArgb(color1, color2, color3);

        //        ////var pixelColor1 = Color.Black;
        //        ////if (pixels[random.Next(1,6), random.Next(1, 6)])
        //        ////    pixelColor = newColor;

        //        //image1.SetPixel(x, y, pixelColor);

        //    }
        //}

        //using (Graphics G1 = Graphics.FromImage(image1))
        //{
        //    G1.Clear(Color.Black);
        //}

        //for (int k = 0; k < pixels.GetLength(0); k++)
        //{
        //    for (int l = 0; l < pixels.GetLength(1); l++)
        //    {
        //        var color1 = int.Parse(hash.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        //        var color2 = int.Parse(hash.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        //        var color3 = int.Parse(hash.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        //        Color newColor = Color.FromArgb(color1, color2, color3);

        //       var pixelColor = Color.Black;
        //        if (pixels[k, l])
        //            pixelColor = newColor;

        //        image1.SetPixel(k*80, l*80, pixelColor);
        //    }
        //}

    }

    static string GetMd5Hash(MD5 md5Hash, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
}