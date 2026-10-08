
namespace MyTelegram.Messenger.Handlers.LatestLayer.AiCompose;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SaveToneHandler : RpcResultObjectHandler<MyTelegram.Schema.AiCompose.RequestSaveTone, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.AiCompose.RequestSaveTone obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}

