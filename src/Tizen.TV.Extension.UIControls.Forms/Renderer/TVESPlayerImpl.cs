using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using TVForms = Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.UIControls.Forms.Renderer;
using Tizen.TV.Extension.UIControls.Forms.Renderer;
using Tizen.TV.Multimedia;
using TM = Tizen.TV.Multimedia;

[assembly: Xamarin.Forms.Dependency(typeof(TVESPlayerImpl))]
namespace Tizen.TV.Extension.UIControls.Forms.Renderer
{
    class TVESPlayerImpl : ITVESPlayer
    {
        protected ESPlayer esPlayer;
        bool _cancelToStart;
        IVideoOutput _videoOutput;
        VideoStreamInfo _videoStreamInfo;
        AudioStreamInfo _audioStreamInfo;
        TaskCompletionSource<bool> _tcsSubmit;
        bool _isMuted = false;
        bool _isStarted = false;
        DisplayAspectMode _aspectMode = DisplayAspectMode.AspectFit;

        public TVESPlayerImpl()
        {
            esPlayer = new ESPlayer();
            esPlayer.EOSEmitted += OnEOSEmitted;
            esPlayer.ErrorOccurred += OnErrorOccurred;
            esPlayer.BufferStatusChanged += OnBufferStatusChanged;
            esPlayer.ResourceConflicted += OnResourceConflicted;
            esPlayer.Open();

            //The Tizen TV emulator is based on the x86 architecture. Using trust zone (DRM'ed content playback) is not supported by the emulator.
            if (RuntimeInformation.ProcessArchitecture != Architecture.X86)
            {
                esPlayer.SetTrustZoneUse(true);
            }
            Tizen.Log.Error("XSFIMP", $"Enter {esPlayer} : 1008 + 0858 PM + SeekReady + PrepareReady");
        }

        public async Task<bool> Start()
        {
            if (_tcsSubmit == null || _tcsSubmit.Task.IsCompleted)
            {
                _tcsSubmit = new TaskCompletionSource<bool>();
            }

            Tizen.Log.Error("XSFIMP", "Enter");
            await esPlayer.PrepareAsync(OnReadyToPrepare);

            if (esPlayer.GetState() == ESPlayerState.Ready)
            {
                await _tcsSubmit.Task;
                esPlayer.Start();
                _isStarted = true;
                Tizen.Log.Error("XSFIMP", "Enter");
                //ApplyDisplay();
            }
            else
            {
                return false;
            }
            PlaybackStarted?.Invoke(this, EventArgs.Empty);
            return true;
        }
        public void Pause()
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            Log.Debug(UIControls.Tag, "Pause");
            try
            {
                esPlayer.Pause();
                PlaybackPaused.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error on Pause : {e.Message}");
            }
        }

