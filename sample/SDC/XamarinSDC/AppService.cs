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
                Title = "ColorMatrixColorFilter",
                OriginalTitle = "Color Matrix Color Filter",
                PosterPath = "ColorMatrixColorFilter.png",
                Id = 21,
                Overview = "Color filters for use with the SkiaSharp.SKPaint.ColorFilter property of a SkiaSharp.SKPaint."
            },
            new AppInfo
            {
                Title = "LumaColorFilter",
                OriginalTitle = "Luma Color Filter",
                PosterPath = "LumaColorFilter.png",
                Id = 22,
                Overview = "Color filters for use with the SkiaSharp.SKPaint.ColorFilter property of a SkiaSharp.SKPaint."
            },
            new AppInfo
            {
                Title = "FilledHeptagram",
                OriginalTitle = "Filled Heptagram",
                PosterPath = "FilledHeptagram.png",
                Id = 23,
                Overview = "This example shows filled heptagram using SkiaSharp."
            },
            new AppInfo
            {
                Title = "BitmapAnnotation",
                OriginalTitle = "Bitmap Annotation",
                PosterPath = "BitmapAnnotation.png",
                Overview = "This example shows how to annotate bitmap image using SkiaSharp.",
                Id = 24,
            },
            new AppInfo
            {
                Title = "BitmapLattice",
                OriginalTitle = "Bitmap Lattice (9-patch)",
                PosterPath = "BitmapLattice.png",
                Id = 25,
                Overview = "DrawBitmapNinePatch is method to draws the bitmap, stretched or shrunk differentially to fit into the destination rectangle",
            },
            new AppInfo
            {
                Title = "Xfermode",
                OriginalTitle = "Blend Mode",
                PosterPath = "Xfermode.png",
                Id = 28,
                Overview = "CreateLinearGradient method creates a shader that generates a linear gradient between the two specified points"
            },
            new AppInfo
            {
                Title = "PathEffects",
                OriginalTitle = "Path Effect",
                PosterPath = "PathEffects.png",
                Id = 29,
                Overview = "This example shows the path effect to use when painting."
            },
            //new AppInfo
            //{
            //    Title = "ThreeDRotation",
            //    OriginalTitle = "3D Rotation",
            //    PosterPath = "ThreeDRotation.png",
            //    Id = 26,
            //},
            new AppInfo
            {
                Title = "SimpleText",
                OriginalTitle = "Simple Text",
                PosterPath = "SimpleText.png",
                Id = 27,
                Overview = "This example shows how to draw simple text using SkiaSharp."
            },
            new AppInfo
            {
                Title = "MicroChart",
                PosterPath = "MicroChart_2.png",
                OriginalTitle = "MicroChart",
                Id = 31,
                AppId = "org.tizen.example.Microchart.Samples.Forms.Tizen",
                Overview ="Microcharts is an extremely simple charting library for a wide range of platforms (see Compatibility section below), with shared code and rendering for all of them."
            },
            new AppInfo
            {
                Title = "FFImageLoading Sample",
                PosterPath = "FFImageLoading_0.png",
                OriginalTitle = "FFImageLoading",
                Id = 32,
                AppId = "simple.TizenForms.Sample",
                Overview = "Library to load images quickly & easily on Xamarin.iOS, Xamarin.Android, Xamarin.Forms, Xamarin.Mac / Xamarin.Tizen and Windows (UWP, WinRT). Authors: Daniel Luberda, Fabien Molinet. If you would like to help maintaining the project, just let us know!"
            },

            new AppInfo
            {
                Title = "Xamanimation",
                PosterPath = "Xamarin_1.png",
                OriginalTitle = "Xamanimation",
                Id = 33,
                AppId = "org.tizen.example.Xamanimation.Sample.Tizen",
                Overview = "Xamanimation is a portable library designed for Xamarin.Forms that aims to facilitate the use of animations to developers. Very simple use from C# and XAML code."
            },
            new AppInfo
            {
                Title = "CarouselView",
                PosterPath = "CarouselView_0.png",
                OriginalTitle = "CarouselView",
                Id = 34,
                AppId = "CarouselView.Demo.Tizen",
                Overview = "CarouselView control for Xamarin Forms. Available on NuGet: https://www.nuget.org/packages/CarouselView.FormsPlugin/ NuGet. Install in your PCL/.Net Standard 2.0 and client projects.",
            },
            new AppInfo
            {
                Title = "TestEmbeddingControlOnPage",
                OriginalTitle = "Embedding Control On Page",
                PosterPath = "TestEmbeddingControlOnPage.png",
                Overview = "MediaPlayer provieds the essential components to play the media contents.",
                Id = 40,
            },
            new AppInfo
            {
                Title = "SimplePlayerPage",
                OriginalTitle = "Simple Player",
                PosterPath = "SimplePlayerPage.png",
                Overview = "MediaPlayer provieds the essential components to play the media contents.",
                Id = 41,
            },
            new AppInfo
            {
                Title = "CustomFocus",
                OriginalTitle ="RecycleItemView Custome Focus Test",
                PosterPath = "CustomFocus.png",
                Overview = "RecycleItemView is a ScrollView that efficiently displays a collections of data using DataTemplate.",
                Id = 43,
            },
            new AppInfo
            {
                Title = "HeaderTest",
                OriginalTitle ="RecycleItemView Header Test",
                PosterPath = "HeaderTest.png",
                Overview = "RecycleItemView is a ScrollView that efficiently displays a collections of data using DataTemplate.",
                Id = 44,
            },
            new AppInfo
            {
                Title = "DrawerBasicTest",
                OriginalTitle ="DrawerLayout Basic Test",
                PosterPath = "DrawerBasicTest.png",
                Overview = "DrawerLayout is a kind of Layout that acts like a MasterDetailPage.",
                Id = 45,
            },
            new AppInfo
            {
                Title = "RightDrawerTest",
                OriginalTitle ="Right DrawerLayout Test",
                PosterPath = "RightDrawerTest.png",
                Overview = "DrawerLayout is a kind of Layout that acts like a MasterDetailPage.",
                Id = 46,
            },
            new AppInfo
            {
                Title = "TMDb",
                IconPath = "TMDb_0.png",
                PosterPath = "TMDb_1.png",
                OriginalTitle = "TMDb",
                Id = 50,
                AppId = "org.tizen.sample.TMDb",
                Overview = "The Movie Database (TMDb) is a community built movie and TV database. Every piece of data has been added by our amazing community dating back to 2008. TMDb's strong international focus and breadth of data is largely unmatched and something we're incredibly proud of."
            },
            new AppInfo
            {
                Title = "SmartHotel 360",
                PosterPath = "SmartHotel_0.png",
                OriginalTitle = "SmartHotel 360",
                Id = 51,
                AppId = "org.tizen.example.SmartHotel.Clients.Tizen",
                Overview = "During our Connect(); 2017 event this year we presented beautiful app demos using Xamarin. We are happy to announce the release of SmartHotel360. This release intends to share a simplified version of SmartHotel360 reference sample apps used at Connect(); 2017 Keynotes.If you missed it, you can watch Scott Guthrie’s Keynote."
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
