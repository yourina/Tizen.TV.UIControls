﻿/*
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
        ElmSharp.GenGrid _genGrid = null;

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
                    return renderer.NativeView;
                }
                return null;
            }
        };

        protected override void OnElementChanged(ElementChangedEventArgs<GridView> e)
        {
            if (Control == null)
            {
                _genGrid = new ElmSharp.GenGrid(Xamarin.Forms.Forms.NativeParent)
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

                _genGrid.ItemSelected += OnItemSelected;
                _genGrid.ItemFocused += OnItemFocused;
                _genGrid.ItemUnfocused += OnItemUnfocused;
                
                if (Element.ItemsSource != null)
                {
                    UpdateItemsSource();
                }
                //_genGrid.Show();
                SetNativeControl(_genGrid);
            }
            base.OnElementChanged(e);
        }

        private void OnItemUnfocused(object sender, GenGridItemEventArgs e)
        {
            GengridItemContext context = e.Item.Data as GengridItemContext;
            Element.SendItemFocused(new GridViewItemFocusedEventArgs(context.Data, context.RealizedView, false));
        }

        private void OnItemFocused(object sender, GenGridItemEventArgs e)
        {
            GengridItemContext context = e.Item.Data as GengridItemContext;
            Element.SendItemFocused(new GridViewItemFocusedEventArgs(context.Data, context.RealizedView, true));
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
            Element.SelectedItem = context.Data;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
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
            else if (e.PropertyName == GridView.ItemsSourceProperty.PropertyName)
            {
                UpdateItemsSource();
            }
            base.OnElementPropertyChanged(sender, e);
        }

        void UpdateItemsSource()
        {
            _genGrid.Clear();
            itemContexts.Clear();
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
                var gridItem = _genGrid.Append(gridItemClass, context);
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItemsSource();
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
