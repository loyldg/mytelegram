// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Media autodownload settings
/// See <a href="https://corefork.telegram.org/constructor/AutoDownloadSettings" />
///</summary>
public interface IAutoDownloadSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Disable automatic media downloads?
    ///</summary>
    bool Disabled { get; set; }

    ///<summary>
    /// Whether to preload the first seconds of videos larger than the specified limit
    ///</summary>
    bool VideoPreloadLarge { get; set; }

    ///<summary>
    /// Whether to preload the next audio track when you're listening to music
    ///</summary>
    bool AudioPreloadNext { get; set; }

    ///<summary>
    /// Whether to enable data saving mode in phone calls
    ///</summary>
    bool PhonecallsLessData { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool StoriesPreload { get; set; }

    ///<summary>
    /// Maximum size of photos to preload
    ///</summary>
    int PhotoSizeMax { get; set; }

    ///<summary>
    /// Maximum size of videos to preload
    ///</summary>
    long VideoSizeMax { get; set; }

    ///<summary>
    /// Maximum size of other files to preload
    ///</summary>
    long FileSizeMax { get; set; }

    ///<summary>
    /// Maximum suggested bitrate for <strong>uploading</strong> videos
    ///</summary>
    int VideoUploadMaxbitrate { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int SmallQueueActiveOperationsMax { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int LargeQueueActiveOperationsMax { get; set; }
}
