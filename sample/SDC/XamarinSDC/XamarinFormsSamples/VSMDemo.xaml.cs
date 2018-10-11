using System;
using System.Windows.Input;
using Xamarin.Forms;
using Tizen;

namespace XamarinSDC
{
    public partial class VSMDemo : ContentPage
    {
	    public VSMDemo()
	    {
		    InitializeComponent ();

            SizeChanged += (sender, args) =>
            {
                string visualState = Width > Height ? "Landscape" : "Portrait";
                VisualStateManager.GoToState(mainStack, visualState);
                VisualStateManager.GoToState(menuScroll, visualState);
                VisualStateManager.GoToState(menuStack, visualState);

                foreach (View child in menuStack.Children)
                {
                    VisualStateManager.GoToState(child, visualState);
                }
            };

            SelectedCommand = new Command<string>((filename) =>
            {
                image.Source = ImageSource.FromFile(filename);
            });

            menuStack.BindingContext = this;
	    }

        public ICommand SelectedCommand { private set; get; }
    }
}