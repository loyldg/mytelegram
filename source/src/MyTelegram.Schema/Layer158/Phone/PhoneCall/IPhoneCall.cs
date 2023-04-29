// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IPhoneCall : IObject
{
    Schema.IPhoneCall PhoneCall { get; set; }
    TVector<Schema.IUser> Users { get; set; }
}
