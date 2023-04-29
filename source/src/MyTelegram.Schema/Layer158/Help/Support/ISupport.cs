// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface ISupport : IObject
{
    string PhoneNumber { get; set; }
    Schema.IUser User { get; set; }
}
