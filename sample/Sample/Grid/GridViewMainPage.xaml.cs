using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sample.RecycleItemsView;

namespace Sample.GridView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridViewMainPage : ContentPage
    {
        public GridViewMainPage()
        {
            InitializeComponent();
            BindingContext = new GridViewModel();
        }

        async void ItemSelected(object sender, ItemTappedEventArgs args)
        {
            TestModel model = (TestModel)args.Item;
            Page page = (Page)Activator.CreateInstance(model.PageType);
            page.BindingContext = model;
            await Navigation.PushAsync(page);
        }
    }
}