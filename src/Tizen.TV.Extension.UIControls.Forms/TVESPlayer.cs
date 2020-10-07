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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.Multimedia;


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
            _esImpl.AudioReady += SendAudioReady;
            _esImpl.VideoReady += SendVideoReady;

            _esImpl.Open();
            Tizen.Log.Error("XSF", "Enter");
        }

        protected override IPlatformMediaPlayer CreateMediaPlayerImpl()
        {
            return DependencyService.Get<ITVESPlayer>(fetchTarget: DependencyFetchTarget.NewInstance) as IPlatformMediaPlayer;
        }

        //public void Open()
        //{
        //    //_esImpl.Open();
        //}

        //public Task Prepare(Action<StreamType> onReadyToPrepare)
        //{
        //    return _esImpl.Prepare(onReadyToPrepare);
        //}

        //public void SetDisplay(ElmSharp.Window window)
        //{
        //    //_esImpl.SetDisplay(window);
        //}

        public void Close()
        {
        }

        public void Resume()
        { 
        }

        public Multimedia.AudioStreamInfo AudioStreamInfo
        {
            set 
            {
                _esImpl.AudioStreamInfo = value;
            }
        }

        public Multimedia.VideoStreamInfo VideoStreamInfo
        {
            set
            {
                _esImpl.VideoStreamInfo = value;
            }
        }

        public ESPlayerState State
        {
            get { return _esImpl.State; }
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

        public event EventHandler<EOSEventArgs> EOSEmitted;

        public event EventHandler<Multimedia.ErrorEventArgs> ErrorOccurred;

        public event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        public event EventHandler<ResourceConflictEventArgs> ResourceConflicted;

        public event EventHandler AudioReady;

        public event EventHandler VideoReady;

        void SendVideoReady(object sender, EventArgs e)
        {
            VideoReady?.Invoke(sender, e);
        }

        void SendAudioReady(object sender, EventArgs e)
        {
            AudioReady?.Invoke(sender, e);
        }

        void SendErrorOccurred(object sender, Multimedia.ErrorEventArgs e)
        {
            ErrorOccurred?.Invoke(sender, e);
        }

        void SendEOSEmitted(object sender, EOSEventArgs e)
        {
            EOSEmitted?.Invoke(sender, e);
        }

        void SendBufferStatusChanged(object sender, BufferStatusEventArgs e)
        {
            BufferStatusChanged?.Invoke(sender, e);
        }

        void SendResourceConflicted(object sender, ResourceConflictEventArgs e)
        {
            ResourceConflicted?.Invoke(sender, e);
        }
    }
}
