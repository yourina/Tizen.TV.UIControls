﻿using Xamarin.Forms;

namespace Tizen.TV.UIControls.Forms
{
    [ContentProperty("Player")]
    public class MediaView : Layout<View>, IVideoOutput
    {
        public static readonly BindableProperty PlayerProperty = BindableProperty.Create("Player", typeof(MediaPlayer), typeof(MediaView), default(MediaPlayer), propertyChanged: (b, o, n) => ((MediaView)b).OnPlayerChanged());
        
        View _controller;

        public MediaPlayer Player
        {
            get { return (MediaPlayer)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public virtual VideoOuputType OuputType => VideoOuputType.Buffer;

        VisualElement IVideoOutput.MediaView => this;
        View IVideoOutput.Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                if (_controller != null)
                {
                    Children.Remove(_controller);
                }

                _controller = value;

                if (_controller != null)
                {
                    Children.Add(_controller);
                }
            }
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            _controller?.Layout(new Rectangle(x, y, width, height));
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            return _controller?.Measure(widthConstraint, heightConstraint) ?? base.OnMeasure(widthConstraint, heightConstraint);
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
    }
}
