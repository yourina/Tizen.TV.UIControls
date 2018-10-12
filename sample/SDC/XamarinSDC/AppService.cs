using System;
using System.Collections.Generic;
using System.Text;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Movies;
using System.Threading.Tasks;
using Tizen;

namespace XamarinSDC
{
    class AppService
    {
        public static Task<AppInfo> GetAppInfoAsync(int id)
        {
            AppInfo result = null;
            foreach (AppInfo movie in apps)
            {
                if (movie.Id == id)
                {
                    result = movie;
                    break;
                }
            }
            return Task.FromResult<AppInfo>(result);
        }

        public static Task<IList<ScreenCapture>> GetScreenCaptureListAsync(int id, string identifier)
        {
            IList<ScreenCapture> items = new List<ScreenCapture>();

            int cnt = 0;
            captureCount.TryGetValue(id, out cnt);
            Log.Debug("Demo", "Cnt :" + cnt);
            for (int i = 1; i <= cnt; i++)
            {
                ScreenCapture newItem = new ScreenCapture
                {
                    Title = "#" + i,
                    PosterPath = identifier + "_" + i + ".png",
                    Id = id,
                };
                items.Add(newItem);
            }
            return Task.FromResult<IList<ScreenCapture>>(items);
        }

        public static async Task<AppListModel> LoadMovieListAsync(string menu)
        {
            IList<AppInfo> items = null;

            if (menu == "Xamarin.Forms Samples")
            {
                items = await GetApps(1);
            }
            else if (menu == "SkiaSharp Sample")
            {
                items = await GetApps(2);
            }
            else if (menu == "3rd Party Library Samples")
            {
                items = await GetApps(3);
            }
            else if (menu == "TV.UIControls Samples")
            {
                items = await GetApps(4);
            }
            else
            {
                items = await GetApps(5);
            }

            return new AppListModel
            {
                Title = menu,
                Items = items
            };
        }

        public static Task<IList<AppInfo>> GetApps(int layer)
        {
            IList<AppInfo> items = new List<AppInfo>();

            foreach (AppInfo movie in apps)
            {
                if ((movie.Id / 10) == layer)
                {
                    Log.Debug("Demo", "Add " + movie.Title);
                    items.Add(movie);
                }
            }
            return Task.FromResult<IList<AppInfo>>(items);
        }

