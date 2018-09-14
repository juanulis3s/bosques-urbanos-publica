using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BUGPublica.Helpers
{
    /// <summary>
    /// CONVERTIDOR DE CADENAS A MAYUSCULAS Y MINUSCULAS PARA SU USO EN XAML
    /// </summary>
    public class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = System.Convert.ToString(parameter) ?? "u";

            switch (param.ToUpper())
            {
                case "U":
                    return ((string)value).ToUpper();
                case "L":
                    return ((string)value).ToLower();
                default:
                    return (string)value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
