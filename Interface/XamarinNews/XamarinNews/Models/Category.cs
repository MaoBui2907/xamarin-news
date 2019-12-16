using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace XamarinNews.Models
{
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
    }
}
