using DermaHacker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DermaHacker.Models.Database;
using Xamarin.Forms;

namespace DermaHacker.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string nameAndSurname;
        private DateTime date;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(nameAndSurname)
                && !String.IsNullOrWhiteSpace(date.ToString());
        }

        public string NameAndSurname
        {
            get => nameAndSurname;
            set => SetProperty(ref nameAndSurname, value);
        }

        public string Date
        {
            get => DateTime.UtcNow.ToString();
           // set => SetProperty(ref date, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                NameAndSurname = NameAndSurname,
                Date = Date
            };

            //await DataStore.AddItemAsync(newItem);
            //TODO
            await App.Database.SaveReportAsync(new Report
            {
                NameAndSurname = NameAndSurname,
                Date = DateTime.UtcNow,
                StandardImagePath = "icon_about.png",
                ThermoImagePath = "icon_about.png",
                Length = 22,
                Width = 10.0,
                Surface = 40,
                GranulationTissuePercentage = 3,
                SludgePercentage = 59,
                NecrosisPercentage = 25,
                WoundBaseTemperature = 31,
                SurroundingsTemperature = 27
                
            });

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
