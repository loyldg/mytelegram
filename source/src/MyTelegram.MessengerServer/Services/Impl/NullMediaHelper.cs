namespace MyTelegram.MessengerServer.Services.Impl;

public class NullMediaHelper : IMediaHelper
{
    public Task<IEncryptedFile> SaveEncryptedFileAsync(long reqMsgId,
        IInputEncryptedFile encryptedFile)
    {
        throw new NotImplementedException();
    }

    public Task<IPhoto> SavePhotoAsync(long reqMsgId,
        long fileId,
        bool hasVideo,
        double? videoStartTs,
        int parts,
        string? name,
        string? md5)
    {
        throw new NotImplementedException();
    }

    public Task<IMessageMedia> SaveMediaAsync(IInputMedia media)
    {
        throw new NotImplementedException();
    }

    public MessageType GeMessageType(IMessageMedia? media)
    {
        throw new NotImplementedException();
    }
}