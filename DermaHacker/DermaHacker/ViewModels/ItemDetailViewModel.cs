using DermaHacker.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

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
    }
}
