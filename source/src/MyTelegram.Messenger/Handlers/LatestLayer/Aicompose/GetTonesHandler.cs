
using MyTelegram.Schema.AiCompose;

namespace MyTelegram.Messenger.Handlers.LatestLayer.AiCompose;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetTonesHandler : RpcResultObjectHandler<MyTelegram.Schema.AiCompose.RequestGetTones, MyTelegram.Schema.AiCompose.ITones>, IObjectHandler
{
    protected override async Task<MyTelegram.Schema.AiCompose.ITones> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.AiCompose.RequestGetTones obj)
    {
        return new TTones
        {
            Tones = [

            ],
            Users = [],
        };
    }
}

