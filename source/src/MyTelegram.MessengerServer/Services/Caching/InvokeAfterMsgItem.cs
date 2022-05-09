namespace MyTelegram.MessengerServer.Services.Caching;

public class InvokeAfterMsgItem
{
    public InvokeAfterMsgItem(IRequestInput input,
        IObject query)
    {
        Input = input;
        Query = query;
    }

    public IRequestInput Input { get; }

    public IObject Query { get; }
}