using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tizen.TV.Extension.UIControls.Forms
{
    public enum StreamType
    {
        Audio,
        Video
    }

    //
    // Summary:
    //     Enumerator for buffer status whether empty or full.
    public enum BufferStatus
    {
        //
        // Summary:
        //     Status of buffer queue in Tizen.TV.Multimedia.ESPlayer is underrun.
        //
        // Remarks:
        //     When status is Tizen.TV.Multimedia.BufferStatus.Underrun, application should
        //     push es packet sufficiently.
        Underrun = 0,
        //
        // Summary:
        //     Status of buffer queue in Tizen.TV.Multimedia.ESPlayer is overrun.
        //
        // Remarks:
        //     When status is Tizen.TV.Multimedia.BufferStatus.Overrun, application should stop
        //     pushing es packet.
        Overrun = 1
    }

    //
    // Summary:
    //     Enumerator for es packet submit status
    public enum SubmitStatus
    {
        //
        // Summary:
        //     Not prepared to get packet
        NotPrepared = 0,
        //
        // Summary:
        //     Invalid packet
        InvalidPacket = 1,
        //
        // Summary:
        //     Out of memory on device
        OutOfMemory = 2,
        //
        // Summary:
        //     Buffer already full
        Full = 3,
        //
        // Summary:
        //     Submit succeeded
        Success = 4
    }

    //
    // Summary:
    //     Enumerator for video mime type
    public enum VideoMimeType
    {
        //
        // Summary:
        //     H.263
        H263 = 1,
        //
        // Summary:
        //     H.254
        H264 = 2,
        //
        // Summary:
        //     HEVC
        Hevc = 3,
        //
        // Summary:
        //     MPEG-1
        Mpeg1 = 4,
        //
        // Summary:
        //     MPEG-2
        Mpeg2 = 5,
        //
        // Summary:
        //     MPEG-4
        Mpeg4 = 6,
        //
        // Summary:
        //     VP8
        Vp8 = 7,
        //
        // Summary:
        //     VP9
        Vp9 = 8,
        //
        // Summary:
        //     WMV3
        Wmv3 = 9
    }

    //
    // Summary:
    //     Enumerator for audio mime type
    public enum AudioMimeType
    {
        //
        // Summary:
        //     Unknown
        Unknown = 0,
        //
        // Summary:
        //     AAC
        Aac = 1,
        //
        // Summary:
        //     MP2
        Mp2 = 2,
        //
        // Summary:
        //     MP3
        Mp3 = 3,
        //
        // Summary:
        //     AC3
        Ac3 = 4,
        //
        // Summary:
        //     EAC3
        Eac3 = 5,
        //
        // Summary:
        //     VORBIS
        Vorbis = 6,
        //
        // Summary:
        //     OPUS
        Opus = 7,
        //
        // Summary:
        //     PCM_S16LE
        PcmS16le = 8,
        //
        // Summary:
        //     PCM_S16BE
        PcmS16be = 9,
        //
        // Summary:
        //     PCM_U16LE
        PcmU16le = 10,
        //
        // Summary:
        //     PCM_U16BE
        PcmU16be = 11,
        //
        // Summary:
        //     PCM_S24LE
        PcmS24le = 12,
        //
        // Summary:
        //     PCM_S24BE
        PcmS24be = 13,
        //
        // Summary:
        //     PCM_U24LE
        PcmU24le = 14,
        //
        // Summary:
        //     PCM_U24BE
        PcmU24be = 15,
        //
        // Summary:
        //     PCM_S32LE
        PcmS32le = 16,
        //
        // Summary:
        //     PCM_S32BE
        PcmS32be = 17,
        //
        // Summary:
        //     PCM_U32LE
        PcmU32le = 18,
        //
        // Summary:
        //     PCM_U32BE
        PcmU32be = 19
    }

    //
    // Summary:
    //     Enumerator for error type from Tizen.TV.Multimedia.ESPlayer
    public enum ErrorType
    {
        //
        // Summary:
        //     Seek operation failure
        SeekFailed = -26476511,
        //
        // Summary:
        //     Invalid esplayer state
        InvalidState = -26476510,
        //
        // Summary:
        //     File format not supported
        NotSupportedFile = -26476509,
        //
        // Summary:
        //     Streaming connection failed
        ConnectionFailed = -26476506,
        //
        // Summary:
        //     Expired license
        DRMExpired = -26476504,
        //
        // Summary:
        //     No license
        DRMNoLicense = -26476503,
        //
        // Summary:
        //     License for future use
        DRMFutureUse = -26476502,
        //
        // Summary:
        //     Format not permitted
        NotPermitted = -26476501,
        //
        // Summary:
        //     Resource limit
        ResourceLimit = -26476500,
        //
        // Summary:
        //     Not supported audio codec but video can be played
        NotSupportedAudioCodec = -26476498,
        //
        // Summary:
        //     Not supported video codec but audio can be played
        NotSupportedVideoCodec = -26476497,
        //
        // Summary:
        //     DRM decryption failed
        DRMDecryptionFailed = -26472443,
        //
        // Summary:
        //     Format not supported
        NotSupportedFormat = -26472440,
        //
        // Summary:
        //     Unknown error
        Unknown = -26472439,
        //
        // Summary:
        //     No buffer space available
        BufferSpace = -105,
        //
        // Summary:
        //     Invalid operation
        InvalidOperator = -38,
        //
        // Summary:
        //     Invalid parameter
        InvalidParameter = -22,
        //
        // Summary:
        //     Permission denied
        PermissionDenied = -13,
        //
        // Summary:
        //     Out of memory
        OutOfMemory = -12,
        //
        // Summary:
        //     Successful
        None = 0
    }
}
