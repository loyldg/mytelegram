// ReSharper disable All

namespace MyTelegram.Schema.Photos;

public interface IPhotos : IObject
{
    TVector<Schema.IPhoto> Photos { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
