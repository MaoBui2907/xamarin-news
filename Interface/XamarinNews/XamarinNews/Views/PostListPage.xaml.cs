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
            viewModel = new PostListViewModel(new List<string> { "0", "Tin nóng", "ic1", "trend" }, 1);
            BindingContext = viewModel;
            PostListView.ItemsSource = viewModel.Posts;
        }
        public PostListPage(PostListViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            BindingContext = viewModel;
            PostListView.ItemsSource = viewModel.Posts;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var post = args.SelectedItem as PostMeta;
            if (post == null)
                return;
            await Navigation.PushAsync(new PostsPage(post));
            PostListView.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Posts.Count == 0)
            {
                await viewModel.FetchPostList();
                await viewModel.CheckMorePost();
            }
            bindData();
        }
        public async void prevPage(object sender, EventArgs e)
        {
            viewModel._page--;
            await viewModel.FetchPostList();
            await viewModel.CheckMorePost();
            bindData();
        }
        public async void nextPage(object sender, EventArgs e)
        {
            viewModel._page++;
            await viewModel.FetchPostList();
            await viewModel.CheckMorePost();
            bindData();
        }

        public async void firstPage(object sender, EventArgs e)
        {
            viewModel._page = 1;
            await viewModel.FetchPostList();
            await viewModel.CheckMorePost();
            bindData();
        }

        public void bindData()
        {
            if (viewModel._page == 1)
                viewModel.hasPrev = false;
            else
                viewModel.hasPrev = true;
            if (viewModel.Posts.Count == 0)
            {
                PostListView.IsVisible = false;
                EmptyData.IsVisible = true;
                Pagination.IsVisible = false;
            }
            else
            {
                PostListView.IsVisible = true;
                EmptyData.IsVisible = false;
                Pagination.IsVisible = true;
            }
            PostListView.ItemsSource = viewModel.Posts;
            if (viewModel.hasPrev)
            {
                prevButton.IsEnabled = true;
                firstButton.IsEnabled = true;
            }
            else
            {
                prevButton.IsEnabled = false;
                firstButton.IsEnabled = false;
            }
            if (viewModel.hasMore)
                nextButton.IsEnabled = true;
            else
                nextButton.IsEnabled = false;
            PageNumber.Text = viewModel._page.ToString();
        }

        private async void RefreshList_Clicked(object sender, EventArgs e)
        {
            await viewModel.FetchPostList();
            await viewModel.CheckMorePost();
            bindData();
        }
    }
}