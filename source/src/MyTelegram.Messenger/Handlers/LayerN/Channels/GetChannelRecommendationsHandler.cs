namespace MyTelegram.Messenger.Handlers.LatestLayer.LayerN.Channels;


///<summary>
/// Obtain a list of similarly themed public channels, selected based on similarities in their <strong>subscriber bases</strong>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// See <a href="https://corefork.telegram.org/method/channels.getChannelRecommendations" />
///</summary>
internal sealed class GetChannelRecommendationsHandler(
    IHandlerHelper handlerHelper,
    IRequestConverter<MyTelegram.Schema.Channels.LayerN.RequestGetChannelRecommendations,
        MyTelegram.Schema.Channels.RequestGetChannelRecommendations> dataConverter)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Channels.LayerN.RequestGetChannelRecommendations,
            MyTelegram.Schema.Channels.RequestGetChannelRecommendations
        >(handlerHelper, dataConverter)
{
}