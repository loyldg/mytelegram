using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetCountriesListHandler : RpcResultObjectHandler<RequestGetCountriesList, ICountriesList>,
    IGetCountriesListHandler, IProcessedHandler
{
    protected override Task<ICountriesList> HandleCoreAsync(IRequestInput input,
        RequestGetCountriesList obj)
    {
        var contryList = new TCountriesList {
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
