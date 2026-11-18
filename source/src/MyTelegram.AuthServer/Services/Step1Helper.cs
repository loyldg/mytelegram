using System.Buffers.Binary;

namespace MyTelegram.AuthServer.Services;

public class Step1Helper(
    IFingerprintHelper fingerprintHelper,
    IOptionsMonitor<MyTelegramAuthServerOptions> options) : IStep1Helper, ISingletonDependency
{
    public Step1Output GetResponse(byte[] nonce)
    {
        byte[] p;
        byte[] q;
        byte[] pq;
        if (options.CurrentValue.UseRandomPq)
        {
            (p, q, pq) = GenerateRandomPq();
        }
        else
        {
            p = AuthConsts.P;
            q = AuthConsts.Q;
            pq = AuthConsts.Pq;
        }

        var serverNonce = RandomNumberGenerator.GetBytes(16);

        var publicKeyFingerprint = fingerprintHelper.GetFingerprint();
        var resPq = new TResPQ
        {
            Nonce = nonce,
            ServerNonce = serverNonce,
            Pq = pq,
            ServerPublicKeyFingerprints = new TVector<long> { publicKeyFingerprint }
        };

        return new Step1Output(p, q, serverNonce, resPq);
    }

    /// <summary>
    /// https://core.telegram.org/mtproto/auth_key: pq is the product of two different odd primes, normally
    /// less than or equal to 2^63-1. Both primes are taken from [2^30, 2^31), so pq is in [2^60, 2^62) and the
    /// big-endian encodings have no leading zero (p and q: 4 bytes, pq: 8 bytes), as the client sends them back.
    /// </summary>
    internal static (byte[] P, byte[] Q, byte[] Pq) GenerateRandomPq()
    {
        var p = GenerateRandomPrime();
        uint q;
        do
        {
            q = GenerateRandomPrime();
        } while (q == p);

        if (p > q)
        {
            (p, q) = (q, p);
        }

        var pBytes = new byte[4];
        var qBytes = new byte[4];
        var pqBytes = new byte[8];
        BinaryPrimitives.WriteUInt32BigEndian(pBytes, p);
        BinaryPrimitives.WriteUInt32BigEndian(qBytes, q);
        BinaryPrimitives.WriteUInt64BigEndian(pqBytes, (ulong)p * q);

        return (pBytes, qBytes, pqBytes);
    }

    private static uint GenerateRandomPrime()
    {
        while (true)
        {
            var candidate = (uint)RandomNumberGenerator.GetInt32(1 << 30, int.MaxValue) | 1;
            if (IsPrime(candidate))
            {
                return candidate;
            }
        }
    }

    /// <summary>
    /// Deterministic Miller-Rabin for 32-bit numbers (bases 2, 7 and 61 are sufficient below 4,759,123,141).
    /// </summary>
    internal static bool IsPrime(uint n)
    {
        if (n < 2)
        {
            return false;
        }

        foreach (var smallPrime in (ReadOnlySpan<uint>)[2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 61])
        {
            if (n == smallPrime)
            {
                return true;
            }

            if (n % smallPrime == 0)
            {
                return false;
            }
        }

        var d = n - 1;
        var s = 0;
        while ((d & 1) == 0)
        {
            d >>= 1;
            s++;
        }

        foreach (var a in (ReadOnlySpan<uint>)[2, 7, 61])
        {
            var x = ModPow(a, d, n);
            if (x == 1 || x == n - 1)
            {
                continue;
            }

            var composite = true;
            for (var r = 1; r < s; r++)
            {
                x = (uint)((ulong)x * x % n);
                if (x == n - 1)
                {
                    composite = false;
                    break;
                }
            }

            if (composite)
            {
                return false;
            }
        }

        return true;
    }

    private static uint ModPow(uint value, uint exponent, uint modulus)
    {
        ulong result = 1;
        ulong current = value % modulus;
        while (exponent > 0)
        {
            if ((exponent & 1) != 0)
            {
                result = result * current % modulus;
            }

            current = current * current % modulus;
            exponent >>= 1;
        }

        return (uint)result;
    }
}
