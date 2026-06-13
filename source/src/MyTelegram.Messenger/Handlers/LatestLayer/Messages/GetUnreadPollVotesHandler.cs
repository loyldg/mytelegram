
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetUnreadPollVotesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetUnreadPollVotes, MyTelegram.Schema.Messages.IMessages>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetUnreadPollVotes obj)
    {
        throw new NotImplementedException();
    }
}

