
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ComposeMessageWithAIHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestComposeMessageWithAI, MyTelegram.Schema.Messages.IComposedMessageWithAI>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IComposedMessageWithAI> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestComposeMessageWithAI obj)
    {
        throw new NotImplementedException();
    }
}

