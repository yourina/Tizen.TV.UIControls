using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class GridViewItemFocusedEventArgs : EventArgs
    {
        public object Data { get; }
        public View TargetView { get; }
        public bool IsFocused { get; }

        internal GridViewItemFocusedEventArgs(object data, View targetView, bool isFocused)
        {
            Data = data;
            TargetView = targetView;
            IsFocused = isFocused;
        }
    }
}
