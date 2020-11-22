using DermaHacker.Models;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using DermaHacker.Models.ImageAnalyse;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Grid;
using Xamarin.Essentials;
using Application = Android.App.Application;
using PointF = Syncfusion.Drawing.PointF;

namespace DermaHacker.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string nameAndSurname;
        private string date;
        private string standardImagePath;
        //private string thermoImagePath;
        private double length;
        private double width;
        private double surface;
        //private double woundBaseTemperature;
        //private double surroundingsTemperature;
        private double granulationTissuePercentage;
        private double sludgePercentage;
        private double necrosisPercentage;

        public Command ExportCommand { get; }

        public ItemDetailViewModel()
        {
            ExportCommand = new Command(OnGeneratePdf);
        }

        public string Id { get; set; }

        public string NameAndSurname
        {
            get => nameAndSurname;
            set => SetProperty(ref nameAndSurname, value);
        }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string ItemId
        {
            get { return itemId; }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public string StandardImagePath
        {
            get => standardImagePath;
            set => SetProperty(ref standardImagePath, value);
        }

        //public string ThermoImagePath
        //{
        //    get => thermoImagePath;
        //    set => SetProperty(ref thermoImagePath, value);
        //}

        public double Length
        {
            get => length;
            set => SetProperty(ref length, value);
        }

        public double Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }

        public double Surface
        {
            get => surface;
            set => SetProperty(ref surface, value);
        }

        //public double WoundBaseTemperature
        //{
        //    get => woundBaseTemperature;
        //    set => SetProperty(ref woundBaseTemperature, value);
        //}

        //public double SurroundingsTemperature
        //{
        //    get => surroundingsTemperature;
        //    set => SetProperty(ref surroundingsTemperature, value);
        //}

        public double GranulationTissuePercentage
        {
            get => granulationTissuePercentage;
            set => SetProperty(ref granulationTissuePercentage, value);
        }

        public double SludgePercentage
        {
            get => sludgePercentage;
            set => SetProperty(ref sludgePercentage, value);
        }

        public double NecrosisPercentage
        {
            get => necrosisPercentage;
            set => SetProperty(ref necrosisPercentage, value);
        }


        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await App.Database.GetReportAsync(int.Parse(itemId));
                Id = item.ID.ToString();
                NameAndSurname = item.NameAndSurname;
                Date = item.Date.ToString("g", CultureInfo.CreateSpecificCulture("en-us"));
                StandardImagePath = item.StandardImagePath;
                //ThermoImagePath = item.ThermoImagePath;
                Length = item.Length;
                Width = item.Width;
                Surface = item.Surface;
                //WoundBaseTemperature = item.WoundBaseTemperature;
                //SurroundingsTemperature = item.SurroundingsTemperature;
                GranulationTissuePercentage = item.GranulationTissuePercentage;
                SludgePercentage = item.SludgePercentage;
                NecrosisPercentage = item.NecrosisPercentage;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        async void OnGeneratePdf()
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.Pages.Add();

            PdfGraphics graphics = page.Graphics;


            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            string imagePath = StandardImagePath;

            Java.IO.File myDir = new Java.IO.File(imagePath);
            FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

            StreamReader r = new StreamReader(fs);

            //Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            byte[] buffer;
            byte[] buffer2;
            PdfBitmap image;
            using (Stream stream2 = r.BaseStream)
            {
                long length = stream2.Length;
                buffer = new byte[length];
                stream2.Read(buffer, 0, (int) length);
                buffer2 = ResizeImage(buffer, 300,400);
                stream2.Read(buffer2, 0, (int)buffer2.Length);
                image = new PdfBitmap(stream2);
            }

            //Draw the image
            graphics.DrawImage(image, 40, 0);

            graphics.DrawString("Name, surname: " + NameAndSurname, font, PdfBrushes.Black, new PointF(40, 520));
            graphics.DrawString("Date: " + Date, font, PdfBrushes.Black, new PointF(40, 540));
            graphics.DrawString("Size: ", font, PdfBrushes.Black, new PointF(40, 560));
            graphics.DrawString("Length: " + Length + " cm", font, PdfBrushes.Black, new PointF(60, 580));
            graphics.DrawString("Width: " + Width + " cm", font, PdfBrushes.Black, new PointF(60, 600));
            graphics.DrawString("Surface: " + Surface + " cm^2", font, PdfBrushes.Black, new PointF(40, 620));
            graphics.DrawString("Wound base: ", font, PdfBrushes.Black, new PointF(40, 640));
            graphics.DrawString("Granulation Tissue: " + GranulationTissuePercentage + "%", font, PdfBrushes.Black,
                new PointF(60, 660));
            graphics.DrawString("Sludge: " + SludgePercentage + "%", font, PdfBrushes.Black, new PointF(60, 680));
            graphics.DrawString("Necrosis: " + NecrosisPercentage + "%", font, PdfBrushes.Black, new PointF(60, 700));
            //graphics.DrawString("Temperature: ", font, PdfBrushes.Black, new PointF(40, 720));
            //graphics.DrawString("Woundbase: " + WoundBaseTemperature + " C", font, PdfBrushes.Black,
            //    new PointF(60, 740));
            //graphics.DrawString("Surroundings: " + SurroundingsTemperature + " C", font, PdfBrushes.Black,
            //    new PointF(60, 760));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            document.Close(true);
            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context,
                    Manifest.Permission.WriteExternalStorage) !=
                Permission.Granted)
            {
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            var intent = Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application / pdf",
                stream, PDFOpenContext.ChooseApp, Application.Context);
            intent.SetFlags(ActivityFlags.NewTask);
            intent.AddFlags(ActivityFlags.MultipleTask);
            intent.AddFlags(ActivityFlags.FromBackground);

            await SendEmail();
        }

        public async Task SendEmail()
        {

            try
            {
                Intent email = new Intent(Android.Content.Intent.ActionSend);

                var fn = "Output.pdf";
                string root = null;

                if (Android.OS.Environment.IsExternalStorageEmulated)
                {
                    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
                }
                else
                    root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                Java.IO.File myDir = new Java.IO.File(root + "/PDFFiles");
                myDir.Mkdir();

                Java.IO.File file = new Java.IO.File(myDir, fn);
                var uri = FileProvider.GetUriForFile(Application.Context,
                    Android.App.Application.Context.PackageName + ".fileprovider", file);

                email.SetFlags(ActivityFlags.NewTask);
                email.AddFlags(ActivityFlags.MultipleTask);
                email.PutExtra(Android.Content.Intent.ExtraSubject, NameAndSurname + Date);
                email.PutExtra(Intent.ExtraStream, uri);
                email.SetType("application/pdf");
                Application.Context.StartActivity(email);

            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred

            }
        }


        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap 
            BitmapFactory.Options
                options =
                    new BitmapFactory.Options(); // Create object of bitmapfactory's option method for further option use
            options.InPurgeable = true; // inPurgeable is used to free up memory while required
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);

            float newHeight = 0;
            float newWidth = 0;

            var originalHeight = originalImage.Height;
            var originalWidth = originalImage.Width;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                float ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                float ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int) newWidth, (int) newHeight, true);

            originalImage.Recycle();

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);

                resizedImage.Recycle();

                return ms.ToArray();
            }

        }
    }

    internal interface ISave
    {
        Intent SaveAndView(string fileName, string contentType, MemoryStream stream, PDFOpenContext context,
            Context appctx);
    }

    public enum PDFOpenContext
    {
        InApp,
        ChooseApp
    }
}
   

