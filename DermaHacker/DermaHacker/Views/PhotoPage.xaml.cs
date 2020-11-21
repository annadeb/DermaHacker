using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using DermaHacker.Models.Extension;
using DermaHacker.Models.ImagePreprocessing;
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
                    this.Title = "Choose a central point of a wound";
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
        private void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            var viewPoint = args.Location;
            SKPoint point =
                new SKPoint((float)(canvasView.CanvasSize.Width * viewPoint.X / canvasView.Width),
                    (float)(canvasView.CanvasSize.Height * viewPoint.Y / canvasView.Height));

            var actionType = args.Type;
            _touchGestureRecognizer.ProcessTouchEvent(args.Id, actionType, point);
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