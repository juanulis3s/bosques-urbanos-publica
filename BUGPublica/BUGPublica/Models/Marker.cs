using System;
using System.Collections.Generic;
using System.Text;

namespace BUGPublica.Models
{
    public class Marker
    {
        public long Id { get; set; }
        public int Detail_Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
