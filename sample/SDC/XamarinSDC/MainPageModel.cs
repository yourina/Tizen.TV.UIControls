using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinSDC
{
    public class AppListModel
    {
        public string Title { get; set; }
        public IList<AppInfo> Items { get; set; }
    }


    public class MovieListModel
    {
        public string Title { get; set; }
        public IList<ScreenCapture> Items { get; set; }
    }

    public class MenuItemModel
    {
        public ImageSource Icon { get; set; }
        public string Text { get; set; }
        public AppListModel Movies { get; set; }
    }

    public class MainPageModel
    {
        public IList<MenuItemModel> MenuItems { get; }

        public MainPageModel()
        {
            MenuItems = new List<MenuItemModel>() {
                new MenuItemModel {Text = "Xamarin.Forms Samples", Icon = "Xamarin_1.png" },
                new MenuItemModel {Text = "Xamarin Essentials Samples" , Icon = "Essential_1.png"},
                new MenuItemModel {Text = "3rd Party Library Samples" , Icon = "Xamarin_1.png"},
                new MenuItemModel {Text = "TV.UIControls Samples", Icon = "TVUI_round_1.png"},
                new MenuItemModel {Text = "Reference Apps", Icon = "TMDb_0.png"}
            };
        }

    }
}
