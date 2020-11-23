using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.GridView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataTemplateTest : ContentPage
    {
        public DataTemplateTest()
        {
            InitializeComponent();
        }

        void GridView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Tizen.TV.UIControls.Forms.GridView recycleView = sender as Tizen.TV.UIControls.Forms.GridView;
            if (recycleView.SelectedItem is PosterModel poster)
            {
                myLabel.Text = poster.Text + " is selected";
            }
        }
    }
}