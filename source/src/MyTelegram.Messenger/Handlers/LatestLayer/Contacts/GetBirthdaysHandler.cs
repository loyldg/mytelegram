namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Fetch all users with birthdays that fall within +1/-1 days, relative to the current day: this method should be invoked by clients every 6-8 hours, and if the result is non-empty, it should be used to appropriately update locally cached birthday information in <a href="https://corefork.telegram.org/constructor/user">user</a>.<code>birthday</code>.<a href="https://corefork.telegram.org/api/profile#birthday">See here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/contacts.getBirthdays" />
///</summary>
internal sealed class GetBirthdaysHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetBirthdays, MyTelegram.Schema.Contacts.IContactBirthdays>
{
    protected override Task<MyTelegram.Schema.Contacts.IContactBirthdays> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetBirthdays obj)
    {
        return Task.FromResult<MyTelegram.Schema.Contacts.IContactBirthdays>(new TContactBirthdays
        {
            Contacts = [],
            Users = []
        });
    }
}
