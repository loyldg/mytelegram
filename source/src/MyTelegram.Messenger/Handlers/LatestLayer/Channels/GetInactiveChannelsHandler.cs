namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Get inactive channels and supergroups
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.getInactiveChannels"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetInactiveChannelsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetInactiveChannels, MyTelegram.Schema.Messages.IInactiveChats>
{
    protected override Task<MyTelegram.Schema.Messages.IInactiveChats> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestGetInactiveChannels obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IInactiveChats>(new TInactiveChats { Chats = [], Dates = [], Users = [] });
    }
}