using DermaHacker.Models;
using Syncfusion.Pdf.Graphics;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.App.Usage;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using Xamarin.Essentials;
using Application = Android.App.Application;

namespace DermaHacker.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string nameAndSurname;
        private string date;
        private string standardImagePath;
        private string thermoImagePath;
        private double length;
        private double width;
        private double surface;
        private double woundBaseTemperature;
        private double surroundingsTemperature;
        private double granulationTissuePercentage;
        private double sludgePercentage;
        private double necrosisPercentage;

        public Command ExportCommand { get; }

        public ItemDetailViewModel()
        {
            ExportCommand = new Command( OnGeneratePdf);
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
            get
            {
                return itemId;
            }
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
        public string ThermoImagePath
        {
            get => thermoImagePath;
            set => SetProperty(ref thermoImagePath, value);
        }

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

        public double WoundBaseTemperature
        {
            get => woundBaseTemperature;
            set => SetProperty(ref woundBaseTemperature, value);
        }

        public double SurroundingsTemperature
        {
            get => surroundingsTemperature;
            set => SetProperty(ref surroundingsTemperature, value);
        }

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
                ThermoImagePath = item.ThermoImagePath;
                Length = item.Length;
                Width = item.Width;
                Surface = item.Surface;
                WoundBaseTemperature = item.WoundBaseTemperature;
                SurroundingsTemperature = item.SurroundingsTemperature;
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

            graphics.DrawString("Name and Surname:"+NameAndSurname, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Date:" + Date, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Size:", font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Length:" + Length, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Width:" + Width, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Surface:" + Surface, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Wound base:", font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Granulation Tissue:" + GranulationTissuePercentage, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Sludge:"+ SludgePercentage, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Necrosis:" + NecrosisPercentage, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Temperature" , font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Woundbase:" + WoundBaseTemperature, font, PdfBrushes.Black, new PointF(0, 0));
            graphics.DrawString("Surroundings:" + SurroundingsTemperature, font, PdfBrushes.Black, new PointF(0, 0));


            MemoryStream stream = new MemoryStream();
            document.Save(stream);
             
            document.Close(true);
            if (ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.WriteExternalStorage) !=
                Permission.Granted)
            {
                var status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            var intent = Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application / pdf", stream, PDFOpenContext.ChooseApp, Application.Context);
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
                var uri = FileProvider.GetUriForFile(Application.Context,Android.App.Application.Context.PackageName + ".fileprovider", file);
                
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
       
    }

    internal interface ISave
    {
        Intent SaveAndView(string fileName, string contentType, MemoryStream stream, PDFOpenContext context, Context appctx);
    }
    public enum PDFOpenContext
    {
        InApp,
        ChooseApp
    }
}
