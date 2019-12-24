using System;
using System.Collections.Generic;
using System.Text;

using XamarinNews.Models;
using XamarinNews.Views;

namespace XamarinNews.ViewModels
{
    class PostViewModel : BaseViewModel
    {
        public string t { get; set; }
        public string Image { get; set; }
        public string Desc { get; set; }
        public string Content { get; set; }
        public PostViewModel(Post _p)
        {
            Title = "Nội dung chi tiết";
            t = _p.Title;
            Image = _p.Image;
            Desc = _p.Description;
            Content = _p.Content;
        }
    }
}
