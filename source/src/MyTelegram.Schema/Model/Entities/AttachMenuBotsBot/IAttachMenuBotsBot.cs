// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAttachMenuBotsBot : IObject
{
    MyTelegram.Schema.IAttachMenuBot Bot { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
