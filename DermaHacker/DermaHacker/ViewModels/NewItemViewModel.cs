using DermaHacker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using DermaHacker.Models.Database;
using DermaHacker.Views;
using Xamarin.Forms;
using System.Linq;

namespace DermaHacker.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string nameAndSurname;
        private DateTime date;
        private Report report;

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
            
            //await DataStore.AddItemAsync(newItem);
            //TODO
            this.report = Report.CreateReport();
            this.report.NameAndSurname = NameAndSurname;
            this.report.ID = App.Database.GetReportsAsync().Result.Count()+1;
            await App.Database.SaveReportAsync(this.report);

            // This will pop the current page off the navigation stack
            // await Shell.Current.GoToAsync("..");
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={this.report.ID}");
            CurrentReport.Instance.ClearCurrentReport();
        }
    }
}
