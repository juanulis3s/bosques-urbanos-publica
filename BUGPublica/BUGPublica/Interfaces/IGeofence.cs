
namespace BUGPublica.Interfaces
{
    public interface IGeofence
    {
        /// <summary>
        /// RETORNA TRUE SI LA LOCALIZACIÓN ESTA DENTRO DE LOS LIMITES DEL BOSQUE DADO
        /// </summary>
        /// <param name="bug">ID DEL BOSQUE</param>
        /// <param name="position">LOCALIZACIÓN A COMPARAR</param>
        /// <returns></returns>
        bool BugContainsLocation(int bug, Xamarin.Forms.Maps.Position position);
    }
}
