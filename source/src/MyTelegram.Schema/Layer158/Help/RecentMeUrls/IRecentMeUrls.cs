// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface IRecentMeUrls : IObject
{
    TVector<MyTelegram.Schema.IRecentMeUrl> Urls { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
