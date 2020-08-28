using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TVGallery
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent();
            BindingContext = new MainPageModel();
		}

        async void ItemSelected(object sender, ItemTappedEventArgs args)
        {
            Tizen.Log.Error("XSF","Enter");
            TestCategory model = (TestCategory)args.Item;
            Page page = (Page)Activator.CreateInstance(model.PageType);
            await Navigation.PushAsync(page);
        }
    }
}