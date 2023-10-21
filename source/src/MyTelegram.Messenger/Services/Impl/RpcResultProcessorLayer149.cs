namespace MyTelegram.Messenger.Services.Impl;

public class RpcResultProcessorLayer149 : RpcResultProcessorLayer143, IRpcResultProcessorLayer149
{
    public RpcResultProcessorLayer149(ILayeredService<IMessageConverter> layeredMessageService,
        ILayeredService<IUserConverter> layeredUserService,
        ILayeredService<IChatConverter> layeredChatService) : base(layeredMessageService, layeredUserService, layeredChatService)
    {
    }

    public override int Layer => Layers.Layer149;

    protected override IMessages ToChannelMessages(IEnumerable<IChat> chats,
        IList<IMessage> messages,
        IEnumerable<IUser> users,
        int channelPts,
        int offsetIdOffset)
    {
        //return new TChannelMessages
        //{
        //    Chats = new TVector<IChat>(chats),
        //    Messages = new TVector<IMessage>(messages),
        //    Users = new TVector<IUser>(users),
        //    Pts = channelPts,
        //    Count = messages.Count,
        //    OffsetIdOffset = offsetIdOffset,
        //    //Topics = new()
        //};

        return new Schema.Messages.TChannelMessages
        {
            Chats = new TVector<IChat>(chats),
            Messages = new TVector<IMessage>(messages),
            Users = new TVector<IUser>(users),
            Pts = channelPts,
            Count = messages.Count,
            OffsetIdOffset = offsetIdOffset,
            Topics = new()
        };
    }
}