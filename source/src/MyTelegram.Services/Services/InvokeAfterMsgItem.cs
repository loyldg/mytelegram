namespace MyTelegram.Services.Services;

public class InvokeAfterMsgItem(
    IRequestInput input,
    IObject query)
{
    public IRequestInput Input { get; } = input;

    public IObject Query { get; } = query;
}