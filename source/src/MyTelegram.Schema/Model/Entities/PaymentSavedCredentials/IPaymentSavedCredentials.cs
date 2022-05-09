// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPaymentSavedCredentials : IObject
{
    string Id { get; set; }
    string Title { get; set; }

}
