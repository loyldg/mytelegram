// ReSharper disable All
namespace MyTelegram.Schema;

public interface ILayeredUser : IUser
{
    //long Id { get; set; }
    long? AccessHash { get; set; }
    MyTelegram.Schema.IUserStatus? Status { get; set; }
    bool Self { get; set; }
    bool Contact { get; set; }
    string? Phone { get; set; }
    string? FirstName { get; set; }
    string? LastName { get; set; }
    int? BotInfoVersion { get; set; }
    bool BotChatHistory { get; set; }
    bool BotNochats { get; set; }
    ///<summary>
    ///See <a href="https://core.telegram.org/type/UserProfilePhoto" />
    ///</summary>
    MyTelegram.Schema.IUserProfilePhoto? Photo { get; set; }

    string? BotInlinePlaceholder { get; set; }
    MyTelegram.Schema.IEmojiStatus? EmojiStatus { get; set; }

    ///<summary>
    /// Whether the account of this user was deleted. <br>Changes to this flag should invalidate the local <a href="https://corefork.telegram.org/constructor/userFull">userFull</a> cache for this user ID, see <a href="https://corefork.telegram.org/api/peers#full-info-database">here »</a> for more info.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    bool Deleted { get; set; }
    TVector<MyTelegram.Schema.IUsername>? Usernames { get; set; }
}