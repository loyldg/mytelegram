// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Delete a <a href="https://corefork.telegram.org/api/channel">chat</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteChat" />
///</summary>
internal sealed class DeleteChatHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteChat, IBool>,
    Messages.IDeleteChatHandler
{
    private readonly ICommandBus _commandBus;

    public DeleteChatHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteChat obj)
    {
        var command = new DeleteChatCommand(ChatId.Create(obj.ChatId), input.ToRequestInfo());
        await _commandBus.PublishAsync(command, default);

        return new TBoolTrue();
    }
}
