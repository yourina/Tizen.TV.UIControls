using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.TV.UIControls.Forms.Renderer;
//using TVRender = Tizen.TV.UIControls.Forms.Renderer;
using Tizen.TV.Extension.UIControls.Forms.Renderer;
using Tizen.TV.Multimedia;
using TM = Tizen.Multimedia;
using Tizen.TV.UIControls.Forms;
using TVForms = Tizen.TV.UIControls.Forms;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using System.Runtime.InteropServices;

[assembly: Xamarin.Forms.Dependency(typeof(TVESPlayerImpl))]
namespace Tizen.TV.Extension.UIControls.Forms.Renderer
{
    class TVESPlayerImpl : ITVESPlayer
    {
        protected ESPlayer esPlayer;

        bool _cancelToStart;
        IVideoOutput _videoOutput;
        //MediaSource _source;
        VideoStreamInfo _videoStreamInfo;
        AudioStreamInfo _audioStreamInfo;
        TaskCompletionSource<bool> _tcsSubmit;
        int audioSubmit = 0;
        int videoSubmit = 0;

        public TVESPlayerImpl()
        {
            esPlayer = new ESPlayer();
            esPlayer.EOSEmitted += OnEOSEmitted;
            esPlayer.ErrorOccurred += OnErrorOccurred;
            esPlayer.BufferStatusChanged += OnBufferStatusChanged;
            esPlayer.ResourceConflicted += OnResourceConflicted;
            Tizen.Log.Error("XSFIMP", $"Enter {esPlayer} : 1007 0700 PM");
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

        bool IsOverlayMode => OverlayOutput != null;


        void ApplyDisplay()
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            if (VideoOutput == null)
            {
                esPlayer.SetDisplay(null);
            }
            //else if (!IsOverlayMode)
            //{
            //    Log.Error("XSF", "Error on MediaView");
            //    var renderer = Platform.GetRenderer(TargetView);
            //    if (renderer is IMediaViewProvider provider && provider.GetMediaView() != null)
            //    {
            //        try
            //        {
            //            //Display display = new Display(provider.GetMediaView());
            //            //_player.Display = display;
            //            //_player.DisplaySettings.Mode = _aspectMode.ToMultimeida();
            //        }
            //        catch
            //        {
            //            Log.Error("XSF", "Error on MediaView");
            //        }
            //    }
            //}
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


        public void SetDisplay(IVideoOutput output)
        {
            Tizen.Log.Error("XSFIMP", "Enter: IVideoOutput");
            VideoOutput = output;
        }

        //public void SetDisplay(ElmSharp.Window window)
        //{
        //    Tizen.Log.Error("XSFIMP", "Not called ******************");
        //    esPlayer.SetDisplay(window);
        //}


        public void SetSource(MediaSource source)
        {
            //Tizen.Log.Error("XSFIMP", "Enter");
            //_source = source;
        }

        public void Open()
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            esPlayer.Open();

            //The Tizen TV emulator is based on the x86 architecture. Using trust zone (DRM'ed content playback) is not supported by the emulator.
            if (RuntimeInformation.ProcessArchitecture != Architecture.X86)
            {
                esPlayer.SetTrustZoneUse(true);
            }

            Tizen.Log.Error("XSFIMP", "Enter");
        }

        public event EventHandler<EOSEventArgs> EOSEmitted;

        public event EventHandler<Multimedia.ErrorEventArgs> ErrorOccurred;

        public event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        public event EventHandler<ResourceConflictEventArgs> ResourceConflicted;

        public event EventHandler AudioReady;

        public event EventHandler VideoReady;


        //public async Task<bool> Prepare(Action<StreamType> onReadyToPrepare)
        //{
        //    Tizen.Log.Error("XSFIMP", $"Enter : {esPlayer.GetState()}");
        //    await esPlayer.PrepareAsync(onReadyToPrepare);
        //    //if (esPlayer.GetState() == ESPlayerState.Ready)
        //    //{
        //    //    await esPlayer.PrepareAsync(onReadyToPrepare);
        //    //}
        //    //else 
        //    //{
        //    //    return false;
        //    //}
        //    return true;
        //}

