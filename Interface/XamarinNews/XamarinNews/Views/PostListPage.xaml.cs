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
        ObservableCollection<Post> posts;
        public PostListPage(List<string> p)
        {
            BindingContext = viewModel = new PostListViewModel(p[1]);



            ItemsListView.ItemsSource = posts;
        }

        private void ItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}