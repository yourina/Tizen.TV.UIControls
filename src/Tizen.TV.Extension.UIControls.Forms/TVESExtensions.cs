using Tizen.TV.UIControls.Forms;
using Tizen.TV.Multimedia;
using TM = Tizen.TV.Multimedia;

namespace Tizen.TV.Extension.UIControls.Forms
{
    public static class TVESConvertExtensions
    {
        public static StreamType ToStreamType(this TM.StreamType type)
        {
            StreamType ret = StreamType.Audio;
            switch (type)
            {
                case TM.StreamType.Video:
                    ret = StreamType.Video;
                    break;
            }
            return ret;
        }

        public static TM.StreamType ToStreamType(this StreamType type)
        {
            TM.StreamType ret = TM.StreamType.Audio;
            switch (type)
            {
                case StreamType.Video:
                    ret = TM.StreamType.Video;
                    break;
            }
            return ret;
        }

        public static BufferStatus ToBufferStatus(this TM.BufferStatus type)
        {
            BufferStatus ret = BufferStatus.Underrun;
            switch (type)
            {
                case TM.BufferStatus.Overrun:
                    ret = BufferStatus.Overrun;
                    break;
            }
            return ret;
        }

        public static TM.ESPacket ToESPacket(this ESPacket packet)
        {
            Tizen.Log.Error("XSF", $"Enter : {packet.type}");
            var nPacket = new TM.ESPacket();
            nPacket.type = packet.type.ToStreamType();
            nPacket.duration = packet.duration;
            nPacket.buffer = packet.buffer;
            nPacket.pts = packet.pts;
            nPacket.matroskaColorInfo = packet.matroskaColorInfo;
            return nPacket;
        }

        public static TM.ESHandlePacket ToESHandlePacket(this ESHandlePacket packet)
        {
            Tizen.Log.Error("XSF", $"Enter : {packet.type}");
            var nPacket = new TM.ESHandlePacket();
            nPacket.type = packet.type.ToStreamType();
            nPacket.handle = packet.handle;
            nPacket.handleSize = packet.handleSize;
            nPacket.pts = packet.pts;
            nPacket.duration = packet.duration;
            nPacket.matroskaColorInfo = packet.matroskaColorInfo;
            return nPacket;
        }

        public static ErrorType ToErrorType(this TM.ErrorType type)
        {
            ErrorType ret = ErrorType.None;
            switch (type)
            {
                case TM.ErrorType.SeekFailed:
                    ret = ErrorType.SeekFailed;
                    break;
                case TM.ErrorType.InvalidState:
                    ret = ErrorType.InvalidState;
                    break;
                case TM.ErrorType.NotSupportedFile:
                    ret = ErrorType.NotSupportedFile;
                    break;
                case TM.ErrorType.ConnectionFailed:
                    ret = ErrorType.ConnectionFailed;
                    break;
                case TM.ErrorType.DRMExpired:
                    ret = ErrorType.DRMExpired;
                    break;
                case TM.ErrorType.DRMNoLicense:
                    ret = ErrorType.DRMNoLicense;
                    break;
                case TM.ErrorType.DRMFutureUse:
                    ret = ErrorType.DRMFutureUse;
                    break;
                case TM.ErrorType.NotPermitted:
                    ret = ErrorType.NotPermitted;
                    break;
                case TM.ErrorType.ResourceLimit:
                    ret = ErrorType.ResourceLimit;
                    break;
                case TM.ErrorType.NotSupportedAudioCodec:
                    ret = ErrorType.NotSupportedAudioCodec;
                    break;
                case TM.ErrorType.NotSupportedVideoCodec:
                    ret = ErrorType.NotSupportedVideoCodec;
                    break;
                case TM.ErrorType.DRMDecryptionFailed:
                    ret = ErrorType.DRMDecryptionFailed;
                    break;
                case TM.ErrorType.NotSupportedFormat:
                    ret = ErrorType.NotSupportedFormat;
                    break;
                case TM.ErrorType.Unknown:
                    ret = ErrorType.Unknown;
                    break;
                case TM.ErrorType.BufferSpace:
                    ret = ErrorType.BufferSpace;
                    break;
                case TM.ErrorType.InvalidOperator:
                    ret = ErrorType.InvalidOperator;
                    break;
                case TM.ErrorType.InvalidParameter:
                    ret = ErrorType.InvalidParameter;
                    break;
                case TM.ErrorType.PermissionDenied:
                    ret = ErrorType.PermissionDenied;
                    break;
                case TM.ErrorType.OutOfMemory:
                    ret = ErrorType.OutOfMemory;
                    break;
            }
            return ret;
        }

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

