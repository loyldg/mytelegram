// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StoryFwdHeader" />
///</summary>
[JsonDerivedType(typeof(TStoryFwdHeader), nameof(TStoryFwdHeader))]
public interface IStoryFwdHeader : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool Modified { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? From { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string? FromName { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? StoryId { get; set; }
}
