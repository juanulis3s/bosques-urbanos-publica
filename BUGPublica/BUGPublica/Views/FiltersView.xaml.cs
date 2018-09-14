using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BUGPublica.Models;

namespace BUGPublica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FiltersView : ContentView
	{
        //DICCIONARIO PARA GUARDAR LAS OPCIONES Y SUS VALORES DE SELECCIONADO
        Dictionary<int, bool> _dictionaryOptions = new Dictionary<int, bool>();

        public event EventHandler<FilterEventArgs> Filtered;

		public FiltersView ()
		{
			InitializeComponent ();

            //EVENTO CLICK DEL BOTON ACEPTAR
            btnAccept.Clicked += (sender, e) =>
            {
                IsVisible = false;
                //SE DISPARA EL EVENTO DE FILTRADO
                FilterEventArgs args = new FilterEventArgs { Options = _dictionaryOptions };
                Filtered?.Invoke(this, args);
            };

            //EVENTO CLICK DEL CHECKBOX TODOS
            checkboxAll.CheckedChanged += (object sender, bool e) =>
            {
                //SE ACTIVA O DESACTIVA TODAS LAS LAS OPCIONES
                foreach(var child in grid.Children)
                {
                    ((ImageCheckBox)child).IsChecked = e;
                    _dictionaryOptions[((ImageCheckBox)child).CID] = e;
                }
            };
		}

        /// <summary>
        /// AGREGA LA LISTA DE OPCIONES DE LUGARES AL GRID DEL FILTRO
        /// </summary>
        /// <param name="options"></param>
        public void AddOptions(List<MarkerPlace> options)
        {
            int col = 0, row = 0;
            foreach(MarkerPlace option in options)
            {
                //SE CREA UN CHECKBOX CON IMAGEN
                ImageCheckBox checkBox = new ImageCheckBox
                {
                    Text = option.Name,
                    ImageSource = option.Icon,
                    FontSize = 12,
                    ImageSize = 30,
                    IsChecked = true,
                    CID = option.Id
                };

                //SE AGREGA EL EVENTO DE TOOGLE AL CHECKBOX
                checkBox.CheckedChanged += (sender, e) =>
                {
                    _dictionaryOptions[e.Id] = e.Checked;
                };

                //SE AGREGA EL VALOR DE LA OPCION AL DICCIONARIO
                _dictionaryOptions.Add(option.Id, true);

                //AGREGA LA OPCION AL GRID Y SE AUMENTA LA COLUMNA
                grid.Children.Add(checkBox, col, row);
                col++;

                //SI LA COLUMNA ES 4 SE REINICA EN 0 Y SE AUMENTA LA FILA
                if (col == 4)
                {
                    row++;
                    col = 0;
                }
            }
        }
	}

    public class FilterEventArgs : EventArgs
    {
        public Dictionary<int, bool> Options { get; set; }
    }
}