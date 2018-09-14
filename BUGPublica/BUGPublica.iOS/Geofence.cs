using System.Collections.Generic;

using BUGPublica.Interfaces;
using BUGPublica.Helpers;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Google.Maps;

[assembly: Dependency(typeof(BUGPublica.iOS.Geofence))]
namespace BUGPublica.iOS
{
    public class Geofence : IGeofence
    {
        public bool BugContainsLocation(int bug, Position position)
        {
            //SE OBTIENE LOS LIMITES DEL BOSQUE
            List<Position> points = BUGBounds.GetBugBounds(bug);

            //SE CONVIERTE LOS LIMITES AL OBJECTO LIMITE NATIVO DE IOs
            MutablePath bounds = ConvertToNativeBounds(points);

            //CONVIERTE LA POSICION A UN OBJETO LATLNG
            CoreLocation.CLLocationCoordinate2D point = 
                new CoreLocation.CLLocationCoordinate2D(position.Latitude, position.Longitude);

            //SE REVISA SI LA POSICION ESTA DENTRO DE LOS LIMITES
            return GeometryUtils.ContainsLocation(point, bounds, true);
        }

        private MutablePath ConvertToNativeBounds(List<Position> points)
        {
            MutablePath path = new MutablePath();
            foreach (Position pos in points)
                path.AddCoordinate(new CoreLocation.CLLocationCoordinate2D(pos.Latitude, pos.Longitude));
            return path;
        }
    }
}