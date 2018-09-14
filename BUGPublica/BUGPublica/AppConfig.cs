  using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using System.Web;

namespace BUGPublica
{
    public static class AppConfig
    {
        //LLAVES PARA DICCIONARIO
        public const string QR_TUTORIAL_KEY = "QrTutorial";

        //CREDENDCIALES PARA LAS CONSULTAS AL SERVIDOR
        static readonly string AUTH_KEY = "bug";
        static readonly string AUTH_PASSWORD = "sw-917300";

        //LLAVE DE NUMERO DEL USUARIO
        public static readonly string PHONE_KEY = "userphone";

        //LLAVE MOSTRAR VENTANA REGISTRO
        public static readonly string REGISTER_KEY = "showRegister";

        /// <summary>
        /// OBTIENE LAS CREDENCIALES AL SERVIDOR
        /// </summary>
        /// <returns></returns>
        public static AuthenticationHeaderValue GetAuth()
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(string.Format("{0}:{1}", AUTH_KEY, AUTH_PASSWORD))));
        }


        /// <summary>
        /// CLASE QUE CONTIENE LAS URLS AL SERVIDOR
        /// </summary>
        public static class Url
        {
            static readonly string SCHEME = "http";
            static readonly string HOST = "bosquesgdlapp.com";
            static readonly string API_PATH = "/api/v1/";
            static readonly int PORT = 80;

            //Urls
            public static readonly string ACTION_REGISTER = "register.json";
            public static readonly string ACTION_PINS = "get_locations.json";
            public static readonly string ACTION_PIN_INFO = "get_location_content.json";
            public static readonly string ACTION_NEWS = "get_news.json";
            public static readonly string ACTION_EVENTS = "get_events.json";
            public static readonly string ACTION_INFO = "get_info.json";
            public static readonly string ACTION_TERMS = "get_terms.json";
            public static readonly string ACTION_RULES = "get_regulations.json";
            public static readonly string ACTION_MAILBOX = "suggestions_mailbox.json";
            public static readonly string ACTION_HELP = "incident.json";
            public static readonly string ACTION_ROUTES = "get_routes.json";
            public static readonly string ACTION_END_ROUTE = "end_route.json";
            public static readonly string ACTION_BANNER = "get_banners.json";

            //TIPOS DE BANNER
            public static readonly string TYPE_MAIN = "main";

            private static readonly string QUERY_LANG = "lang";
            private static readonly string QUERY_NEW = "last_new";
            private static readonly string QUERY_EVENT = "last_event";
            private static readonly string QUERY_NAME = "name";
            private static readonly string QUERY_MAIL = "mail";
            private static readonly string QUERY_MESSAGE = "message";
            private static readonly string QUERY_LAT = "lat";
            private static readonly string QUERY_LON = "lon";
            private static readonly string QUERY_PHONE = "phone";
            private static readonly string QUERY_BUG = "bug_id";
            private static readonly string QUERY_ID = "id";
            private static readonly string QUERY_ROUTE = "route";
            private static readonly string QUERY_TYPE = "type";

            /// <summary>
            /// OBTIENE EL URI DE LA API DE REGISTRO
            /// </summary>
            /// <param name="name"></param>
            /// <param name="email"></param>
            /// <param name="annotation"></param>
            /// <returns></returns>
            public static Uri GetRegisterUri(string phone)
            {
                string path = API_PATH + ACTION_REGISTER;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGAN LOS PARAMETROS
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_PHONE] = phone;

                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE EL URI DE LA API DE ENVIO DE SUGERENCIA
            /// </summary>
            /// <param name="name"></param>
            /// <param name="email"></param>
            /// <param name="annotation"></param>
            /// <returns></returns>
            public static Uri GetSuggestionMailboxUri(int bug, string name, string email, string annotation)
            {
                string path = API_PATH + ACTION_MAILBOX;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGAN LOS PARAMETROS
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_NAME] = name;
                query[QUERY_MAIL] = email;
                query[QUERY_MESSAGE] = annotation;
                query[QUERY_BUG] = Convert.ToString(bug);
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DE AUXILIO
            /// </summary>
            /// <param name="bug"></param>
            /// <param name="lat"></param>
            /// <param name="lon"></param>
            /// <param name="phone"></param>
            /// <returns></returns>
            public static Uri GetHelpUri(int bug, double lat, double lon, string phone)
            {
                string path = API_PATH + ACTION_HELP;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGAN LOS PARAMETROS
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_BUG] = Convert.ToString(bug);
                query[QUERY_LAT] = Convert.ToString(lat);
                query[QUERY_LON] = Convert.ToString(lon);
                query[QUERY_PHONE] = phone;

                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DE INFORMACION DE UN BOSQUE
            /// </summary>
            /// <param name="bug"></param>
            /// <param name="lang"></param>
            /// <returns></returns>
            public static Uri GetInfoBugUri(int bug, string lang = "es")
            {
                string path = API_PATH + ACTION_INFO;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_LANG] = lang;
                query[QUERY_BUG] = Convert.ToString(bug);

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DEL REGLAMENTO DE UN BOSQUE
            /// </summary>
            /// <param name="bug"></param>
            /// <param name="lang"></param>
            /// <returns></returns>
            public static Uri GetRegulationUri(int bug, string lang = "es")
            {
                string path = API_PATH + ACTION_RULES;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_LANG] = lang;
                query[QUERY_BUG] = Convert.ToString(bug);

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DE LAS NOTICIAS
            /// </summary>
            /// <param name="lang"></param>
            /// <param name="lastItem"></param>
            /// <returns></returns>
            public static Uri GetNewsUri(string lang = "es", int lastItem = 0)
            {
                string path = API_PATH + ACTION_NEWS;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_LANG] = lang;

                //AGREGA EL ULTIMO ITEM OBTENIDO
                query[QUERY_NEW] = Convert.ToString(lastItem);

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DE LOS EVENTOS
            /// </summary>
            /// <param name="lang"></param>
            /// <param name="lastItem"></param>
            /// <returns></returns>
            public static Uri GetEventsUri(string lang = "es", int lastItem = 0)
            {
                string path = API_PATH + ACTION_EVENTS;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_LANG] = lang;

                //AGREGA EL ULTIMO ITEM OBTENIDO
                query[QUERY_EVENT] = Convert.ToString(lastItem);

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DE LAS LOCALIZACIONES DEL BOSQUE
            /// </summary>
            /// <param name="bugId"></param>
            /// <param name="lang"></param>
            /// <returns></returns>
            public static Uri GetPinsUri(int bugId, string lang = "es")
            {
                string path = API_PATH + ACTION_PINS;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_LANG] = lang;

                //AGREGA EL ID DEL BOSQUE
                query[QUERY_BUG] = Convert.ToString(bugId);

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DE TERMINOS Y CONDICIONES DE LA APP
            /// </summary>
            /// <param name="lang"></param>
            /// <returns></returns>
            public static Uri GetTermsUri(string lang = "es")
            {
                string path = API_PATH + ACTION_TERMS;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_LANG] = lang;

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DEL CONTENIDO DE UNA LOCALIZACION DEL BOSQUE
            /// </summary>
            /// <param name="bugId"></param>
            /// <param name="lang"></param>
            /// <returns></returns>
            public static Uri GetPinContentUri(int id, int bugId, string lang = "es")
            {
                string path = API_PATH + ACTION_PIN_INFO;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                var query = HttpUtility.ParseQueryString(builder.Query);

                //AGREGA EL ID DEL BOSQUE Y DEL LUGAR
                query[QUERY_ID] = Convert.ToString(id);
                query[QUERY_BUG] = Convert.ToString(bugId);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                query[QUERY_LANG] = lang;

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI DE LAS RUTAS DEL BOSQUE
            /// </summary>
            /// <param name="bugId"></param>
            /// <param name="lang"></param>
            /// <returns></returns>
            public static Uri GetRoutesUri(int bugId, string lang = "es")
            {
                string path = API_PATH + ACTION_ROUTES;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //SE AGREGA EL TIPO DE LENGUAJE
                if (lang != "en" && lang != "fr")
                    lang = "es";
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_LANG] = lang;

                //AGREGA EL ID DEL BOSQUE
                query[QUERY_BUG] = Convert.ToString(bugId);

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI PARA UNA RUTA COMPLETADA
            /// </summary>
            /// <param name="routeId"></param>
            /// <param name="phone"></param>
            /// <returns></returns>
            public static Uri GetEndRouteUri(int routeId, string phone)
            {
                string path = API_PATH + ACTION_END_ROUTE;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //AGREGA EL ID DE LA RUTA Y EL TELEFONO
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_ROUTE] = Convert.ToString(routeId);
                query[QUERY_PHONE] = phone;

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }

            /// <summary>
            /// OBTIENE LA URI PARA ANUNCIOS
            /// </summary>
            /// <param name="routeId"></param>
            /// <param name="phone"></param>
            /// <returns></returns>
            public static Uri GetBannerUri(string type)
            {
                string path = API_PATH + ACTION_BANNER;
                UriBuilder builder = new UriBuilder(SCHEME, HOST, PORT, path);

                //AGREGA TIPO DE ANUNCIO
                var query = HttpUtility.ParseQueryString(builder.Query);
                query[QUERY_TYPE] = type;

                //AGREGA LAS QUERYS
                builder.Query = query.ToString();
                return builder.Uri;
            }
        }
    }
}
