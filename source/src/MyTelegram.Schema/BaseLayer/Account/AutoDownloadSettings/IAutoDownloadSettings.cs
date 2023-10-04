// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Media autodownload settings
/// See <a href="https://corefork.telegram.org/constructor/account.AutoDownloadSettings" />
///</summary>
public interface IAutoDownloadSettings : IObject
{
    ///<summary>
    /// Low data usage preset
    /// See <a href="https://corefork.telegram.org/type/AutoDownloadSettings" />
    ///</summary>
    MyTelegram.Schema.IAutoDownloadSettings Low { get; set; }

    ///<summary>
    /// Medium data usage preset
    /// See <a href="https://corefork.telegram.org/type/AutoDownloadSettings" />
    ///</summary>
    MyTelegram.Schema.IAutoDownloadSettings Medium { get; set; }

    ///<summary>
    /// High data usage preset
    /// See <a href="https://corefork.telegram.org/type/AutoDownloadSettings" />
    ///</summary>
    MyTelegram.Schema.IAutoDownloadSettings High { get; set; }
}
