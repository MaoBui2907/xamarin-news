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
    public partial class PostListPage : ContentPage
    {
        PostListViewModel viewModel;

        public PostListPage()
        {
            viewModel = new PostListViewModel(new List<string> { "0", "Tin nóng", "ic1", "/trend" }, 1);
            BindingContext = viewModel;
        }
        public PostListPage(PostListViewModel vm)
        {
            viewModel = vm;
            BindingContext = viewModel;
        }

        /*protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.posts.Count == 0)
                viewModel.FetchPostListCommand.Execute(null);
        }*/

        private void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}