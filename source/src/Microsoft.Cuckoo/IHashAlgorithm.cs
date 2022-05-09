// -----------------------------------------------------------------------
// <copyright file="IHashAlgorithm.cs" company="Microsoft">
//     Copyright (c) Microsoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.Cuckoo
{
  /// <summary>
  /// Describes a type used for hashing a value for the cuckoo filter.
  /// </summary>
  public interface IHashAlgorithm
  {
    /// <summary>
    /// Returns a hash of the value.
    /// </summary>
    /// <param name="target">Target array to hash to</param>
    /// <param name="value">Value to hash</param>
    /// <param name="hashLength">Number of bytes to write into the target.</param>
    void Hash(byte[] target, byte[] value, int hashLength);
  }
}
