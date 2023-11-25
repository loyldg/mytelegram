// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about an RTMP stream in a group call or livestream
/// See <a href="https://corefork.telegram.org/constructor/GroupCallStreamChannel" />
///</summary>
[JsonDerivedType(typeof(TGroupCallStreamChannel), nameof(TGroupCallStreamChannel))]
public interface IGroupCallStreamChannel : IObject
{
    ///<summary>
    /// Channel ID
    ///</summary>
    int Channel { get; set; }

    ///<summary>
    /// Specifies the duration of the video segment to fetch in milliseconds, by bitshifting <code>1000</code> to the right <code>scale</code> times: <code>duration_ms := 1000 &gt;&gt; scale</code>.
    ///</summary>
    int Scale { get; set; }

    ///<summary>
    /// Last seen timestamp to easily start fetching livestream chunks using <a href="https://corefork.telegram.org/constructor/inputGroupCallStream">inputGroupCallStream</a>
    ///</summary>
    long LastTimestampMs { get; set; }
}
