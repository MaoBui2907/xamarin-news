using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using XamarinNews.Models;
using XamarinNews.Views;
using XamarinNews.ViewModels;

namespace XamarinNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;

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
            
            viewModel.getCategory();
            categories = viewModel.Categories;
            ListViewMenu.ItemsSource = categories;

            menuItems = new List<HomeMenuItem>();
        

            ListViewMenu.SelectedItem = categories[0];
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Category item = args.SelectedItem as Category;
            if (item == null)
                return;

            await Navigation.PushAsync(new PostListPage(Category.toList()));

        }
    }
}