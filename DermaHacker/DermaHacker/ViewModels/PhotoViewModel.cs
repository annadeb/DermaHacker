using System;
using System.Windows.Input;
using DermaHacker.Models.ImagePreprocessing;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DermaHacker.ViewModels
{
    public class PhotoViewModel : BaseViewModel
    {
        public PhotoViewModel()
        {
            Title = "Take a picture";
            //TakePhotoCommand = new Command(TakePhotoAsync());

            //PhotoTapped = new Command<int[]>(OnTapp());
        }

        //private Action<int[]> OnTapp()
        //{
        //    //throw new NotImplementedException();
        //}

        //public Command<int[]> PhotoTapped { get; }
        //public Command TakePhotoCommand { get; }

        //public async System.Threading.Tasks.Task TakePhotoAsync()
        //{
        //    try
        //    {


        //        var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
        //        {
        //            DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
        //            Directory = "Xamarin",
        //            SaveToAlbum = true
        //        });

        //        if (photo != null)
        //        {
        //            byte[] targetImageByte = ImagePreprocessing.GetByteArrayFromStream(photo.GetStream());
        //            var imagesource = ImagePreprocessing.GetImageSourceFromByteArray(targetImageByte);
        //            TakenPhoto = imagesource;
        //            //  System.Drawing.Bitmap bitmap = ImagePreprocessing.GetBitmapFromImageSource(ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte));
        //            // Mat
        //            //   ImagePreprocessing.GetImage(TargetImageByte);
        //            //       Image<Gray, byte> grayFrame = new Image<Gray, byte>(ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte).ToString());

        //            //    imgCam.Source = ImagePreprocessing.GetImageSourceFromByteArray(TargetImageByte);
        //            //  Accord.Imaging.Image.SetGrayscalePalette(bitmap);
        //            //var a =    ImagePreprocessing.GetBitmapFromStream(photo.GetStream());

        //            //    using (var stream = new MemoryStream())
        //            //    {
        //            // //       bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //            ////        imgCam.Source = ImageSource.FromStream(() => stream);
        //            //    }
        //        }
        //        // byte[] imageArray = System.IO.File.ReadAllBytes(photo.Path);
        //        //   System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imgCam.Source);  // Android.Graphics.BitmapFactory.DecodeByteArray(imageArray,0 ,imageArray.Length).to;



        //    }
        //    catch (Exception ex)
        //    {
        //        //await DisplayAlert("Error", ex.Message.ToString(), "Ok");
        //    }
        //}

        //TakenPhoto = "xamarin_logo.png";
    }


}