        private static IList<AppInfo> apps = new List<AppInfo>()
        {
            new AppInfo
            {
                Title = "FlexLayoutDemo",
                OriginalTitle = "FlexLayout Demo",
                PosterPath = "FlexLayoutDemo_0.png",
                Id = 11,
                Overview ="This program demonstrates the FlexLayout introduced in Xamarin.Forms 3.0, and allows experimentating with it. by Charles Petzold"
            },
            new AppInfo
            {
                Title = "VSMDemo",
                OriginalTitle = "Visual State Manager Demo",
                PosterPath = "VsmDemos_0.png",
                Id = 12,
                Overview = "This program demonstrates features of the Visual State Manager (VSM) introduced in Xamarin.Forms 3.0. by Charles Petzold"
            },
            new AppInfo
            {
                Title = "MapPage",
                OriginalTitle = "Map Page",
                IconPath = "MapPage_0.png",
                PosterPath = "MapPage_0.png",
                Id = 16,
                Overview = "This sample relates to the Working with Maps in Xamarin.Forms doc. by Craig Dunn"
            },
            new AppInfo
            {
                Title = "ChildAnimation",
                OriginalTitle = "Child Animation",
                PosterPath = "AnimationDemo_4.png",
                Id = 14,
                Overview  ="The Animation class is the building block of all Xamarin.Forms animations, with the extension methods in the ViewExtensions class creating one or more Animation objects. This sample demonstrates how to use the Animation class to create and cancel animations, synchronize multiple animations, and create custom animations that animate properties that aren't animated by the existing animation methods. by Charles Petzold"
            },
            new AppInfo
            {
                Title = "BoxViewClock",
                OriginalTitle = "BoxView Clock",
                PosterPath = "BoxViewClock_0.png",
                Id = 13,
                Overview ="A classic analog clock realized entirely with BoxView. Although Xamarin.Forms doesn't have a vector graphics programming interface, it does have a BoxView. Although normally used for displaying rectangular blocks of color, BoxView can be sized, positioned, and rotated. This is enough to render a classic analog clock. by Charles Petzold"
            },
            new AppInfo
            {
                Title = "RelativeScaleAnimation",
                OriginalTitle = "Relative Scale Animation",
                PosterPath = "RelativeScaleAnimation_0.png",
                Id = 15,
                Overview = "The ViewExtensions class provides extension methods that can be used to construct simple animations. This sample demonstrates creating and canceling animations using the ViewExtensions class. by David Britch"
            },
            new AppInfo
            {
                Title = "UnevenRowsPage",
                OriginalTitle = "Working With ListView",
                PosterPath = "WorkingWithListView_0.png",
                Id = 17,
                Overview ="These samples relate to the Working with ListView in Xamarin.Forms doc.Many people have questions about specific ListView features, this sample attempts to cover some of them: De-selecting the row after tapping, Uneven row heights, Adding clickable buttons to cells that work on Android by Craig Dunn"
            },
            // TitleView 예재 추가 필요
            new AppInfo
            {
                Title = "BitmapRotatorPage",
                OriginalTitle = "Working With ListView",
                PosterPath = "WorkingWithListView_0.png",
                Id = 25,
            },
            new AppInfo
            {
                Title = "SkiaSharp Form Demo",
                IconPath = "SkiaSharpFormDemo_2.png",
                PosterPath = "SkiaSharpFormDemo_0.png",
                Identifier = "SkiaSharpFormDemo",
                Id = 25,
                AppId = "org.tizen.example.WorkingWithListView.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "SpinPaint Demo",
                IconPath = "SpinPaintDemo_1.png",
                PosterPath = "SpinPaintDemo_1.png",
                Identifier = "SpinPaintDemo",
                Id = 22,
                AppId = "org.tizen.example.SpinPaint.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "Xamarin Essential Sample",
                IconPath = "Essential_1.png",
                PosterPath = "Essential_1.png",
                Identifier = "Essential",
                Id = 20,
                AppId = "com.xamarin.essentials",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "SkiaSharp Gallery",
                IconPath = "SkiaSharp_1.png",
                PosterPath = "SkiaSharp_0.png",
                Identifier = "SkiaSharp",
                Id = 30,
                AppId = "org.tizen.example.TizenOS",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "MicroChart",
                IconPath = "MicroChart_2.png",
                PosterPath = "MicroChart_2.png",
                Identifier = "MicroChart",
                Id = 31,
                AppId = "org.tizen.example.Microchart.Samples.Forms.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "FFImageLoading Sample",
                IconPath = "FFImageLoading_1.png",
                PosterPath = "FFImageLoading_0.png",
                Identifier = "FFImageLoading",
                Id = 32,
                AppId = "simple.TizenForms.Sample",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },

            new AppInfo
            {
                Title = "Xamanimation",
                IconPath = "Xamarin_1.png",
                PosterPath = "Xamarin_1.png",
                Identifier = "Xamarin",
                Id = 33,
                AppId = "org.tizen.example.Xamanimation.Sample.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "CarouselView",
                IconPath = "CarouselView_0.png",
                PosterPath = "CarouselView_0.png",
                Identifier = "CarouselView",
                Id = 34,
                AppId = "CarouselView.Demo.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "TV.UIControls",
                IconPath = "TVUI_round_1.png",
                PosterPath = "TVUI_round_1.png",
                Identifier = "TVUI_round",
                Id = 40,
                AppId = "org.tizen.example.Sample.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "TMDb",
                IconPath = "TMDb_0.png",
                PosterPath = "TMDb_1.png",
                Identifier = "TMDb",
                Id = 50,
                AppId = "org.tizen.sample.TMDb",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "SmartHotel 360",
                IconPath = "SmartHotel_0.png",
                PosterPath = "SmartHotel_0.png",
                Identifier = "SmartHotel",
                Id = 51,
                AppId = "org.tizen.example.SmartHotel.Clients.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            }
        };

        private static Dictionary<int, int> captureCount = new Dictionary<int, int>()
        {
            { 10, 2 }, { 11, 1 }, { 12, 3 }, { 13, 3 }, { 14, 2 },
            { 15, 2 }, { 16, 3 }, { 17, 1 }, { 18, 3 }, { 19, 1 },
            { 20, 1 },
            { 30, 5 }, { 31, 2 }, { 32, 3 }, { 33, 1 }, { 34, 3 },
            { 40, 1 },
            { 50, 4 }, { 51, 8 },
        };
    }
}
