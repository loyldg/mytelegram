// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Name, ISO code, localized name and phone codes/patterns of all available countries
/// See <a href="https://corefork.telegram.org/constructor/help.CountriesList" />
///</summary>
[JsonDerivedType(typeof(TCountriesListNotModified), nameof(TCountriesListNotModified))]
[JsonDerivedType(typeof(TCountriesList), nameof(TCountriesList))]
public interface ICountriesList : IObject
{

}
