using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms.Internals;
using Tizen;
using System.Runtime.CompilerServices;
//using ElmSharp;

namespace FirstDemo
{
    public class File
    {
        public File(ImageSource source, string name)
        {
            Source = source;
            Name = name;
        }

        public ImageSource Source { get; set; }
        public string Name { get; set; }
    }

    class MainPage : ContentPage
    {
        AbsoluteLayout top = null;
        AbsoluteLayout top2 = null;
        AbsoluteLayout top3 = null;
        List<File> files = null;

        string[,] _textDetail = new string[5, 2] {{ "Attack of the 50 Foot Woman",
                                                             "Attack of the 50 Foot Woman is a 1958 independently made American black-and-white science fiction film directed by Nathan H. Juran (credited as Nathan Hertz) and starring Allison Hayes, William Hudson and Yvette Vickers." },
                                                           { "JAWS",
                                                             "Jaws is a 1975 American thriller film directed by Steven Spielberg and based on Peter Benchley's 1974 novel of the same name." },
                                                           { "STAR WARS",
                                                             "Star Wars is an American epic space-opera media franchise created by George Lucas, which began with the eponymous 1977 film and quickly became a worldwide pop-culture phenomenon." },
                                                           { "E.T. the Extra-Terrestrial",
                                                             "E.T. the Extra-Terrestrial is a 1982 American science fiction film produced and directed by Steven Spielberg, and written by Melissa Mathison. It tells the story of Elliott, a boy who befriends an extraterrestrial, dubbed E.T., who is stranded on Earth." },
                                                           { "JURASSIC PARK",
                                                             "Jurassic Park is a 1993 American science fiction adventure film directed by Steven Spielberg and produced by Kathleen Kennedy and Gerald R. Molen. It is the first installment in the Jurassic Park franchise, and is based on the 1990 novel of the same name by Michael Crichton and a screenplay written by Crichton and David Koepp." } };


        void CreateDetailLayout()
        {
            ////// 1
            top = new AbsoluteLayout()
            {
                BackgroundColor = new Color(1, 1, 1, 0.6),
            };
            AbsoluteLayout.SetLayoutBounds(top, new Rectangle(0, 570, 1920, 350));
            //AbsoluteLayout.SetLayoutBounds(top, new Rectangle(0, 220, 1920, 700));
            AbsoluteLayout.SetLayoutFlags(top, AbsoluteLayoutFlags.None);

            var label = new Label()
            {
                Text = "What to Watch",
                //FontSize = 32.0,
                TextColor = Color.Black
            };
            AbsoluteLayout.SetLayoutBounds(label, new Rectangle(30, 30, 500, 30));
            AbsoluteLayout.SetLayoutFlags(label, AbsoluteLayoutFlags.None);
            top.Children.Add(label);

            var files2 = new List<File>();
            for (int i = 0; i < 20; i++)
            {
                files2.Add(new File(ImageSource.FromFile(string.Format("2/{0}.png", i)), "This is name"));
            };

            var dataTemplage = new DataTemplate(() =>
            {
                var image = new Image();
                image.SetBinding(Image.SourceProperty, "Source");
                return new ViewCell { View = image };
            });

            var detailGrid = new GridView()
            {
                ItemWidth = 360,
                ItemHeight = 250,
                Orientation = GridViewOrientation.Horizontal,
                HorizontalScrollBarVisible = true
                //ItemHorizontalAlignment = 1.0,
                //ItemVerticalAlignment = 1.0
            };
            detailGrid.ItemsSource = files2;
            detailGrid.ItemTemplate = dataTemplage;
            AbsoluteLayout.SetLayoutBounds(detailGrid, new Rectangle(0, 20, 1920, 330));
            AbsoluteLayout.SetLayoutFlags(detailGrid, AbsoluteLayoutFlags.None);

            top.Children.Add(detailGrid);
        }

