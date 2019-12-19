using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace XamarinNews.Models
{
    
    
    data = [
        ["0", "giao duc", "/giao-duc", ]
        ]

    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Index { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(20000)]
        public string Icon { get; set; }
        [MaxLength(255)]
        public string Path { get; set; }


        public static List<Category> GetCategories()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.CreateTable<Category>();



                return conn.Table<Category>().ToList();
            }
        }
    }
}
