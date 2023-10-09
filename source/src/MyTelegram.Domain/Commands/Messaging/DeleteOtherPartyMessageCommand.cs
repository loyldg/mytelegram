namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteOtherPartyMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public DeleteOtherPartyMessageCommand(MessageId aggregateId, RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
        //Revoke = revoke;
    }
}