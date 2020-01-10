using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreWater
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage : ContentPage
    {
        
        Label global_label = new Label() {
            FontFamily = "18386.ttf#v_CCMeanwhile-Regular"
        };

        Stepper global_stepper = new Stepper();
        Button global_button = new Button() { 
            FontFamily = "18386.ttf#v_CCMeanwhile-Regular"
        };
        public static string global_name;
        public static string global_img;
        public static int count_drinks = 0;
        public static bool is_confirmed = false;
        private List<Drink> drinks_main = new List<Drink>();
        public BasketPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewMB
            {
                 Drinks = InitDrinkData()
            };
        }

        private List<Drink> InitDrinkData()
        {
            var drink_ = new List<Drink>();

            drink_.Add(
                new Drink
                {
                    Name = "Non-carbonated water",
                    ImageUrl = "https://kuldom.ua/wa-data/public/shop/products/31/15/21531/images/6428/6428.750x0.jpg"

                });

            drink_.Add(
               new Drink
               {
                   Name = "Sparkling water",
                   ImageUrl = "https://zharino.ru/wp-content/uploads/2019/10/bonakva-1-l-600x840.jpeg"

               });

            drink_.Add(
               new Drink
               {
                   Name = "Grink",
                   ImageUrl = "https://shop.samberi.com/upload/iblock/503/5031dcca85ac48c123c9ef9b4ca41736.png"

               });

            drink_.Add(
              new Drink
              {
                  Name = "Coca-Cola",
                  ImageUrl = "https://zakadmin.ru/assets/photos/2019/05/25/item_3469339_1.jpg"

              });
            
            drink_.Add(
              new Drink
              {
                  Name = "Fanta",
                  ImageUrl = "https://www.watermos.ru/upload/iblock/1ae/1ae4acf644f6bebd1a81aed7f08ca616.jpg"

              });

            drinks_main = drink_;

            return drink_;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StackLayoutMap.Children.Contains(global_label) &&
                StackLayoutMap.Children.Contains(global_stepper) &&
                StackLayoutMap.Children.Contains(global_button))
            {
                StackLayoutMap.Children.Remove(global_stepper);
                StackLayoutMap.Children.Remove(global_label);
                StackLayoutMap.Children.Remove(global_button);
                count_drinks = 0;
                global_name = "";
                global_img = "";
                is_confirmed = false;
            }

            is_confirmed = false;

            Picker picker = sender as Picker;
            if (picker.SelectedIndex == 0)
            {

                global_name = drinks_main[0].Name;
                global_img = drinks_main[0].ImageUrl;
            }
            else if (picker.SelectedIndex == 1)
            {
                global_name = drinks_main[1].Name;
                global_img = drinks_main[1].ImageUrl;
            }
            else if (picker.SelectedIndex == 2)
            {
                global_name = drinks_main[2].Name;
                global_img = drinks_main[2].ImageUrl;
            }
            else if (picker.SelectedIndex == 3)
            {
                global_name = drinks_main[3].Name;
                global_img = drinks_main[3].ImageUrl;
            }
            else if (picker.SelectedIndex == 4)
            {
                global_name = drinks_main[4].Name;
                global_img = drinks_main[4].ImageUrl;
            }

            Stepper stepper = new Stepper
            {
                Value = 0,
                Minimum = 0,
                Maximum = 10,
                Increment = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            stepper.ValueChanged += OnStepperValueChanged;


            Binding binding = new Binding { Source = stepper, Path = "Value" };
            // установка привязки для свойства TextProperty
            Label label = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontFamily = "18386.ttf#v_CCMeanwhile-Regular"
            };

            label.SetBinding(Label.TextProperty, binding);
            global_label = label;
            global_stepper = stepper;

            StackLayoutMap.Children.Add(stepper);
            StackLayoutMap.Children.Add(label);

            Button button = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                Margin = 10,
                Padding = 0,
                HeightRequest = 50,
                WidthRequest = 100,
                CornerRadius = 20,
                Text = "push",
                BackgroundColor = Color.FromHex("#f5f5dc"),
                TextColor = Color.FromHex("#848482"),
                BorderColor = Color.FromHex("#848482"),
                BorderWidth = 1,
                FontFamily = "18386.ttf#v_CCMeanwhile-Regular"
            };

            button.Clicked += OnButtonClicked;
            StackLayoutMap.Children.Add(button);
            global_button = button;
        }

        void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            global_label.Text = String.Format("{0}", e.NewValue);
            count_drinks = Convert.ToInt32(e.NewValue);
        }
        void OnButtonClicked(object sender, System.EventArgs e)
        {
            if (count_drinks > 0)
            {
                is_confirmed = true;
                Navigation.PopAsync();
            }
        }
    }
}