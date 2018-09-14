using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace BUGPublica.CustomRenders
{
    public class CustomMap : Map
    {
        ObservableCollection<CustomPin> _customPins = new ObservableCollection<CustomPin>();

        public ObservableCollection<CustomPin> CustomPins
        {
            get { return _customPins; }
        }
        public Position Position { get; set; }
        public Position LimitNorthEast { get; set; }
        public Position LimitSouthWest { get; set; }
        public float MaxZoom { get; set; }
        public float MinZoom { get; set; }
        public bool ShowIndexPin { get; set; }

        protected virtual void OnMapReady(EventArgs e)
        {
            MapReady?.Invoke(this, e);
        }

        public event EventHandler MapReady;

        public void MapLoaded()
        {
            OnMapReady(EventArgs.Empty);
        }
    }
}
