using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinNews.Services;
using XamarinNews.Views;

namespace XamarinNews
{
    public partial class App : Application
    {
        public static string DatabasePath;

        public static bool LoadImage { get; set; }
        public App(string databasePath)
        {
            InitializeComponent();

            DatabasePath = databasePath;

            MainPage = new MainPage();
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
