using System.Collections.Generic;

namespace BUGPublica.Models
{
    public class RouteItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Duration { get; set; }
        public List<RouteMarker> Markers { get; set; }
    }
}
