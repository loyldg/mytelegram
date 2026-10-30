
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetOnrampAvailabilityHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetOnrampAvailability, MyTelegram.Schema.IOnrampAvailability>
{
    protected override Task<MyTelegram.Schema.IOnrampAvailability> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetOnrampAvailability obj)
    {
        throw new NotImplementedException();
    }
}

