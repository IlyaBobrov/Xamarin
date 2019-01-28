using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace task1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class ListColorsPage : ContentPage
    {
        //Создает объект ColorTypeConverter со значениями по умолчанию
        readonly ColorTypeConverter converter = new ColorTypeConverter();

        public ListColorsPage()
        {
            InitializeComponent();

            var dictionary = new Dictionary<String, Color>();

            foreach (var field in typeof(Color).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field != null && !String.IsNullOrEmpty(field.Name))
                    //Создает цвет на основе допустимого названия цвета
                    dictionary[field.Name] = (Color)(converter.ConvertFromInvariantString(field.Name));
            }

            NameListView.ItemsSource = dictionary;
        }
    }
}