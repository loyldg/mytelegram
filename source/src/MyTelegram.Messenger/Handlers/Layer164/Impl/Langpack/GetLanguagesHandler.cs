// ReSharper disable All

namespace MyTelegram.Handlers.Langpack;

///<summary>
/// Get information about all languages in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguages" />
///</summary>
internal sealed class GetLanguagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetLanguages, TVector<MyTelegram.Schema.ILangPackLanguage>>,
    Langpack.IGetLanguagesHandler
{
    protected override Task<TVector<MyTelegram.Schema.ILangPackLanguage>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetLanguages obj)
    {
        throw new NotImplementedException();
    }
}
