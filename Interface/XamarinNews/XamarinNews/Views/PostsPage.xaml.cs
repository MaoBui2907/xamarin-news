﻿using System;
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
            bindData();
        }
        void bindData()
        {
            PostTitle.Text = viewModel.p.Title;
            if (App.LoadImage)
            {
                PostImage.Source = viewModel.p.Image;
            }
            if (viewModel.summar)
            {
                viewModel.Title = "Nội dung tóm tắt";
                PostContent.Text = viewModel.p.Summar;
            }
            else
            {
                PostContent.Text = viewModel.p.Content;
                viewModel.Title = "Nội dung đầy đủ";
            }

            PostAuthor.Text = viewModel.p.Author;
        }
        void toggleContent_Clicked(object sender, EventArgs e)
        {
            viewModel.summar = !viewModel.summar;
            bindData();
        }

        private void shareButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}