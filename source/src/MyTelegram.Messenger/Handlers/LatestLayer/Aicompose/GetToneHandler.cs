
using MyTelegram.Schema.AiCompose;

namespace MyTelegram.Messenger.Handlers.LatestLayer.AiCompose;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetToneHandler : RpcResultObjectHandler<MyTelegram.Schema.AiCompose.RequestGetTone, MyTelegram.Schema.AiCompose.ITones>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.AiCompose.ITones> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.AiCompose.RequestGetTone obj)
    {
        return Task.FromResult<ITones>(new TTones
        {
            Users = [],
            Tones = []
        });
    }
}

