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
        }
    }
}