// ReSharper disable All

namespace MyTelegram.Schema;

public interface IFileHash : IObject
{
    int Offset { get; set; }
    int Limit { get; set; }
    byte[] Hash { get; set; }

}
