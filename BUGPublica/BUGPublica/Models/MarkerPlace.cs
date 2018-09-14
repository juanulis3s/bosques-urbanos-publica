using System;
using System.Collections.Generic;
using System.Text;

namespace BUGPublica.Models
{
    public class MarkerPlace
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Id { get; set; }
        public List<Marker> Markers { get; set; }
    }
}
