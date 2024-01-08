// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Media autodownload settings
/// See <a href="https://corefork.telegram.org/constructor/AutoDownloadSettings" />
///</summary>
[JsonDerivedType(typeof(TAutoDownloadSettings), nameof(TAutoDownloadSettings))]
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
    /// Whether to preload <a href="https://corefork.telegram.org/api/stories">stories</a>; in particular, the first <a href="https://corefork.telegram.org/constructor/documentAttributeVideo">documentAttributeVideo</a>.<code>preload_prefix_size</code> bytes of story videos should be preloaded.
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
    /// A limit, specifying the maximum number of files that should be downloaded in parallel from the same DC, for files smaller than 20MB.
    ///</summary>
    int SmallQueueActiveOperationsMax { get; set; }

    ///<summary>
    /// A limit, specifying the maximum number of files that should be downloaded in parallel from the same DC, for files bigger than 20MB.
    ///</summary>
    int LargeQueueActiveOperationsMax { get; set; }
}
