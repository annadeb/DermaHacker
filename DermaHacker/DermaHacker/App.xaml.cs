using DermaHacker.Services;
using DermaHacker.Views;
using System;
using System.IO;
using DermaHacker.Models.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DermaHacker
{
    public partial class App : Application
    {

        public static bool IsWorks = false;
        static DermaHackerDatabase database;
        public App()
        {
            InitializeComponent();


            DependencyService.Register<DermaHackerDatabase>();
            MainPage = new AppShell();

            //DependencyService.Register<MockDataStore>();
            //MainPage = new PhotoView();

        }

        public static DermaHackerDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DermaHackerDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DermaHacker.db3"));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
