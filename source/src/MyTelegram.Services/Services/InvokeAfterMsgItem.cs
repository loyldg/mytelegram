using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

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