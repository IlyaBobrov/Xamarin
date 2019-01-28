using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;


namespace task1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                //Создает экземпляр типа с заданным именем в указанном удаленном домене, 
                //используя для этого именованную сборку и конструктор, который наилучшим 
                //образом соответствует заданным параметрам.
                Page page = (Page)Activator.CreateInstance(pageType);
                await Navigation.PushAsync(page);
            });

            BindingContext = this;
        }

        public ICommand NavigateCommand { private set; get; }

    }
}
