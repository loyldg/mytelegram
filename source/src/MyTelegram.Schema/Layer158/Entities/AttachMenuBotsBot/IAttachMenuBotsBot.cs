// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAttachMenuBotsBot : IObject
{
    Schema.IAttachMenuBot Bot { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
