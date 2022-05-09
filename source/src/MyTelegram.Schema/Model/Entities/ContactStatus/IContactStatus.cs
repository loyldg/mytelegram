// ReSharper disable All

namespace MyTelegram.Schema;

public interface IContactStatus : IObject
{
    long UserId { get; set; }
    MyTelegram.Schema.IUserStatus Status { get; set; }

}
