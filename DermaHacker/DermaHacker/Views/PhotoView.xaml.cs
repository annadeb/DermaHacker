using Android.Content;
using Android.Graphics;
using DermaHacker.Models.Extension;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Xaml;
using DermaHacker.Models.ImagePreprocessing;

namespace DermaHacker.Views
{

    public partial class PhotoView : ContentPage
    {
        public PhotoView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            this.Title = "Take Me Photo";
        }
      
        public static async Task<Android.Graphics.Bitmap> GetBitmapFromImageSourceAsync(ImageSource source, Context context)
        {
            var handler = ExtensionMethod.GetHandler(source);
            var returnValue = (Android.Graphics.Bitmap)null;
            returnValue = await handler.LoadImageAsync(source, context);
            return returnValue;
        }
        //public static byte[] ReadFully(Stream input)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        input.CopyTo(ms);
        //        return ms.ToArray();
        //    }

        //}
        private async void BtnCam_Clicked(object sender, EventArgs e)
        {
            try
            {


                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                    Directory = "Xamarin",
                    SaveToAlbum = true
                });

                if (photo != null)
                {
                    byte[] TargetImageByte = ImagePreprocessing.GetByteArrayFromStream(photo.GetStream());
                    //  System.Drawing.Bitmap bitmap = ImagePreprocessing.GetBitmapFromImageSource(ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte));
                    // Mat
                    ImagePreprocessing.GetImage(TargetImageByte);
                 //       Image<Gray, byte> grayFrame = new Image<Gray, byte>(ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte).ToString());

                 //    imgCam.Source = ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte);
                 //  Accord.Imaging.Image.SetGrayscalePalette(bitmap);
                 var a =    ImagePreprocessing.GetBitmapFromStream(photo.GetStream());

                    using (var stream = new MemoryStream())
                    {
                 //       bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                //        imgCam.Source = ImageSource.FromStream(() => stream);
                    }
                }
               // byte[] imageArray = System.IO.File.ReadAllBytes(photo.Path);
             //   System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imgCam.Source);  // Android.Graphics.BitmapFactory.DecodeByteArray(imageArray,0 ,imageArray.Length).to;



            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {

        }


    }
}

//Do przetworzenia !!!!
//byte[] TargetImageByte = ReadFully(photo.GetStream());
//imgCam.Source = ImageSource.FromStream(() => new MemoryStream(TargetImageByte));
