using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinNews.Models;
using XamarinNews.ViewModels;

namespace XamarinNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        CategoryViewModel viewModel;
        ObservableCollection<Category> categories;
        public CategoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoryViewModel();
            categories = new ObservableCollection<Category>();
            LoadImageSwitch.IsToggled = App.LoadImage;
            SummaRateValue.Text = App.SummaRate.ToString() + "%";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            categories = new ObservableCollection<Category>(viewModel.GetCategories());
            ListViewMenu.ItemsSource = categories;
            ListViewMenu.SelectedItem = categories[0];
            if(App.SummaRate == 10)
            {
                SummaDecr.IsEnabled = false;
            }
            if(App.SummaRate == 90)
            {
                SummaIncr.IsEnabled = false;
            }
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                var item = e.SelectedItem as Category;
                if (item == null)
                {
                    return;
                }
                var _i = item.toList();
                await RootPage.NavigateFromMenu(_i);
            };
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Category item = args.SelectedItem as Category;
            if (item == null)
                return;
            await RootPage.NavigateFromMenu(item.toList());
        }

        private void LoadImageSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            App.LoadImage = !App.LoadImage;
        }

        private void SummaIncr_Clicked(object sender, EventArgs e)
        {
            App.SummaRate = App.SummaRate + 10;
            SummaRateValue.Text = App.SummaRate.ToString() + "%";
            if (App.SummaRate == 10)
            {
                SummaDecr.IsEnabled = false;
            }
            else
            {
                SummaDecr.IsEnabled = true;
            }
            if (App.SummaRate == 90)
            {
                SummaIncr.IsEnabled = false;
            }
            else
            {
                SummaIncr.IsEnabled = true;
            }
        }

        private void SummaDecr_Clicked(object sender, EventArgs e)
        {
            App.SummaRate = App.SummaRate - 10;
            SummaRateValue.Text = App.SummaRate.ToString() + "%";
            if (App.SummaRate == 10)
            {
                SummaDecr.IsEnabled = false;
            }
            else
            {
                SummaDecr.IsEnabled = true;
            }
            if (App.SummaRate == 90)
            {
                SummaIncr.IsEnabled = false;
            }
            else
            {
                SummaIncr.IsEnabled = true;
            }
        }
    }
}