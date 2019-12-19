using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Diagnostics;
using SQLite;

namespace XamarinNews.Models
{
    public class Category
    {
        [PrimaryKey]
        [MaxLength(10)]
        public string Index { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(20000)]
        public string Icon { get; set; }
        [MaxLength(255)]
        public string Path { get; set; }

        public List<string> toList()
        {
            return new List<string> { this.Index, this.Title, this.Icon, this.Path };
        }
    }
}
