using System;
using System.Collections.Generic;
using System.Text;

namespace BUGPublica.Models
{
    public class EventItem
    {
        public int Id { get; set; }
        public long Key { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Publication_Date { get; set; }
        public string Schedule { get; set; }
        public string Content { get; set; }
    }
}
