// ReSharper disable All

using MyTelegram.Schema.Langpack.LayerN;

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
    protected override Task<ILangPackDifference> HandleCoreAsync(IRequestInput request, RequestGetLangPack obj)
    {
        ILangPackDifference r = new TLangPackDifference
        {
            FromVersion = 0,
            LangCode = obj.LangCode,
            Strings = new TVector<ILangPackString>(),
            Version = 0
        };
        return Task.FromResult(r);
    }
}
