
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetOnrampLimitsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetOnrampLimits, MyTelegram.Schema.IOnrampLimits>
{
    protected override Task<MyTelegram.Schema.IOnrampLimits> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetOnrampLimits obj)
    {
        throw new NotImplementedException();
    }
}

