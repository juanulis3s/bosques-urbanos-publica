
using System.Threading.Tasks;

namespace BUGPublica.Interfaces
{
    public interface IUserLocation
    {
        /// <summary>
        /// OBTIENE LA GEOLOCALIZACIÓN DEL USUARIO 
        /// </summary>
        /// <returns></returns>
        Task<Xamarin.Forms.Maps.Position> GetUserPosition();
    }
}
