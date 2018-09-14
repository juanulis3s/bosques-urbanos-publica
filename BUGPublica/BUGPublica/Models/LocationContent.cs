using System;
using System.Collections.Generic;
using System.Text;

namespace BUGPublica.Models
{
    public class LocationContent
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string[] Gallery { get; set; }
    }
}
