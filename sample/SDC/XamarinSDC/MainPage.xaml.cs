using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMDbLib.Objects.Search;
using System.Collections.Generic;
using Tizen.TV.UIControls.Forms;
using Tizen;

namespace XamarinSDC
{
    public class MenuItemsView : Tizen.TV.UIControls.Forms.RecycleItemsView
    {
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            StackLayout layout = (StackLayout)targetView;
            Label label = (Label)layout.Children[1];

            if (isFocused)
            {
                //layout.BackgroundColor = Color.FromRgba(220, 220, 245, 230);
                layout.BackgroundColor = Color.FromRgba(245, 245, 255, 200);
                label.TextColor = Color.FromRgb(5, 5, 5);
            }
            else
            {
                label.TextColor = Color.FromHex("f0f0f0");
                layout.BackgroundColor = Color.Transparent;
            }
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        MainPageModel _model;
        public MainPage ()
        {
            InitializeComponent ();
            _model = new MainPageModel();
            BindingContext = _model;

            Device.BeginInvokeOnMainThread(() =>
            {
                MenuList.Focus();
                MenuItemsView_ItemSelected(this, new SelectedItemChangedEventArgs(_model.MenuItems[0]));
            });

            InputEvents.GetEventHandlers(MenuList).Add(new RemoteKeyHandler((evt) =>
            {
                if (evt.KeyName == RemoteControlKeyNames.Right)
                {
                    evt.Handled = true;
                    PosterView.Focus();
                }
            }, RemoteControlKeyTypes.KeyDown));
        }

        protected override bool OnBackButtonPressed()
        {
            if (!Drawer.IsOpen)
            {
                MenuList.Focus();
                return true;
            }

            DisplayAlert("Exit", "Do you want to exit?", "Yes", "No").ContinueWith(task =>
            {
                if (task.Result)
                {
                    ElmSharp.EcoreMainloop.Quit();
                }
            });
            return true;
        }

        async void MenuItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuItemModel itemModel = e.SelectedItem as MenuItemModel;
            if (itemModel.Movies == null)
            {
                ContentHolder.Content = new WaitingView
                {
                    Opacity = 0.8
                };
                itemModel.Movies = await AppService.LoadMovieListAsync(itemModel.Text);
            }
            ContentHolder.Content = PosterView;
            PosterView.BindingContext = itemModel.Movies;
            BackdropImage.SetBinding(FFImageLoading.Forms.CachedImage.SourceProperty, new Binding("Backdrops", source: PosterView)); 
            PosterView.Backdrops = itemModel.Movies.Items[0].BackdropPath;
        }
    }
}