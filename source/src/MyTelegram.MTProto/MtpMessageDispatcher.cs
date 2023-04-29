namespace MyTelegram.MTProto;

public class MtpMessageDispatcher : IMtpMessageDispatcher
{
    private readonly IMessageQueueProcessor<EncryptedMessage> _encryptedMessageQueueProcessor;
    private readonly IMessageQueueProcessor<UnencryptedMessage> _unencryptedMessageQueueProcessor;

    public MtpMessageDispatcher(IMessageQueueProcessor<UnencryptedMessage> unencryptedMessageQueueProcessor,
        IMessageQueueProcessor<EncryptedMessage> encryptedMessageQueueProcessor)
    {
        _unencryptedMessageQueueProcessor = unencryptedMessageQueueProcessor;
        _encryptedMessageQueueProcessor = encryptedMessageQueueProcessor;
    }

    public Task DispatchAsync(IMtpMessage message)
    {
        switch (message)
        {
            case EncryptedMessage encryptedMessage:
                _encryptedMessageQueueProcessor.Enqueue(encryptedMessage, encryptedMessage.AuthKeyId);
                break;
            case UnencryptedMessage unencryptedMessage:
                _unencryptedMessageQueueProcessor.Enqueue(unencryptedMessage, unencryptedMessage.AuthKeyId);
                break;
        }

        return Task.CompletedTask;
    }
}
