using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace T2_notes
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        protected void addNote(string text)
        {
            DateTime timeChanged = DateTime.Now;
            
            Label newNote = new Label()
            {
                Margin = new Thickness(0, 0, 0, 0),
                LineBreakMode = LineBreakMode.TailTruncation,
                Text = text
            };
            Frame newNoteFrame = new Frame()
            {
                BackgroundColor = Color.Bisque,
                BorderColor = Xamarin.Forms.Color.DarkSeaGreen,
                Margin = new Thickness(0, 0, 0, 0),
                Padding = new Thickness(5, 5, 0, 5),
                Content = newNote
            };

            var pan = new PanGestureRecognizer();
            double totalX = 0;
            pan.PanUpdated += async (panSender, panArgs) =>
            {
                switch (panArgs.StatusType)
                {
                    case GestureStatus.Canceled:
                    case GestureStatus.Started:
                        newNoteFrame.TranslationX = 0;
                        break;
                    case GestureStatus.Completed:
                        if (totalX > 0 && rightContainer.Children.Contains(panSender as Frame))  // < 0 for left
                        {
                            if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                            {
                                rightContainer.Children.Remove(panSender as Frame);
                            }
                            totalX = 0;
                        }
                        else if (totalX < 0 && leftContainer.Children.Contains(panSender as Frame))  // < 0 for left
                        {
                            if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                            {
                                leftContainer.Children.Remove(panSender as Frame);
                            }
                            totalX = 0;
                        }
                        newNoteFrame.TranslationX = 0;
                        break;
                    case GestureStatus.Running:
                        if (panArgs.TotalX > 0 && rightContainer.Children.Contains(panSender as Frame))  // < 0 for left
                        {
                            newNoteFrame.TranslationX = panArgs.TotalX;
                            totalX = panArgs.TotalX;
                        }
                        else if (panArgs.TotalX < 0 && leftContainer.Children.Contains(panSender as Frame))  // < 0 for left
                        {
                            newNoteFrame.TranslationX = panArgs.TotalX;
                            totalX = panArgs.TotalX;
                        }
                        break;
                }
            };
            newNoteFrame.GestureRecognizers.Add(pan);

            if (leftContainer.Height <= rightContainer.Height)
            {
                leftContainer.Children.Add(newNoteFrame);
            }
            else
            {
                rightContainer.Children.Add(newNoteFrame);
            }



            var tap = new TapGestureRecognizer();
            tap.Tapped += (tapSender, tapEven) =>
            {
                EditorPage editorPage_ = new EditorPage(timeChanged, text);
                Navigation.PushAsync(editorPage_);
                editorPage_.Disappearing += (_, __) =>
                {
                    text = editorPage_.text;
                    newNote.Text = text;
                    timeChanged = editorPage_.timeChanged;
                };
            };
            newNote.GestureRecognizers.Add(tap);
        }

        public MainPage()
        {
            InitializeComponent();


            // add saved notes
        }

        private void add_Clicked(object sender, EventArgs e)
        {
            EditorPage editorPage = new EditorPage();
            editorPage.Disappearing += (object editorPageSender, EventArgs editorPageargs) =>
            {
                if (editorPage.text == null)
                    return;

                string text = editorPage.text;
                addNote(text);
            };
            Navigation.PushAsync(editorPage);
        }
    }
}
