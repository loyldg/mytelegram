// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on nearest data center.
/// See <a href="https://corefork.telegram.org/constructor/NearestDc" />
///</summary>
[JsonDerivedType(typeof(TNearestDc), nameof(TNearestDc))]
public interface INearestDc : IObject
{
    ///<summary>
    /// Country code determined by geo-ip
    ///</summary>
    string Country { get; set; }

    ///<summary>
    /// Number of current data center
    ///</summary>
    int ThisDc { get; set; }

    ///<summary>
    /// Number of nearest data center
    ///</summary>
    int NearestDc { get; set; }
}
