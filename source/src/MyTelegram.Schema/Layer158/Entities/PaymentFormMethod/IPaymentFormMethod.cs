// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPaymentFormMethod : IObject
{
    string Url { get; set; }
    string Title { get; set; }
}
