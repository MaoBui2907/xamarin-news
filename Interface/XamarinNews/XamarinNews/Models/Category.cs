using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using System.Diagnostics;
namespace XamarinNews.Models
{



    public class Category
    {


        [PrimaryKey]
        public string Index { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(20000)]
        public string Icon { get; set; }
        [MaxLength(255)]
        public string Path { get; set; }


        public Category() { }

        /*public Category(string[] c)
        {
            this.Index = int.Parse(c[0]);
            this.Title = c[1];
            this.Icon = c[2];
            this.Path = c[3];
        }*/  
        public static bool InitialDataBase()
        {

            //List<Category> Initialdata = new List<Category> { new Category { Title = "Trend", Icon = "D", Path = "/Trend" }, new Category { Title = "Văn Hóa", Icon = "DD", Path = "/van-hoa" } };
            string[][] initialdata = new string[][] {new string[] {"1", "Trend", "EX1", "/tin-hot"}, new string[] { "10", "Văn Hóa", "EX1", "/van-hoa"},
            new string[] {"2", "Sức khỏe", "EX1", "/suc-khoe"}, new string[] {"3", "Giáo dục", "EX1", "/giao-duc"}, new string[] {"4", "Thể thao", "EX1", "/the-thao"},
            new string[] {"5", "Showbiz", "EX1", "/showbiz"}, new string[] {"6", "Đời sống", "EX1", "/doi-song"}};

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Category>();
                foreach (string[] item in initialdata)
                {
                    Debug.WriteLine(item.ToString());
                    Category c = new Category { Index = item[0], Title = item[1], Icon = item[2], Path = item[3] };
                    conn.Insert(c);
                }

            }
            return true;
        }

        public static List<Category> GetCategories()
        {
           // ObservableCollection<Category> Categories = new ObservableCollection<Category>();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                return conn.Table<Category>().ToList();
            }
        }
        public static void DeleteCategory()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.Execute("Delete from Category");
            }
        }
    }
}
