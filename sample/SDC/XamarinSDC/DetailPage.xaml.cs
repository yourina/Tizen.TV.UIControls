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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace XamarinSDC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        AppInfo _movie;
        MovieListModel _similars;

        public DetailPage(int id)
        {
            InitializeComponent();

            WaitingView.Opacity = 1.0;

            Task.Run(async () =>
            {
                AppInfo movie = await AppService.GetAppInfoAsync(id);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    WaitingView.Opacity = 0.0;
                    _movie = movie;
                    BindingContext = movie;

                    var button = new Button
                    {
                        Text = "Show Demo",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    };
                    button.Clicked += async (s, e) =>
                    {
                        if ((movie.Id / 10 == 3) || (movie.Id / 10 == 5))
                        {
                            try
                            {
                                AppControl appControl = new AppControl();
                                appControl.ApplicationId = movie.AppId;
                                appControl.Operation = AppControlOperations.Default;
                                appControl.ExtraData.Add("key", "value");
                                AppControl.SendLaunchRequest(appControl);
                            }
                            catch (Exception ee)
                            {
                                Log.Error("Demo", "Launch App " + ee+" "+ee.Message);

                            }
                            Log.Error("Demo", "Launch App "+movie.AppId);
                        }
                        else
                        {
                            var StartClassType = GetType().GetTypeInfo();
                            Assembly asm = StartClassType.Assembly;

                            IEnumerable<Type> _tcs = from tc in asm.DefinedTypes
                                                     where tc.Name == movie.Title
                                                     select tc.AsType();

                            foreach (Type type in _tcs)
                            {

                                Page page = (Page)Activator.CreateInstance(type);
                                page.Title = movie.OriginalTitle;
                                Log.Error("Demo", "Push page ");
                                await Navigation.PushAsync(page);
                            }
                        }
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

            //InputEvents.GetEventHandlers(SimilarList).Add(new RemoteKeyHandler(async (evt) =>
            //{
            //    if (evt.KeyName == RemoteControlKeyNames.Up)
            //    {
            //        //await ScrollView.ScrollToAsync(CastList, ScrollToPosition.Center, true);
            //        //CastList.Focus();
            //    }
            //}, RemoteControlKeyTypes.KeyDown));
        }
    }
}