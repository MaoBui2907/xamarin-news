using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


using XamarinNews.Models;
using XamarinNews.ViewModels;

namespace XamarinNews.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add(0, new NavigationPage(new PostListPage(new PostListViewModel(new List<string> { "0", "Tin nóng", "ic1", "trend" }, 1))));

            //var run = new Command (async () => await NavigateFromMenu(new List<string> { "0", "Tin nóng", "ic1", "trend" }));
        }

        public async Task NavigateFromMenu(List<string> p)
        {
            int id = int.Parse(p[0]);
            if (!MenuPages.ContainsKey(id))
            {
                MenuPages.Add(id, new NavigationPage(new PostListPage(new PostListViewModel(p, 1))));
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}