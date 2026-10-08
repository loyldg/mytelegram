
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class TranslateRichMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestTranslateRichMessage, MyTelegram.Schema.Messages.ITranslatedRichMessage>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.ITranslatedRichMessage> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestTranslateRichMessage obj)
    {
        throw new NotImplementedException();
    }
}

