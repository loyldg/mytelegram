// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface ICountryCode : IObject
{
    BitArray Flags { get; set; }
    string CountryCode { get; set; }
    TVector<string>? Prefixes { get; set; }
    TVector<string>? Patterns { get; set; }

}
