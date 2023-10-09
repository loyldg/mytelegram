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
        throw new NotImplementedException();
    }
}
