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
        private string date;

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
                && !String.IsNullOrWhiteSpace(date);
        }

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
                //Size = new Models.Database.Size()
                //{
                //    Length = 22,
                //    Width = 10,
                //    Surface = 40,
                //},
                //WoundBase = new WoundBase()
                //{
                //    GranulationTissuePercentage = 3,
                //    SludgePercentage = 59,
                //    NecrosisPercentage = 25
                //},
                //Temperature = new Temperature()
                //{
                //    WoundBase = 31,
                //    Surroundings = 27
                //}
            });

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
