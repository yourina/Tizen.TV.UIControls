using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class GridViewFocusedEventArgs : FocusEventArgs
    {
        public object Data { get; }

        internal GridViewFocusedEventArgs(object data, View targetView, bool isFocused) : base(targetView, isFocused)
        {
            Data = data;
        }
    }
}
