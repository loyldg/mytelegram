// ReSharper disable All

namespace MyTelegram.Schema.Photos;

public interface IPhoto : IObject
{
    Schema.IPhoto Photo { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
