namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Associate (or remove) a personal <a href="https://corefork.telegram.org/api/channel">channel »</a>, that will be listed on our personal <a href="https://corefork.telegram.org/api/profile#personal-channel">profile page »</a>.Changing it will emit an <a href="https://corefork.telegram.org/constructor/updateUser">updateUser</a> update.
/// See <a href="https://corefork.telegram.org/method/account.updatePersonalChannel" />
///</summary>
internal sealed class UpdatePersonalChannelHandler(
    ICommandBus commandBus,
    IAccessHashHelper accessHashHelper,
    IChannelAppService channelAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdatePersonalChannel, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdatePersonalChannel obj)
    {
        long? personalChannelId = null;
        switch (obj.Channel)
        {
            case TInputChannel inputChannel:
                await accessHashHelper.CheckAccessHashAsync(input, inputChannel);
                var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);
                if (channelReadModel!.CreatorId != input.UserId)
                {
                    RpcErrors.RpcErrors400.ChatIdInvalid.ThrowRpcError();
                }

                personalChannelId = inputChannel.ChannelId;
                break;
        }

        var command =
            new UpdatePersonalChannelCommand(UserId.Create(input.UserId), input.ToRequestInfo(), personalChannelId);
        await commandBus.PublishAsync(command);

        return new TBoolTrue();
    }
}
