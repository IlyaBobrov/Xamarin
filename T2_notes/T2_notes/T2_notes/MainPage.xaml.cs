using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
        protected void checkCount()
        {
            while (leftContainer.Children.Count <= rightContainer.Children.Count - 2) 
            {
                var note = rightContainer.Children[rightContainer.Children.Count - 1];
                rightContainer.Children.Remove(note);

                leftContainer.Children.Add(note);
            }

            while (rightContainer.Children.Count <= leftContainer.Children.Count - 2) 
            {
                var note = leftContainer.Children[leftContainer.Children.Count - 1];
                leftContainer.Children.Remove(note);

                rightContainer.Children.Add(note);
            }

        }

        protected void addNote(string text, DateTime timeChanged, char pos = ' ')
        {            
            Label noteText = new Label()
            {
                Margin = new Thickness(0, 0, 5, 5),
                LineBreakMode = LineBreakMode.TailTruncation,
                TextColor = Color.Black,
                Text = text
                
            };
            Label noteTime = new Label()
            {
                LineBreakMode = LineBreakMode.TailTruncation,
                Text = timeChanged.ToString()
            };

            StackLayout newNote = new StackLayout()
            {
                Children = { noteText, noteTime }
            };

            Frame newNoteFrame = new Frame()
            {
                BackgroundColor = Color.AliceBlue,
                BorderColor = Xamarin.Forms.Color.YellowGreen,
                Padding = new Thickness(15, 15, 0, 15),
                Content = newNote
            };

            //--сдвиги--
            var pan = new PanGestureRecognizer();
            double totalX = 0;
            //распознователь
            pan.PanUpdated += async (panSender, panArgs) =>
            {
                switch (panArgs.StatusType)
                {
                    case GestureStatus.Canceled:
                    case GestureStatus.Started:
                        //степень перемещенеия элемента
                        newNoteFrame.TranslationX = 0;
                        break;
                    case GestureStatus.Completed:
                        if (totalX > 0 && rightContainer.Children.Contains(newNoteFrame))
                        {
                            if (await DisplayAlert("Подтверждение удаления", "Удалить заметку безвозвратно?", "Да", "Нет"))
                            {
                                rightContainer.Children.Remove(newNoteFrame);
                                checkCount();
                                safeNotes();
                            }
                            totalX = 0;
                        }
                        else if (totalX < 0 && leftContainer.Children.Contains(newNoteFrame))
                        {
                            if (await DisplayAlert("Подтверждение удаления", "Удалить заметку безвозвратно?", "Да", "Нет"))
                            {
                                leftContainer.Children.Remove(newNoteFrame);
                                checkCount();
                                safeNotes();
                            }
                            totalX = 0;
                        }
                        newNoteFrame.TranslationX = 0;
                        break;
                    case GestureStatus.Running:
                        if (panArgs.TotalX > 0 && rightContainer.Children.Contains(newNoteFrame))
                        {
                            newNoteFrame.TranslationX = panArgs.TotalX;
                            totalX = panArgs.TotalX;
                        }
                        else if (panArgs.TotalX < 0 && leftContainer.Children.Contains(newNoteFrame))
                        {
                            newNoteFrame.TranslationX = panArgs.TotalX;
                            totalX = panArgs.TotalX;
                        }
                        break;
                }
            };

            //выбираем стек
            newNote.GestureRecognizers.Add(pan);
            if (pos == 'l')
            {
                leftContainer.Children.Add(newNoteFrame);
            }
            else if (pos == 'r')
            {
                rightContainer.Children.Add(newNoteFrame);
            }
            else
            {
                if (leftContainer.Children.Count <= rightContainer.Children.Count)
                {
                    leftContainer.Children.Add(newNoteFrame);
                }
                else
                {
                    rightContainer.Children.Add(newNoteFrame);
                }
            }
            


            var tapOpen = new TapGestureRecognizer();
            tapOpen.Tapped += (tapSender, tapEven) =>
            {
                //залетаем в редактирование (перегруженную функцию)
                EditorPage edit = new EditorPage(timeChanged, text);
                Navigation.PushAsync(edit);
                edit.Disappearing += (_, __) =>
                {
                    text = edit.text;
                    noteText.Text = text;
                    timeChanged = edit.timeChanged;
                    noteTime.Text = timeChanged.ToString();
                    checkCount();
                    safeNotes();
                };
            };
            newNote.GestureRecognizers.Add(tapOpen);
        }

        public MainPage()
        {
            InitializeComponent();
            string newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savedNotes.json");
            if (File.Exists(newFile))
            {
                //Инициализирует новый экземпляр класса
                JObject savedNotes = JObject.Parse(File.ReadAllText(newFile));
                foreach (JObject item in savedNotes["left"])
                {
                    addNote((string)item.GetValue("text"), DateTime.Parse((string)item.GetValue("time")), 'l');
                }
                foreach (JObject item in savedNotes["right"])
                {
                    addNote((string)item.GetValue("text"), DateTime.Parse((string)item.GetValue("time")), 'r');
                }
            }

            int rig = 0; 
            int lef = 0;
            findEntry.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                foreach (Frame note in leftContainer.Children)
                {
                    if (!((Label)((StackLayout)note.Content).Children[0]).Text.Contains(findEntry.Text) &&
                         ((Label)((StackLayout)note.Content).Children[0]).Text != null)
                    {
                        note.IsVisible = false;
                    }
                    else
                    {

                        note.IsVisible = true;
                        lef++;
                    }
                }
                foreach (Frame note in rightContainer.Children)
                {
                    if (!((Label)((StackLayout)note.Content).Children[0]).Text.Contains(findEntry.Text) &&
                         ((Label)((StackLayout)note.Content).Children[0]).Text != null)
                    {
                        note.IsVisible = false;
                    }
                    else
                    {
                        note.IsVisible = true;
                        rig++;
                    }
                }
                /*
                foreach (Frame note in rightContainer.Children) {
                    if ((note.IsVisible == true)&&(Math.Abs(lef - rig) != 0 || Math.Abs(lef - rig) != 1))
                    {
                        StackLayout leftContainerCopy = new StackLayout()
                        {
                            VerticalOptions = LayoutOptions.Start,
                            Margin = new Thickness(0, 5, 5, 5)
                        };
                        StackLayout rightContainerCopy = new StackLayout()
                        {
                            VerticalOptions = LayoutOptions.Start,
                            Margin = new Thickness(0, 5, 5, 5)
                        };
                        if (rig > lef)
                        {
                            leftContainerCopy.Children.Add(note);
                            note.IsVisible = false;
                            rig--;
                            lef++;
                            leftContainer.Children.Add(leftContainerCopy);
                        }
                        else
                        {
                            rightContainerCopy.Children.Add(note);
                            note.IsVisible = false;
                            rig++;
                            lef--;
                            rightContainer.Children.Add(rightContainerCopy);
                        }
                    }
                }
            
            foreach (Frame note in leftContainer.Children)
            {
                if ((note.IsVisible == true) && (Math.Abs(lef - rig) != 0 || Math.Abs(lef - rig) != 1))
                {
                    StackLayout leftContainerCopy = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 5, 5)
                    };
                    StackLayout rightContainerCopy = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.Start,
                        Margin = new Thickness(0, 5, 5, 5)
                    };
                    if (rig > lef)
                    {
                        leftContainerCopy.Children.Add(note);
                        note.IsVisible = false;
                        rig--;
                        lef++;
                        leftContainer.Children.Add(leftContainerCopy);
                    }
                    else
                    {
                        rightContainerCopy.Children.Add(note);
                        note.IsVisible = false;
                        rig++;
                        lef--;
                        rightContainer.Children.Add(rightContainerCopy);
                    }
                }
            }*/
            };

        
    }

        
        
        private void safeNotes()
        {
            string newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "savedNotes.json");

            JArray left = new JArray();
            JArray right = new JArray();

            foreach (Frame note in leftContainer.Children)
            {
                //создаем экз класса и заполняем данными со стеков
                left.Add(new JObject( new JProperty("text", ((Label)((StackLayout)note.Content).Children[0]).Text),
                                      new JProperty("time", ((Label)((StackLayout)note.Content).Children[1]).Text) ));
            }
            foreach (Frame note in rightContainer.Children)
            {
                right.Add(new JObject(  new JProperty("text", ((Label)((StackLayout)note.Content).Children[0]).Text),
                                        new JProperty("time", ((Label)((StackLayout)note.Content).Children[1]).Text) ));
            }

            JObject savedNotes = new JObject
            (
                new JProperty("left", left),
                new JProperty("right", right)
            );
            //Создает новый файл, записывает в него содержимое и затем закрывает файл
            File.WriteAllText(newFile, savedNotes.ToString());

        }

        private void addNote_Clicked(object sender, EventArgs e)
        {
            EditorPage editorPage = new EditorPage();
            editorPage.Disappearing += (object editorPageSender, EventArgs editorPageargs) =>
            {
                if (editorPage.text == null || editorPage.text == "") return;
                string text = editorPage.text;
                DateTime time = DateTime.Now;
                addNote(text, time);
                safeNotes();
            };
            Navigation.PushAsync(editorPage);
        }

    }
}
