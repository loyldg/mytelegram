
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetPersonalChannelHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPersonalChannelHistory, MyTelegram.Schema.Messages.IMessages>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetPersonalChannelHistory obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IMessages>(new TChannelMessages
        {
            Chats = [],
            Messages = [],
            Users = [],
            Topics = []
        });
    }
}

