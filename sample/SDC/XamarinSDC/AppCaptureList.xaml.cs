using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;
using Tizen;

namespace XamarinSDC
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppCaptureListView : ContentView
    {
        string _backdrops = null;
        double _itemWidth = 0;
        double _itemHeight = 0;
        public AppCaptureListView()
        {
            Log.Debug("Demo","App");
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
            Log.Debug("Demo", "Enter");
        }
    }
}