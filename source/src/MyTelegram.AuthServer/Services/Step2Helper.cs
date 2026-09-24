namespace MyTelegram.AuthServer.Services;

public class Step2Helper(
    ILogger<Step2Helper> logger,
    IAesHelper aesHelper,
    IMtpHelper mtpHelper,
    IMyRsaHelper myRsaHelper,
    ICacheManager<AuthCacheItem> cacheManager,
    IRsaKeyProvider rsaKeyProvider
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

        var tInnerData = DeserializeRequestTpqInnerData(req, rsaKeyProvider.GetRsaPrivateKey());
        CheckRequestData(cachedAuthKey.P, tInnerData.P);
        CheckRequestData(cachedAuthKey.Q, tInnerData.Q);
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
        // It needs to be converted into a 256-byte array.
        // sometimes the auth key data length is only 255, and 0 needs to be added to the first position.
        var innerDataWithHash = myRsaHelper.Decrypt(reqDhParams.EncryptedData, privateKey);
        if (innerDataWithHash.Length == 256)
        {
            return ParsePqInnerData(innerDataWithHash);
        }

        return ParsePqInnerDataOld(innerDataWithHash);
    }

    private IPQInnerData ParsePqInnerDataOld(byte[] innerDataWithHash)
    {
        var span = innerDataWithHash.AsSpan();
        var shaHash = span[..20];
        var innerData = span[20..];
        ReadOnlyMemory<byte> buffer = innerDataWithHash.AsMemory(20, innerDataWithHash.Length - 20);
        var oldLength = buffer.Length;
        var tPqInnerData = buffer.Read<IPQInnerData>();
        var length = oldLength - buffer.Length;
        var realInnerData = innerData[..length];

        Span<byte> calcHash = stackalloc byte[20];
        SHA1.HashData(realInnerData, calcHash);
        if (!shaHash.SequenceEqual(calcHash))
        {
            logger.PqInnerDataSha1HashMismatch();
        }

        return tPqInnerData;
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

    private IPQInnerData ParsePqInnerData(ReadOnlySpan<byte> keyAesEncryptedBytes)
    {
        const int tempKeyLength = 32;
        var tempBytes = ArrayPool<byte>.Shared.Rent(keyAesEncryptedBytes.Length + 32 + 32);

        try
        {
            var tempSpan = tempBytes.AsSpan(0, keyAesEncryptedBytes.Length + 32 + 32 + 32);
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

                throw new ArgumentException("PQInnerData hash mismatch");
            }

            var tPqInnerData = tempBytes.ToTObject<IPQInnerData>();

            return tPqInnerData;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
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
            ArrayPool<byte>.Shared.Return(tempBytes);
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