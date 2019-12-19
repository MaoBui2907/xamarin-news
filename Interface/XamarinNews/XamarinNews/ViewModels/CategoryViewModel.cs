using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

using XamarinNews.Models;
using XamarinNews.Views;

namespace XamarinNews.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public int index { get; set; }

        public ObservableCollection<Category> Categories { get; set; }
        public CategoryViewModel()
        {
            Title = "Danh sách";
            Categories = new ObservableCollection<Category>();
            Category.DeleteCategory();
            Category.InitialDataBase();
        }

        public void getCategory()
        {
            List<Category> categories = Category.GetCategories();
            foreach(Category category in categories)
            {
                Categories.Add(category);
            }
        }
        public void deleteCategory()
        {
            Category.DeleteCategory();
        }
    }
}

