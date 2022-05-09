// ReSharper disable All

namespace MyTelegram.Schema.Photos;

public interface IPhotos : IObject
{
    TVector<MyTelegram.Schema.IPhoto> Photos { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
