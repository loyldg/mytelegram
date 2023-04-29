//using MyTelegram.Handlers.Folders;
//using MyTelegram.Schema.Folders;

//namespace MyTelegram.MessengerServer.Handlers.Impl.Folders;

//public class DeleteFolderHandler : RpcResultObjectHandler<RequestDeleteFolder, IUpdates>,
//    IDeleteFolderHandler, IProcessedHandler
//{
//    private readonly ICommandBus _commandBus;

//    public DeleteFolderHandler(ICommandBus commandBus)
//    {
//        _commandBus = commandBus;
//    }

//    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
//        RequestDeleteFolder obj)
//    {
//        var command =
//            new DeleteDialogFilterCommand(DialogFilterId.Create(input.UserId, obj.FolderId), input.ToRequestInfo());
//        await _commandBus.PublishAsync(command, default);

//        var updates = new TUpdateShort
//        {
//            Date = CurrentDate,
//            Update = new TUpdateDialogFilter
//            {
//                Filter = null,
//                Id = obj.FolderId
//            }
//        };
//        return updates;
//    }
//}
