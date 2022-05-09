namespace MyTelegram.Domain.Commands.Dialog;

public class SaveDraftCommand : RequestCommand<DialogAggregate, DialogId, IExecutionResult>
{
    public SaveDraftCommand(DialogId aggregateId,
        long reqMsgId,
        string message,
        bool noWebpage,
        int date,
        int? replyToMsgId,
        byte[]? entities) : base(aggregateId, reqMsgId)
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
