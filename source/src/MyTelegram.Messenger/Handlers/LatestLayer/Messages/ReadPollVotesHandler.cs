
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ReadPollVotesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadPollVotes, MyTelegram.Schema.Messages.IAffectedHistory>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReadPollVotes obj)
    {
        throw new NotImplementedException();
    }
}

