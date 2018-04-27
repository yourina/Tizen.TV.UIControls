﻿using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

using NPage = Xamarin.Forms.Platform.Tizen.Native.Page;

namespace Tizen.TV.UIControls.Forms.Impl
{
    class OverlayPageRenderer : PageRenderer
    {
        Box _overlaySurface;
        EvasObject _embeddingControls;

        NPage Control => NativeView as NPage;
        OverlayPage OverlayPage => Element as OverlayPage;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (_overlaySurface == null)
            {
                _overlaySurface = new Box(Xamarin.Forms.Platform.Tizen.Forms.NativeParent);
                Control.LayoutUpdated += OnLayoutUpdated;
                Control.Children.Add(_overlaySurface);
                NativeView.Color = ElmSharp.Color.FromRgba(0, 0, 0, 0);
            }
        }

        void OnLayoutUpdated(object sender, Xamarin.Forms.Platform.Tizen.Native.LayoutEventArgs e)
        {
            if (_overlaySurface != null)
            {
                if (OverlayPage.OverlayArea.IsEmpty)
                {
                    _overlaySurface.Geometry = NativeView.Geometry;
                }
                else
                {
                    _overlaySurface.Geometry = OverlayPage.OverlayArea.ToPixel();
                }
            }
            if (_embeddingControls != null)
            {
                if (OverlayPage.OverlayArea.IsEmpty)
                {
                    _embeddingControls.Geometry = NativeView.Geometry;
                }
                else
                {
                    _embeddingControls.Geometry = OverlayPage.OverlayArea.ToPixel();
                }
            }
        }

        public void SetEmbeddingControls(EvasObject layout)
        {
            if (_embeddingControls != null)
            {
                _overlaySurface.UnPackAll();
            }

            _embeddingControls = layout;

            if (layout != null)
            {
                _overlaySurface.PackEnd(layout);
            }
        }
    }
}
