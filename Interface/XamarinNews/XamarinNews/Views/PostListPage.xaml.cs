using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinNews.ViewModels;
using XamarinNews.Models;


namespace XamarinNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostListPage : ContentPage
    {
        PostListViewModel viewModel;

        public PostListPage()
        {
            InitializeComponent();
            viewModel = new PostListViewModel(new List<string> { "0", "Tin nóng", "ic1", "trend"}, 1);
            BindingContext = viewModel;
        }
        public PostListPage(PostListViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            BindingContext = viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.posts.Count == 0)
            {
                PostListView.IsVisible = false;
                EmptyNoti.IsVisible = true; 
            }
            else
            {
                PostListView.IsVisible = true;
                EmptyNoti.IsVisible = false;
                PostListView.ItemsSource = viewModel.posts;
            }
        }
    }
}