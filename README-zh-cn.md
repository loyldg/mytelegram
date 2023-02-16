# MyTelegram 中文|[English](README.md)
[![API Layer](https://img.shields.io/badge/API_Layer-148-blueviolet)](https://corefork.telegram.org/methods)
[![MTProto](https://img.shields.io/badge/MTProto_Protocol-2.0-green)](https://corefork.telegram.org/mtproto/)
[![Support Chat](https://img.shields.io/badge/Chat_with_us-on_Telegram-0088cc)](https://t.me/+S-aNBoRvCRpPyXrR)

MyTelegram是使用C#编写的[Telegram服务端Api](https://core.telegram.org/api)的开源实现,支持私有化部署服务端,客户端使用官方的开源客户端

## 演示地址
[https://webz.mytelegram.top/](https://webz.mytelegram.top/)  
**Verification Code:22222**
## 特性
* 支持的Api Layer:**`143`~`148`**  
开源版本:**`148`**  
Pro版本:**`143`**~**`148`**  
Pro版本支持不同Layer的客户端通信,客户端可以多版本共存,开源版本仅支持单一的Layer,只支持某一个版本  
* 支持的[传输协议](https://corefork.telegram.org/mtproto/mtproto-transports):**`Abridged`**,**`Intermediate`**(支持[Transport error](https://corefork.telegram.org/mtproto/mtproto-transports#transport-errors)和[Transport obfuscation](https://corefork.telegram.org/mtproto/mtproto-transports#transport-obfuscation))  
* 私聊
* 普通群
* 超级群
* 频道
* 点对点加密聊天(Pro版本)
* 语音视频聊天(Pro版本)
* 机器人(部分支持,Pro版本)
* 2FA(Pro版本)
* 表情包(Pro版本)
* Reactions(Pro版本)
* ForumTopics(Pro版本)

## 编译MyTelegram服务端
1. 安装[.NET SDK 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. 使用Powershell或者Linux终端执行以下代码 
```
git clone https://github.com/loyldg/mytelegram.git 
cd mytelegram/source
dotnet restore
cd .\src\MyTelegram.MessengerServer.Abp\
dotnet publish -c Release -p:PublishSingleFile=true 

```

## 运行MyTelegram服务端
- ### 在docker中运行MyTelegram服务端
1. 下载docker-compose配置文件,将其放到同一个目录里
    ```
    https://github.com/loyldg/mytelegram/blob/dev/docker/compose/docker-compose.yml
    https://github.com/loyldg/mytelegram/blob/dev/docker/compose/.env
    ```
2. 修改 `.env` 文件里的服务器IP地址,替换里面的`127.0.0.1`为你的Gateway服务器的IP地址
3. 在docker-compose.yml文件所在的目录运行下面的命令
    ```
    docker compose up
    ```

- ### 手动配置MyTelegram服务端
1. 下载服务端程序,支持win-x64和linux-x64 **https://github.com/loyldg/mytelegram/releases**
2. 解压服务端程序到当前目录
3. 安装Redis服务端
4. 安装MongoDB
5. 安装RabbitMQ
6. 安装Minio
7. 修改目录里的`start-all.bat`/`start-all.ps1`/`start-all.sh`配置信息,替换里面的`127.0.0.1`为网关服务器的IP地址,Redis/MongoDB/RabbitMQ根据你的相关信息进行调整
8. 运行`start-all.bat`/`start-all.ps1`/`start-all.sh`

- ### 使用编译好的客户端进行测试
1. 下载[已编译好的TDesktop客户端(4.2.2)](https://github.com/loyldg/mytelegram/releases/download/v0.11.1102/Telegram-4.2.2-x64.zip)
2. 将Gateway server的IP地址加入`%SystemRoot%/system32/drivers/etc/hosts`,比如Gateway Server服务端的IP地址为`192.168.1.100`,那么需要在hosts文件里添加以下内容  
```
192.168.1.100    demos.telegram2.com
```
3. 运行客户端
4. 默认验证码为`22222`


## 编译Telegram客户端
所有客户端编译方法均参考官方客户端的编译方法,只需要替换掉服务器地址和RSA公钥
> 注意:服务端监听的默认TCP端口为20443,默认HTTPS端口为30443,默认HTTP端口为30444,编译各个客户端的时候,需要使用对应的端口,桌面客户端/Android客户端/iOS客户端,使用TCP端口20443,Web客户端使用30443(HTTPS)或者30443(HTTP)

默认公钥:
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
PKCS#1格式
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
默认公钥的Fingerprint为: **`0xce27f5081215bda4`** (安卓客户端里的Fingerprint需要提前计算好值并进行替换)  
- ### 编译桌面客户端 [TDesktop](https://github.com/telegramdesktop/tdesktop)
1. 切换到Layer146所在的分支(4.2.x版本)
2. 替换**Telegram/SourceFiles/mtproto/mtproto_dc_options.cpp**里的服务器地址,端口,RSA公钥

- ### 编译[安卓客户端](https://github.com/DrKLO/Telegram)
1. 切换到Layer146所在的分支(9.x版本)

2. **Telegram\TMessagesProj\src\main\java\org\telegram\ui\Components\StickerEmptyView.java** 由于目前不支持Stickers相关功能,需要注释setSticker方法的以下代码,否则客户端会不停的调用获取Stickers的方法
    ```java
    MediaDataController.getInstance(currentAccount).loadStickersByEmojiOrName(AndroidUtilities.STICKERS_PLACEHOLDER_PACK_NAME, false, set == null);
            stickerView.getImageReceiver().clearImage();
    ```
3. **Telegram\TMessagesProj\jni\tgnet\ConnectionsManager.cpp** 修改服务器IP地址和端口为GatewayServer的IP址和端口  

4. **Telegram\TMessagesProj\jni\tgnet\Datacenter.cpp** 修改decodeSimpleConfig方法内的RSA公钥
5. **Telegram\TMessagesProj\jni\tgnet\Handshake.cpp** 修改processHandshakeResponse方法内的RSA公钥和fingerprint

- ### 编译[iOS客户端](https://github.com/TelegramMessenger/Telegram-iOS) 
  新版本暂时未进行编译,后面会补上需要修改的内容

- ### 编译Web客户端 [Telegram Web K](https://github.com/morethanwords/tweb)
1.  **src\lib\mtproto\rsaKeysManager.ts**文件里**modulus**的值替换为
  ```
        bbededbec7160c0944bd5ca54de32be45a54d808e0ab3a101cf8f3a7af6bd1802dab46bcad7d0c51eefc17f15102a05a11b656e960731770233a5358a4eb6fbf01a197dac60a0ce2ba76ddf67c1c28904c0d64bd3bb333ffcc63cffb30201e15e7a5dc8ce86b8d41c9fc69e214aa2e9b4d317847189ebe719cb7acbe954cabdec66ba6fec6ddc745fb4763f672d5d1b9cecf2ea6e8803a51222a2961bb522d85f323146dcd17a4e21ab3bd614dd88b115b272ebb8ed1e4bf915aaec70cd9f0b989643678fd72ea35d1eb8b065374239dcbe8cd839e3eb1fd8c67279b35268f8db1fc7dbc223250f448c4736dac3ceb9ab8ad0817642208687e4dfb0a08ad7cf7
  ```
2.  **src\lib\mtproto\dcConfigurator.ts**替换里面的服务器地址和域名
3.  **src\config\app.ts** 替换里面的域名
4.  **webpack.common.js** 替换里面的域名和IP地址  
- ### 编译Web客户端 [Telegram Web Z](https://github.com/Ajaxy/telegram-tt)
1. **/src/lib/gramjs/network/Authenticator.ts** 47行开始
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
以上代码修改为
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
2. **/src/lib/gramjs/extensions/PromisedWebSockets.js** 69行  
将`port === 443`修改为`port === 30443`
``` typescript
getWebSocketLink(ip, port, testServers) {
        if (port === 443) {
            return `wss://${ip}:${port}/apiws${testServers ? '_test' : ''}`;
        } else {
            return `ws://${ip}:${port}/apiws${testServers ? '_test' : ''}`;
        }
    }
```
3. **src\lib\gramjs\crypto\RSA.ts** SERVER_KEYS替换为以下内容
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
29行:`zws2.web.telegram.org`替换为Gateway服务器的IP地址  
30行:`[2001:67c:4e8:f002::a]`替换为Gateway服务器的IPV6地址,如果Gateway服务器没有启用IPV6,这里也可以直接替换为IPV4地址  
70行:`useWSS: false`(HTTP) `useWSS: true`(HTTPS),根据情况进行修改   
224行:`this._args.useWSS ? 443 : 80);`修改为`this._args.useWSS ? 30443 : 30444);`

5. **src\api\gramjs\methods\users.ts**   
146行:
```
const result = await invokeRequest(new GramJs.users.GetUsers({
    id: users.map(({ id, accessHash }) => buildInputPeer(id, accessHash)),
  }));
```
替换为如下代码:
```
const result = await invokeRequest(new GramJs.users.GetUsers({
    id: users.map(({ id, accessHash }) => new GramJs.InputUser({ userId: BigInt(id), accessHash: BigInt(accessHash!) })),
  }));
```
6. **src\lib\gramjs\Utils.js**
640行:  
```
function getDC(dcId, downloadDC = false) {
    // TODO Move to external config
```
修改为以下代码,并替换gateway服务器IP地址
```
function getDC(dcId, downloadDC = false) {
return { id: 2, ipAddress: '这里替换为gateway服务器Ip地址', port: 30443 };
```
7. **src\api\gramjs\gramjsBuilders\index.ts**  
31行:`const CHANNEL_ID_MIN_LENGTH = 11;`修改为`const CHANNEL_ID_MIN_LENGTH = 13;`  
51行:`chatOrUserId <= -1000000000`修改为`chatOrUserId <= -800000000000`

## 支持MyTelegram
如果你喜欢这个项目,请点一个⭐

## 反馈
联系作者:[https://t.me/mytelegram666](https://t.me/mytelegram666)  
加入电报群:[https://t.me/+S-aNBoRvCRpPyXrR](https://t.me/+S-aNBoRvCRpPyXrR)
