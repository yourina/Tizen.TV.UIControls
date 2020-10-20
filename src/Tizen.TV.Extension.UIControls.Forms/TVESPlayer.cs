/*
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
using Tizen.TV.UIControls.Forms;
using Tizen.TV.Multimedia;
using TM = Tizen.TV.Multimedia;

namespace Tizen.TV.Extension.UIControls.Forms
{
    /// <summary>
    /// TVESMediaPlayer provieds the essential components to play the element stream.
    /// </summary>
    public class TVESPlayer : MediaPlayer
    {
        ITVESPlayer _esImpl;

        public TVESPlayer() : base()
        {
            Tizen.Log.Error("XSF", "Enter");
            if (_impl == null) {
                _impl = CreateMediaPlayerImpl();
            }
            _esImpl = _impl as ITVESPlayer;
            _esImpl.ResourceConflicted += SendResourceConflicted;
            _esImpl.BufferStatusChanged += SendBufferStatusChanged;
            _esImpl.EOSEmitted += SendEOSEmitted;
            _esImpl.ErrorOccurred += SendErrorOccurred;
            _esImpl.StreamReady += SendStreamReady;
            _esImpl.SeekReady += SendSeekReady;

            Tizen.Log.Error("XSF", "Enter");
        }

        protected override IPlatformMediaPlayer CreateMediaPlayerImpl()
        {
            return DependencyService.Get<ITVESPlayer>(fetchTarget: DependencyFetchTarget.NewInstance) as IPlatformMediaPlayer;
        }

        public void Close()
        {
        }

        public void Resume()
        {
            Tizen.Log.Error("XSIMPF", "Enter");
            _esImpl.Resume();
        }

        public AudioStreamInfo AudioStreamInfo
        {
            set 
            {
                _esImpl.AudioStreamInfo = value;
            }
        }

        public VideoStreamInfo VideoStreamInfo
        {
            set
            {
                _esImpl.VideoStreamInfo = value;
            }
        }

        public TimeSpan PlayingTime
        {
            get { return _esImpl.PlayingTime; }
        }

        public SubmitStatus SubmitEosPacket(StreamType type)
        {
            return _esImpl.SubmitEosPacket(type);
        }

        public SubmitStatus SubmitPacket(ESPacket packet)
        {
            return _esImpl.SubmitPacket(packet);
        }

        public SubmitStatus SubmitPacket(ESHandlePacket packet)
        {
            return _esImpl.SubmitPacket(packet);
        }

        public event EventHandler EOSEmitted;

        public event EventHandler ResourceConflicted;

        public event EventHandler<ErrorEventArgs> ErrorOccurred;

        public event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        public event EventHandler<StreamEventArgs> StreamReady;

        public event EventHandler<SeekEventArgs> SeekReady;
 
        void SendSeekReady(object sender, SeekEventArgs e)
        {
            SeekReady?.Invoke(sender, e);
        }


        void SendStreamReady(object sender, StreamEventArgs e)
        {
            StreamReady?.Invoke(sender, e);
        }

        void SendErrorOccurred(object sender, ErrorEventArgs e)
        {
            ErrorOccurred?.Invoke(sender, e);
        }

        void SendEOSEmitted(object sender, EventArgs e)
        {
            EOSEmitted?.Invoke(sender, e);
        }

        void SendBufferStatusChanged(object sender, BufferStatusEventArgs e)
        {
            BufferStatusChanged?.Invoke(sender, e);
        }

        void SendResourceConflicted(object sender, EventArgs e)
        {
            ResourceConflicted?.Invoke(sender, e);
        }
    }
}
