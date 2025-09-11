namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Langpack;

///<summary>
/// Get localization pack strings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANGUAGE_INVALID The specified lang_code is invalid.
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLangPack" />
///</summary>
internal sealed class GetLangPackHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Langpack.LayerN.RequestGetLangPack,
        MyTelegram.Schema.Langpack.RequestGetLangPack> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Langpack.LayerN.RequestGetLangPack,
            MyTelegram.Schema.Langpack.RequestGetLangPack
        >(handlerHelper, dataConverter)
{
}