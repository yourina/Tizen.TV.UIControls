using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.Multimedia;
using TM = Tizen.TV.Multimedia;

namespace Tizen.TV.Extension.UIControls.Forms
{
    public interface ITVESPlayer : IPlatformMediaPlayer
    {
        //void Open();

        //Task<bool> Prepare(Action<StreamType> onReadyToPrepare);

        //void SetDisplay(ElmSharp.Window window);

        SubmitStatus SubmitPacket(ESPacket packet);

        SubmitStatus SubmitEosPacket(TM.StreamType type);

        SubmitStatus SubmitPacket(ESHandlePacket packet);

        event EventHandler<EOSEventArgs> EOSEmitted;

        event EventHandler<Multimedia.ErrorEventArgs> ErrorOccurred;

        event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        event EventHandler<ResourceConflictEventArgs> ResourceConflicted;

        //event EventHandler AudioReady;

        //event EventHandler VideoReady;

        event EventHandler<StreamEventArgs> StreamReady;

        event EventHandler<SeekEventArgs> SeekReady;

        //ESPlayerState State { get; }

        TimeSpan PlayingTime { get; }

        VideoStreamInfo VideoStreamInfo { set; }

        AudioStreamInfo AudioStreamInfo { set; }

        void Resume();

    }

    public enum StreamType
    {
        Audio,
        Video
    }

    public class StreamEventArgs : EventArgs
    {
        public StreamEventArgs(StreamType type)
        {
            Type = type;
        }

        public StreamType Type { get; set; }
    }

    public class SeekEventArgs : EventArgs
    {
        public SeekEventArgs(StreamType type, TimeSpan time)
        {
            Type = type;
            Time = time;
        }

        public TimeSpan Time { get; set; }

        public StreamType Type { get; set; }
    }


}