        void CreateDetailLayout2()
        {
            ////// 2
            top2 = new AbsoluteLayout()
            {
                BackgroundColor = new Color(1, 1, 1, 0.6),
            };
            AbsoluteLayout.SetLayoutBounds(top2, new Rectangle(0, 270, 1920, 650));
            AbsoluteLayout.SetLayoutFlags(top2, AbsoluteLayoutFlags.None);

            var label2 = new Label()
            {
                Text = "What to Watch",
                //FontSize = 32.0,
                TextColor = Color.Black
            };
            AbsoluteLayout.SetLayoutBounds(label2, new Rectangle(30, 30, 500, 30));
            AbsoluteLayout.SetLayoutFlags(label2, AbsoluteLayoutFlags.None);
            top.Children.Add(label2);

            var files3 = new List<File>();
            for (int i = 0; i <= 48; i++)
            {
                files3.Add(new File(ImageSource.FromFile(string.Format("3/{0}.jpg", i)), "This is name"));
            };

            var dataTemplage = new DataTemplate(() =>
            {
                var layout = new RelativeLayout();

                var image = new Image();
                image.SetBinding(Image.SourceProperty, "Source");

                layout.Children.Add(image,
                    widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                    heightConstraint: Constraint.RelativeToParent(parent => parent.Height));

                var hlabel = new Label();
                hlabel.Text = "Attack of the 50 Foot Woman";

                var tlabel = new Label();
                tlabel.Text = "Attack of the 50 Foot Woman is a 1958 independently made American black-and-white science fiction film directed by Nathan H. Juran (credited as Nathan Hertz) and starring Allison";

                var stLayout = new StackLayout()
                {
                    BackgroundColor = new Color(0.1, 0.1, 0.1, 0.7),
                    Children = { hlabel, tlabel },
                    IsVisible = false
                };

                layout.Children.Add(stLayout,
                    widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                    heightConstraint: Constraint.RelativeToParent(parent => parent.Height * 0.7),
                    yConstraint: Constraint.RelativeToParent(parent => parent.Height * 0.3));

                return new ViewCell { View = layout };
            });




            var detailGrid2 = new GridView()
            {
                ItemWidth = 350,
                ItemHeight = 550,
                Orientation = GridViewOrientation.Horizontal,
                //ItemStyle = "poster",
                //BackgroundColor = Color.Yellow
            };
            detailGrid2.ItemsSource = files3;
            detailGrid2.ItemTemplate = dataTemplage;
            //detailGrid2.ItemFocusedTemplate = dataFocusedTemplage;
            detailGrid2.ItemFocused += OnItemFocused;

            AbsoluteLayout.SetLayoutBounds(detailGrid2, new Rectangle(0, 20, 1920, 600));
            AbsoluteLayout.SetLayoutFlags(detailGrid2, AbsoluteLayoutFlags.None);

            top2.Children.Add(label2);
            top2.Children.Add(detailGrid2);
            top2.IsVisible = false;
        }

        void OnItemFocused(object sender, GridViewFocusedEventArgs e)
        {
            Tizen.Log.Error("XSF", "Enter" + e.VisualElement);
            View target = e.VisualElement as View;

            if (e.IsFocused == true)
            {
                RelativeLayout layout = target as RelativeLayout;
                StackLayout stLayout = layout.Children[1] as StackLayout;
                stLayout.IsVisible = true;
            }
            else 
            {
                RelativeLayout layout = target as RelativeLayout;
                StackLayout stLayout = layout.Children[1] as StackLayout;
                stLayout.IsVisible = false;
            }
        }

