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
        public PostsPage(PostMeta _p)
        {
            InitializeComponent();
            BindingContext = viewModel = new PostViewModel(_p);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.GetPost();
            await bindData();
        }
        async Task bindData()
        {
            PostTitle.Text = viewModel.p.Title;
            PostImage.Source = viewModel.p.Image;
            PostContent.Text = viewModel.p.Content;
            await Task.Delay(100);
        }

    }
}