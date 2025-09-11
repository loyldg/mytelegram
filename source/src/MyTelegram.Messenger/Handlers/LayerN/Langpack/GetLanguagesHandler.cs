namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Langpack;

///<summary>
/// Get information about all languages in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguages" />
///</summary>
internal sealed class GetLanguagesHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Langpack.LayerN.RequestGetLanguages,
        MyTelegram.Schema.Langpack.RequestGetLanguages> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Langpack.LayerN.RequestGetLanguages,
            MyTelegram.Schema.Langpack.RequestGetLanguages
        >(handlerHelper, dataConverter)
{
}