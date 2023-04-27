// ReSharper disable All

namespace MyTelegram.Schema.Photos;

public interface IPhoto : IObject
{
    MyTelegram.Schema.IPhoto Photo { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
