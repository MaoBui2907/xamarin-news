using System;
using System.Collections.Generic;
using System.Text;

using XamarinNews.Models;
using XamarinNews.Views;
namespace XamarinNews.ViewModels
{
    class PostListViewModel : BaseViewModel
    {
        public PostListViewModel(string t)
        {
            Title = t;
        }


    }
}
