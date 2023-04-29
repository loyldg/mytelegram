// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface IRecentMeUrls : IObject
{
    TVector<Schema.IRecentMeUrl> Urls { get; set; }
    TVector<Schema.IChat> Chats { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
