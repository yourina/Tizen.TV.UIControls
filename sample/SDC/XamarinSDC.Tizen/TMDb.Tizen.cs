using System;
using XamarinSDC;

namespace XamarinSDC
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Tizen.TV.UIControls.Forms.Renderer.UIControls.MainWindowProvider = () => MainWindow;
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(app);
            Tizen.TV.UIControls.Forms.Renderer.UIControls.PreInit();
            Xamarin.FormsMaps.Init("HERE", "pE-W9LeqN7zB9RtnwgBN/tZuCgj-LtWQ4RWN56XrVpA");
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            Tizen.TV.UIControls.Forms.Renderer.UIControls.PostInit();
            app.Run(args);
        }
    }
}
