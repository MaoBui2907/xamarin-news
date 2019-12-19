using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinNews.ViewModels;
using Xamarin.Forms.Xaml;

namespace XamarinNews.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostListPage : ContentPage
    {
        PostListViewModel viewModel;

        public PostListPage(List<string> p)
        {
            BindingContext = viewModel = new PostListViewModel();

        }
    }
}