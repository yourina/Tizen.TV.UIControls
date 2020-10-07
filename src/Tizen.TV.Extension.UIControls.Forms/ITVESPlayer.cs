using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Tizen.TV.UIControls.Forms;
using Tizen.TV.Multimedia;

namespace Tizen.TV.Extension.UIControls.Forms
{
    public interface ITVESPlayer : IPlatformMediaPlayer
    {
        void Open();

        //Task<bool> Prepare(Action<StreamType> onReadyToPrepare);

        //void SetDisplay(ElmSharp.Window window);

        SubmitStatus SubmitPacket(ESPacket packet);

        SubmitStatus SubmitEosPacket(StreamType type);

        SubmitStatus SubmitPacket(ESHandlePacket packet);

        event EventHandler<EOSEventArgs> EOSEmitted;

        event EventHandler<Multimedia.ErrorEventArgs> ErrorOccurred;

        event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        event EventHandler<ResourceConflictEventArgs> ResourceConflicted;

        event EventHandler AudioReady;

        event EventHandler VideoReady;

        ESPlayerState State { get; }

        TimeSpan PlayingTime { get; }

        VideoStreamInfo VideoStreamInfo { set; }

        AudioStreamInfo AudioStreamInfo { set; }


    }
}
