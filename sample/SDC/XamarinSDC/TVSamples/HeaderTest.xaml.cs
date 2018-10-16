using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSDC
{
    public class ColorListView : Tizen.TV.UIControls.Forms.RecycleItemsView
    {
        protected override void OnItemFocused(object data, View targetView, bool isFocused)
        {
            if (data == Header)
                return;
            base.OnItemFocused(data, targetView, isFocused);
        }
    }


	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderTest : ContentPage
	{
		public HeaderTest ()
		{
			InitializeComponent ();
            BindingContext = new HeaderTestsModel();
		}

    }
    public class HeaderTestsModel
    {
        public IList Items { get; } = ColorModel.MakeModel();
    }


    class ColorModel
    {
        public Color Color { get; set; }
        public string Text { get; set; }

        public static List<ColorModel> MakeModel(int count = 3000)
        {
            List<ColorModel> list = new List<ColorModel>();
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                Color color = Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                list.Add(new ColorModel
                {
                    Color = color,
                    Text = $"Color: {(int)(color.R * 255)}, {(int)(color.G * 255)}, {(int)(color.B * 255)}"
                });
            }
            return list;
        }
    }

}