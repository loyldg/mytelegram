using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.AuthServer.Services;

public class Step2Helper(
    ILogger<Step2Helper> logger,
    IAesHelper aesHelper,
    IMtpHelper mtpHelper,
    IMyRsaHelper myRsaHelper,
    ICacheManager<AuthCacheItem> cacheManager,
    IRsaKeyProvider rsaKeyProvider,
    IFingerprintHelper fingerprintHelper
) : Step1To3Helper, IStep2Helper, ISingletonDependency
{
    public async Task<Step2Output> GetServerDhParamsAsync(RequestReqDHParams req)
    {
        var cacheKey = GetAuthCacheKey(req.ServerNonce);
        var cachedAuthKey = await cacheManager.GetAsync(cacheKey);
        if (cachedAuthKey == null)
        {
            throw new InvalidOperationException(
                $"GetServerDhParamsAsync: can not find cached auth key info, nonce={req.Nonce.ToHexString()}"
            );
        }

        #region check request

        CheckRequestData(cachedAuthKey.Nonce, req.Nonce);
        CheckRequestData(cachedAuthKey.ServerNonce, req.ServerNonce);
        CheckRequestData(cachedAuthKey.P, req.P);
        CheckRequestData(cachedAuthKey.Q, req.Q);

        if (cachedAuthKey.A != null)
        {
            // req_DH_params was already processed for this server_nonce. Generating a new a would make the
            // first response unusable, and every repeat would cost another RSA private key operation.
            // The client restarts the handshake after the -404 error.
            throw new InvalidOperationException(
                $"req_DH_params has already been processed, server_nonce: {req.ServerNonce.ToHexString()}");
        }

        if (req.PublicKeyFingerprint != fingerprintHelper.GetFingerprint())
        {
            throw new ArgumentException($"Unknown public key fingerprint: {req.PublicKeyFingerprint}");
        }

        var tInnerData = DeserializeRequestTpqInnerData(req, rsaKeyProvider.GetRsaPrivateKey());
        CheckRequestData(cachedAuthKey.P, tInnerData.P);
        CheckRequestData(cachedAuthKey.Q, tInnerData.Q);
        var expectedPq = (cachedAuthKey.P.ToBigEndianBigInteger() * cachedAuthKey.Q.ToBigEndianBigInteger())
            .ToByteArray(true, true);
        CheckRequestData(expectedPq, tInnerData.Pq);
        CheckRequestData(cachedAuthKey.ServerNonce, tInnerData.ServerNonce);
        CheckRequestData(cachedAuthKey.Nonce, tInnerData.Nonce);

        #endregion check request

        var isPermanentAuthKey = false;
        int? dcId = null;
        switch (tInnerData)
        {
            case TPQInnerData:
                isPermanentAuthKey = true;
                break;
            case TPQInnerDataDc:
                isPermanentAuthKey = true;
                break;
            case TPQInnerDataTemp:

                break;
            case TPQInnerDataTempDc pqInnerDataTempDc:
                dcId = pqInnerDataTempDc.Dc;
                break;
        }

        var dh2048P = AuthConsts.Dh2048P;
        var g = AuthConsts.G;
        var aAndGa = GenerateAAndGa();

        var newCachedAuthKey = cachedAuthKey with
        {
            IsPermanent = isPermanentAuthKey,
            NewNonce = tInnerData.NewNonce,
            A = aAndGa.a,
            Ga = aAndGa.ga,
            DcId = dcId
        };

        var serverDhInnerData = new TServerDHInnerData
        {
            DhPrime = dh2048P,
            G = g[0],
            GA = aAndGa.ga,
            Nonce = cachedAuthKey.Nonce,
            ServerNonce = cachedAuthKey.ServerNonce,
            ServerTime = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        await cacheManager.SetAsync(cacheKey, newCachedAuthKey, 600);

        var serverDhParams = SerializeResponse(tInnerData, serverDhInnerData);

        return new Step2Output(tInnerData.NewNonce, serverDhParams);
    }

    private IPQInnerData DeserializeRequestTpqInnerData(
        RequestReqDHParams reqDhParams,
        string privateKey
    )
    {
        // The decrypted value is always 256 bytes (left-padded with zeros, see MyRsaHelper.Decrypt).
        // Previously a value with a leading zero byte (1/256 of RSA_PAD requests) was 255 bytes long
        // and was parsed with the legacy format, which made the handshake fail.
        var decrypted = myRsaHelper.Decrypt(reqDhParams.EncryptedData, privateKey);
        try
        {
            if (TryParsePqInnerData(decrypted, out var pqInnerData))
            {
                return pqInnerData;
            }

            // Legacy RSA encryption (before RSA_PAD): SHA1(data) + data + random padding, 255 bytes.
            if (decrypted[0] == 0 && TryParsePqInnerDataOld(decrypted.AsMemory(1), out pqInnerData))
            {
                logger.LogDebug("Legacy (non RSA_PAD) p_q_inner_data accepted");
                return pqInnerData;
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(decrypted);
        }

        logger.PqInnerDataSha1HashMismatch();
        throw new ArgumentException("PQInnerData hash mismatch");
    }

    private bool TryParsePqInnerDataOld(ReadOnlyMemory<byte> innerDataWithHash, [NotNullWhen(true)] out IPQInnerData? pqInnerData)
    {
        pqInnerData = null;
        var span = innerDataWithHash.Span;
        var shaHash = span[..20];
        var buffer = innerDataWithHash[20..];
        var oldLength = buffer.Length;
        IPQInnerData tPqInnerData;
        try
        {
            tPqInnerData = buffer.Read<IPQInnerData>();
        }
        catch (Exception)
        {
            return false;
        }

        var length = oldLength - buffer.Length;
        Span<byte> calcHash = stackalloc byte[20];
        SHA1.HashData(span.Slice(20, length), calcHash);
        // The hash authenticates the data: never accept a mismatch.
        if (!shaHash.SequenceEqual(calcHash))
        {
            return false;
        }

        pqInnerData = tPqInnerData;
        return true;
    }

    private (byte[] a, byte[] ga) GenerateAAndGa()
    {
        var g = AuthConsts.G.ToBigEndianBigInteger();
        var dhPrime = AuthConsts.DhPrime;
        while (true)
        {
            var aBytes = RandomNumberGenerator.GetBytes(256);
            var a = aBytes.ToBigEndianBigInteger();

            var ga = BigInteger.ModPow(g, a, dhPrime);
            if (IsGoodGaOrGb(ga, dhPrime))
            {
                return (aBytes, ga.ToByteArray(true, true));
            }
        }
    }

    private bool TryParsePqInnerData(ReadOnlySpan<byte> keyAesEncryptedBytes, [NotNullWhen(true)] out IPQInnerData? pqInnerData)
    {
        // https://core.telegram.org/mtproto/auth_key#41-rsa-paddata-server-public-key-mentioned-above-is-implemented-as-follows
        const int tempKeyLength = 32;
        pqInnerData = null;
        // data_with_hash (224) + SHA256(aes_encrypted) (32) + calculated hash (32)
        var tempLength = keyAesEncryptedBytes.Length - tempKeyLength + 32 + 32;
        var tempBytes = ArrayPool<byte>.Shared.Rent(tempLength);

        try
        {
            var tempSpan = tempBytes.AsSpan(0, tempLength);
            var startIndex = keyAesEncryptedBytes.Length - tempKeyLength;
            var dataWithHash = tempSpan[..(keyAesEncryptedBytes.Length - tempKeyLength)];

            var aesEncryptedSha256Hash = tempSpan.Slice(startIndex, 32);
            var calculatedHash = tempSpan.Slice(startIndex + 32, 32);
            var aesEncrypted = keyAesEncryptedBytes[tempKeyLength..];

            var tempKeyXor = keyAesEncryptedBytes[..tempKeyLength];
            SHA256.HashData(aesEncrypted, aesEncryptedSha256Hash);
            var tempKey = Xor(tempKeyXor, aesEncryptedSha256Hash);
            Span<byte> tempIv1 = stackalloc byte[32];
            aesHelper.DecryptIge(aesEncrypted, tempKey, tempIv1, dataWithHash);

            var dataPaddingReversed = dataWithHash[..^32];
            var hash = dataWithHash[^32..];
            dataPaddingReversed.Reverse();
            var dataWithPadding = dataPaddingReversed;
            using var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
            hasher.AppendData(tempKey);
            hasher.AppendData(dataWithPadding);
            hasher.GetHashAndReset(calculatedHash);

            if (!hash.SequenceEqual(calculatedHash))
            {
                logger.PqInnerDataSha1HashMismatch();
                return false;
            }

            pqInnerData = tempBytes.ToTObject<IPQInnerData>();

            return true;
        }
        finally
        {
            // The buffer contains new_nonce.
            ArrayPool<byte>.Shared.Return(tempBytes, clearArray: true);
        }
    }

    private TServerDHParamsOk SerializeResponse(
        IPQInnerData pqInnerData,
        TServerDHInnerData dhInnerData
    )
    {
        return SerializeResponse(
            pqInnerData.Nonce,
            pqInnerData.NewNonce,
            pqInnerData.ServerNonce,
            dhInnerData
        );
    }

    private TServerDHParamsOk SerializeResponse(
        byte[] nonce,
        byte[] newNonce,
        byte[] serverNonce,
        TServerDHInnerData dhInnerData
    )
    {
        using var writer = new ArrayPoolBufferWriter<byte>();
        dhInnerData.Serialize(writer);

        var writtenCount = writer.WrittenCount;
        var totalLength = writtenCount + 20;// 20=SHA1 hash length
        var tempBytes = ArrayPool<byte>.Shared.Rent(totalLength + 32 + 16);
        var tempSpan = tempBytes.AsSpan();
        try
        {
            var sha1Hash = tempSpan.Slice(0, 20);
            var answerWithHashLength = writtenCount + 20;
            if (answerWithHashLength % 16 != 0)
            {
                answerWithHashLength += 16 - (answerWithHashLength % 16);
            }
            var answerWithHashSpan = tempSpan.Slice(0, answerWithHashLength);
            SHA1.HashData(writer.WrittenSpan, sha1Hash);
            sha1Hash.CopyTo(answerWithHashSpan);
            writer.WrittenSpan.CopyTo(answerWithHashSpan.Slice(20));
            // answer_with_hash := SHA1(answer) + answer + (0-15 random bytes).
            // The padding must be random: the rented buffer may contain data of other requests.
            RandomNumberGenerator.Fill(answerWithHashSpan[(20 + writtenCount)..]);
            var aesKey = new byte[32];
            Span<byte> aesIv = stackalloc byte[32];
            mtpHelper.CalcTempAesKeyData(newNonce, serverNonce, aesKey, aesIv);

            aesHelper.EncryptIge(answerWithHashSpan, aesKey, aesIv, answerWithHashSpan);

            return new TServerDHParamsOk
            {
                EncryptedAnswer = answerWithHashSpan.ToArray(),
                Nonce = nonce,
                ServerNonce = serverNonce
            };
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes, clearArray: true);
        }
    }

    private byte[] Xor(ReadOnlySpan<byte> src, ReadOnlySpan<byte> dest)
    {
        var bytes = new byte[src.Length];
        for (var i = 0; i < src.Length; i++)
        {
            bytes[i] = (byte)(src[i] ^ dest[i]);
        }

        return bytes;
    }
}