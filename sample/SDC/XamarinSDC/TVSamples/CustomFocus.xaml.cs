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
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen;

namespace XamarinSDC
{
    public class MyRecycleItemsView : Tizen.TV.UIControls.Forms.RecycleItemsView
    {
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            AbsoluteLayout layout = (AbsoluteLayout)targetView;
            View textarea = layout.Children[1];
            if (isFocused)
            {
                targetView.ScaleTo(1.2);
                var animation = new Animation((rate) =>
                {
                    AbsoluteLayout.SetLayoutBounds(textarea, new Rectangle(0, 1, 480, 100 + rate * 100));
                });
                animation.Commit(this, $"Focused - {data.GetHashCode()}");
            }
            else
            {
                targetView.ScaleTo(1.0);
                var animation = new Animation((rate) =>
                {
                    AbsoluteLayout.SetLayoutBounds(textarea, new Rectangle(0, 1, 480, 200 - rate * 100));
                });
                animation.Commit(this, $"Focused - {data.GetHashCode()}");
            }
        }

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomFocus : ContentPage
    {
        public CustomFocus()
        {
            InitializeComponent();
            BindingContext = new CustomFocusModel();
        }

        void OnSelected(object sender, EventArgs args)
        {
            var text = ((sender as Tizen.TV.UIControls.Forms.RecycleItemsView).SelectedItem as PosterModel).Text;
            System.Console.WriteLine("{0} is selected", text);
            this.DisplayAlert("Selected", text, "OK");
        }
    }

    public class CustomFocusModel
    {
        public IList Items { get; } = PosterModel.MakeModel();
    }

    public class PosterModel
    {
        public ImageSource Source { get; set; }
        public string Text { get; set; }
        public string DetailText { get; set; }

        public static List<PosterModel> MakeModel(bool shortdetail = false)
        {
            Log.Debug("Demo","Enter");
            List<string> posters = new List<string>
            {
                "01 Jaws.jpg",
                "02 Raiders of the Lost Ark.jpg",
                "03 star wars.jpg",
                "04 E.T. 2.jpeg",
                "05 Vertigo.jpg",
                "06 Alien.jpg",
                "07 Jurassic Park.jpg",
                "08 The Silence of the Lambs.jpg",
                "09 American Beauty.jpg",
                "10 Back to the Future.jpg",
                "11 Chinatown.jpg",
                "12 The Godfather.jpg",
                "13 Airplane.jpg",
                "14 Pulp Fiction.jpg",
                "15 Ghostbusters.jpg",
                "16 The Usual Suspects.jpg",
                "17 the people vs larry flynt.jpg",
                "18 Trainspotting.jpg",
                "19 Goodfellas.jpg",
                "20 the truman show.jpg",
                "21 Blade Runner.jpg",
                "22 Full Metal Jacket.jpg",
                "23 Attack of the 50ft Woman.jpg",
                "24 Batman.jpg",
                "25 nymph.jpg",
                "26 the driver.jpeg",
                "27 The Phantom Menace.jpg",
                "28 Platoon.jpg",
                "29 Gone with the Wind.jpg",
                "30 Forbidden Planet.jpg",
                "31 The Exorcist.jpg",
                "32 Anatomy of a Murder.jpg",
                "33 Metropolis.jpg",
                "34 Clockwork Orange.jpg",
                "35 Halloween.jpg",
                "36 Apocalypse Now.jpg",
                "37 Rocketeer.jpg",
                "38 Rosemary's Baby.jpg",
                "39 Moon.jpg",
                "40 Scream.jpg",
                "41 Breakfast at Tiffany's.jpg",
                "42 The Social Network.jpg",
                "43 The Thing.jpg",
                "44 Fear and Loathing in Las Vegas.jpg",
                "45 Love In The Afternoon.jpeg",
                "46 Manhattan.jpeg",
                "47 Lord of War.jpg",
                "48 Mean Streets.jpg",
                "49 The Graduate.jpg",
                "50 little miss sunshine.jpg"
            };
            List<PosterModel> items = new List<PosterModel>();
            foreach (var i in posters)
            {
                var texts = i.Split('.');
                texts[texts.Length - 1] = "";
                items.Add(new PosterModel
                {
                    Source = ImageSource.FromFile("poster/" + i),
                    Text = string.Join(" ", texts),
                    DetailText = shortdetail ? "A great example" : "A great example of colour scheme that extends from a film to its marketing. Yellow emanates from this heartwarming Sundance hit, seen on Paul Dano’s t-shirt and the lovably rubbish VW campervan, here flooding the negative space of both trailer and poster.",
                });
            }

            return items;
        }
    }
}