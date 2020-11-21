using DermaHacker.Models;
using Syncfusion.Pdf.Graphics;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using Xamarin.Essentials;

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
        void OnGeneratePdf()
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
            
            SendEmail();
        }
       
        public async Task SendEmail()
        {
            
                try
                {
                    var message = new EmailMessage
                    {
                        Subject = NameAndSurname+ Date,
                        Body = "The attachment is a pdf file.",
                        //To = "recipients",
                        //Subject = subject,
                        //Body = body,
                        //To = recipients,
                        //Cc = ccRecipients,
                        //Bcc = bccRecipients
                    };
               // var fn = "attachment.pdf";
               // var filePath = Path.Combine(FileSystem.CacheDirectory, fn);
              //  string folderPath = DependencyService.Get<>().SavePath(stream, filePath);

              //  message.Attachments.Add(new EmailAttachment(folderPath));
                await Email.ComposeAsync(message);
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
}
