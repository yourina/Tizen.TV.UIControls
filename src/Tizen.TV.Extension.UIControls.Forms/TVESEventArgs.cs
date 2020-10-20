using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tizen.TV.Extension.UIControls.Forms
{
    //
    // Summary:
    //     Provides event arguments for Tizen.TV.Multimedia.ESPlayer.BufferStatusChanged.
    public class BufferStatusEventArgs : EventArgs
    {
        public BufferStatusEventArgs(StreamType type, BufferStatus status)
        {
            StreamType = type;
            BufferStatus = status;
        }
        //
        // Summary:
        //     The stream type of the emitted event.
        public StreamType StreamType { get; internal set; }
        //
        // Summary:
        //     The buffer status of ESPlayer as Tizen.TV.Multimedia.BufferStatus.Underrun and
        //     Tizen.TV.Multimedia.BufferStatus.Overrun status.
        public BufferStatus BufferStatus { get; internal set; }
    }

    //
    // Summary:
    //     Provides event arguments for Tizen.TV.Multimedia.ESPlayer.ErrorOccurred.
    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(ErrorType type)
        {
            ErrorType = type;
        }
        //
        // Summary:
        //     The type of error from ESPlayer
        public ErrorType ErrorType { get; internal set; }
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
