using DermaHacker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DermaHacker.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}