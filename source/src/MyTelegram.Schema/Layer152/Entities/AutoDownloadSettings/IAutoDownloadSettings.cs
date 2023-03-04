// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAutoDownloadSettings : IObject
{
    BitArray Flags { get; set; }
    bool Disabled { get; set; }
    bool VideoPreloadLarge { get; set; }
    bool AudioPreloadNext { get; set; }
    bool PhonecallsLessData { get; set; }
    int PhotoSizeMax { get; set; }
    long VideoSizeMax { get; set; }
    long FileSizeMax { get; set; }
    int VideoUploadMaxbitrate { get; set; }
}
