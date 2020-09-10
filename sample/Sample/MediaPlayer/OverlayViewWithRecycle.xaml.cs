using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverlayViewWithRecycle : ContentPage
    {
        public OverlayViewWithRecycle()
        {
            InitializeComponent ();

            Xamarin.Forms.PlatformConfiguration.TizenSpecific.VisualElement.SetFocusAllowed(VideoView, false);
        }
    }
}