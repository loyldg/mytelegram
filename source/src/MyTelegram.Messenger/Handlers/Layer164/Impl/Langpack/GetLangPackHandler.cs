// ReSharper disable All

namespace MyTelegram.Handlers.Langpack;

///<summary>
/// Get localization pack strings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLangPack" />
///</summary>
internal sealed class GetLangPackHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetLangPack, MyTelegram.Schema.ILangPackDifference>,
    Langpack.IGetLangPackHandler
{
    protected override Task<MyTelegram.Schema.ILangPackDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetLangPack obj)
    {
        throw new NotImplementedException();
    }
}
