namespace MyTelegram.Messenger.Handlers.LatestLayer.Users;
/// <summary>
/// Check whether we can write to the specified users, used to implement bulk checks for <a href="https://corefork.telegram.org/api/privacy#require-premium-for-new-non-contact-users">Premium-only messages »</a> and <a href="https://corefork.telegram.org/api/paid-messages">paid messages »</a>.For each input user, returns a <a href="https://corefork.telegram.org/type/RequirementToContact">RequirementToContact</a> constructor (at the same offset in the vector) containing requirements to contact them.
/// <para><c>See <a href="https://corefork.telegram.org/method/users.getRequirementsToContact"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetRequirementsToContactHandler : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetRequirementsToContact, TVector<MyTelegram.Schema.IRequirementToContact>>
{
    protected override Task<TVector<MyTelegram.Schema.IRequirementToContact>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Users.RequestGetRequirementsToContact obj)
    {
        return Task.FromResult<TVector<MyTelegram.Schema.IRequirementToContact>>([]);
    }
}