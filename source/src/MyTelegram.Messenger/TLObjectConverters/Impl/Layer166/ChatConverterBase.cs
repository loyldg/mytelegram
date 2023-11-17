namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer166;

public abstract class ChatConverterBase : LayeredConverterBase
{
    public abstract ILayeredChannel ToChannel(IChannelReadModel channelReadModel);

    public abstract ILayeredChannel ToChannel(ChannelCreatedEvent channelCreatedEvent);

    public abstract IChat ToChat(ChatCreatedEvent chatCreatedEvent);

    public abstract ILayeredChat ToChat(IChatReadModel chatReadModel);

    public abstract ILayeredChannelFull ToChatFull(IChannelFullReadModel channelFullReadModel);

    public abstract ILayeredChatFull ToChatFull(IChatReadModel chatReadModel);
}