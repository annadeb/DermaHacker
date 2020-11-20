﻿using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DermaHacker.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(TakePhoto);
        }

        public void TakePhoto()
        {
            TakenPhoto = "xamarin_logo.png";
        } 
        public ICommand OpenWebCommand { get; }
    }
}