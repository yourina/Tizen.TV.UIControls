using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace FirstDemo
{
	class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
	{
		protected override void OnCreate()
		{
			base.OnCreate();
            this.MainWindow.Alpha = true;
			LoadApplication(new App());
		}

		static void Main(string[] args)
		{
			var app = new Program();
            
			Forms.Init(app);
			app.Run(args);
		}
	}
}
