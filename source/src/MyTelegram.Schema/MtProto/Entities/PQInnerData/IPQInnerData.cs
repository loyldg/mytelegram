// ReSharper disable All

namespace MyTelegram.Schema;

public interface IPQInnerData : IObject
{
    byte[] Pq { get; set; }
    byte[] P { get; set; }
    byte[] Q { get; set; }
    /// <summary>
    /// int128
    /// </summary>
    byte[] Nonce { get; set; }

    /// <summary>
    /// int128
    /// </summary>
    byte[] ServerNonce { get; set; }

    /// <summary>
    /// int256
    /// </summary>
    byte[] NewNonce { get; set; }
    //int Dc { get; set; }
}
