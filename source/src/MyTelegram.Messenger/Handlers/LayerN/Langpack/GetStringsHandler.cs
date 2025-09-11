namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Langpack;

///<summary>
/// Get strings from a language pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getStrings" />
///</summary>
internal sealed class GetStringsHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Langpack.LayerN.RequestGetStrings,
        MyTelegram.Schema.Langpack.RequestGetStrings> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Langpack.LayerN.RequestGetStrings,
            MyTelegram.Schema.Langpack.RequestGetStrings
        >(handlerHelper, dataConverter)
{
}