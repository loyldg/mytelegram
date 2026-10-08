
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ComposeRichMessageWithAIHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestComposeRichMessageWithAI, MyTelegram.Schema.Messages.IComposedRichMessageWithAI>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IComposedRichMessageWithAI> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestComposeRichMessageWithAI obj)
    {
        throw new NotImplementedException();
    }
}

