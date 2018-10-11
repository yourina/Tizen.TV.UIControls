using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.TV.UIControls.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Movies;
using Tizen.Applications;
using Tizen;

namespace XamarinSDC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPageWithCapture : ContentPage
    {
        AppInfo _movie;
        MovieListModel _similars;

        public DetailPageWithCapture(int id)
        {
            InitializeComponent();

            WaitingView.Opacity = 1.0;

            Task.Run(async () =>
            {
                AppInfo movie = await AppService.GetAppInfoAsync(id);
                var taskSimilar = await AppService.GetScreenCaptureListAsync(id, movie.Identifier);
                Log.Debug("Demo", id + " "+movie.Identifier);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    WaitingView.Opacity = 0.0;
                    _movie = movie;
                    BindingContext = movie;

                    _similars = new MovieListModel
                    {
                        Title = "Screen Captures",
                        Items = taskSimilar,
                    };
                    SimilarList.BindingContext = _similars;

                    var button = new Button
                    {
                        Text = "Launch Application",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    };
                    button.Clicked += (s, e) =>
                    {
                        AppControl appControl = new AppControl();
                        appControl.ApplicationId = movie.AppId;
                        appControl.Operation = AppControlOperations.Default;
                        AppControl.SendLaunchRequest(appControl);
                    };
                    ButtonArea.Children.Add(button);

                    InputEvents.GetEventHandlers(button).Add(
                        new RemoteKeyHandler((arg) =>
                        {
                            if (arg.KeyName == RemoteControlKeyNames.Up)
                            {
                                ScrollView.ScrollToAsync(0, 0, true);
                                arg.Handled = true;
                            }
                        }, RemoteControlKeyTypes.KeyDown
                    ));

                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //InputEvents.GetEventHandlers(CastList).Add(new RemoteKeyHandler(async (evt) =>
            //{
            //    if (evt.KeyName == RemoteControlKeyNames.Up && ButtonArea.Children.Count > 0)
            //    {
            //        var btn = ButtonArea.Children[0];
            //        await ScrollView.ScrollToAsync(btn, ScrollToPosition.Center, true);
            //        btn.Focus();
            //    }
            //    else if (evt.KeyName == RemoteControlKeyNames.Down)
            //    {
            //        await ScrollView.ScrollToAsync(SimilarList, ScrollToPosition.Center, true);
            //        SimilarList.Focus();
            //    }
            //}, RemoteControlKeyTypes.KeyDown));

            InputEvents.GetEventHandlers(SimilarList).Add(new RemoteKeyHandler(async (evt) =>
            {
                if (evt.KeyName == RemoteControlKeyNames.Up)
                {
                    //await ScrollView.ScrollToAsync(CastList, ScrollToPosition.Center, true);
                    //CastList.Focus();
                }
            }, RemoteControlKeyTypes.KeyDown));
        }
    }
}