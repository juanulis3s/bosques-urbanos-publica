using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using System.Threading.Tasks;

namespace BUGPublica.Helpers
{
    /// <summary>
    /// CONTIENE MÉTODOS AUXILIARES PARA LAS LOCALIZACIONES
    /// </summary>
    public class LocationHelper
    {
        /// <summary>
        /// VALIDA SI EL USUARIO ESTA DENTRO DEL BOSQUE
        /// </summary>
        public static async Task<Position> ValidateLocation(Xamarin.Forms.Page page, int bug)
        {
            //VERIFICA SI EL USUARIO ESTA REGISTRADO
            if (!IsUserRegistered())
            {
                //SE MUESTRA UN MENSAJE QUE DEBE REGISTRARSE
                DisplayAlert(Resources.AppResources.Warning, Resources.AppResources.MustRegister, page);
                //SE ABRE LA PAGINA DE REGISTRO
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new LoginPage());
                
                return new Position();
            }

            //VERIFICA SI EL SENSOR ESTA PRENDIDO
            if (await CrossGeolocator.Current.GetIsGeolocationEnabledAsync())
            {
                Plugin.Geolocator.Abstractions.Position position = null;
                try
                {
                    // SE ESTABLECE LA EXACTITUD DE LA MEDICION
                    CrossGeolocator.Current.DesiredAccuracy = 10; // 10 METROS

                    // SE OBTIENE LA LOCALIZACIÓN
                    position = await CrossGeolocator.Current.GetPositionAsync(
                        TimeSpan.FromSeconds(20), null, true);
                    //System.Diagnostics.Debug.WriteLine(position.Latitude + "," + position.Longitude);
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    //TODO Cambiar mensaje de error
                    DisplayAlert(Resources.AppResources.Error, Resources.AppResources.Error, page);
                    return new Position();
                }

                //SE REVISA QUE EXISTA LA LOCALIZACIÓN
                if (position == null)
                {
                    //TODO Cambiar mensaje de error
                    DisplayAlert(Resources.AppResources.Error, Resources.AppResources.Error, page);
                    return new Position();
                }

                //SE TRANSFORMA LA POSICION
                Position pos = new Position(position.Latitude, position.Longitude);

                //SE REVISA SI LA POSICION ESTA DENTRO DE LOS LIMITES DEL BOSQUE
                Interfaces.IGeofence geofence = Xamarin.Forms.DependencyService.Get<Interfaces.IGeofence>();
                if (geofence.BugContainsLocation(bug, pos))
                {
                    return pos;
                }
                else
                {
                    DisplayAlert(Resources.AppResources.Warning, Resources.AppResources.NotInsideBug, page);
                }
            }
            else
            {
                DisplayAlert(Resources.AppResources.Warning, Resources.AppResources.GPSMessageDisable, page);
            }
            return new Position();
        }

        /// <summary>
        /// MUESTRA UNA ALERTA CON LOS DATOS DADOS
        /// </summary>
        /// <param name="title">TÍTULO DE LA ALERTA</param>
        /// <param name="msg">MENSAJE DE LA ALERTA</param>
        private static async void DisplayAlert(String title, String msg, Xamarin.Forms.Page page)
        {
            await page.DisplayAlert(title, msg, Resources.AppResources.Accept);
        }

        /// <summary>
        /// VERIFICA SI EL USUARIO HA DADO DE ALTA SU NUMERO
        /// </summary>
        /// <returns></returns>
        private static bool IsUserRegistered()
        {
            return (Xamarin.Forms.Application.Current.Properties.ContainsKey(AppConfig.PHONE_KEY)
                && Xamarin.Forms.Application.Current.Properties[AppConfig.PHONE_KEY] != null);        
        }
    }
}
