// ReSharper disable All
namespace MyTelegram.Schema;

public interface ILayeredChannel : IChat
{
    long? AccessHash { get; set; }
    bool Creator { get; set; }
    IChatPhoto Photo { get; set; }
    bool Left { get; set; }
    IChatBannedRights? BannedRights { get; set; }
    IChatAdminRights? AdminRights { get; set; }
    IChatBannedRights? DefaultBannedRights { get; set; }
    int? ParticipantsCount { get; set; }
    //bool Deactivated { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/emoji-status">Emoji status</a>
    /// See <a href="https://corefork.telegram.org/type/EmojiStatus" />
    ///</summary>
    MyTelegram.Schema.IEmojiStatus? EmojiStatus { get; set; }
}