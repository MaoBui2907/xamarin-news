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
    public partial class PostsPage : ContentPage
    {
        PostViewModel viewModel;
        public PostsPage(Post _p)
        {
            InitializeComponent();
            BindingContext = viewModel = new PostViewModel(_p);
        }
    }
}