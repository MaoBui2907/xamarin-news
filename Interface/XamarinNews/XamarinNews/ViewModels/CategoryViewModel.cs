using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using SQLite;

using XamarinNews.Models;
using XamarinNews.Views;

namespace XamarinNews.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {

        public CategoryViewModel()
        {
            Title = "Danh sách";
            if (!HasCategory())
                CreateCategory();
            DeleteCategory();
            InitialDataBase();
        }

        public bool InitialDataBase()
        {
            bool _s = false;
            //List<Category> Initialdata = new List<Category> { new Category { Title = "Trend", Icon = "D", Path = "/Trend" }, new Category { Title = "Văn Hóa", Icon = "DD", Path = "/van-hoa" } };
            string[][] initialdata = new string[][] {new string[] {"1", "Tin nóng", "EX1", "/tin-hot"}, new string[] { "10", "Văn Hóa", "EX1", "/van-hoa"},
            new string[] {"2", "Sức khỏe", "EX1", "/suc-khoe"}, new string[] {"3", "Giáo dục", "EX1", "/giao-duc"}, new string[] {"4", "Thể thao", "EX1", "/the-thao"},
            new string[] {"5", "Showbiz", "EX1", "/showbiz"}, new string[] {"6", "Đời sống", "EX1", "/doi-song"}};

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                foreach (string[] item in initialdata)
                {
                    Category c = new Category { Index = item[0], Title = item[1], Icon = item[2], Path = item[3] };
                    conn.Insert(c);
                }
                _s = true;
            }
            return _s;
        }
        public List<Category> GetCategories()
        {
            // ObservableCollection<Category> Categories = new ObservableCollection<Category>();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                return conn.Table<Category>().ToList();
            }
        }

        public bool DeleteCategory()
        {
            bool _s = false;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.Execute("Delete from Category");
                _s = true;
            }
            return _s;
        }

        public bool CreateCategory()
        {
            bool _s = false;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Category>();
                _s = true;
            }
            return _s;
        }

        public bool HasCategory()
        {
            bool _s = false;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                //var _t = conn.Execute("SELECT * FROM sqlite_master WHERE type='table' AND tbl_name='Category'");
                var _tables = conn.GetTableInfo("Category");
                _s = _tables.Count > 0;
            }
            return _s;
        }

    }
}

