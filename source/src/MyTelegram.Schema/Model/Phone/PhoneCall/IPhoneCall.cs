// ReSharper disable All

namespace MyTelegram.Schema.Phone;

public interface IPhoneCall : IObject
{
    MyTelegram.Schema.IPhoneCall PhoneCall { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }

}