        void CreateDetailLayout3()
        {
            ////// 3
            top3 = new AbsoluteLayout()
            {
                BackgroundColor = new Color(1, 1, 1, 0.6),
            };
            AbsoluteLayout.SetLayoutBounds(top3, new Rectangle(0, 570, 1920, 350));
            AbsoluteLayout.SetLayoutFlags(top3, AbsoluteLayoutFlags.None);

            var label3 = new Label()
            {
                Text = "What to Watch",
                //FontSize = 32.0,
                TextColor = Color.Black
            };
            AbsoluteLayout.SetLayoutBounds(label3, new Rectangle(30, 30, 500, 30));
            AbsoluteLayout.SetLayoutFlags(label3, AbsoluteLayoutFlags.None);
            top3.Children.Add(label3);

            var files4 = new List<File>();
            for (int i = 0; i < 44; i++)
            {
                files4.Add(new File(ImageSource.FromFile(string.Format("4/{0}.jpg", i)), "This is name"));
            };

            var dataTemplage = new DataTemplate(() =>
            {
                var image = new Image();
                image.SetBinding(Image.SourceProperty, "Source");
                return new ViewCell { View = image };
            });

            var detailGrid3 = new GridView()
            {
                ItemWidth = 360,
                ItemHeight = 250,
                Orientation = GridViewOrientation.Horizontal,
            };
            detailGrid3.ItemsSource = files4;
            detailGrid3.ItemTemplate = dataTemplage;
            AbsoluteLayout.SetLayoutBounds(detailGrid3, new Rectangle(0, 20, 1920, 330));
            AbsoluteLayout.SetLayoutFlags(detailGrid3, AbsoluteLayoutFlags.None);

            top3.Children.Add(detailGrid3);
            top3.IsVisible = false;
        }

        public MainPage()
        {
            this.BackgroundColor = Color.Transparent ;

            var backLayout = new AbsoluteLayout();

            files = new List<File>();
            File selectItem = null;
            for (int i = 0; i < 30; i++)
            {
                var file = new File(ImageSource.FromFile(string.Format("1/mi{0}.png", i % 13)), "This is name");
                files.Add(file);

                if (i == 5)
                    selectItem = file;
            };

            var bottom = new AbsoluteLayout()
            {
                BackgroundColor = new Color(1,1,1,0.7), 
                
            };
            AbsoluteLayout.SetLayoutBounds(bottom, new Rectangle(0, 920, 1920, 160));
            AbsoluteLayout.SetLayoutFlags(bottom, AbsoluteLayoutFlags.None);

            var dataTemplage = new DataTemplate(() =>
            {
                var image = new Image();
                image.SetBinding(Image.SourceProperty, "Source");
                return new ViewCell { View = image };
            });

            var menuGrid = new GridView()
            {
                ItemWidth = 120,
                ItemHeight = 120,
                ItemHorizontalAlignment = 0.5,
                ItemVerticalAlignment = 0.5,
                ThemeStyle = "small",
                Orientation = GridViewOrientation.Horizontal,
            };
            menuGrid.ItemsSource = files;
            menuGrid.ItemTemplate = dataTemplage;
            menuGrid.ItemSelected += OnMenuSelectedItemChanged;
            AbsoluteLayout.SetLayoutBounds(menuGrid, new Rectangle(100, 0, 1920, 160));
            AbsoluteLayout.SetLayoutFlags(menuGrid, AbsoluteLayoutFlags.None);
            bottom.Children.Add(menuGrid);

            var button = new Button()
            {
                Text = "Click"
            };
            AbsoluteLayout.SetLayoutBounds(button, new Rectangle(100, 0, 1920, 10));
            AbsoluteLayout.SetLayoutFlags(button, AbsoluteLayoutFlags.None);
            button.Clicked += (s, e) =>
            {
                menuGrid.SelectedItem = selectItem;
            };
            bottom.Children.Add(button);

            CreateDetailLayout();
            CreateDetailLayout2();
            CreateDetailLayout3();

            backLayout.Children.Add(top);
            backLayout.Children.Add(top2);
            backLayout.Children.Add(top3);
            backLayout.Children.Add(bottom);


            this.Content = backLayout;
        }

        void OnMenuSelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            File item = e.SelectedItem as File;
            Tizen.Log.Error("XSF", "Enter [" + item?.Source+"]");
            int index = files.FindIndex(x => x==item);
            //int index = 0;
            if (index == 0)
            {
                top.IsVisible = true;
                top2.IsVisible = false;
                top3.IsVisible = false;
            }
            else if (index == 1)
            {
                top.IsVisible = false;
                top2.IsVisible = true;
                top3.IsVisible = false;
            }
            else if (index == 2)
            {
                top.IsVisible = false;
                top2.IsVisible = false;
                top3.IsVisible = true;
            }
        }
    }
}
