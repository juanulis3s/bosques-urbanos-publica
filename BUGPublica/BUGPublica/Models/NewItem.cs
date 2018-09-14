using System;
using System.Collections.Generic;
using System.Text;

namespace BUGPublica.Models
{
    public class NewItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Publication_Date { get; set; }
        public string Category { get; set; }
        public string Intro { get; set; }
        public string Content { get; set; }
    }
}
