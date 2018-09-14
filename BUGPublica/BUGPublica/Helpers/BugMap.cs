using System;
using System.Collections.Generic;
using System.Text;
using BUGPublica.CustomRenders;
using BUGPublica.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BUGPublica.Helpers
{
    /// <summary>
    /// CONTIENE LAS UBICACIONES DE LOS PARQUES DE LA RED DE BUG
    /// </summary>
    public class BugMap
    {
        /// <summary>
        /// OBTIENE EL MAPA DEL BOSQUE
        /// </summary>
        /// <param name="bug">ID DEL BOSQUE</param>
        /// <returns></returns>
        public static CustomMap GetCustomMap(int bug)
        {
            switch (bug)
            {
                case BUG.ID_BUG_COLOMOS: return GetBugColomos();
                case BUG.ID_BUG_AGUA_AZUL: return GetBugAguaAzul();
                case BUG.ID_BUG_MORELOS: return GetBugMorelos();
                case BUG.ID_BUG_MIRADOR_INDEPENDENCIA: return GetBugMirador();
                case BUG.ID_BUG_PUERTA_BARRANCA: return GetBugBarranca();
                case BUG.ID_BUG_NATURAL_HUENTITAN: return GetBugHuentitan();
                case BUG.ID_BUG_AVILA_CAMACHO: return GetBugAvila();
                case BUG.ID_BUG_ALCALDE: return GetBugAlcalde();
                case BUG.ID_BUG_GONZALEZ_GALLO: return GetBugGallo();
                case BUG.ID_BUG_DEAN: return GetBugDean();
                case BUG.ID_BUG_ARBOLEDAS_SUR: return GetBugArboledas();
                default: throw new Exception("Tipo de Bug inválido");
            }
        }

        private static CustomMap GetBugColomos()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.7057398, -103.3970703),
                LimitNorthEast = new Position(20.7124, -103.38774),
                LimitSouthWest = new Position(20.7006, -103.40542),
                MaxZoom = 21f,
                MinZoom = 15f
            };
        }

        private static CustomMap GetBugAguaAzul()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.6601122, -103.3481152),
                LimitNorthEast = new Position(20.663192, -103.344235),
                LimitSouthWest = new Position(20.657031, -103.353256),
                MaxZoom = 21f,
                MinZoom = 16f
            };
        }

        private static CustomMap GetBugMorelos()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.680315, -103.340563),
                LimitNorthEast = new Position(20.682207, -103.337801),
                LimitSouthWest = new Position(20.678298, -103.342917),
                MaxZoom = 21f,
                MinZoom = 16.5f
            };
        }

        private static CustomMap GetBugMirador()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.740337, -103.310352),
                LimitNorthEast = new Position(20.741746, -103.307222),
                LimitSouthWest = new Position(20.738279, -103.312861),
                MaxZoom = 21f,
                MinZoom = 16.5f
            };
        }

        private static CustomMap GetBugBarranca()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.724147, -103.291325),
                LimitNorthEast = new Position(20.724795, -103.289265),
                LimitSouthWest = new Position(20.723070, -103.292955),
                MaxZoom = 21f,
                MinZoom = 17f
            };
        }

        private static CustomMap GetBugHuentitan()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.723338, -103.314034),
                LimitNorthEast = new Position(20.725994, -103.307463),
                LimitSouthWest = new Position(20.720914, -103.317083),
                MaxZoom = 21f,
                MinZoom = 16.5f
            };
        }

        private static CustomMap GetBugAvila()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.712321, -103.372592),
                LimitNorthEast = new Position(20.714471, -103.369686),
                LimitSouthWest = new Position(20.710288, -103.375458),
                MaxZoom = 21f,
                MinZoom = 16.5f
            };
        }

        private static CustomMap GetBugAlcalde()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.690794, -103.350649),
                LimitNorthEast = new Position(20.693574, -103.348203),
                LimitSouthWest = new Position(20.687683, -103.352762),
                MaxZoom = 21f,
                MinZoom = 16.5f
            };
        }

        private static CustomMap GetBugGallo()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.647788, -103.337027),
                LimitNorthEast = new Position(20.650942, -103.335122),
                LimitSouthWest = new Position(20.644507, -103.339580),
                MaxZoom = 21f,
                MinZoom = 16.5f
            };
        }

        private static CustomMap GetBugDean()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.636782, -103.345941),
                LimitNorthEast = new Position(20.639719, -103.342417),
                LimitSouthWest = new Position(20.633528, -103.349620),
                MaxZoom = 21f,
                MinZoom = 16.5f
            };
        }

        private static CustomMap GetBugArboledas()
        {
            return new CustomMap()
            {
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Position = new Position(20.618228, -103.387783),
                LimitNorthEast = new Position(20.619277, -103.387271),
                LimitSouthWest = new Position(20.617426, -103.388555),
                MaxZoom = 21f,
                MinZoom = 17.5f
            };
        }
    }
}
