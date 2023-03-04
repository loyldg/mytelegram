// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPostAddress : IObject
{
    string StreetLine1 { get; set; }
    string StreetLine2 { get; set; }
    string City { get; set; }
    string State { get; set; }
    string CountryIso2 { get; set; }
    string PostCode { get; set; }
}
