using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace task1
{
    public class ARGB : IMarkupExtension<Color>
    {
        public double R { set; get; } = 0;

        public double G { set; get; } = 0;

        public double B { set; get; } = 0;

        public double A { set; get; } = 1.0;

        public Color ProvideValue(IServiceProvider serviceProvider)
        {
            return Color.FromRgba(R, G, B, A);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Color>).ProvideValue(serviceProvider);
        }
    }
}
