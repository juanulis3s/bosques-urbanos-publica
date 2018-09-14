using System;
using System.Collections.Generic;
using System.Text;

namespace BUGPublica.Models
{
    public class BUG
    {
        //ID BOSQUES DE LA RED BUG
        public const int ID_BUG_MORELOS = 11;
        public const int ID_BUG_AGUA_AZUL = 5;
        public const int ID_BUG_COLOMOS = 3;
        public const int ID_BUG_MIRADOR_INDEPENDENCIA = 10;
        public const int ID_BUG_PUERTA_BARRANCA = 8;
        public const int ID_BUG_NATURAL_HUENTITAN = 7;
        public const int ID_BUG_AVILA_CAMACHO = 4;
        public const int ID_BUG_ALCALDE = 1;
        public const int ID_BUG_GONZALEZ_GALLO = 6;
        public const int ID_BUG_DEAN = 2;
        public const int ID_BUG_ARBOLEDAS_SUR = 9;

        //NOMBRES BOSQUES DE LA RED BUG
        private const string NAME_BUG_MORELOS = "Parque Morelos";
        private const string NAME_BUG_AGUA_AZUL = "Parque Agua Azul";
        private const string NAME_BUG_COLOMOS = "Bosques Los Colomos";
        private const string NAME_BUG_MIRADOR_INDEPENDENCIA = "Parque Mirador Independencia";
        private const string NAME_BUG_PUERTA_BARRANCA = "Parque Puerta de la Barranca";
        private const string NAME_BUG_NATURAL_HUENTITAN = "Parque Natural Huentitán";
        private const string NAME_BUG_AVILA_CAMACHO = "Parque Ávila Camacho";
        private const string NAME_BUG_ALCALDE = "Parque Alcalde";
        private const string NAME_BUG_GONZALEZ_GALLO = "Parque González Gallo";
        private const string NAME_BUG_DEAN = "Parque Dean";
        private const string NAME_BUG_ARBOLEDAS_SUR = "Parque Arboledas Sur";

        //NOMBRES CORTOS BOSQUES DE LA RED BUG
        private const string SHORTNAME_BUG_MORELOS = "PARQUE MORELOS";
        private const string SHORTNAME_BUG_AGUA_AZUL = "PARQUE AGUA AZUL";
        private const string SHORTNAME_BUG_COLOMOS = "BOSQUE LOS COLOMOS";
        private const string SHORTNAME_BUG_MIRADOR_INDEPENDENCIA = "MIRADOR INDEPENDENCIA";
        private const string SHORTNAME_BUG_PUERTA_BARRANCA = "PUERTA DE LA BARRANCA";
        private const string SHORTNAME_BUG_NATURAL_HUENTITAN = "NATURAL HUENTITÁN";
        private const string SHORTNAME_BUG_AVILA_CAMACHO = "PARQUE ÁVILA CAMACHO";
        private const string SHORTNAME_BUG_ALCALDE = "PARQUE ALCALDE";
        private const string SHORTNAME_BUG_GONZALEZ_GALLO = "PARQUE GONZÁLEZ GALLO";
        private const string SHORTNAME_BUG_DEAN = "PARQUE LIBERACIÓN (EL DEAN)";
        private const string SHORTNAME_BUG_ARBOLEDAS_SUR = "ARBOLEDAS DEL SUR";

        public string Name { get; set; }
        public string Shortname { get; set; }
        public int Id { get; set; }

        public static BUG BUG_COLOMOS
        {
            get { return new BUG { Name = NAME_BUG_COLOMOS, Shortname = SHORTNAME_BUG_COLOMOS, Id = ID_BUG_COLOMOS }; }
        }

        public static BUG BUG_MORELOS
        {
            get { return new BUG { Name = NAME_BUG_MORELOS, Shortname = SHORTNAME_BUG_MORELOS, Id = ID_BUG_MORELOS }; }
        }

        public static BUG BUG_AGUA_AZUL
        {
            get { return new BUG { Name = NAME_BUG_AGUA_AZUL, Shortname = SHORTNAME_BUG_AGUA_AZUL, Id = ID_BUG_AGUA_AZUL }; }
        }

        public static BUG BUG_MIRADOR_HUENTITAN
        {
            get { return new BUG { Name = NAME_BUG_MIRADOR_INDEPENDENCIA, Shortname = SHORTNAME_BUG_MIRADOR_INDEPENDENCIA, Id = ID_BUG_MIRADOR_INDEPENDENCIA }; }
        }

        public static BUG BUG_PUERTA_BARRANCA
        {
            get { return new BUG { Name = NAME_BUG_PUERTA_BARRANCA, Shortname = SHORTNAME_BUG_PUERTA_BARRANCA, Id = ID_BUG_PUERTA_BARRANCA }; }
        }

        public static BUG BUG_NATURAL_HUENTITAN
        {
            get { return new BUG { Name = NAME_BUG_NATURAL_HUENTITAN, Shortname = SHORTNAME_BUG_NATURAL_HUENTITAN, Id = ID_BUG_NATURAL_HUENTITAN }; }
        }

        public static BUG BUG_AVILA_CAMACHO
        {
            get { return new BUG { Name = NAME_BUG_AVILA_CAMACHO, Shortname = SHORTNAME_BUG_AVILA_CAMACHO, Id = ID_BUG_AVILA_CAMACHO }; }
        }

        public static BUG BUG_ALCALDE
        {
            get { return new BUG { Name = NAME_BUG_ALCALDE, Shortname = SHORTNAME_BUG_ALCALDE, Id = ID_BUG_ALCALDE }; }
        }

        public static BUG BUG_GONZALEZ_GALLO
        {
            get { return new BUG { Name = NAME_BUG_GONZALEZ_GALLO, Shortname = SHORTNAME_BUG_GONZALEZ_GALLO, Id = ID_BUG_GONZALEZ_GALLO }; }
        }

        public static BUG BUG_DEAN
        {
            get { return new BUG { Name = NAME_BUG_DEAN, Shortname = SHORTNAME_BUG_DEAN, Id = ID_BUG_DEAN }; }
        }

        public static BUG BUG_ARBOLEDAS_SUR
        {
            get { return new BUG { Name = NAME_BUG_ARBOLEDAS_SUR, Shortname = SHORTNAME_BUG_ARBOLEDAS_SUR, Id = ID_BUG_ARBOLEDAS_SUR }; }
        }

        public static List<BUG> ALL
        {
            get
            {
                List<BUG> bugs = new List<BUG>();
                bugs.Add(BUG_COLOMOS);
                bugs.Add(BUG_ALCALDE);
                bugs.Add(BUG_AVILA_CAMACHO);
                bugs.Add(BUG_DEAN);
                bugs.Add(BUG_AGUA_AZUL);
                bugs.Add(BUG_GONZALEZ_GALLO);
                bugs.Add(BUG_NATURAL_HUENTITAN);
                bugs.Add(BUG_PUERTA_BARRANCA);
                bugs.Add(BUG_ARBOLEDAS_SUR);
                bugs.Add(BUG_MIRADOR_HUENTITAN);
                bugs.Add(BUG_MORELOS);
                return bugs;
            }
        }
    }
}
