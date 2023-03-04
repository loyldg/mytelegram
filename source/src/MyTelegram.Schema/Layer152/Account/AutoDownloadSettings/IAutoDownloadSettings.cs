// ReSharper disable All

namespace MyTelegram.Schema.Account;

public interface IAutoDownloadSettings : IObject
{
    MyTelegram.Schema.IAutoDownloadSettings Low { get; set; }
    MyTelegram.Schema.IAutoDownloadSettings Medium { get; set; }
    MyTelegram.Schema.IAutoDownloadSettings High { get; set; }
}
