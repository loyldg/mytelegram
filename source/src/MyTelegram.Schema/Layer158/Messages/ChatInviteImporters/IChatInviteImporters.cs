// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface IChatInviteImporters : IObject
{
    int Count { get; set; }
    TVector<Schema.IChatInviteImporter> Importers { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
