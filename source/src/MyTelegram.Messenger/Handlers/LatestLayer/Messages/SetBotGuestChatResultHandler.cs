
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SetBotGuestChatResultHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetBotGuestChatResult, MyTelegram.Schema.IInputBotInlineMessageID>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IInputBotInlineMessageID> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetBotGuestChatResult obj)
    {
        throw new NotImplementedException();
    }
}

