namespace MyTelegram.Domain.Commands.Dialog;

public class SaveDraftCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public SaveDraftCommand(DialogId aggregateId,
        RequestInfo requestInfo,
        string message,
        bool noWebpage,
        int date,
        int? replyToMsgId,
        byte[]? entities) : base(aggregateId, requestInfo)
    {
        Message = message;
        NoWebpage = noWebpage;
        Date = date;
        ReplyToMsgId = replyToMsgId;
        Entities = entities;
    }

    public int Date { get; }
    public byte[]? Entities { get; }
    public string Message { get; }
    public bool NoWebpage { get; }
    public int? ReplyToMsgId { get; }
}
