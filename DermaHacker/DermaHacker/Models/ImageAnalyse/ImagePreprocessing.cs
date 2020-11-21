using Android.Content;
using DermaHacker.Models.Extension;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DermaHacker.Models.ImagePreprocessing
{
    class ImagePreprocessing
    {


        public static byte[] GetByteArrayFromStream(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public static ImageSource GetImageSourceFromByteArray(byte[] image)
        {
            return ImageSource.FromStream(() => new MemoryStream(image));
        }

        //public static Bitmap GetBitmapFromImageSource(ImageSource imageSource)
        //{

        //    Context context = Android.App.Application.Context;
        //     var a = GetBitmapFromImageSourceAsync(imageSource, context);
        //    return a.Result;
        //}
        public static   Image<Gray, byte> GetImage(byte[] array)
        {
            Image<Gray, byte> image = new Image<Gray, byte>(640, 480);
            image.Bytes = array;
            return image;
        }
        public static Bitmap GetBitmapFromStream(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return (Bitmap)System.Drawing.Bitmap.FromStream(stream);
            }
          
        }
    }
}
