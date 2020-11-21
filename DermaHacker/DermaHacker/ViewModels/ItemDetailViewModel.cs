using DermaHacker.Models;
using System;
using System.Diagnostics;
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

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                NameAndSurname = item.NameAndSurname;
                Date = item.Date;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
