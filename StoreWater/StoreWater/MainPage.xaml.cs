using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StoreWater
{

    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public struct Obj
        {
            public string name;
            public int count;
            public string img;
        }

        List<Obj> Basket = new List<Obj>();
        private void Create_basket(string count_, string name_, string img_, int id_)
        {
            Image img = new Image
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 30,
                Source = img_,
                HeightRequest = 30
            };


            Label name_drink = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = name_,
                FontSize = 11,
                FontFamily = "18386.ttf#v_CCMeanwhile-Regular"

                //HeightRequest = 45,
                //WidthRequest = 230
            };

            Label numbers = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = count_,
                FontFamily = "18386.ttf#v_CCMeanwhile-Regular"
                //HeightRequest = 45,
                //WidthRequest = 45
            };

            Button plus_drinks = new Button
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 40,
                WidthRequest = 30,
                CornerRadius = 5,
                Text = "+",
                BackgroundColor = Color.FromHex("#f5f5dc"),
                TextColor = Color.FromHex("#848482"),
                BorderColor = Color.FromHex("#848482"),
                BorderWidth = 1,
                FontFamily = "18386.ttf#v_CCMeanwhile-Regular"

                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
            };

            plus_drinks.Clicked += (s, w) =>
            {
                numbers.Text = Convert.ToString(Convert.ToInt32(numbers.Text) + 1);
                Obj b = Basket[id_];
                b.count = Convert.ToInt32(numbers.Text);
                Basket[id_] = b;
            };

            Button minus_drinks = new Button
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 40,
                WidthRequest = 30,
                CornerRadius = 5,
                Text = "-",
                BackgroundColor = Color.FromHex("#f5f5dc"),
                TextColor = Color.FromHex("#848482"),
                BorderColor = Color.FromHex("#848482"),
                BorderWidth = 1,
                FontFamily = "18386.ttf#v_CCMeanwhile-Regular"

                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
            };

            Button delete_drinks = new Button
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 40,
                WidthRequest = 40,
                CornerRadius = 10,
                Text = "x",
                BackgroundColor = Color.FromHex("#f2bfbf"),
                TextColor = Color.FromHex("#848482"),
                BorderColor = Color.FromHex("#848482"),
                BorderWidth = 1,
                FontFamily = "18386.ttf#v_CCMeanwhile-Regular"


                //FontFamily = Device.RuntimePlatform == Device.Android ? "Lobster-Regular.ttf#Lobster-Regular" : "Assets/Fonts/Lobster-Regular.ttf#Lobster"
            };

            Frame frame = new Frame()
            {
                Padding = 6,
                BackgroundColor = Color.FromHex("#faebd7")
            };

            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.FromHex("#faebd7")
            };

            stack.Children.Add(img);
            stack.Children.Add(name_drink);
            stack.Children.Add(numbers);
            stack.Children.Add(minus_drinks);
            stack.Children.Add(plus_drinks);
            stack.Children.Add(delete_drinks);

            frame.Content = stack;

            delete_drinks.Clicked += (s, w) =>
            {
                Stack.Children.Remove(frame);
                Basket.RemoveAll(x => x.name == name_drink.Text);
            };

            

            minus_drinks.Clicked += (s, w) =>
            {
                if (Convert.ToInt32(numbers.Text) - 1 > 0)
                {
                    numbers.Text = Convert.ToString(Convert.ToInt32(numbers.Text) - 1);

                    Obj b = Basket[id_];
                    b.count = Convert.ToInt32(numbers.Text);
                    Basket[id_] = b;
                }
                //else
                //{
                //    Stack.Children.Remove(frame);
                //    Basket.RemoveAll(x => x.Name == name_drink.Text);
                //}
            };

            Stack.Children.Add(frame);
        }

        private void New_drink(object sender, EventArgs e)
        {

            var page_select_drink = new BasketPage();
            page_select_drink.Disappearing += (s, _e) =>
            {
                if (BasketPage.is_confirmed)
                {
                    if (BasketPage.count_drinks != 0)
                    {
                        bool is_in = false;
                        if (Basket.Count() == 0)
                        {
                            Obj obj_;
                            obj_.count = BasketPage.count_drinks;
                            obj_.name = BasketPage.global_name;
                            obj_.img = BasketPage.global_img;
                            is_in = true;
                            Basket.Add(obj_);
                            BasketPage.count_drinks = 0;
                        }
                        else
                        {
                            var pos = Basket.FindIndex(x => x.name == BasketPage.global_name);
                            if (pos != -1)
                            {
                                Obj obj_;
                                obj_.name = Basket[pos].name;
                                obj_.count = Basket[pos].count + BasketPage.count_drinks;
                                obj_.img = Basket[pos].img;
                                Basket[pos] = obj_;
                                is_in = true;
                            }
                            else
                            {
                                Obj obj_;
                                obj_.count = BasketPage.count_drinks;
                                obj_.name = BasketPage.global_name;
                                obj_.img= BasketPage.global_img;
                                is_in = true;
                                Basket.Add(obj_);
                            }

                            BasketPage.count_drinks = 0;
                        }

                        if (is_in)
                        {
                            Stack.Children.Clear();
                            for (int i = 0; i < Basket.Count(); i++)
                            Create_basket(Convert.ToString(Basket[i].count), 
                                          Convert.ToString(Basket[i].name), 
                                          Convert.ToString(Basket[i].img), i);
                        }
                    }
                }
            };

            Navigation.PushAsync(page_select_drink);
        }
        private async void Confirm_drinks(object sender, EventArgs e)
        {
            if (Basket.Count() == 0)
            {
                await DisplayAlert("!!!", "Your basket is empty :( \r\n You should select smth", "Ok");
            }
            else
            {
                string purchase = "Your order:\r\n";
                for (var i = 0; i != Basket.Count(); i++)
                {
                    purchase += $"{Basket[i].count} {Basket[i].name}\r\n";
                }

                var accept = await DisplayAlert("Is the order correct?", $"{purchase}", "Yes", "No");

                if (accept)
                {
                    await DisplayAlert("Successfully!", "Your order is accepted", "OK");
                    Stack.Children.Clear();
                    Basket.Clear();
                }
            }
        }

        Label label2 = new Label();

    }
}
