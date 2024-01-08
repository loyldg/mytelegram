// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about the original poster of a reposted story.
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
    /// Whether the story media was modified before reposting it (for example by overlaying a round video with a reaction).
    ///</summary>
    bool Modified { get; set; }

    ///<summary>
    /// Peer that originally posted the story; will be empty for stories forwarded from a user with forwards privacy enabled, in which case <code>from_name</code> will be set, instead.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? From { get; set; }

    ///<summary>
    /// Will be set for stories forwarded from a user with forwards privacy enabled, in which case <code>from</code> will also be empty.
    ///</summary>
    string? FromName { get; set; }

    ///<summary>
    /// , contains the story ID
    ///</summary>
    int? StoryId { get; set; }
}
