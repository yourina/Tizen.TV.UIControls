﻿/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ElmSharp;
using System;
using System.Reflection;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EColor = ElmSharp.Color;

[assembly: ExportRenderer(typeof(OverlayMediaView), typeof(OverlayViewRenderer))]
namespace Tizen.TV.UIControls.Forms.Renderer
{
    public class OverlayViewRenderer : ViewRenderer<OverlayMediaView, LayoutCanvas>
    {
        EvasObject _overlayHolder;
        protected override void OnElementChanged(ElementChangedEventArgs<OverlayMediaView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new LayoutCanvas(Xamarin.Forms.Forms.NativeParent));
                Control.LayoutUpdated += (s, evt) => OnLayout();
                _overlayHolder = new ElmSharp.Rectangle(Xamarin.Forms.Forms.NativeParent)
                {
                    Color = EColor.Transparent
                };
                _overlayHolder.Show();
                Control.Children.Add(_overlayHolder);
                Control.AllowFocus(true);
                MakeTransparent();
            }
            base.OnElementChanged(e);
        }

        void OnLayout()
        {
            _overlayHolder.Geometry = Control.Geometry;
        }

        void MakeTransparent()
        {
            try
            {
                _overlayHolder.GetType()?.GetProperty("RenderOperation", BindingFlags.Public | BindingFlags.Instance)?.SetValue(_overlayHolder, 2);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error MakeTransparent : {e.Message}");
            }
        }
    }
}
