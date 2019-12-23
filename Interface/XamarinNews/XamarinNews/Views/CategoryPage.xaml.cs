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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            categories = new ObservableCollection<Category>(viewModel.GetCategories());
            ListViewMenu.ItemsSource = categories;
            ListViewMenu.SelectedItem = categories[0];

            
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Category item = args.SelectedItem as Category;
            if (item == null)
                return;
            await RootPage.NavigateFromMenu(item.toList());
        }

    }
}