
namespace MyTelegram.Messenger.Handlers.LatestLayer.AiCompose;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteToneHandler : RpcResultObjectHandler<MyTelegram.Schema.AiCompose.RequestDeleteTone, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.AiCompose.RequestDeleteTone obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}

