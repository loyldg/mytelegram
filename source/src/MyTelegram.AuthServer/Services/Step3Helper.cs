namespace MyTelegram.AuthServer.Services;

public class Step3Helper(
    IAesHelper aesHelper,
    IHashHelper hashHelper,
    IMtpHelper mtpHelper,
    ILogger<Step3Helper> logger,
    IAuthKeyIdHelper authKeyIdHelper,
    ICacheManager<AuthCacheItem> cacheManager
) : Step1To3Helper, IStep3Helper, ISingletonDependency
{
    public async Task<Step3Output> SetClientDhParamsAnswerAsync(RequestSetClientDHParams req)
    {
        var cacheKey = GetAuthCacheKey(req.ServerNonce);
        var cachedAuthKey = await cacheManager.GetAsync(cacheKey);
        if (cachedAuthKey?.A == null)
        {
            throw new InvalidOperationException(
                $"Cannot find cached auth key info, nonce: {req.Nonce.ToHexString()}"
            );
        }

        if (cachedAuthKey.NewNonce == null)
        {
            throw new ArgumentNullException(nameof(cachedAuthKey.NewNonce));
        }

        CheckRequestData(cachedAuthKey.Nonce, req.Nonce, "Nonce");
        CheckRequestData(cachedAuthKey.ServerNonce, req.ServerNonce, "ServerNonce");

        var aesKey = new byte[32];
        Span<byte> aesIv = stackalloc byte[32];
        mtpHelper.CalcTempAesKeyData(
           cachedAuthKey.NewNonce,
           cachedAuthKey.ServerNonce,
           aesKey,
           aesIv
       );
        var dhInnerData = DeserializeRequest(req, aesKey, aesIv);

        CheckRequestData(cachedAuthKey.Nonce, dhInnerData.Nonce, "Nonce");
        CheckRequestData(cachedAuthKey.ServerNonce, dhInnerData.ServerNonce, "ServerNonce");
        var a = cachedAuthKey.A;
        var gb = dhInnerData.GB;

        var authKeyBytes = BigInteger
            .ModPow(gb.ToBigEndianBigInteger(), a.ToBigEndianBigInteger(), AuthConsts.DhPrime)
            .ToByteArray(true, true)
            .ToBytes256();

        var dto = new Step3Output(
            authKeyIdHelper.GetAuthKeyId(authKeyBytes),
            authKeyBytes,
            mtpHelper.ComputeSalt(cachedAuthKey.NewNonce, dhInnerData.ServerNonce),
            cachedAuthKey.IsPermanent,
            CreateDhGenOkAnswer(req, cachedAuthKey.NewNonce, authKeyBytes),
            cachedAuthKey.DcId
        );

        return dto;
    }

    private TClientDHInnerData DeserializeRequest(
        RequestSetClientDHParams serverDhParams,
        byte[] key,
        ReadOnlySpan<byte> iv
    )
    {
        var tempBytes = ArrayPool<byte>.Shared.Rent(serverDhParams.EncryptedData.Length + 20);
        var tempSpan = tempBytes.AsSpan(0, serverDhParams.EncryptedData.Length + 20);
        var answerWithHash = tempSpan.Slice(0, serverDhParams.EncryptedData.Length);
        try
        {
            aesHelper.DecryptIge(
                serverDhParams.EncryptedData,
                key,
                iv,
                answerWithHash
            );

            var hash = answerWithHash[..20];
            var answer = answerWithHash[20..];
            ReadOnlyMemory<byte> buffer = tempBytes.AsMemory(20, answerWithHash.Length - 20);
            var oldLength = buffer.Length;
            var obj = buffer.Read<TClientDHInnerData>();
            var consumed = oldLength-buffer.Length;
            var paddingCount = (int)(answer.Length - consumed);
            var data = answer[..^paddingCount];
            var calcHash = tempSpan[^20..];
            SHA1.HashData(data, calcHash);
            if (!hash.SequenceEqual(calcHash))
            {
                logger.AnswerSha1HashMismatch();
                throw new ArgumentException($"Answer sha1 hash mismatch.");
            }

            return obj;
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(tempBytes);
        }
    }

    private TDhGenOk CreateDhGenOkAnswer(
        RequestSetClientDHParams req,
        byte[] newNonce,
        byte[] authKey
    )
    {
        var newNonceHash1 = CreateNewNonceHash(newNonce, authKey, 1);

        return new TDhGenOk
        {
            Nonce = req.Nonce,
            ServerNonce = req.ServerNonce,
            NewNonceHash1 = newNonceHash1
        };
    }

    //private TDhGenRetry CreateDhGenRetryRetryAnswer(RequestSetClientDHParams req, byte[] newNonce, byte[] authKey)
    //{
    //    var newNonceHash2 = CreateNewNonceHash(newNonce, authKey, 2);

    //    return new TDhGenRetry
    //    {
    //        Nonce = req.Nonce,
    //        ServerNonce = req.ServerNonce,
    //        NewNonceHash2 = newNonceHash2
    //    };
    //}

    private byte[] CreateNewNonceHash(byte[] newNonce, byte[] authKey, byte n)
    {
        // https://core.telegram.org/mtproto/auth_key#9-server-responds-in-one-of-three-ways
        // new_nonce_hash1, new_nonce_hash2, and new_nonce_hash3 are obtained as the 128 lower - order bits of SHA1 of
        // the byte string derived from the new_nonce string by adding a single byte with the value of 1, 2, or 3, and followed
        // by another 8 bytes with auth_key_aux_hash.Different values are required to prevent an intruder from changing server
        // response dh_gen_ok into dh_gen_retry.

        var authKeyAuxHash = SHA1.HashData(authKey).AsSpan(0, 8);
        Span<byte> newNonceWithAuxHashBytes = stackalloc byte[newNonce.Length + 1 + 8];
        newNonce.CopyTo(newNonceWithAuxHashBytes);
        newNonceWithAuxHashBytes[newNonce.Length] = n;
        authKeyAuxHash.CopyTo(newNonceWithAuxHashBytes[(newNonce.Length + 1)..]);
        var newNonceHashN = hashHelper.Sha1(newNonceWithAuxHashBytes);

        return newNonceHashN.AsSpan(4).ToArray();
    }
}