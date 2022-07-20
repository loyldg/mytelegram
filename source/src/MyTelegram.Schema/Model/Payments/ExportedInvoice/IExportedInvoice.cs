// ReSharper disable All

namespace MyTelegram.Schema.Payments;

public interface IExportedInvoice : IObject
{
    string Url { get; set; }

}
