using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace StoreWater
{
    public class ViewMB : INotifyPropertyChanged
    {

        List<Drink> drink_;
        public List<Drink> Drinks
        {
            get { return drink_; }
            set
            {
                if (drink_ != value)
                {
                    drink_ = value;
                    OnPropertyChanged();
                }
            }
        }

        Drink selectedDrink;
        public Drink SelectedDrink
        {
            get { return selectedDrink; }
            set
            {
                if (selectedDrink != value)
                {
                    selectedDrink = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
