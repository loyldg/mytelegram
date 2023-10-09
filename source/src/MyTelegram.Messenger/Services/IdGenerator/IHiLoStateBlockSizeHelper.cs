namespace MyTelegram.Messenger.Services.IdGenerator;

public interface IHiLoStateBlockSizeHelper
{
    int GetBlockSize(IdType idType);
}