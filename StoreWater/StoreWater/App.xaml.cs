using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreWater
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#f5f5dc"),
                BarTextColor = Color.FromHex("#848482")

            };
            
        }

        //обработчик приложения при запуске
        protected override void OnStart()
        {
        }
        //обработчик приложения в спящем режиме
        protected override void OnSleep()
        {
        }
        //обработчик приложения при возобнавлениии его работы
        protected override void OnResume()
        {
        }
    }
}
