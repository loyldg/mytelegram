
namespace MyTelegram.Messenger.Handlers.LatestLayer.AiCompose;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class CreateToneHandler : RpcResultObjectHandler<MyTelegram.Schema.AiCompose.RequestCreateTone, MyTelegram.Schema.IAiComposeTone>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IAiComposeTone> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.AiCompose.RequestCreateTone obj)
    {
        throw new NotImplementedException();
    }
}

