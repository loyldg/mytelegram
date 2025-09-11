namespace MyTelegram.Converters.Requests.LayerN;
internal sealed class InitConnectionConverter
    : IRequestConverter<
        Schema.LayerN.RequestInitConnection,
        RequestInitConnection
    >, ITransientDependency
{
    public RequestInitConnection ToLatestLayerData(IRequestInput request, Schema.LayerN.RequestInitConnection obj)
    {
        return new RequestInitConnection
        {
            ApiId = obj.ApiId,
            DeviceModel = obj.DeviceModel,
            SystemVersion = obj.SystemVersion,
            AppVersion = obj.AppVersion,
            SystemLangCode = obj.SystemLangCode,
            LangPack = obj.LangPack,
            LangCode = obj.LangCode,
            Proxy = obj.Proxy,
            Query = obj.Query,
        };
    }
}
