namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Deletes a device by its token, stops sending PUSH-notifications to it.
/// Possible errors
/// Code Type Description
/// 400 TOKEN_INVALID The provided token is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.unregisterDevice"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UnregisterDeviceHandler(ICommandBus commandBus) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUnregisterDevice, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, RequestUnregisterDevice obj)
    {
        var command = new UnRegisterDeviceCommand(PushDeviceId.Create(obj.Token), input.ToRequestInfo(), obj.TokenType, obj.Token, obj.OtherUids.ToList());
        await commandBus.PublishAsync(command, CancellationToken.None);
        return new TBoolTrue();
    }
}