// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateColor" />
///</summary>
internal sealed class UpdateColorHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateColor, IBool>,
    Account.IUpdateColorHandler
{
    private readonly ICommandBus _commandBus;
    public UpdateColorHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateColor obj)
    {
        var color = new PeerColor(obj.Color, obj.BackgroundEmojiId);
        var command =
            new UpdateUserColorCommand(UserId.Create(input.UserId), input.ToRequestInfo(), color, obj.ForProfile);
        await _commandBus.PublishAsync(command, default);
        return new TBoolTrue();
    }
}
