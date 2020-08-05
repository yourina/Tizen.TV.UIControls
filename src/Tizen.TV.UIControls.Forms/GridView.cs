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
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Tizen;

namespace Tizen.TV.UIControls.Forms
{
    public class GridView : View
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(GridView), null, propertyChanged: (b, o, n) => ((GridView)b).UpdateItemsSource());

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsView<GridView>), null, validateValue: (b, v) => ((GridView)b).ValidateItemTemplate((DataTemplate)v));

        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create("ItemHeight", typeof(int), typeof(GridView), -1);

        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create("ItemWidth", typeof(int), typeof(GridView), -1);

        public static readonly BindableProperty ItemHorizontalAlignmentProperty = BindableProperty.Create("ItemHorizontalAlignment", typeof(double), typeof(GridView), 0.0);

        public static readonly BindableProperty ItemVeticalAlignmentProperty = BindableProperty.Create("ItemVeticalAlignment", typeof(double), typeof(GridView), 0.0);

        public static readonly BindableProperty ThemeStyleProperty = BindableProperty.Create("ThemeStyle", typeof(string), typeof(GridView), default(string));

        public static readonly BindableProperty ItemStyleProperty = BindableProperty.Create("ItemStyle", typeof(string), typeof(GridView), default(string));

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(GridView), null, propertyChanged: (b, o, n) => ((GridView)b).UpdateSelectedItems());

        public event EventHandler<GridViewSelectedItemChangedEventArgs> SelectedItemChanged;

        public event EventHandler<GridViewItemFocusedEventArgs> ItemFocused;

        public GridView()
        {
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public int ItemHeight
        {
            get { return (int)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public int ItemWidth
        {
            get { return (int)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public double ItemHorizontalAlignment
        {
            get { return (double)GetValue(ItemHorizontalAlignmentProperty); }
            set { SetValue(ItemHorizontalAlignmentProperty, value); }
        }

        public double ItemVerticalAlignment
        {
            get { return (double)GetValue(ItemVeticalAlignmentProperty); }
            set { SetValue(ItemVeticalAlignmentProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string ThemeStyle
        {
            get { return (string)GetValue(ThemeStyleProperty); }
            set { SetValue(ThemeStyleProperty, value); }
        }

        public string ItemStyle
        {
            get { return (string)GetValue(ItemStyleProperty); }
            set { SetValue(ItemStyleProperty, value); }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
        protected virtual bool ValidateItemTemplate(DataTemplate template)
        {
            return true;
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = newValue as Element;
            if (element == null)
                return;
            element.Parent = (Element)bindable;
        }


        void UpdateSelectedItems()
        {
            SelectedItemChanged?.Invoke(this, new GridViewSelectedItemChangedEventArgs(SelectedItem));
        }


        void UpdateItemsSource()
        {
            // ClearItemContext();
            Console.Error.WriteLine("XSF" ,"*************");

            if (ItemsSource != null)
            {
                //foreach (var item in ItemsSource)
                //{
                //    var itemContext = new ItemContext
                //    {
                //        Data = item,
                //        RealizedView = CreateContent(item),
                //    };

                //    _itemContexts.Add(itemContext);
                //}

                if (ItemsSource is INotifyCollectionChanged collection)
                {
                    //collection.CollectionChanged += OnCollectionChanged;
                }
            }
            //LayoutInvalidate();
        }

        View CreateContent(object data)
        {
            if (ItemTemplate != null)
            {

                var content = ItemTemplate.CreateContent();
                if (content is View view) return view;
                else if (content is ViewCell viewCell) return viewCell.View;
                //else if (content is ImageCell imageCell) return CreateContent(imageCell);
                //else if (content is TextCell textCell) return CreateContent(textCell);
            }
            return null;// CreateContent(new TextCell { Text = data.ToString() });
        }

        public void SendItemFocused(GridViewItemFocusedEventArgs args)
        {
            ItemFocused?.Invoke(this, args);
        }
       
    }
}