        async void OnReadyToPrepare(StreamType streamType)
        {
            Log.Error("XSFIMP", $"Stream Type : {streamType}");

            switch (streamType)
            {
                case StreamType.Audio:
                    AudioReady?.Invoke(this, new EventArgs());
                    break;
                case StreamType.Video:
                    VideoReady?.Invoke(this, new EventArgs());
                    break;
            }
        }


        public async Task<bool> Start()
        {
            if (_tcsSubmit == null || _tcsSubmit.Task.IsCompleted)
            {
                Tizen.Log.Error("XSFIMP", "Enter");
                Tizen.Log.Error("XSFIMP", "Enter");
                _tcsSubmit = new TaskCompletionSource<bool>();
            }

            Tizen.Log.Error("XSFIMP", "Enter");
            await esPlayer.PrepareAsync(OnReadyToPrepare);

            Tizen.Log.Error("XSFIMP", "Enter");
            Tizen.Log.Error("XSFIMP", "Enter");
            if (esPlayer.GetState() == ESPlayerState.Ready)
            {
                Tizen.Log.Error("XSFIMP", "Enter");
                Tizen.Log.Error("XSFIMP", "Enter");
                await _tcsSubmit.Task;

                Tizen.Log.Error("XSFIMP", "Enter");
                esPlayer.Start();

                Tizen.Log.Error("XSFIMP", "Enter");
                //ApplyDisplay();
                //Tizen.Log.Error("XSFIMP", "Enter");
            }
            else
            {
                return false;
            }
            return true;
        }

        public SubmitStatus SubmitPacket(ESPacket packet)
        {
            if (packet.type == StreamType.Audio)
                audioSubmit++;
            else
                videoSubmit++;

            if (audioSubmit >= 10 && videoSubmit >= 10)
                _tcsSubmit?.TrySetResult(true);

            Tizen.Log.Error("XSFIMP", $"Enter {audioSubmit} {videoSubmit} {packet.type}");
            return esPlayer.SubmitPacket(packet);
        }

        public SubmitStatus SubmitEosPacket(StreamType type)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            return esPlayer.SubmitEosPacket(type);
        }

        public SubmitStatus SubmitPacket(ESHandlePacket packet)
        {
            //Tizen.Log.Error("XSFIMP", "Enter");
            return esPlayer.SubmitPacket(packet);
        }

        public ESPlayerState State
        {
            get { return esPlayer.GetState(); }
        }

        public async Task<int> Seek(int ms)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            TimeSpan time = new TimeSpan(0, 0, 0, 0, ms);

            await esPlayer.SeekAsync(time, OnReadyToSeek);
            Tizen.Log.Error("XSF", "Etner");
            return -1;
        }

        async void OnReadyToSeek(StreamType streamType, TimeSpan time)
        {
            Tizen.Log.Error("XSFIMP", "Enter");
            switch (streamType)
            {
                case StreamType.Audio:
                    AudioReady?.Invoke(this, new EventArgs());
                    break;
                case StreamType.Video:
                    VideoReady?.Invoke(this, new EventArgs());
                    break;
            }
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

        public void Stop()
        {
            Tizen.Log.Error("XSF", "Etner");
        }

        public bool UsesEmbeddingControls { get; set; }

        public bool AutoPlay { get; set; }

        public bool AutoStop { get; set; }

        public double Volume { get; set; }

        public bool IsMuted { get; set; }

        public int Position { get; set; }

        public int Duration { get; set; }

        public DisplayAspectMode AspectMode { get; set; }

        public event EventHandler PlaybackCompleted;
        public event EventHandler PlaybackStarted;
        public event EventHandler PlaybackPaused;
        public event EventHandler PlaybackStopped;
        public event EventHandler UpdateStreamInfo;
        public event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;

        public async Task<Stream> GetAlbumArts()
        {
            Tizen.Log.Error("XSF", "Etner");
            return new MemoryStream();
        }

        public async Task<IDictionary<string, string>> GetMetadata()
        {
            Tizen.Log.Error("XSF", "Etner");
            Dictionary<string, string> metadata = new Dictionary<string, string>();
            return metadata;
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
