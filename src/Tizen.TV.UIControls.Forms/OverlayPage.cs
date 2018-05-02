﻿using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    public class OverlayPage : ContentPage, IOverlayOutput
    {
        public static readonly BindableProperty OverlayAreaProperty = BindableProperty.Create("OverlayArea", typeof(Rectangle), typeof(OverlayPage), default(Rectangle));
        public static readonly BindableProperty PlayerProperty = BindableProperty.Create("Player", typeof(MediaPlayer), typeof(OverlayPage), default(MediaPlayer), propertyChanged: (b, o, n) => ((OverlayPage)b).OnPlayerChanged());

        View _controller;

        public OverlayPage()
        {
        }


        public Rectangle OverlayArea
        {
            get { return (Rectangle)GetValue(OverlayAreaProperty); }
            set { SetValue(OverlayAreaProperty, value); }
        }

        public MediaPlayer Player
        {
            get { return (MediaPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        VisualElement IVideoOutput.MediaView => this;

        View IVideoOutput.Controller
        {
            get { return _controller; }
            set
            {
                if (_controller != null)
                {
                    InternalChildren.Remove(_controller);
                }
                    
                _controller = value;
                if (_controller != null)
                {
                    InternalChildren.Insert(0, _controller);
                    OnChildrenReordered();
                }
            }
        }

        public event EventHandler AreaUpdated;

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            (this as IVideoOutput).Controller?.Layout(new Rectangle(x, y, width, height));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(OverlayArea))
            {
                AreaUpdated?.Invoke(this, EventArgs.Empty);
            }

            if (propertyName == nameof(Content) || propertyName == nameof(ControlTemplate))
            {
                if (_controller != null)
                {
                    var controller = _controller;
                    (this as IOverlayOutput).Controller = null;
                    controller.Layout(new Rectangle(0, 0, -1, -1));
                    Device.BeginInvokeOnMainThread(() => (this as IOverlayOutput).Controller = controller);
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (Player != null)
            {
                SetInheritedBindingContext(Player, BindingContext);
            }
        }

        void OnPlayerChanged()
        {
            if (Player != null)
            {
                Player.VideoOutput = this;
                SetInheritedBindingContext(Player, BindingContext);
            }
        }

        public VideoOuputType OuputType => VideoOuputType.Overlay;
    }
}