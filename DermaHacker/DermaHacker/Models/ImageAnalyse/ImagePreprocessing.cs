using Android.Content;
using DermaHacker.Models.Extension;
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
       

        public static  byte[] GetByteArrayFromStream(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
        public static ImageSource GetImageSourceFromByteArray(byte[] image)
        {
          return  ImageSource.FromStream(() => new MemoryStream(image));
        }

        //public static Bitmap GetBitmapFromImageSource(ImageSource imageSource)
        //{

        //    Context context = Android.App.Application.Context;
        //     var a = GetBitmapFromImageSourceAsync(imageSource, context);
        //    return a.Result;
        //}

        //public static async Task<Android.Graphics.Bitmap> GetBitmapFromImageSourceAsync(ImageSource source, Context context)
        //{
        //    var handler = ExtensionMethod.GetHandler(source);
        //    var returnValue = (Android.Graphics.Bitmap)null;
        //    returnValue = await handler.LoadImageAsync(source, context);
        //    return returnValue;
        //}
    }
}
