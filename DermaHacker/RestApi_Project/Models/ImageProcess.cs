using MathWorks.MATLAB.NET.Arrays;
using RestApi_Dicom.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using analyseImage;

namespace RestApi_Dicom.Models
{
    public class ImageProcess
    {

       

        public void SaveToFile(ImageData imageData)
        {
            byte[] a = Convert.FromBase64String(imageData.Base64);
            MemoryStream memoryStream = new MemoryStream(a);
            
            Image image = System.Drawing.Image.FromStream(memoryStream);
            image.Save(imageData.guid.ToString() + ".png", ImageFormat.Png);
        }
        public void GrayScaleImageTest(ImageData imageData)
        {
            byte[] a = Convert.FromBase64String(imageData.Base64);
            //MemoryStream memoryStream = new MemoryStream(a);
            //Image image = System.Drawing.Image.FromStream(memoryStream);
            Analyse z = new Analyse();
            MWNumericArray mWNumericArray = a;
            MWArray I = z.analyseImage(mWNumericArray, imageData.CoordinateXY[0], imageData.CoordinateXY[1]);
            MWNumericArray I_num = I.ToArray();
            Byte[] I_bytes = (Byte[])I_num.ToVector(MWArrayComponent.Real);

            int w = I.Dimensions[0];
            int h = I.Dimensions[1];

            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format8bppIndexed);
            ColorPalette cp = bmp.Palette;

            for (Int32 i = 0; i < 256; ++i)
                cp.Entries[i] = Color.FromArgb(255, i, i, i);

            bmp.Palette = cp;

            BitmapData data = bmp.LockBits((new Rectangle(0, 0, bmp.Width, bmp.Height)), ImageLockMode.WriteOnly, bmp.PixelFormat);
            Marshal.Copy(I_bytes, 0, data.Scan0, I_bytes.Length);
            bmp.UnlockBits(data);
            bmp.Save("image.png", ImageFormat.Png);
        }

        public void ReadImage(ImageData imageData)
        {
            byte[] a = Convert.FromBase64String(imageData.Base64);
            MemoryStream memoryStream = new MemoryStream(a);

            Image image = System.Drawing.Image.FromStream(memoryStream);
            image.Save(imageData.guid.ToString() + ".png", ImageFormat.Png);

            try
            {
                Analyse z = new Analyse();
        MWArray[] I = z.analyseImage(5,imageData.guid.ToString() + ".png", imageData.CoordinateXY[0], imageData.CoordinateXY[1]);
      //          MWNumericArray I_num = I[4].ToArray();
        //        Byte[] I_bytess = (Byte[])I_num.ToVector(MWArrayComponent.Real);
           //     Byte[] I_bytes = I_bytess.SelectMany(value => BitConverter.GetBytes(value)).ToArray();

                int w =  I[4].Dimensions[0];
                int h =  I[4].Dimensions[1];
                //I[3] = null;
                imageData.Base64 = null;
                Bitmap bmp = new Bitmap(w, h, PixelFormat.Format8bppIndexed);
                ColorPalette cp = bmp.Palette;

                for (Int32 i = 0; i < 256; ++i)
                    cp.Entries[i] = Color.FromArgb(255, i, i, i);

                bmp.Palette = cp;

                BitmapData data = bmp.LockBits((new Rectangle(0, 0, bmp.Width, bmp.Height)), ImageLockMode.WriteOnly, bmp.PixelFormat);
          //      Marshal.Copy(I_bytess, 0, data.Scan0, I_bytess.Length);
                bmp.UnlockBits(data);
                bmp.Save(imageData.guid.ToString() + ".png", ImageFormat.Png);
                byte[] arr = File.ReadAllBytes(imageData.guid.ToString() + ".png");
                imageData.Base64 = Convert.ToBase64String(arr);
                imageData.Matlab = new FromMatlab();
                imageData.Matlab.Width = I[0].ToString();
                imageData.Matlab.Height = I[1].ToString();
                imageData.Matlab.Arena = I[2].ToString();
                double[,] ale = (double[,])I[3].ToArray();
                imageData.Matlab.MatrixC1 =Math.Round(((double [,]) I[3].ToArray())[0,0],2).ToString();
                imageData.Matlab.MatrixC2 = Math.Round(((double[,])I[3].ToArray())[1, 0], 2).ToString();
                imageData.Matlab.MatrixC3 = Math.Round(((double[,])I[3].ToArray())[2, 0], 2).ToString();

                imageData.IsFinish();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
