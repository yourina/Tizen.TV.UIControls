using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.TV.UIControls.Forms
{
    public class GridViewSelectedItemChangedEventArgs : EventArgs
    {
        public object SelectedItem { get; }

        internal GridViewSelectedItemChangedEventArgs(object item)
        {
            SelectedItem = item;
        }
    }
}
