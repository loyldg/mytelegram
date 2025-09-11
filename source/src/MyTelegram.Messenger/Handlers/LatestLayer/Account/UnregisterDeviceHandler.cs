namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Deletes a device by its token, stops sending PUSH-notifications to it.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TOKEN_INVALID The provided token is invalid.
/// See <a href="https://corefork.telegram.org/method/account.unregisterDevice" />
///</summary>
internal sealed class UnregisterDeviceHandler(ICommandBus commandBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUnregisterDevice, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUnregisterDevice obj)
    {
        var command = new UnRegisterDeviceCommand(PushDeviceId.Create(obj.Token),
            input.ToRequestInfo(),
            obj.TokenType,
            obj.Token,
            obj.OtherUids.ToList());
        await commandBus.PublishAsync(command, CancellationToken.None);

        return new TBoolTrue();
    }
}
