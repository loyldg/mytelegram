// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// Info about RTMP streams in a group call or livestream
/// See <a href="https://corefork.telegram.org/constructor/phone.GroupCallStreamChannels" />
///</summary>
public interface IGroupCallStreamChannels : IObject
{
    ///<summary>
    /// RTMP streams
    /// See <a href="https://corefork.telegram.org/type/GroupCallStreamChannel" />
    ///</summary>
    TVector<MyTelegram.Schema.IGroupCallStreamChannel> Channels { get; set; }
}
