using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinNews.Models;
namespace XamarinNews.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public int index { get; set; }
        public CategoryViewModel() { }

        public CategoryViewModel(Category category)
        {
            index = category.Index;
            _title = category.Title;
            icon = category.Icon;
            path = category.Path;
        }
        public string _title { get; set; }
        public string icon { get; set; }
        public string path { get; set; }

        public ImageSource IconImage
        {
            get
            {
                return ImageSource.FromResource("https://cdn3.iconfinder.com/data/icons/media-1-3/32/Internet-Globe-Communication-512.png");
            }
        }
    }


}
}
