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
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class GridView : View
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(GridView), null);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsView<GridView>), null, validateValue: (b, v) => ((GridView)b).ValidateItemTemplate((DataTemplate)v));

        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create("ItemHeight", typeof(int), typeof(GridView), -1);

        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create("ItemWidth", typeof(int), typeof(GridView), -1);

        public static readonly BindableProperty ItemHorizontalAlignmentProperty = BindableProperty.Create("ItemHorizontalAlignment", typeof(double), typeof(GridView), 0.0);

        public static readonly BindableProperty ItemVeticalAlignmentProperty = BindableProperty.Create("ItemVeticalAlignment", typeof(double), typeof(GridView), 0.0);

        public static readonly BindableProperty ThemeStyleProperty = BindableProperty.Create("ThemeStyle", typeof(string), typeof(GridView), default(string));

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(GridView), null, propertyChanged: (b, o, n) => ((GridView)b).UpdateSelectedItems());

        public static readonly BindableProperty IsHorizontalProperty = BindableProperty.Create("IsHorizontal", typeof(bool), typeof(GridView), false);

        public static readonly BindableProperty IsHorizontalScrollBarVisibleProperty = BindableProperty.Create("IsHorizontalScrollBarVisible", typeof(bool), typeof(GridView), false);

        public static readonly BindableProperty IsVerticalScrollBarVisibleProperty = BindableProperty.Create("IsVerticalScrollBarVisible", typeof(bool), typeof(GridView), false);

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public event EventHandler<GridViewFocusedEventArgs> ItemFocused;

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

        public bool IsHorizontal
        {
            get { return (bool)GetValue(IsHorizontalProperty); }
            set { SetValue(IsHorizontalProperty, value); }
        }

        public bool IsHorizontalScrollBarVisible
        {
            get { return (bool)GetValue(IsHorizontalScrollBarVisibleProperty); }
            set { SetValue(IsHorizontalScrollBarVisibleProperty, value); }
        }

        public bool IsVerticalScrollBarVisible
        {
            get { return (bool)GetValue(IsVerticalScrollBarVisibleProperty); }
            set { SetValue(IsVerticalScrollBarVisibleProperty, value); }
        }

        public void SendItemFocused(GridViewFocusedEventArgs args)
        {
            ItemFocused?.Invoke(this, args);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        protected bool ValidateItemTemplate(DataTemplate template)
        {
            if (template == null)
                return true;
            if (template is DataTemplateSelector)
                return false;

            object content = template.CreateContent();
            if (content is View || content is Cell)
                return true;

            return false;
        }

        void UpdateSelectedItems()
        {
            ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs(SelectedItem, -1));
        }

       
    }
}
