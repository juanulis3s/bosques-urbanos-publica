using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;
using BUGPublica.Models;

namespace BUGPublica.Helpers
{
    public class BUGBounds
    {
        /// <summary>
        /// OBTIENE UN LISTADO DE LAS COORDENADAS LIMITANTES DEL BOSQUE
        /// </summary>
        /// <param name="bug">ID DEL BOSQUE</param>
        /// <returns></returns>
        public static List<Position> GetBugBounds(int bug)
        {
            switch (bug)
            {
                case BUG.ID_BUG_COLOMOS: return GetColomosBounds();
                case BUG.ID_BUG_AGUA_AZUL: return GetAguaAzulBounds();
                case BUG.ID_BUG_MORELOS: return GetMorelosBounds();
                case BUG.ID_BUG_MIRADOR_INDEPENDENCIA: return GetMiradorBounds();
                case BUG.ID_BUG_PUERTA_BARRANCA: return GetPuertaBarrancaBounds();
                case BUG.ID_BUG_NATURAL_HUENTITAN: return GetNaturalHuentitanBounds();
                case BUG.ID_BUG_AVILA_CAMACHO: return GetAvilaCamachoBounds();
                case BUG.ID_BUG_ALCALDE: return GetAlcaldeBounds();
                case BUG.ID_BUG_GONZALEZ_GALLO: return GetGonzalezGalloBounds();
                case BUG.ID_BUG_DEAN: return GetDeanBounds();
                case BUG.ID_BUG_ARBOLEDAS_SUR: return GetArboledasSurBounds();
                case BUG.ID_BUG_SOLIDARIDAD: return GetSolidaridadBounds();
                case BUG.ID_BUG_METROPOLITANO: return GetMetropolitanoBounds();
                case BUG.ID_BUG_MONTENEGRO: return GetMontenegroBounds();
                default: throw new Exception("Tipo de Bug inválido");
            }
        }

