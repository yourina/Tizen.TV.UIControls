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
            Log.Debug("Demo", "Cnt :"+ cnt);
            for(int i = 1; i<= cnt; i++)
            {
                ScreenCapture newItem = new ScreenCapture
                {
                    Title = "#" + i,
                    PosterPath = identifier + "_"+i+".png",
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
            else if (menu == "Xamarin Essentials Samples")
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
                Title = "SkiaSharp Form Demo",
                IconPath = "SkiaSharpFormDemo_2.png",
                PosterPath = "SkiaSharpFormDemo_0.png",
                Identifier = "SkiaSharpFormDemo",
                Id = 16,
                AppId = "org.tizen.example.WorkingWithListView.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "SpinPaint Demo",
                IconPath = "SpinPaintDemo_1.png",
                PosterPath = "SpinPaintDemo_1.png",
                Identifier = "SpinPaintDemo",
                Id = 17,
                AppId = "org.tizen.example.SpinPaint.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "XAML Sample",
                IconPath = "XamlSample_1.png",
                PosterPath = "XamlSample_0.png",
                Identifier = "XamlSample",
                Id = 18,
                AppId = "org.tizen.example.XamlSamples.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "BoxView Clock",
                IconPath = "BoxViewClock_2.png",
                PosterPath = "BoxViewClock_0.png",
                Identifier = "BoxViewClock",
                Id = 10,
                AppId = "org.tizen.example.BoxViewClock.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "FlexLayout Demo",
                IconPath = "FlexLayoutDemo_1.png",
                PosterPath = "FlexLayoutDemo_0.png",
                Identifier = "FlexLayoutDemo",
                Id = 15,
                AppId = "org.tizen.example.FlexLayoutDemo.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "Basic Animation",
                IconPath = "BasicAnimation_1.png",
                PosterPath = "BasicAnimation_0.png",
                Identifier = "BasicAnimation",
                Id = 11,
                AppId = "org.tizen.example.BasicAnimation.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "ResponsiveLayout",
                IconPath = "ResponsiveLayout_1.png",
                PosterPath = "ResponsiveLayout_0.png",
                Identifier = "ResponsiveLayout",
                Id = 12,
                AppId = "org.tizen.example.ResponsiveLayout.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "Visual State Manager Demo",
                IconPath = "VsmDemos_1.png",
                PosterPath = "VsmDemos_0.png",
                Identifier = "VsmDemos",
                Id = 13,
                AppId = "org.tizen.example.VsmDemos.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "Animation Demo",
                IconPath = "AnimationDemo_1.png",
                PosterPath = "AnimationDemo_0.png",
                Identifier = "AnimationDemo",
                Id = 14,
                AppId = "org.tizen.example.AnimationDemo.Tizen",
                Overview = "Tizen TV UIControls The Tizen TV UIControls is a set of helpful extensions to the Xamarin Forms framework for the Samsung TV device.The binaries are available via NuGet(package name is Tizen.TV.UIControls) with the source available here."
            },
            new AppInfo
            {
                Title = "Working with ListView",
                IconPath = "WorkingWithListView_1.png",
                PosterPath = "WorkingWithListView_1.png",
                Identifier = "WorkingWithListView",
                Id = 19,
                AppId = "org.tizen.example.WorkingWithListView.Tizen",
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
