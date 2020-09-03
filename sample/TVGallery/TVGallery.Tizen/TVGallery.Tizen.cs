using System;
using Xamarin.Forms;

namespace TVGallery
{
	class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
	{
		protected override void OnCreate()
		{
			base.OnCreate();

			LoadApplication(new App());
		}

		static void Main(string[] args)
		{
			var app = new Program();
            try
            {
                Tizen.TV.UIControls.Forms.UIControls.Init();
                Tizen.TV.UIControls.Forms.UIControls.MainWindowProvider = () => app.MainWindow;
                Forms.Init(app);
                app.Run(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Exception : {e.Message}");
            }

        }
    }
}
