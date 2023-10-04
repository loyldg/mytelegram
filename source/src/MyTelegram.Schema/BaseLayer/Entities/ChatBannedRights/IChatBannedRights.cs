// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents the rights of a normal user in a <a href="https://corefork.telegram.org/api/channel">supergroup/channel/chat</a>.
/// See <a href="https://corefork.telegram.org/constructor/ChatBannedRights" />
///</summary>
public interface IChatBannedRights : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, does not allow a user to view messages in a <a href="https://corefork.telegram.org/api/channel">supergroup/channel/chat</a>
    ///</summary>
    bool ViewMessages { get; set; }

    ///<summary>
    /// If set, does not allow a user to send messages in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool SendMessages { get; set; }

    ///<summary>
    /// If set, does not allow a user to send any media in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool SendMedia { get; set; }

    ///<summary>
    /// If set, does not allow a user to send stickers in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool SendStickers { get; set; }

    ///<summary>
    /// If set, does not allow a user to send gifs in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool SendGifs { get; set; }

    ///<summary>
    /// If set, does not allow a user to send games in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool SendGames { get; set; }

    ///<summary>
    /// If set, does not allow a user to use inline bots in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendInline { get; set; }

    ///<summary>
    /// If set, does not allow a user to embed links in the messages of a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool EmbedLinks { get; set; }

    ///<summary>
    /// If set, does not allow a user to send polls in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool SendPolls { get; set; }

    ///<summary>
    /// If set, does not allow any user to change the description of a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool ChangeInfo { get; set; }

    ///<summary>
    /// If set, does not allow any user to invite users in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool InviteUsers { get; set; }

    ///<summary>
    /// If set, does not allow any user to pin messages in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>
    ///</summary>
    bool PinMessages { get; set; }

    ///<summary>
    /// If set, does not allow any user to create, delete or modify <a href="https://corefork.telegram.org/api/forum#forum-topics">forum topics »</a>.
    ///</summary>
    bool ManageTopics { get; set; }

    ///<summary>
    /// If set, does not allow a user to send photos in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendPhotos { get; set; }

    ///<summary>
    /// If set, does not allow a user to send videos in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendVideos { get; set; }

    ///<summary>
    /// If set, does not allow a user to send round videos in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendRoundvideos { get; set; }

    ///<summary>
    /// If set, does not allow a user to send audio files in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendAudios { get; set; }

    ///<summary>
    /// If set, does not allow a user to send voice messages in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendVoices { get; set; }

    ///<summary>
    /// If set, does not allow a user to send documents in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendDocs { get; set; }

    ///<summary>
    /// If set, does not allow a user to send text messages in a <a href="https://corefork.telegram.org/api/channel">supergroup/chat</a>.
    ///</summary>
    bool SendPlain { get; set; }

    ///<summary>
    /// Validity of said permissions (it is considered forever any value less then 30 seconds or more then 366 days).
    ///</summary>
    int UntilDate { get; set; }
}
