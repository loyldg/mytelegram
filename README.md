# MyTelegram  [中文](README-zh-cn.md)|English
[![API Layer](https://img.shields.io/badge/API_Layer-148-blueviolet)](https://corefork.telegram.org/methods)
[![MTProto](https://img.shields.io/badge/MTProto_Protocol-2.0-green)](https://corefork.telegram.org/mtproto/)
[![Support Chat](https://img.shields.io/badge/Chat_with_us-on_Telegram-0088cc)](https://t.me/+S-aNBoRvCRpPyXrR)

MyTelegram is [Telegram server side api](https://core.telegram.org/api) implementation written in c#,support private deployment
## Demo
[https://webz.mytelegram.top/](https://webz.mytelegram.top/)  
**Verification Code:22222**
## Features
* Supported Api Layer:**`143`~`148`**  
Open source version: **`148`**  
Pro version:**`143`**~**`148`**  
Pro version supports client communication with different layers,open source version only supports single layer  
* [MTProto transports](https://corefork.telegram.org/mtproto/mtproto-transports):**`Abridged`**,**`Intermediate`**(also support [Transport error](https://corefork.telegram.org/mtproto/mtproto-transports#transport-errors) and [Transport obfuscation](https://corefork.telegram.org/mtproto/mtproto-transports#transport-obfuscation))  
* Private chat
* Group chat
* Supergroup chat
* Channel
* End-to-end-encryption chat(Pro version)
* Voice/video call(Pro version)
* Bot(Partial support,Pro version)
* 2FA(Pro version)
* Stickers(Pro version)
* Reactions(Pro version)
* ForumTopics(Pro version)

## Build MyTelegram Server
1. Install [.NET SDK 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. Execute the following command using powershell or Linux terminal
```
git clone https://github.com/loyldg/mytelegram.git 
cd mytelegram/source
dotnet restore
cd .\src\MyTelegram.MessengerServer.Abp\
dotnet publish -c Release -p:PublishSingleFile=true
```

## Run MyTelegram server
- ### Run MyTelegram server with docker
1. Download docker-compose configuration files
    ```
    https://github.com/loyldg/mytelegram/blob/dev/docker/compose/docker-compose.yml
    https://github.com/loyldg/mytelegram/blob/dev/docker/compose/.env
    ```
2. Change the IP address configuration in **.env**,replace `127.0.0.1` with the IP address of gateway server
3. Run the following command in the directory where the docker-compose.yml file is located
    ```
    docker compose up
    ```

- ### Run MyTelegram manually
1. Download mytelegram server from [https://github.com/loyldg/mytelegram/releases](https://github.com/loyldg/mytelegram/releases),supported platform:`win-x64` and `linux-x64`
2. Uncompress downloaded file
3. Install Redis
4. Install MongoDB
5. Intall RabbitMQ
6. Install Minio
7. Modify server configuration in `start-all.bat`/`start-all.ps1`/`start-all.sh`,replace `127.0.0.1` with the IP address of gateway server
8. Run `start-all.bat`/`start-all.ps1`/`start-all.sh`

- ### Test with compiled client
1. Download [TDesktop client(4.2.2)](https://github.com/loyldg/mytelegram/releases/download/v0.11.1102/Telegram-4.2.2-x64.zip)
2. Add the IP address of the gateway server to hosts file(`%SystemRoot%/system32/drivers/etc/hosts`),for example, the IP address of the gateway server is `192.168.1.100`,add the following line to hosts file
```
192.168.1.100    demos.telegram2.com
```
3. Run Telegram.exe
4. Default verification code is `22222`

## Build Telegram client
For all clients,only need to replace server address and RSA public key,and then follow the offical documents to build it  
> Tips:The default TCP port is 20443,default HTTPS port is 30443,default HTTP port is 30444,you need this port when compile the clients,Tdesktop client/Android client/iOS client use tcp port 20443,Web client use 30443(HTTPS)/30444(HTTP)
Public key:
```
-----BEGIN RSA PUBLIC KEY-----
MIIBCgKCAQEAu+3tvscWDAlEvVylTeMr5FpU2AjgqzoQHPjzp69r0YAtq0a8rX0M
Ue78F/FRAqBaEbZW6WBzF3AjOlNYpOtvvwGhl9rGCgziunbd9nwcKJBMDWS9O7Mz
/8xjz/swIB4V56XcjOhrjUHJ/GniFKoum00xeEcYnr5xnLesvpVMq97Ga6b+xt3H
RftHY/Zy1dG5zs8upuiAOlEiKilhu1IthfMjFG3NF6TiGrO9YU3YixFbJy67jtHk
v5FarscM2fC5iWQ2eP1y6jXR64sGU3QjncvozYOePrH9jGcnmzUmj42x/H28IjJQ
9EjEc22sPOuauK0IF2QiCGh+TfsKCK189wIDAQAB
-----END RSA PUBLIC KEY-----
```
PKCS#1 format
```
-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAu+3tvscWDAlEvVylTeMr
5FpU2AjgqzoQHPjzp69r0YAtq0a8rX0MUe78F/FRAqBaEbZW6WBzF3AjOlNYpOtv
vwGhl9rGCgziunbd9nwcKJBMDWS9O7Mz/8xjz/swIB4V56XcjOhrjUHJ/GniFKou
m00xeEcYnr5xnLesvpVMq97Ga6b+xt3HRftHY/Zy1dG5zs8upuiAOlEiKilhu1It
hfMjFG3NF6TiGrO9YU3YixFbJy67jtHkv5FarscM2fC5iWQ2eP1y6jXR64sGU3Qj
ncvozYOePrH9jGcnmzUmj42x/H28IjJQ9EjEc22sPOuauK0IF2QiCGh+TfsKCK18
9wIDAQAB
-----END PUBLIC KEY-----
```

The default publick key's fingerprint is **`0xce27f5081215bda4`**(Android client need calculate fingerpint and replace using this value),
if you want to use your own public key,replace `private.pkcs8.key` in auth folder

- ### Build [Tdesktop client](https://github.com/telegramdesktop/tdesktop)
1. Switch to the branch which the layer is 146(version 4.2.x)
2. Replace server addresses,port and RSA public key in **Telegram/SourceFiles/mtproto/mtproto_dc_options.cpp**

- ### Build [Android client](https://github.com/DrKLO/Telegram)
1. Switch to the branch which the layer is 146(version 9.x)
2. **Telegram\TMessagesProj\src\main\java\org\telegram\ui\Components\StickerEmptyView.java** Mytelegram not support Stickers in current version,need comment the following code in method `setSticker`  
   ``` java
   MediaDataController.getInstance(currentAccount).loadStickersByEmojiOrName(AndroidUtilities.STICKERS_PLACEHOLDER_PACK_NAME, false, set == null);
                stickerView.getImageReceiver().clearImage();     
   ```
3. **Telegram\TMessagesProj\jni\tgnet\ConnectionsManager.cpp** Replace server ip address and port using gateway server's ip and port  
4. **Telegram\TMessagesProj\jni\tgnet\Datacenter.cpp** Replace RSA public key in method `decodeSimpleConfig`
5. **Telegram\TMessagesProj\jni\tgnet\Handshake.cpp** Replace RSA public key and fingerprint in method `processHandshakeResponse`  

- ### Build [iOS client](https://github.com/TelegramMessenger/Telegram-iOS)
  Coming soon
- ### Build [Telegram Web K](https://github.com/morethanwords/tweb)
1. **src\lib\mtproto\rsaKeysManager.ts** Replace **modulus** with the following value
  ```
  bbededbec7160c0944bd5ca54de32be45a54d808e0ab3a101cf8f3a7af6bd1802dab46bcad7d0c51eefc17f15102a05a11b656e960731770233a5358a4eb6fbf01a197dac60a0ce2ba76ddf67c1c28904c0d64bd3bb333ffcc63cffb30201e15e7a5dc8ce86b8d41c9fc69e214aa2e9b4d317847189ebe719cb7acbe954cabdec66ba6fec6ddc745fb4763f672d5d1b9cecf2ea6e8803a51222a2961bb522d85f323146dcd17a4e21ab3bd614dd88b115b272ebb8ed1e4bf915aaec70cd9f0b989643678fd72ea35d1eb8b065374239dcbe8cd839e3eb1fd8c67279b35268f8db1fc7dbc223250f448c4736dac3ceb9ab8ad0817642208687e4dfb0a08ad7cf7      
  ```      
2. **src\lib\mtproto\dcConfigurator.ts** Replace the domain and server address
3. **src\config\app.ts** Replace the domain
4. **webpack.common.js** Replace the domain and server address

- ### Build [Telegram Web Z](https://github.com/Ajaxy/telegram-tt)
1. **/src/lib/gramjs/network/Authenticator.ts** Line 47
```typescript
const pqInnerData = new Api.PQInnerData({
        pq: Helpers.getByteArray(pq), // unsigned
        p: pBuffer,
        q: qBuffer,
        nonce: resPQ.nonce,
        serverNonce: resPQ.serverNonce,
        newNonce,
    }).getBytes();
```
replace the code with the following code
```typescript
const pqInnerData = new Api.PQInnerDataDc({
        pq: Helpers.getByteArray(pq), // unsigned
        p: pBuffer,
        q: qBuffer,
        nonce: resPQ.nonce,
        serverNonce: resPQ.serverNonce,
        newNonce,
        dc: 2
    }).getBytes();
```

2. **/src/lib/gramjs/extensions/PromisedWebSockets.js** Line 69
Replace `port === 443` with `port === 30443`
``` typescript
getWebSocketLink(ip, port, testServers) {
        if (port === 443) {
            return `wss://${ip}:${port}/apiws${testServers ? '_test' : ''}`;
        } else {
            return `ws://${ip}:${port}/apiws${testServers ? '_test' : ''}`;
        }
    }
```
3. **src\lib\gramjs\crypto\RSA.ts** Replace SERVER_KEYS with the following code
```typescript
export const SERVER_KEYS = [
    {
        fingerprint: bigInt('-3591632762792723036'),
        n: bigInt('bbededbec7160c0944bd5ca54de32be45a54d808e0ab3a101cf8f3a7af6bd1802dab46bcad7d0c51eefc17f15102a05a11b656e960731770233a5358a4eb6fbf01a197dac60a0ce2ba76ddf67c1c28904c0d64bd3bb333ffcc63cffb30201e15e7a5dc8ce86b8d41c9fc69e214aa2e9b4d317847189ebe719cb7acbe954cabdec66ba6fec6ddc745fb4763f672d5d1b9cecf2ea6e8803a51222a2961bb522d85f323146dcd17a4e21ab3bd614dd88b115b272ebb8ed1e4bf915aaec70cd9f0b989643678fd72ea35d1eb8b065374239dcbe8cd839e3eb1fd8c67279b35268f8db1fc7dbc223250f448c4736dac3ceb9ab8ad0817642208687e4dfb0a08ad7cf7',16),
        e: 65537
    }
].reduce((acc, { fingerprint, ...keyInfo }) => {
    acc.set(fingerprint.toString(), keyInfo);
    return acc;
}, new Map<string, { n: bigInt.BigInteger; e: number }>());
```

4. **src\lib\gramjs\client\TelegramClient.js**  
Line 29:Replace `zws2.web.telegram.org` with the IP address of the gateway server  
Line 30:Replace `[2001:67c:4e8:f002::a]` with the IPV6 address of the gateway server,if gateway server IPV6 not enable,you can also replace the value with the IPV4 address of gateway server  
Line 70:`useWSS: false`(HTTP) `useWSS: false`(HTTPS)  
Line 224:Replace `this._args.useWSS ? 443 : 80` with `this._args.useWSS ? 30443 : 30444`
5. **src\api\gramjs\methods\users.ts**    
Line 146:
```
const result = await invokeRequest(new GramJs.users.GetUsers({
    id: users.map(({ id, accessHash }) => buildInputPeer(id, accessHash)),
  }));
```
Replace with the following code:
```
const result = await invokeRequest(new GramJs.users.GetUsers({
    id: users.map(({ id, accessHash }) => new GramJs.InputUser({ userId: BigInt(id), accessHash: BigInt(accessHash!) })),
  }));
```

6. **src\lib\gramjs\Utils.js**
Line 640:  
```
function getDC(dcId, downloadDC = false) {
    // TODO Move to external config
```
Replace with the following code,and replace ipAddress
```
function getDC(dcId, downloadDC = false) {
return { id: 2, ipAddress: 'IPAddress of your gateway server', port: 30443 };
```
7. **src\api\gramjs\gramjsBuilders\index.ts**  
Line 31:Change `const CHANNEL_ID_MIN_LENGTH = 11;` to  `const CHANNEL_ID_MIN_LENGTH = 13;`  
Line 51:Change `chatOrUserId <= -1000000000` to `chatOrUserId <= -800000000000`

## Support MyTelegram
Love MyTelegram? Please give a star to this repository ⭐

## Feedback
Contact author:[https://t.me/mytelegram666](https://t.me/mytelegram666)  
Join telegram group:[https://t.me/+S-aNBoRvCRpPyXrR](https://t.me/+S-aNBoRvCRpPyXrR)