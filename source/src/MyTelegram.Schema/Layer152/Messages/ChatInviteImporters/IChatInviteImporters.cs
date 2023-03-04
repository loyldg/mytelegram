// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChatInviteImporters : IObject
{
    int Count { get; set; }
    TVector<MyTelegram.Schema.IChatInviteImporter> Importers { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
