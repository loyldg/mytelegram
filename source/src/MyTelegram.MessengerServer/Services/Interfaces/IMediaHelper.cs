namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IMediaHelper
{
    MessageType GeMessageType(IMessageMedia? media);

    Task<IEncryptedFile> SaveEncryptedFileAsync(long reqMsgId,
        IInputEncryptedFile encryptedFile);

    Task<IMessageMedia> SaveMediaAsync(IInputMedia media);

    Task<IPhoto> SavePhotoAsync(long reqMsgId,
        long fileId,
        bool hasVideo,
        double? videoStartTs,
        int parts,
        string? name,
        string? md5);
}
