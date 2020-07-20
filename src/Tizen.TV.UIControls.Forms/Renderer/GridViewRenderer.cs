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

using ElmSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForm = Xamarin.Forms;

[assembly: ExportRenderer(typeof(GridView), typeof(GridViewRenderer))]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    class GengridItemContext
    {
        public object Data { get; set; }
        public View RealizedView { get; set; }
    }

    public class GridViewRenderer : ViewRenderer<GridView, ElmSharp.GenGrid>
    {
        IList<GengridItemContext> itemContexts = new List<GengridItemContext>();

        ElmSharp.GenItemClass gridItemClass = new ElmSharp.GenItemClass("default")
        {
            GetContentHandler = (obj, part) =>
            {
                GengridItemContext context = (GengridItemContext)obj;

                if (part == "elm.swallow.icon")
                {
                    var renderer = Platform.GetOrCreateRenderer(context.RealizedView);

                    if (renderer is LayoutRenderer)
                    {
                        (renderer as LayoutRenderer).RegisterOnLayoutUpdated();
                    }
                    //renderer.NativeView.Show();
                    return renderer.NativeView;
                }
                return null;
            }
        };

        ElmSharp.GenItemClass defaultClass = new ElmSharp.GenItemClass("default")
        {
            GetContentHandler = (obj, part) =>
            {
                string index = (string)obj;
                Log.Error("XSF","Enter *** "+index );
                if (part == "elm.swallow.icon")
                {
                    ElmSharp.Image image = new ElmSharp.Image(Xamarin.Forms.Forms.NativeParent)
                    {
                        AlignmentX = -1,
                        AlignmentY = -1,
                        WeightX = 1,
                        WeightY = 1,
                    };
                    image.Show();

                    //string path = string.Format("mi{0}.png", index % 13);
                    image.Load(Path.Combine(@"/home/owner/apps_rw/org.FirstDemo.Tizen/res/", index));

                    return image;

                }
                return null;
            }
        };
        ElmSharp.GenItemClass posterClass = new ElmSharp.GenItemClass("poster")
        {
            GetTextHandler = (obj, part) =>
            {
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

                if (part == "elm.text.title")
                {
                    return _textDetail[0, 0];
                }
                else if (part == "elm.text")
                {
                    return _textDetail[0, 1];
                }
                return null;
            },
            GetContentHandler = (obj, part) =>
            {
                string index = (string)obj;
                //Log.Error("XSF","Enter *** "+index );
                if (part == "elm.swallow.icon")
                {
                    ElmSharp.Image image = new ElmSharp.Image(Xamarin.Forms.Forms.NativeParent)
                    {
                        AlignmentX = -1,
                        AlignmentY = -1,
                        WeightX = 1,
                        WeightY = 1,
                    };
                    image.Show();

                    //string path = string.Format("mi{0}.png", index % 13);
                    image.Load(Path.Combine(@"/home/owner/apps_rw/org.FirstDemo.Tizen/res/", index));

                    return image;

                }
                return null;
            }
        };


        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e)
        {
            if (Control == null)
            {
                var gengrid = new ElmSharp.GenGrid(Xamarin.Forms.Forms.NativeParent)
                {
                    HorizontalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible,
                    VerticalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible,
                    IsHorizontal = true,
                    ItemWidth = Element.ItemWidth,
                    ItemHeight = Element.ItemHeight,
                    ItemAlignmentX = Element.ItemHorizontalAlignment,
                    ItemAlignmentY = Element.ItemVerticalAlignment,
                    Style = Element.ThemeStyle,
                };

                gengrid.ItemSelected += OnItemSelected;


                foreach (var item in Element.ItemsSource)
                {
                    View realview = CreateContent(Element.ItemTemplate, item);
                    realview.BindingContext = item;
                    var context = new GengridItemContext
                    {
                        Data = item, 
                        RealizedView = realview as View,
                    };
                    itemContexts.Add(context);
                    var gridItem = gengrid.Append(gridItemClass, context);
                    gridItem.IsSelected = true;
                }

                //if (Element.ItemStyle == "poster")
                //{
                //    Log.Error("XSF", "" + gengrid.ItemAlignmentY);
                //    foreach (var item in Element.ItemsSource)
                //    {
                //        var gridItem = gengrid.Append(posterClass, item);
                //    }
                //}
                //else
                //{
                //    Log.Error("XSF", "" + Element._itemContexts.Count);
                //    //foreach (var item in Element.ItemsSource)
                //    //{
                //    //    var gridItem = gengrid.Append(defaultClass, item);
                //    //}
                //    foreach (var item in Element._itemContexts)
                //    {
                //        Log.Error("XSF", "&&& " + Element._itemContexts.Count);
                //        var gridItem = gengrid.Append(gridItemClass, item);
                //    }
                //}

                if (Element.ItemsSource is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged += OnCollectionChanged;
                }

                gengrid.Show();
                SetNativeControl(gengrid);
            }
            base.OnElementChanged(e);
        }

        View CreateContent(DataTemplate template, Object data)
        {
            var content = template.CreateContent();
            if (content is View view)
                return view;
            else if (content is ViewCell viewCell)
                return viewCell.View;
            else if (content is ImageCell imageCell) 
                return CreateContent(imageCell);
            else if (content is TextCell textCell) 
                return CreateContent(textCell);
            else
                return CreateContent(new TextCell { Text = data?.ToString() });
        }


        void OnItemSelected(object sender, GenGridItemEventArgs e)
        {
            GengridItemContext context = e.Item.Data as GengridItemContext;
            Log.Error("XSF", "12 " + e.Item.Data);
            Element.SelectedItem = context.Data;
            Log.Error("XSF", "12 " + Element.SelectedItem);
        }

        protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GridView.ItemWidthProperty.PropertyName)
            {
                Control.ItemWidth = Element.ItemWidth;
            }
            else if (e.PropertyName == GridView.ItemHeightProperty.PropertyName)
            {
                Control.ItemHeight = Element.ItemHeight;
            }
            else if (e.PropertyName == GridView.ThemeStyleProperty.PropertyName)
            {
                Control.Style = Element.ThemeStyle;
            }
            base.OnElementPropertyChanged(sender, e);
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        View CreateContent(ImageCell cell)
        {
            XForm.Label text = new XForm.Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontAttributes = FontAttributes.Bold,
            };
            XForm.Label detailLabel = new XForm.Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            detailLabel.FontSize = Device.GetNamedSize(NamedSize.Micro, detailLabel);

            text.SetBinding(XForm.Label.TextProperty, new Binding("Text", source: cell));
            text.SetBinding(XForm.Label.TextColorProperty, new Binding("TextColor", source: cell));

            detailLabel.SetBinding(XForm.Label.TextProperty, new Binding("Detail", source: cell));
            detailLabel.SetBinding(XForm.Label.TextColorProperty, new Binding("DetailColor", source: cell));

            XForm.Image image = new XForm.Image
            {
                HeightRequest = Element.ItemHeight,
                WidthRequest = Element.ItemWidth,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.Fill
            };
            image.SetBinding(XForm.Image.SourceProperty, new Binding("ImageSource", source: cell));

            var view = new AbsoluteLayout();
            view.Children.Add(image, new XForm.Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            view.Children.Add(new StackLayout
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new StackLayout {
                        VerticalOptions = LayoutOptions.EndAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 15,
                        Spacing = 0,
                        BackgroundColor = XForm.Color.FromHex("#2b7c87"),
                        Children = { text, detailLabel }
                    }
                }
            }, new XForm.Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            cell.SetBinding(GridView.BindingContextProperty, new Binding("BindingContext", source: view));
            return view;
        }

        View CreateContent(TextCell cell)
        {
            XForm.Label text = new XForm.Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                FontAttributes = FontAttributes.Bold,
            };
            XForm.Label detailLabel = new XForm.Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            detailLabel.FontSize = Device.GetNamedSize(NamedSize.Micro, detailLabel);

            text.SetBinding(XForm.Label.TextProperty, new Binding("Text", source: cell));
            text.SetBinding(XForm.Label.TextColorProperty, new Binding("TextColor", source: cell));

            detailLabel.SetBinding(XForm.Label.TextProperty, new Binding("Detail", source: cell));
            detailLabel.SetBinding(XForm.Label.TextColorProperty, new Binding("DetailColor", source: cell));

            var view = new AbsoluteLayout();
            view.Children.Add(new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new StackLayout {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 15,
                        Spacing = 0,
                        BackgroundColor = XForm.Color.FromHex("#2b7c87"),
                        Children = { text, detailLabel }
                    }
                }
            }, new XForm.Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

            cell.SetBinding(GridView.BindingContextProperty, new Binding("BindingContext", source: view));
            return view;
        }
    }
}
