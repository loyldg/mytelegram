namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IMediaHelper
{
    Task<IEncryptedFile> SaveEncryptedFileAsync(long reqMsgId,
        IInputEncryptedFile encryptedFile);
    Task<IPhoto> SavePhotoAsync(long reqMsgId, long fileId, bool hasVideo, double? videoStartTs, int parts, string? name, string? md5);
    Task<IMessageMedia> SaveMediaAsync(IInputMedia media);
    MessageType GeMessageType(IMessageMedia? media);
}