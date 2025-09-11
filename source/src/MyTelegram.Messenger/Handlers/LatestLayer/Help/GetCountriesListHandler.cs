namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

///<summary>
/// Get name, ISO code, localized name and phone codes/patterns of all available countries
/// See <a href="https://corefork.telegram.org/method/help.getCountriesList" />
///</summary>
internal sealed class GetCountriesListHandler(ICountryHelper countryHelper) : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetCountriesList, MyTelegram.Schema.Help.ICountriesList>
{

    private static ICountriesList? _countriesList;
    private static int _hash = -432121763;

    protected override Task<ICountriesList> HandleCoreAsync(IRequestInput input,
        RequestGetCountriesList obj)
    {
        InitCountriesListIfNeed();

        if (obj.Hash == _hash)
        {
            return Task.FromResult<ICountriesList>(new TCountriesListNotModified());
        }

        return Task.FromResult(_countriesList!);
    }

    private void InitCountriesListIfNeed()
    {
        if (_countriesList == null)
        {
            var countriesList =
                countryHelper.GetAllCountryList();

            _countriesList = new TCountriesList
            {
                Countries = [.. countriesList!.Select(p => new TCountry
                {
                    Hidden = p.Hidden,
                    Iso2 = p.Iso2,
                    DefaultName = p.DefaultName,
                    Name = p.Name,
                    CountryCodes = [.. p.CountryCodes.Select(p => new TCountryCode
                    {
                        CountryCode = p.CountryCode,
                        Patterns = p.Patterns == null ? null : [.. p.Patterns],
                        Prefixes = p.Prefixes == null ? null : [.. p.Prefixes]
                    })]
                })],
                Hash = _hash
            };
        }
    }
}
