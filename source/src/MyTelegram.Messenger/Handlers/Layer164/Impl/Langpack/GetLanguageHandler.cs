// ReSharper disable All

namespace MyTelegram.Handlers.Langpack;

///<summary>
/// Get information about a language in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguage" />
///</summary>
internal sealed class GetLanguageHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetLanguage, MyTelegram.Schema.ILangPackLanguage>,
    Langpack.IGetLanguageHandler
{
    protected override Task<MyTelegram.Schema.ILangPackLanguage> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetLanguage obj)
    {
        throw new NotImplementedException();
    }
}