        public static SubmitStatus ToSubmitStatus(this TM.SubmitStatus mode)
        {
            SubmitStatus ret = SubmitStatus.Success;
            switch (mode)
            {
                case TM.SubmitStatus.Success:
                    ret = SubmitStatus.Success;
                    break;
                case TM.SubmitStatus.Full:
                    ret = SubmitStatus.Full;
                    break;
                case TM.SubmitStatus.InvalidPacket:
                    ret = SubmitStatus.InvalidPacket;
                    break;
                case TM.SubmitStatus.NotPrepared:
                    ret = SubmitStatus.NotPrepared;
                    break;
                case TM.SubmitStatus.OutOfMemory:
                    ret = SubmitStatus.OutOfMemory;
                    break;
            }
            return ret;
        }

        public static TM.AudioMimeType ToMimeType(this AudioMimeType mode)
        {
            TM.AudioMimeType ret = TM.AudioMimeType.Unknown;
            switch (mode)
            {
                case AudioMimeType.Aac:
                    ret = TM.AudioMimeType.Aac;
                    break;
                case AudioMimeType.Mp2:
                    ret = TM.AudioMimeType.Mp2;
                    break;
                case AudioMimeType.Mp3:
                    ret = TM.AudioMimeType.Mp3;
                    break;
                case AudioMimeType.Ac3:
                    ret = TM.AudioMimeType.Ac3;
                    break;
                case AudioMimeType.Eac3:
                    ret = TM.AudioMimeType.Eac3;
                    break;
                case AudioMimeType.Vorbis:
                    ret = TM.AudioMimeType.Vorbis;
                    break;
                case AudioMimeType.Opus:
                    ret = TM.AudioMimeType.Opus;
                    break;
                case AudioMimeType.PcmS16le:
                    ret = TM.AudioMimeType.PcmS16le;
                    break;
                case AudioMimeType.PcmS16be:
                    ret = TM.AudioMimeType.PcmS16be;
                    break;
                case AudioMimeType.PcmU16le:
                    ret = TM.AudioMimeType.PcmU16le;
                    break;
                case AudioMimeType.PcmU16be:
                    ret = TM.AudioMimeType.PcmU16be;
                    break;
                case AudioMimeType.PcmS24le:
                    ret = TM.AudioMimeType.PcmS24le;
                    break;
                case AudioMimeType.PcmS24be:
                    ret = TM.AudioMimeType.PcmS24be;
                    break;
                case AudioMimeType.PcmU24le:
                    ret = TM.AudioMimeType.PcmU24le;
                    break;
                case AudioMimeType.PcmU24be:
                    ret = TM.AudioMimeType.PcmU24be;
                    break;
                case AudioMimeType.PcmS32le:
                    ret = TM.AudioMimeType.PcmS32le;
                    break;
                case AudioMimeType.PcmS32be:
                    ret = TM.AudioMimeType.PcmS32be;
                    break;
                case AudioMimeType.PcmU32le:
                    ret = TM.AudioMimeType.PcmU32le;
                    break;
                case AudioMimeType.PcmU32be:
                    ret = TM.AudioMimeType.PcmU32be;
                    break;
            }
            return ret;
        }


        public static TM.VideoMimeType ToMimeType(this VideoMimeType mode)
        {
            TM.VideoMimeType ret = TM.VideoMimeType.H264;
            switch (mode)
            {
                case VideoMimeType.H263:
                    ret = TM.VideoMimeType.H263;
                    break;
                case VideoMimeType.H264:
                    ret = TM.VideoMimeType.H264;
                    break;
                case VideoMimeType.Hevc:
                    ret = TM.VideoMimeType.Hevc;
                    break;
                case VideoMimeType.Mpeg1:
                    ret = TM.VideoMimeType.Mpeg1;
                    break;
                case VideoMimeType.Mpeg2:
                    ret = TM.VideoMimeType.Mpeg2;
                    break;
                case VideoMimeType.Mpeg4:
                    ret = TM.VideoMimeType.Mpeg4;
                    break;
                case VideoMimeType.Vp8:
                    ret = TM.VideoMimeType.Vp8;
                    break;
                case VideoMimeType.Vp9:
                    ret = TM.VideoMimeType.Vp9;
                    break;
                case VideoMimeType.Wmv3:
                    ret = TM.VideoMimeType.Wmv3;
                    break;
            }
            return ret;
        }

    }
}
