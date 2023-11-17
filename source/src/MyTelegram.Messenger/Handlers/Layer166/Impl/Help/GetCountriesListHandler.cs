// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get name, ISO code, localized name and phone codes/patterns of all available countries
/// See <a href="https://corefork.telegram.org/method/help.getCountriesList" />
///</summary>
internal sealed class GetCountriesListHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetCountriesList, MyTelegram.Schema.Help.ICountriesList>,
    Help.IGetCountriesListHandler
{
    protected override Task<MyTelegram.Schema.Help.ICountriesList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetCountriesList obj)
    {
        var contryList = new TCountriesList
        {
            Countries = new TVector<ICountry> {
                new TCountry {
                    Hidden = false,
                    Iso2 = "CN",
                    DefaultName = "China",
                    CountryCodes = new TVector<ICountryCode> {
                        new TCountryCode {
                            CountryCode = "86",
                            Patterns = new TVector<string> { "XXX XXXX XXXX" },
                            Prefixes = new TVector<string>()
                        }
                    }
                },
                new TCountry {
                    Hidden = false,
                    Iso2 = "US",
                    DefaultName = "USA",
                    CountryCodes = new TVector<ICountryCode> {
                        new TCountryCode {
                            CountryCode = "1",
                            Patterns = new TVector<string> { "XXX XXX XXXX" },
                            Prefixes = new TVector<string>()
                        }
                    }
                }
            }
        };

        return Task.FromResult<ICountriesList>(contryList);
    }
}
