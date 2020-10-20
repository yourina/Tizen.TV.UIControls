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
        VideoStreamInfo VideoStreamInfo { set; }

        AudioStreamInfo AudioStreamInfo { set; }

        TimeSpan PlayingTime { get; }

        SubmitStatus SubmitPacket(ESPacket packet);

        SubmitStatus SubmitPacket(ESHandlePacket packet);
        
        SubmitStatus SubmitEosPacket(StreamType type);

        void Resume();

        event EventHandler EOSEmitted;

        event EventHandler ResourceConflicted;

        event EventHandler<ErrorEventArgs> ErrorOccurred;

        event EventHandler<BufferStatusEventArgs> BufferStatusChanged;

        event EventHandler<StreamEventArgs> StreamReady;

        event EventHandler<SeekEventArgs> SeekReady;

    }

    //
    // Summary:
    //     Represents one of es packet which is demuxed from application, contains stream
    //     type, es packet buffer, pts and duration.
    public class ESPacket
    {
        //
        // Summary:
        //     Stream type of ESPacket
        public StreamType type { get; set; }
        //
        // Summary:
        //     Buffer containing demuxed es packet
        public byte[] buffer { get; set; }
        //
        // Summary:
        //     PTS(Presentation Time Stamp) of es packet
        public ulong pts { get; set; }
        //
        // Summary:
        //     Duration of es packet
        public ulong duration { get; set; }
        //
        // Summary:
        //     The matroska color information. This value is only for video packet. If you set
        //     this value on a packet of other type, you can see an error when you submit the
        //     packet.
        public ESMatroskaColor? matroskaColorInfo { get; set; }
    }

    //
    // Summary:
    //     Represents one of es packet which includes handle of data inside the trust zone,
    //     contains stream type, handle for encrypted es data packet, handle size, pts and
    //     duration.
    public struct ESHandlePacket
    {
        //
        // Summary:
        //     Stream type of ESPacket
        public StreamType type;
        //
        // Summary:
        //     Handle for encrypted es packet inside the trust zone
        public uint handle;
        //
        // Summary:
        //     Handle size for Tizen.TV.Multimedia.ESHandlePacket.handle
        public uint handleSize;
        //
        // Summary:
        //     PTS(Presentation Time Stamp) of es packet
        public ulong pts;
        //
        // Summary:
        //     DUration of es packet
        public ulong duration;
        //
        // Summary:
        //     The matroska color information. this value is only for video packet. If you set
        //     this value on a packet of other type, you can see an error when you submit the
        //     packet.
        public ESMatroskaColor? matroskaColorInfo;
    }


    //
    // Summary:
    //     Represents stream information for video stream which is demuxed from application,
    //     contains codec data, mime type, width, height, max width, max hgeight and frame
    //     rate(num, den).
    public class VideoStreamInfo
    {
        //
        // Summary:
        //     Codec data for the associated video stream
        public byte[] codecData { get; set; }
        //
        // Summary:
        //     Mime type for the associated video stream
        public VideoMimeType mimeType { get; set; }
        //
        // Summary:
        //     width for the associated video stream
        public int width { get; set; }
        //
        // Summary:
        //     Height for the associated video stream
        public int height { get; set; }
        //
        // Summary:
        //     Max width for the associated video stream
        public int maxWidth { get; set; }
        //
        // Summary:
        //     Max height for the associated video stream
        public int maxHeight { get; set; }
        //
        // Summary:
        //     Numerator of framerate for the associated video stream
        public int num { get; set; }
        //
        // Summary:
        //     Denominator of framerate for the associated video stream
        public int den { get; set; }
    }

    public class AudioStreamInfo
    {
        //
        // Summary:
        //     Codec data for the associated audio stream
        public byte[] codecData { get; set; }
        //
        // Summary:
        //     Mime type for the associated audio stream
        public AudioMimeType mimeType { get; set; }
        //
        // Summary:
        //     Bitrate for the associated audio stream
        public int bitrate { get; set; }
        //
        // Summary:
        //     Channels for the associated audio stream
        public int channels { get; set; }
        //
        // Summary:
        //     Sample rate for the associated audio stream
        public int sampleRate { get; set; }
    }
}
