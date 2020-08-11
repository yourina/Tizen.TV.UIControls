using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class GridViewItemFocusedEventArgs : FocusEventArgs
    {
        public object Data { get; }
        public View TargetView { get; }
        //public bool IsFocused { get; }

        internal GridViewItemFocusedEventArgs(object data, View targetView, bool isFocused) : base(targetView, isFocused)
        {
            Data = data;
            TargetView = targetView;
            //IsFocused = isFocused;
        }
    }
}