        private static List<Position> GetColomosBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.712807, -103.395681));
            bounds.Add(new Position(20.711603, -103.400831));
            bounds.Add(new Position(20.712045, -103.403921));
            bounds.Add(new Position(20.712647, -103.405294));
            bounds.Add(new Position(20.712085, -103.406625));
            bounds.Add(new Position(20.710319, -103.407269));
            bounds.Add(new Position(20.708392, -103.405638));
            bounds.Add(new Position(20.706947, -103.401475));
            bounds.Add(new Position(20.707669, -103.399630));
            bounds.Add(new Position(20.707027, -103.399115));
            bounds.Add(new Position(20.705100, -103.400960));
            bounds.Add(new Position(20.703494, -103.399458));
            bounds.Add(new Position(20.705100, -103.397698));
            bounds.Add(new Position(20.705301, -103.394995));
            bounds.Add(new Position(20.702129, -103.396711));
            bounds.Add(new Position(20.699159, -103.394995));
            bounds.Add(new Position(20.698597, -103.392763));
            bounds.Add(new Position(20.699560, -103.390360));
            bounds.Add(new Position(20.703093, -103.389416));
            bounds.Add(new Position(20.703093, -103.387785));
            bounds.Add(new Position(20.706224, -103.386927));
            bounds.Add(new Position(20.707990, -103.388300));
            bounds.Add(new Position(20.710479, -103.388300));
            bounds.Add(new Position(20.710158, -103.391047));
            bounds.Add(new Position(20.711563, -103.393621));
            bounds.Add(new Position(20.712807, -103.395681));
            return bounds;
        }

        private static List<Position> GetAguaAzulBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.662105, -103.345683));
            bounds.Add(new Position(20.662320, -103.347105));
            bounds.Add(new Position(20.662595, -103.348745));
            bounds.Add(new Position(20.658302, -103.350903));
            bounds.Add(new Position(20.657917, -103.349813));
            bounds.Add(new Position(20.657960, -103.348944));
            bounds.Add(new Position(20.658193, -103.348259));
            bounds.Add(new Position(20.657794, -103.345501));
            bounds.Add(new Position(20.662105, -103.345683));
            return bounds;
        }

        private static List<Position> GetAlcaldeBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.693162, -103.349802));
            bounds.Add(new Position(20.693168, -103.350222));
            bounds.Add(new Position(20.692951, -103.350523));
            bounds.Add(new Position(20.693006, -103.350893));
            bounds.Add(new Position(20.693257, -103.351065));
            bounds.Add(new Position(20.693242, -103.351795));
            bounds.Add(new Position(20.688791, -103.351924));
            bounds.Add(new Position(20.688987, -103.349666));
            bounds.Add(new Position(20.693162, -103.349802));
            return bounds;
        }

        private static List<Position> GetDeanBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.636646, -103.342750));
            bounds.Add(new Position(20.638468, -103.345185));
            bounds.Add(new Position(20.640593, -103.349092));
            bounds.Add(new Position(20.640201, -103.349859));
            bounds.Add(new Position(20.639985, -103.349639));
            bounds.Add(new Position(20.636665, -103.349080));
            bounds.Add(new Position(20.636750, -103.348112));
            bounds.Add(new Position(20.635742, -103.348186));
            bounds.Add(new Position(20.635250, -103.348081));
            bounds.Add(new Position(20.635293, -103.346705));
            bounds.Add(new Position(20.631719, -103.346134));
            bounds.Add(new Position(20.631478, -103.345942));
            bounds.Add(new Position(20.634379, -103.344088));
            bounds.Add(new Position(20.634592, -103.344126));
            bounds.Add(new Position(20.636646, -103.342750));
            return bounds;
        }

        private static List<Position> GetGonzalezGalloBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.650610, -103.335991));
            bounds.Add(new Position(20.650218, -103.337342));
            bounds.Add(new Position(20.648452, -103.340544));
            bounds.Add(new Position(20.646041, -103.338937));
            bounds.Add(new Position(20.645546, -103.335885));
            bounds.Add(new Position(20.646271, -103.334367));
            bounds.Add(new Position(20.648138, -103.334701));
            bounds.Add(new Position(20.650610, -103.335991));
            return bounds;
        }

        private static List<Position> GetMorelosBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.681204, -103.338655));
            bounds.Add(new Position(20.681254, -103.340312));
            bounds.Add(new Position(20.681615, -103.342007));
            bounds.Add(new Position(20.679126, -103.341949));
            bounds.Add(new Position(20.679252, -103.339741));
            bounds.Add(new Position(20.681204, -103.338655));
            return bounds;
        }

        private static List<Position> GetMiradorBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.740926, -103.308709));
            bounds.Add(new Position(20.741732, -103.309846));
            bounds.Add(new Position(20.741836, -103.311121));
            bounds.Add(new Position(20.739606, -103.311860));
            bounds.Add(new Position(20.739460, -103.311422));
            bounds.Add(new Position(20.739030, -103.311573));
            bounds.Add(new Position(20.738573, -103.309695));
            bounds.Add(new Position(20.740926, -103.308709));
            return bounds;
        }

        private static List<Position> GetAvilaCamachoBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.713923, -103.371067));
            bounds.Add(new Position(20.713945, -103.371291));
            bounds.Add(new Position(20.713232, -103.374253));
            bounds.Add(new Position(20.712228, -103.375095));
            bounds.Add(new Position(20.710552, -103.372407));
            bounds.Add(new Position(20.711115, -103.372036));
            bounds.Add(new Position(20.711463, -103.370428));
            bounds.Add(new Position(20.713661, -103.370605));
            bounds.Add(new Position(20.713923, -103.371067));
            return bounds;
        }

        private static List<Position> GetArboledasSurBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.619231, -103.388234));
            bounds.Add(new Position(20.618690, -103.388548));
            bounds.Add(new Position(20.614645, -103.388602));
            bounds.Add(new Position(20.613819, -103.388126));
            bounds.Add(new Position(20.615413, -103.384316));
            bounds.Add(new Position(20.617010, -103.385248));
            bounds.Add(new Position(20.619231, -103.388234));
            return bounds;
        }

        private static List<Position> GetNaturalHuentitanBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.726079, -103.313565));
            bounds.Add(new Position(20.726202, -103.314209));
            bounds.Add(new Position(20.725205, -103.314568));
            bounds.Add(new Position(20.725437, -103.315630));
            bounds.Add(new Position(20.723279, -103.316293));
            bounds.Add(new Position(20.721592, -103.314445));
            bounds.Add(new Position(20.721150, -103.313290));
            bounds.Add(new Position(20.721358, -103.312083));
            bounds.Add(new Position(20.721872, -103.311321));
            bounds.Add(new Position(20.722291, -103.310818));
            bounds.Add(new Position(20.723692, -103.311036));
            bounds.Add(new Position(20.723498, -103.311587));
            bounds.Add(new Position(20.723681, -103.311701));
            bounds.Add(new Position(20.723627, -103.312022));
            bounds.Add(new Position(20.723890, -103.312147));
            bounds.Add(new Position(20.723783, -103.312405));
            bounds.Add(new Position(20.726079, -103.313565));
            return bounds;
        }

        private static List<Position> GetPuertaBarrancaBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.724666, -103.292113));
            bounds.Add(new Position(20.724552, -103.291140));
            bounds.Add(new Position(20.723732, -103.289953));
            bounds.Add(new Position(20.723697, -103.292328));
            bounds.Add(new Position(20.723954, -103.292584));
            bounds.Add(new Position(20.724666, -103.292113));
            return bounds;
        }

        private static List<Position> GetSolidaridadBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.679480, -103.267618));
            bounds.Add(new Position(20.678147, -103.265837));
            bounds.Add(new Position(20.675236, -103.264450));
            bounds.Add(new Position(20.671764, -103.263756));
            bounds.Add(new Position(20.669915, -103.263938));
            bounds.Add(new Position(20.668216, -103.263593));
            bounds.Add(new Position(20.667045, -103.265268));
            bounds.Add(new Position(20.664335, -103.267397));
            bounds.Add(new Position(20.659897, -103.268039));
            bounds.Add(new Position(20.658732, -103.267661));
            bounds.Add(new Position(20.655059, -103.267419));
            bounds.Add(new Position(20.653049, -103.269047));
            bounds.Add(new Position(20.649165, -103.269932));
            bounds.Add(new Position(20.648106, -103.271412));
            bounds.Add(new Position(20.646600, -103.272437));
            bounds.Add(new Position(20.647454, -103.272387));
            bounds.Add(new Position(20.649023, -103.272511));
            bounds.Add(new Position(20.651447, -103.271449));
            bounds.Add(new Position(20.653217, -103.271142));
            bounds.Add(new Position(20.654499, -103.271292));
            bounds.Add(new Position(20.657027, -103.270102));
            bounds.Add(new Position(20.659143, -103.269804));
            bounds.Add(new Position(20.661319, -103.270391));
            bounds.Add(new Position(20.663578, -103.270387));
            bounds.Add(new Position(20.665686, -103.270548));
            bounds.Add(new Position(20.667473, -103.270054));
            bounds.Add(new Position(20.672250, -103.270049));
            bounds.Add(new Position(20.674758, -103.269080));
            bounds.Add(new Position(20.678263, -103.268358));
            bounds.Add(new Position(20.679480, -103.267618));
            return bounds;
        }

        private static List<Position> GetMetropolitanoBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.68076, -103.44159));
            bounds.Add(new Position(20.68042, -103.43825));
            bounds.Add(new Position(20.67804, -103.43827));
            bounds.Add(new Position(20.67738, -103.43752));
            bounds.Add(new Position(20.67718, -103.43624));
            bounds.Add(new Position(20.67783, -103.43402));
            bounds.Add(new Position(20.67497, -103.43461));
            bounds.Add(new Position(20.67510, -103.43568));
            bounds.Add(new Position(20.67499, -103.43642));
            bounds.Add(new Position(20.67449, -103.43702));
            bounds.Add(new Position(20.67388, -103.43740));
            bounds.Add(new Position(20.67339, -103.43756));
            bounds.Add(new Position(20.67284, -103.43762));
            bounds.Add(new Position(20.67220, -103.43752));
            bounds.Add(new Position(20.67170, -103.43734));
            bounds.Add(new Position(20.67101, -103.43659));
            bounds.Add(new Position(20.67078, -103.43659));
            bounds.Add(new Position(20.67097, -103.43738));
            bounds.Add(new Position(20.66382, -103.43880));
            bounds.Add(new Position(20.66382, -103.44459));
            bounds.Add(new Position(20.68076, -103.44159));
            return bounds;
        }

        private static List<Position> GetMontenegroBounds()
        {
            List<Position> bounds = new List<Position>();
            bounds.Add(new Position(20.57553, -103.31500));
            bounds.Add(new Position(20.57512, -103.31492));
            bounds.Add(new Position(20.57510, -103.31427));
            bounds.Add(new Position(20.57267, -103.31313));
            bounds.Add(new Position(20.57221, -103.31328));
            bounds.Add(new Position(20.57286, -103.31821));
            bounds.Add(new Position(20.57587, -103.31776));
            bounds.Add(new Position(20.57553, -103.31500));
            return bounds;
        }
    }
}
