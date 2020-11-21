using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using DermaHacker.Models.Extension;
using DermaHacker.Models.ImagePreprocessing;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DermaHacker.Views
{
    public partial class PhotoPage : ContentPage
    {
        public PhotoPage()
        {
            InitializeComponent();
           // this.Title = "Take Me Photo";
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
                    byte[] targetImageByte = ImagePreprocessing.GetByteArrayFromStream(photo.GetStream());
                    var imagesource = ImagePreprocessing.GetImageSourceFromByteArray(targetImageByte);
                    imgCam.Source = imagesource;
                    //  System.Drawing.Bitmap bitmap = ImagePreprocessing.GetBitmapFromImageSource(ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte));
                    // Mat
                    //   ImagePreprocessing.GetImage(TargetImageByte);
                    //       Image<Gray, byte> grayFrame = new Image<Gray, byte>(ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte).ToString());

                    //    imgCam.Source = ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte);
                    //  Accord.Imaging.Image.SetGrayscalePalette(bitmap);
                    //var a =    ImagePreprocessing.GetBitmapFromStream(photo.GetStream());

                    //    using (var stream = new MemoryStream())
                    //    {
                    // //       bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    ////        imgCam.Source = ImageSource.FromStream(() => stream);
                    //    }
                }
                // byte[] imageArray = System.IO.File.ReadAllBytes(photo.Path);
                //   System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imgCam.Source);  // Android.Graphics.BitmapFactory.DecodeByteArray(imageArray,0 ,imageArray.Length).to;



            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
        }

    }
}