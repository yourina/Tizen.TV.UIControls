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

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.TV.UIControls.Forms;

namespace XamarinSDC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SimplePlayerPage : OverlayPage
    {
		public SimplePlayerPage ()
		{
			InitializeComponent ();
            Player.Source = MediaSource.FromFile("tvcm.mp4");
        }

        void OnClickPlay(object sender, ClickedEventArgs e)
        {
            if (Player.State == PlaybackState.Playing)
                Player.Pause();
            else
            {
                var unused = Player.Start();
            }
        }

        void OnClickStop(object sender, ClickedEventArgs e)
        {
            Player.Stop();
        }

        async void OnSeekChanged(object sender, ValueChangedEventArgs e)
        {
            await Player.Seek((int)(Player.Duration * e.NewValue));
        }
    }
}