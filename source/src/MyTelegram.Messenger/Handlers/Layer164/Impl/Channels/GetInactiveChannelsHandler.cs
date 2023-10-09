// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get inactive channels and supergroups
/// See <a href="https://corefork.telegram.org/method/channels.getInactiveChannels" />
///</summary>
internal sealed class GetInactiveChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetInactiveChannels, MyTelegram.Schema.Messages.IInactiveChats>,
    Channels.IGetInactiveChannelsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IInactiveChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetInactiveChannels obj)
    {
        throw new NotImplementedException();
    }
}
