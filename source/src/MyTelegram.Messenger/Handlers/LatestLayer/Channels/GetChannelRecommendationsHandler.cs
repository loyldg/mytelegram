namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Obtain a list of similarly themed public channels, selected based on similarities in their <strong>subscriber bases</strong>.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.getChannelRecommendations"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetChannelRecommendationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetChannelRecommendations, MyTelegram.Schema.Messages.IChats>
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestGetChannelRecommendations obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IChats>(new TChats { Chats = [] });
    }
}