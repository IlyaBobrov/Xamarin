using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace T2_notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditorPage : ContentPage
    {
        public string text { private set; get; }
        public DateTime timeChanged { private set; get; }

        List<string> cash;
        int index = 49;
        bool newCash = true;

        void PropLeft()
        {
            if (cash == null)
            {
                return;
            }
            if (index != 49)
            {
                List<string> cash1 = new List<string>(50);
                for (int j = 0; j < 50; ++j)
                {
                    cash1.Add(null);
                }
                int z = index;
                int count = 0;
                while (cash[z] != null && z != 0)
                {
                    ++count;
                    --z;
                }
                int i1 = z;
                for (int k = 50 - count; k < 50; ++k)
                {
                    cash1[k] = cash[i1];
                    ++i1;
                }
                cash = cash1;
                index = 49;
                return;
            }
            int i = index;
            string prevStr = cash[i];
            string str = cash[i];
            while (cash[i] != null && i != 0)
            {
                prevStr = cash[i - 1];
                cash[--i] = str;
                str = prevStr;
            }
        }

        public EditorPage()
        {
            InitializeComponent();
            timeChanged = DateTime.Now;
            timeAndSymb.Text = timeChanged.ToString();
            timeAndSymb.Text += "  len: ";

            if (text != null)
            {
                len.Text = text.Length.ToString();
            }
            else
            {
                len.Text = "0";
            }

            cash = new List<string>(50);
            for (int i = 0; i < 50; ++i)
            {
                cash.Add(null);
            }
        }
        public EditorPage(DateTime timeChanged, string text)
        {
            InitializeComponent();
            this.text = text;
            textHolder.Text = text;
            this.timeChanged = timeChanged;
            timeAndSymb.Text = timeChanged.ToString();
            timeAndSymb.Text += "  len: ";
            if (text != null)
            {
                len.Text = text.Length.ToString();
            }
            else
            {
                len.Text = "ты что сделал?";

            }

        }
        private void textHolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = textHolder.Text;
            timeChanged = DateTime.Now;
            len.Text = text.Length.ToString();
            //if (len.Text == "0") text = null;
        }

        private void addNote_Clicked(object sender, EventArgs e)
        {
            timeChanged = DateTime.Now;
            text = textHolder.Text;
            Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            timeChanged = DateTime.Now;
            text = textHolder.Text;
            Navigation.PopAsync();
            return true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            /*if (index + 1 < 50)
            {
                ++index;
                newCash= false;
                text = cash[index];
                textHolder.Text = text;
            }*/
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            /*if (cash[index - 1] != null)
            {
                if (index - 1 >= 0 && cash[index - 1] != null)
                {
                    --index;
                }
                newCash = false;
                text = cash[index];
                textHolder.Text = text;
            }*/
        }
    }

}