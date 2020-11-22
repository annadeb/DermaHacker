using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using DermaHacker.Models;
using DermaHacker.Models.Connection;
using DermaHacker.Models.Database;
using DermaHacker.Models.Extension;
using DermaHacker.Models.ImageAnalyse;
using DermaHacker.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaScene;
using SkiaScene.TouchManipulation;
using SkiaSharp;
using TouchTracking;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace DermaHacker.Views
{
    public partial class PhotoPage : ContentPage
    {
        public PhotoPage()
        {
            InitializeComponent();
            this.Title = "Take a picture";
            _touchGestureRecognizer = new TouchGestureRecognizer();
       
    }
 private ISKScene _scene;
        private ITouchGestureRecognizer _touchGestureRecognizer;
        private ISceneGestureResponder _sceneGestureResponder;
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
        byte[] targetImageByte;
        private async void BtnCam_Clicked(object sender, EventArgs e)
        {
            try
            {

                App.IsWorks = false;
                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear,
                    Directory = "Xamarin",
                    SaveToAlbum = true
                });

                if (photo != null)
                {
                    this.Title = "Choose a central point of a wound";
                    targetImageByte = ImagePreprocessing.GetByteArrayFromStream(photo.GetStream());
                    var imagesource = ImagePreprocessing.GetImageSourceFromByteArray(targetImageByte);
                    CurrentReport.Instance.StandardImagePath = photo.AlbumPath;
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
        private async void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            if (!App.IsWorks)
            {
                App.IsWorks = true;
                var viewPoint = args.Location;
                SKPoint point =
                    new SKPoint((float)(canvasView.CanvasSize.Width * viewPoint.X / canvasView.Width),
                        (float)(canvasView.CanvasSize.Height * viewPoint.Y / canvasView.Height));

                var actionType = args.Type;
                _touchGestureRecognizer.ProcessTouchEvent(args.Id, actionType, point);

                ICommanderReceivedData commanderReceivedData = new ImageStored();
                RequestClass requestClass = RequestClass.Instance(commanderReceivedData);

       
                ImageData image = new ImageData();
                image.CoordinateXY = new int[] {  (int)point.X, (int)point.Y };
                image.Base64 = Convert.ToBase64String(targetImageByte);
                image.Id = 1;
                image = requestClass.SendAndTakeImage(image);

                if (image.Base64 != null)
                {
                    targetImageByte = Convert.FromBase64String(image.Base64);
                    imgCam.Source = ImagePreprocessing.GetImageSourceFromByteArray(targetImageByte);
               //     App.IsWorks = false;
                }
                else
                {
                    IsBusy = false;
         //           App.IsWorks = false;
                }

                CurrentReport.Instance.Date = DateTime.UtcNow;
                // CurrentReport.Instance.ThermoImagePath = "icon_about.png";
                CurrentReport.Instance.Length = Math.Round(double.Parse(image.Matlab.Height),2);
                CurrentReport.Instance.Width = Math.Round(double.Parse(image.Matlab.Width),2);
                CurrentReport.Instance.Surface = Math.Round(double.Parse(image.Matlab.Arena),2);
                CurrentReport.Instance.GranulationTissuePercentage = Math.Round(double.Parse(image.Matlab.MatrixC1), 2);
                CurrentReport.Instance.SludgePercentage = Math.Round(double.Parse(image.Matlab.MatrixC2), 2);
                CurrentReport.Instance.NecrosisPercentage = Math.Round(double.Parse(image.Matlab.MatrixC3), 2);
                //CurrentReport.Instance.WoundBaseTemperature = 0;
                // CurrentReport.Instance.SurroundingsTemperature = 0;
                var secondPage = new NewItemPage();

                await Navigation.PushAsync(secondPage);
            }
        }
        //private void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        //{
        //    var viewPoint = args.Location;
        //    SKPoint point =
        //        new SKPoint((float)(imgCam.Width * viewPoint.X / imgCam.Width),
        //            (float)(imgCam.Height * viewPoint.Y / imgCam.Height));

        //    var actionType = args.Type;
        //    _touchGestureRecognizer.ProcessTouchEvent(args.Id, actionType, point);
        //}
        private void SetSceneCenter()
        {
            if (_scene == null)
            {
                return;
            }
            var centerPoint = new SKPoint(canvasView.CanvasSize.Width / 2, canvasView.CanvasSize.Height / 2);
            _scene.ScreenCenter = centerPoint;
        }
        private void InitSceneObjects()
        {
            _scene = new SKScene(new TestScenereRenderer())
            {
                MaxScale = 10,
                MinScale = 0.3f,
            };
            SetSceneCenter();
            _touchGestureRecognizer = new TouchGestureRecognizer();
            _sceneGestureResponder = new SceneGestureRenderingResponder(() => canvasView.InvalidateSurface(), _scene, _touchGestureRecognizer)
            {
                TouchManipulationMode = TouchManipulationMode.IsotropicScale,
                MaxFramesPerSecond = 100,
            };
            _sceneGestureResponder.StartResponding();
        }


        private void OnPaint(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs skPaintSurfaceEventArgs)
        {
            if (_scene == null)
            {
                InitSceneObjects();

            }
            SKImageInfo info = skPaintSurfaceEventArgs.Info;
            SKSurface surface = skPaintSurfaceEventArgs.Surface;
            SKCanvas canvas = surface.Canvas;
            _scene.Render(canvas);
        }
    }
}