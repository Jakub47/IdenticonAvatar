using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

public class Program
{
    ///https://stackoverflow.com/questions/17208254/how-to-change-pixel-color-of-an-image-in-c-net
    public static Bitmap ChangeColor(Bitmap scrBitmap,Color newColorToApply)
    {
        Color actualColor;
        //make an empty bitmap the same size as scrBitmap
        Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
        for (int i = 0; i < scrBitmap.Width; i++)
        {
            for (int j = 0; j < scrBitmap.Height; j++)
            {
                //get the pixel from the scrBitmap image
                actualColor = scrBitmap.GetPixel(i, j);
                // > 150 because.. Images edges can be of low pixel colr. if we set all pixel color to new then there will be no smoothness left.
                if (actualColor.A > 150)
                    newBitmap.SetPixel(i, j, newColorToApply);
                else
                    newBitmap.SetPixel(i, j, actualColor);
            }
        }
        newBitmap.Save(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\tos1.png");

        return newBitmap;
    }


    static void Main(string[] args)
    {
        //Prepare main image 
        Bitmap mainImage = new Bitmap(60, 60);
        Graphics newGraphics = Graphics.FromImage(mainImage);
        newGraphics.Clear(Color.White); //Set background color to white
        

        //create hash
        string nameOfUser = "Jakub Bergmann";
        string hash = "";
        using (MD5 md5 = MD5.Create())
        {
            hash = GetMd5Hash(md5, nameOfUser);
        }
        var source = hash.GetHashCode();


        int centerindex = source & 3; // 2 lowest bits
        int sideindex = (source >> 2) & 15; // next 4 bits for side shapes
        int cornerindex = (source >> 6) & 15; // next 4 for corners
        int siderot = (source >> 10) & 3; // 2 bits for side offset rotation
        int cornerrot = (source >> 12) & 3; // 2 bits for corner offset rotation


        //Create Main colour for pixels
        var color1 = int.Parse(hash.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        var color2 = int.Parse(hash.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        var color3 = int.Parse(hash.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        Color newColor = Color.FromArgb(color1, color2, color3);

        var imagesToPlace = new Bitmap[16];
        var centerImages = new Bitmap[4];

        int countForCenter = 0;
        for (int i = 1; i <= 16; i++)
        {
            Bitmap imageFile = (Bitmap)Image.FromFile(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\forIdenticon\" + i.ToString() + ".png");
            imageFile = ChangeColor(imageFile, newColor);
            imagesToPlace[i - 1] = imageFile;

            if(i == 1 || i == 5 || i == 9 || i == 16)
            {
                centerImages[countForCenter] = imageFile;
                countForCenter++;
            }
        }


        using (Graphics g = Graphics.FromImage(mainImage))
        {
            int centerHor = 20;
            int centerVer = 20;
            g.DrawImage(centerImages[centerindex], centerVer, centerHor);

            // sideImage.RotateFlip(RotateFlipType.Rotate90FlipY); to da na prawo
            // sideImage.RotateFlip(RotateFlipType.Rotate180FlipX);  to da dół

            //sideImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //g.DrawImage(sideImage, 20, 0);

            //<vertical,horizontal>
            //var listOfSideLocations = new List<KeyValuePair<int, int>>()
            //{
            //    new KeyValuePair<int, int>(0,20),
            //    new KeyValuePair<int, int>(40,20),
            //    new KeyValuePair<int, int>(20,40),
            //};

            //var z = listOfSideLocations.ToLookup()
            //var sideLocation = new Lookup<int, int>();
            //sideLocation.Add(0, 40); sideLocation.Add(20, 0); sideLocation.Add(20, 60); sideLocation.Add(60, 40);

            var sideImage = imagesToPlace[sideindex];
            sideImage = InitalizeFirstRotation(sideImage, siderot);
            g.DrawImage(sideImage, 20, 0);
            var sideImageNextRot = new Bitmap(sideImage);
            sideImageNextRot.RotateFlip(RotateFlipType.Rotate90FlipNone); //Rotate image to right
            g.DrawImage(sideImageNextRot, 40,20);
            sideImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(sideImage, 20, 40);
            sideImageNextRot.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(sideImageNextRot, 0, 20);


            var cornerImage = imagesToPlace[cornerindex];
            cornerImage = InitalizeFirstRotation(cornerImage, siderot);
            g.DrawImage(cornerImage, 0, 0);



            var cornerImageNextRot = new Bitmap(cornerImage);
            cornerImageNextRot.RotateFlip(RotateFlipType.Rotate90FlipNone); //Rotate image to right
            g.DrawImage(cornerImageNextRot, 40, 0);


            cornerImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(cornerImage, 40, 40);

            cornerImageNextRot.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(cornerImageNextRot, 0, 40);
            

            //foreach (KeyValuePair<int, int> item in listOfSideLocations)
            //{
            //    //var tmp = new Bitmap(sideImage);
            //    sideImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //    g.DrawImage(sideImage, item.Key, item.Value);
            //}


            //int vertical = 0;
            //int horizontal = 0;

            //hashedValues.ForEach(a =>
            //{
            //    if (a % 2 == 0 && horizontal < 40)
            //    {
            //        g.DrawImage(imageFile, horizontal, vertical);
            //        if (horizontal != 20)
            //            g.DrawImage(imageFile, 40 - horizontal, vertical);
            //        mainImage.Save(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\tos1.png");
            //    }

            //    if (horizontal >= 20)
            //    {
            //        vertical += 20;
            //        horizontal = 0;
            //    }
            //    else
            //        horizontal += 20;

            //});
        }


        mainImage.Save(@"C:\Users\Ragnus\Desktop\Identicon\IdenticonAvatar\IdenticonDraw\IdenticonDraw\yos.png");

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

    private static Bitmap InitalizeFirstRotation(Bitmap tmp, int siderot)
    {
        for (int i = 0; i < siderot; i++)
        {
            tmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }
        return tmp;
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