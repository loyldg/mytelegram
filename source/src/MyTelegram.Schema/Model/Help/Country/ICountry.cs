// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface ICountry : IObject
{
    BitArray Flags { get; set; }
    bool Hidden { get; set; }
    string Iso2 { get; set; }
    string DefaultName { get; set; }
    string? Name { get; set; }
    TVector<MyTelegram.Schema.Help.ICountryCode> CountryCodes { get; set; }

}
