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
        public DateTime timeChanged;

        public EditorPage()
        {
            InitializeComponent();

            timeChanged = DateTime.Now;
            timeAndSymb.Text = timeChanged.ToString();
            if (text != null)
            {
                timeAndSymb.Text += "    " + text.Length;
            }
        }

        public EditorPage(DateTime timeChanged, string text)
        {
            InitializeComponent();
            this.text = text;
            textHolder.Text = text;
            this.timeChanged = timeChanged;

            timeAndSymb.Text = timeChanged.ToString();
            if (text != null)
            {
                timeAndSymb.Text += "    " + text.Length;
            }
        }

        private void textHolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = textHolder.Text;
            timeChanged = DateTime.Now;
        }
    }
}