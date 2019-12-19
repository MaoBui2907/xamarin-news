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

        public static CategoryManager CategoryManager { get; private set; }
        public static PostManager PostManager { get; private set; }
        
        public App(string databasePath)
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            CategoryManager = new CategoryManager (new RestService ());
            PostManager = new PostManager(new RestService());

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
