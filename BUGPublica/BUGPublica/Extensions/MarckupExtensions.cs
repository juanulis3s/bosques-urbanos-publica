using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BUGPublica.Extensions
{
    public class ScreenMarckupExtension : IMarkupExtension<double>
    {
        public double FullValue { get; set; }
        public double RequiredPercentage { get; set; }
        public double AddValueToResult { get; set; } = 0;
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return (RequiredPercentage * FullValue * .01) + AddValueToResult;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }

    public class ThicknessExtension : IMarkupExtension<Thickness>
    {
        public double Left { get; set; } = 0;
        public double Right { get; set; } = 0;
        public double Up { get; set; } = 0;
        public double Bottom { get; set; } = 0;

        public Thickness ProvideValue(IServiceProvider serviceProvider)
        {
            return new Thickness(Left, Up, Right, Bottom);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Thickness>).ProvideValue(serviceProvider);
        }
    }

    public class MathMinMarckupExtension : IMarkupExtension<double>
    {
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return Math.Min(Value1, Value2);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }

    public class MathMaxMarckupExtension : IMarkupExtension<double>
    {
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return Math.Max(Value1, Value2);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }
}
