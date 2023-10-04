// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// RTMP URL and stream key to be used in streaming software
/// See <a href="https://corefork.telegram.org/constructor/phone.GroupCallStreamRtmpUrl" />
///</summary>
public interface IGroupCallStreamRtmpUrl : IObject
{
    ///<summary>
    /// RTMP URL
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Stream key
    ///</summary>
    string Key { get; set; }
}
