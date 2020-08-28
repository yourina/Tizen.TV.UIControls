/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;
using System.Collections.Generic;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridViewTest : ContentPage
    {
        public class File
        {
            public File(ImageSource source,string name)
            {
                Source = source;
                Name = name;
            }

            public ImageSource Source { get; set; }
            public string Name { get; set; }
        }

        public GridViewTest()
        {
            InitializeComponent();

            var al = new AbsoluteLayout();

            var files = new List<File>();

            //files.Add(new File("img_profile.png"));
            for (int i = 0; i < 100; i++)
            {
                files.Add(new File(ImageSource.FromFile(string.Format("1/mi{0}.png", i % 13)), "This is name"));
                //files.Add(string.Format("1/mi{0}.png", i % 13));
            };

            var dataTemplage = new DataTemplate(() =>
            {
                var image = new Image()
                {
                    //WidthRequest = 120,
                    //HeightRequest = 120,
                    //Source = ImageSource.FromFile(string.Format("1/mi{0}.png", 3))
                };
                image.SetBinding(Image.SourceProperty, "Source");
                //image.BindingContext = files;

                //var button = new Button();
                //button.Text = "This Button";
 
                //var layout = new StackLayout()
                //{
                //    Children = { image, button }
                //};

                return new ViewCell { View = image };
                //return new TextCell();
            });

            var grid = new GridView()
            {
                ItemWidth = 120,
                ItemHeight = 120,
                ItemHorizontalAlignment = 0.5,
                ItemVerticalAlignment = 0.5,
                ThemeStyle = "small",
                BackgroundColor = Color.Yellow
            };
            grid.ItemsSource = files;
            grid.ItemTemplate = dataTemplage;
            AbsoluteLayout.SetLayoutBounds(grid, new Rectangle(0, 0, 1920, 160));
            AbsoluteLayout.SetLayoutFlags(grid, AbsoluteLayoutFlags.None);

            al.Children.Add(grid);
            this.Content = al;
            this.Title = "GridView Test";
        }
    }
}