        public void Resume()
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            Log.Debug(UIControls.Tag, "Pause");
            try
            {
                esPlayer.Resume();
                PlaybackPaused.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Error(UIControls.Tag, $"Error on Resume : {e.Message}");
            }
        }
        public void Stop()
        {
            Tizen.Log.Error("XSF", "Etner");

            _cancelToStart = true;
            var unusedTask = ChangeToIdleState();
            PlaybackStopped.Invoke(this, EventArgs.Empty);
        }

        public SubmitStatus SubmitPacket(ESPacket packet)
        {
            _tcsSubmit?.TrySetResult(true);
            Tizen.Log.Error("XSF", $"Enter : {packet.type}");
            return esPlayer.SubmitPacket(packet);
        }

        public SubmitStatus SubmitEosPacket(Tizen.TV.Multimedia.StreamType type)
        {
            Tizen.Log.Error("XSFIMP", $"Enter {Position}");
            var status = esPlayer.SubmitEosPacket(type);
            return status;
        }

        public SubmitStatus SubmitPacket(ESHandlePacket packet)
        {
            Tizen.Log.Error("XSF", $"Enter {Position}");
            return esPlayer.SubmitPacket(packet);
        }

        public async Task<int> Seek(int ms)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            TimeSpan time = new TimeSpan(0, 0, 0, 0, ms);

            await esPlayer.SeekAsync(time, OnSeekReady);
            Tizen.Log.Error("XSF", "Etner");
            return ms;
        }

        public void SetDisplay(IVideoOutput output)
        {
            Tizen.Log.Error("XSFIMP", "Enter: IVideoOutput");
            VideoOutput = output;
        }

        public void SetSource(MediaSource source)
        {
        }

        public event EventHandler PlaybackCompleted;
        public event EventHandler PlaybackStarted;
        public event EventHandler PlaybackPaused;
        public event EventHandler PlaybackStopped;
        public event EventHandler UpdateStreamInfo;
        public event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;


        public event EventHandler<EOSEventArgs> EOSEmitted;
        public event EventHandler<Multimedia.ErrorEventArgs> ErrorOccurred;
        public event EventHandler<BufferStatusEventArgs> BufferStatusChanged;
        public event EventHandler<ResourceConflictEventArgs> ResourceConflicted;
        public event EventHandler<StreamEventArgs> StreamReady;
        //public event EventHandler VideoReady;
        public event EventHandler<SeekEventArgs> SeekReady;


        public bool UsesEmbeddingControls { get; set; }

        public bool AutoPlay { get; set; }

        public bool AutoStop { get; set; }

        public double Volume { get; set; }

        public bool IsMuted 
        {
            get
            {
                return _isMuted;
            }
            set
            {
                _isMuted = value;
                esPlayer.SetAudioMute(_isMuted);
            }
            
        }

        public int Position 
        {
            get
            {
                if (!_isStarted)
                    return 0;

                TimeSpan time;
                esPlayer.GetPlayingTime(out time);
                var position = (int)time.TotalMilliseconds;
                return position;
            }
        }

        public int Duration 
        {
            get
            {
                return -1;
            }
        }

        public DisplayAspectMode AspectMode 
        { 
            get
            { 
                return _aspectMode; 
            }
            set
            {
                Tizen.Log.Error("XSFIMP","Enter ****************");
                _aspectMode = value;
                esPlayer.SetDisplayMode(_aspectMode.ToMultimeida());
            }
        }

        public async Task<Stream> GetAlbumArts()
        {
            return null;
        }

        public async Task<IDictionary<string, string>> GetMetadata()
        {
            return null;
        }

        public TimeSpan PlayingTime
        {
            get
            {
                //Tizen.Log.Error("XSFIMP", "Enter");
                TimeSpan time;
                esPlayer.GetPlayingTime(out time);
                return time;
            }
        }

        public VideoStreamInfo VideoStreamInfo
        {
            get
            { 
                return _videoStreamInfo; 
            }
            set
            {
                Tizen.Log.Error("XSFIMP", "Enter");
                _videoStreamInfo = value;
                esPlayer.SetStream(_videoStreamInfo);
                UpdateStreamInfo?.Invoke(this, EventArgs.Empty);
            }
        }

        public AudioStreamInfo AudioStreamInfo
        {
            get
            {
                return _audioStreamInfo;
            }
            set
            {
                Tizen.Log.Error("XSFIMP", "Enter");
                _audioStreamInfo = value;
                esPlayer.SetStream(_audioStreamInfo);
                UpdateStreamInfo?.Invoke(this, EventArgs.Empty);
            }
        }

        void OnResourceConflicted(object sender, ResourceConflictEventArgs e)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            ResourceConflicted?.Invoke(sender, e);
        }

        void OnBufferStatusChanged(object sender, BufferStatusEventArgs e)
        {
            Tizen.Log.Error("XSFIMP", $"Enter {e.BufferStatus}");
            BufferStatusChanged?.Invoke(sender, e);
        }

        void OnErrorOccurred(object sender, Multimedia.ErrorEventArgs e)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            ErrorOccurred?.Invoke(sender, e);
        }

        void OnEOSEmitted(object sender, EOSEventArgs e)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            EOSEmitted?.Invoke(sender, e);
        }

        async Task ChangeToIdleState()
        {
            switch (esPlayer.GetState())
            {
                case ESPlayerState.Playing:
                case ESPlayerState.Paused:
                    esPlayer.Stop();
                    esPlayer.Close();
                    break;
                case ESPlayerState.Ready:
                    esPlayer.Close();
                    break;
            }
        }

        async void OnReadyToPrepare(Tizen.TV.Multimedia.StreamType streamType)
        {
            Log.Error("XSFIMP", $"Stream Type : {streamType}");

            switch (streamType)
            {
                case Tizen.TV.Multimedia.StreamType.Audio:
                    StreamReady?.Invoke(this, new StreamEventArgs(StreamType.Audio));
                    break;
                case Tizen.TV.Multimedia.StreamType.Video:
                    StreamReady?.Invoke(this, new StreamEventArgs(StreamType.Video));
                    break;
            }
        }


        async void OnSeekReady(TM.StreamType streamType, TimeSpan time)
        {
            Tizen.Log.Error("XSFIMP", $"Enter {streamType} : {time}");
            switch (streamType)
            {
                case TM.StreamType.Audio:
                    SeekReady?.Invoke(this, new SeekEventArgs(StreamType.Audio, time));
                    break;
                case TM.StreamType.Video:
                    SeekReady?.Invoke(this, new SeekEventArgs(StreamType.Video, time));
                    break;
            }
        }

        IVideoOutput VideoOutput
        {
            get { return _videoOutput; }
            set
            {
                if (TargetView != null)
                    TargetView.PropertyChanged -= OnTargetViewPropertyChanged;
                if (OverlayOutput != null)
                    OverlayOutput.AreaUpdated -= OnOverlayAreaUpdated;

                _videoOutput = value;

                if (TargetView != null)
                    TargetView.PropertyChanged += OnTargetViewPropertyChanged;
                if (OverlayOutput != null)
                    OverlayOutput.AreaUpdated += OnOverlayAreaUpdated;
            }
        }

        VisualElement TargetView => VideoOutput?.MediaView;

        IOverlayOutput OverlayOutput => TargetView as IOverlayOutput;

        void ApplyDisplay()
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            if (VideoOutput == null)
            {
                esPlayer.SetDisplay(null);
            }
            else
            {
                esPlayer.SetDisplay(TVForms.UIControls.MainWindowProvider());
                OverlayOutput.AreaUpdated += OnOverlayAreaUpdated;
                ApplyOverlayArea();
                Log.Error("XSFIMP", "Enter");
            }
        }

        async void OnTargetViewPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            Tizen.Log.Error("XSFIMP", $"Enter {e.PropertyName}");
            if (e.PropertyName == "Renderer")
            {
                ApplyDisplay();
            }
        }

        void OnOverlayAreaUpdated(object sender, EventArgs e)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            ApplyOverlayArea();
        }

        async void ApplyOverlayArea()
        {
            try
            {
                if (OverlayOutput.OverlayArea.IsEmpty)
                {
                    Tizen.Log.Error("XSFIMP", "Enter");
                    esPlayer.SetDisplayMode(AspectMode.ToMultimeida());
                }
                else
                {
                    Tizen.Log.Error("XSFIMP", "Enter");
                    var bound = OverlayOutput.OverlayArea.ToPixel();
                    var renderer = Platform.GetRenderer(TargetView);
                    if (renderer is OverlayViewRenderer)
                    {
                        var parentArea = renderer.NativeView.Geometry;
                        if (parentArea.Width == 0 || parentArea.Height == 0)
                        {
                            await Task.Delay(1);
                            parentArea = renderer.NativeView.Geometry;
                        }
                        bound = parentArea;
                    }
                    var roiBound = bound.ToMultimedia();
                    esPlayer.SetDisplayRoi(roiBound.X, roiBound.Y, roiBound.Width, roiBound.Height);
                    Tizen.Log.Error("XSFIMP", $"Enter {bound} // {roiBound}");
                }
            }
            catch (Exception e)
            {
                Log.Error("XSF", $"Error on Update Overlayarea : {e.Message}");
            }
        }


    }

    public static class MultimediaConvertExtensions
    {
        public static DisplayMode ToMultimeida(this DisplayAspectMode mode)
        {
            DisplayMode ret = DisplayMode.LetterBox;
            switch (mode)
            {
                case DisplayAspectMode.AspectFill:
                    ret = DisplayMode.CroppedFull;
                    break;
                case DisplayAspectMode.AspectFit:
                    ret = DisplayMode.LetterBox;
                    break;
                case DisplayAspectMode.Fill:
                    ret = DisplayMode.FullScreen;
                    break;
                case DisplayAspectMode.OrignalSize:
                    ret = DisplayMode.OriginSize;
                    break;
            }
            return ret;
        }
    }
}
