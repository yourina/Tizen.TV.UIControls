using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinSDC
{
    public class AppInfo
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string AppId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public string IconPath { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public ContentPage Page { get; set; }
    }

    public class ScreenCapture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
    }
}
