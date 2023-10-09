// ReSharper disable All

namespace MyTelegram.Handlers.Langpack.LayerN;

///<summary>
/// Get localization pack strings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLangPack" />
///</summary>
internal sealed class GetLangPackHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.LayerN.RequestGetLangPack, ILangPackDifference>,
    Langpack.LayerN.IGetLangPackHandler, IProcessedHandler
{
    private readonly ILanguageManager _languageManager;

    public GetLangPackHandler(ILanguageManager languageManager)
    {
        _languageManager = languageManager;
    }

    protected override async Task<ILangPackDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.LayerN.RequestGetLangPack obj)
    {
        ILangPackDifference r = new TLangPackDifference
        {
            FromVersion = 0,
            LangCode = obj.LangCode,
            Strings = new TVector<ILangPackString>((await _languageManager.GetAllLangPacksAsync(obj.LangCode)).Select(p => new TLangPackString
            {
                Key = p.Key,
                Value = p.Value
            })),
            Version = 0
        };

        return r;
    }
}
