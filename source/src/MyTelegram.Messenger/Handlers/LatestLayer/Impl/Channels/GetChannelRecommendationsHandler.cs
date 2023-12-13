// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.getChannelRecommendations" />
///</summary>
internal sealed class GetChannelRecommendationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetChannelRecommendations, MyTelegram.Schema.Messages.IChats>,
    Channels.IGetChannelRecommendationsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetChannelRecommendations obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IChats>(new TChats
        {
            Chats = new()
        });
    }
}
