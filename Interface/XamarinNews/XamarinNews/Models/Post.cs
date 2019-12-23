using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNews.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
        public bool Trend { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
    }
}
