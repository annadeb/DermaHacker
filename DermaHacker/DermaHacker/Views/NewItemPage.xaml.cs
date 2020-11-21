using DermaHacker.Models;
using DermaHacker.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DermaHacker.Models.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DermaHacker.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Report Report { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}