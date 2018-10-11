using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;
using Tizen;

namespace XamarinSDC
{
    public class PosterUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Log.Debug("Demo","Enter "+value);
            return ImageSource.FromUri(new Uri($"https://image.tmdb.org/t/p/w500/{value}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PosterListView : ContentView
    {
        string _backdrops = null;
        double _itemWidth = 0;
        double _itemHeight = 0;
        public PosterListView ()
        {
            InitializeComponent();
            ItemsView.SetBinding(RecycleItemsView.ItemHeightProperty, new Binding("ItemHeight", source: this));
            ItemsView.SetBinding(RecycleItemsView.ItemWidthProperty, new Binding("ItemWidth", source: this));
        }

        public double ItemWidth
        {
            get { return _itemWidth; }
            set
            {
                _itemWidth = value;
                OnPropertyChanged();
            }
        }
        public double ItemHeight
        {
            get { return _itemHeight; }
            set
            {
                _itemHeight = value;
                OnPropertyChanged();
            }
        }

        public string Backdrops
        {
            get { return _backdrops; }
            set
            {
                _backdrops = value;
                OnPropertyChanged();
            }
        }

        public Tizen.TV.UIControls.Forms.RecycleItemsView ItemContent => ItemsView;

        async void RecycleItemsView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var movie = e.SelectedItem as AppInfo;
            Backdrops = movie.BackdropPath;

            Log.Debug("Demo","Enter" + movie.OriginalTitle);
            await Navigation.PushAsync(new DetailPage(movie.Id));
            if (Navigation.NavigationStack[Navigation.NavigationStack.Count - 2] is DetailPage page)
            {
                Navigation.RemovePage(page);
            }
        }
    }
}