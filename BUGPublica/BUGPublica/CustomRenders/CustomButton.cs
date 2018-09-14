using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BUGPublica.CustomRenders
{
    /// <summary>
    /// BOTON PERSONALIZADO PARA LA APLICACION
    /// </summary>
    public class CustomButton : Button
    {
        //BINDABLE PROPERTY PARA EL RADIO DEL BORDE DEL BOTON
        public static readonly BindableProperty CustomRadiusProperty = BindableProperty.Create(
            nameof(CustomRadius), typeof(int), typeof(CustomButton), 4);

        //BINDABLE PROPERTY PARA EL COLOR DEL BOTON
        public static readonly BindableProperty CustomBackgroundColorProperty = BindableProperty.Create(
            nameof(CustomBackgroundColor), typeof(Color), typeof(CustomButton), Color.Default);

        //BINDABLE PROPERTY PARA EL GROSOR DEL BORDE DEL BOTON
        public static readonly BindableProperty CustomBorderWidthProperty = BindableProperty.Create(
            nameof(CustomBorderWidth), typeof(int), typeof(CustomButton), 4);

        //BINDABLE PROPERTY PARA EL COLOR DEL BORDE DEL BOTON
        public static readonly BindableProperty CustomBorderColorProperty = BindableProperty.Create(
            nameof(CustomBorderColor), typeof(Color), typeof(CustomButton), Color.Default);

        //BINDABLE PROPERTY PARA EL COLOR AL PRESIONAR EL BOTON
        public static readonly BindableProperty CustomBackgroundColorPressedProperty = BindableProperty.Create(
            nameof(CustomBackgroundColorPressed), typeof(Color), typeof(CustomButton), Color.Default);


        public int CustomRadius
        {
            get { return (int)GetValue(CustomRadiusProperty); }
            set { SetValue(CustomRadiusProperty, value); }
        }

        public Color CustomBackgroundColor
        {
            get { return (Color)GetValue(CustomBackgroundColorProperty); }
            set { SetValue(CustomBackgroundColorProperty, value); }
        }

        public int CustomBorderWidth
        {
            get { return (int)GetValue(CustomBorderWidthProperty); }
            set { SetValue(CustomBorderWidthProperty, value); }
        }

        public Color CustomBorderColor
        {
            get { return (Color)GetValue(CustomBorderColorProperty); }
            set { SetValue(CustomBorderColorProperty, value); }
        }

        public Color CustomBackgroundColorPressed
        {
            get { return (Color)GetValue(CustomBackgroundColorPressedProperty); }
            set { SetValue(CustomBackgroundColorPressedProperty, value); }
        }

        public CustomButton()
        {
            Clicked += (sender, e) =>
            {
                if (!IsEnabled)
                    return;

                BackgroundColor = CustomBackgroundColorPressed;
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    BackgroundColor = CustomBackgroundColor;
                    return false;
                });
            };
        }

    }
}
