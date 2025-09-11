namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Delete contacts by phone number
/// See <a href="https://corefork.telegram.org/method/contacts.deleteByPhones" />
///</summary>
internal sealed class DeleteByPhonesHandler(ICommandBus commandBus, IQueryProcessor queryProcessor)
    : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestDeleteByPhones, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestDeleteByPhones obj)
    {
        // Return results immediately and delete contacts in the background
        Task.Run(async () =>
        {
            var contactReadModels =
                await queryProcessor.ProcessAsync(new GetContactsByPhonesQuery(input.UserId, obj.Phones.ToList()));
            var requestInfo = input.ToRequestInfo() with { ReqMsgId = 0 };
            foreach (var contactReadModel in contactReadModels)
            {
                var command = new DeleteContactCommand(ContactId.Create(input.UserId, contactReadModel.TargetUserId),
                    requestInfo, contactReadModel.TargetUserId);
                await commandBus.PublishAsync(command, default);
            }
        });

        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
