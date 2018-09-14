using System.Collections.Generic;

using BUGPublica.Interfaces;
using BUGPublica.Helpers;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Android.Gms.Maps.Model;

[assembly: Dependency(typeof(BUGPublica.Droid.Geofence))]
namespace BUGPublica.Droid
{
    public class Geofence : IGeofence
    {
        public bool BugContainsLocation(int bug, Position position)
        {
            //SE OBTIENE LOS LIMITES DEL BOSQUE
            List<Position> points = BUGBounds.GetBugBounds(bug);

            //SE CONVIERTE LOS LIMITES AL OBJECTO LIMITE NATIVO DE ANDROID
            LatLngBounds bounds = ConvertToNativeBounds(points);

            //CONVERITE LA POSICION A UN OBJETO LATLNG
            LatLng point = new LatLng(position.Latitude, position.Longitude);

            //SE REVISA SI LA POSICION ESTA DENTRO DE LOS LIMITES
            return bounds.Contains(point);
        }

        /// <summary>
        /// CONVIERTE EL LISTADO DE LIMITES AL OBJETO NATIVO DE LIMITES DE ANDROID
        /// </summary>
        /// <param name="points">LISTA DE LIMITES</param>
        /// <returns></returns>
        private LatLngBounds ConvertToNativeBounds(List<Position> points)
        {
            LatLngBounds.Builder builder = new LatLngBounds.Builder();
            foreach (Position pos in points)
                builder.Include(new LatLng(pos.Latitude, pos.Longitude));
            return builder.Build();
        }
    }
}