// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAutoDownloadSettings : IObject
{
    Schema.IAutoDownloadSettings Low { get; set; }
    Schema.IAutoDownloadSettings Medium { get; set; }
    Schema.IAutoDownloadSettings High { get; set; }
}
