using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    /// <summary>
    /// Arguments for the event that is raised when one item of GridView has received focus. 
    /// </summary>
    public class GridViewFocusedEventArgs : FocusEventArgs
    {
        /// <summary>
        /// The data for focused item.
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// Constructs a new GridViewFocusedEventArgs object.
        /// </summary>
        public GridViewFocusedEventArgs(object data, View targetView, bool isFocused) : base(targetView, isFocused)
        {
            Data = data;
        }
    }
}
