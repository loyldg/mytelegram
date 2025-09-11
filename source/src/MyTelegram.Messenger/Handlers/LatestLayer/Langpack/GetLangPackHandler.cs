namespace MyTelegram.Messenger.Handlers.LatestLayer.Langpack;

///<summary>
/// Get localization pack strings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANGUAGE_INVALID The specified lang_code is invalid.
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLangPack" />
///</summary>
internal sealed class GetLangPackHandler(ILanguageCacheService languageCacheService, ILangPackStringConverterService langPackStringConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetLangPack, MyTelegram.Schema.ILangPackDifference>
{
    protected override async Task<ILangPackDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetLangPack obj)
    {
        var langPack = obj.LangPack ?? input.DeviceType.ToString().ToLower();
        var languageReadModel = await languageCacheService.GetLanguageAsync(obj.LangCode, langPack);
        if (languageReadModel == null)
        {
            RpcErrors.RpcErrors400.LangPackInvalid.ThrowRpcError();
        }

        var texts = await languageCacheService.GetLanguageTextAsync(obj.LangCode, langPack);

        var langPackDifference = new TLangPackDifference
        {
            FromVersion = 0,
            LangCode = obj.LangCode,
            Version = languageReadModel!.LanguageVersion,
            Strings = langPackStringConverterService.ToLangPackStrings(texts, input.Layer)
        };

        return langPackDifference;
    }
}