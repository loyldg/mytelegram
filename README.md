# MyTelegram  [中文](README-zh-cn.md)|English
MyTelegram is [Telegram server side api](https://core.telegram.org/api) implementation written in c#,support private deployment

## Features
* Supported MTProto Layer:**`143`**  
* Supported [MTProto Protocol(2.0)](https://core.telegram.org/mtproto):**`Abridged`**,**`Intermediate`**  
* Private chat
* Group chat
* Supergroup chat
* Channel
* End-to-end-encryption chat(Pro version)
* Voice/video call(Pro version)
* Bot(Partial support,Pro version)
* 2FA(Pro version)

## Run MyTelegram server
- ### Run MyTelegram server with docker
1. Download docker-compose configuration files
    ```
    https://github.com/loyldg/mytelegram/blob/dev/docker/compose/docker-compose.yml
    https://github.com/loyldg/mytelegram/blob/dev/docker/compose/.env
    ```
2. Change the ip address configuration in **.env**
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
6. Modify server configuration in start-all.bat/start-all.sh
7. Run start-all.bat/start-all.sh
8. Test using Telegram client,if you want to use the [compiled  tdesktop client(win-x64)](https://github.com/loyldg/mytelegram/releases/download/v0.7.720/Telegram-4.0.2-win-x64.zip),add the gateway server's ip address into hosts file(`%SystemRoot%/system32/drivers/etc/hosts`),for example your gateway server ip is `192.168.1.100`,add the following line into hosts file
```
192.168.1.100    demos.telegram2.com
```

## Build Telegram client
For all clients,only need to replace server address and RSA public key,and then follow the offical documents to build it  

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

1. Build telegram desktop client https://github.com/telegramdesktop/tdesktop   
    * Replace server address and RSA public key in **Telegram/SourceFiles/mtproto/mtproto_dc_options.cpp**

2. Build telegram android client https://github.com/DrKLO/Telegram
    * **Telegram\TMessagesProj\src\main\java\org\telegram\ui\Components\StickerEmptyView.java** Mytelegram not support Stickers in current version,need comment the following code in method `setSticker`  
        ```
        MediaDataController.getInstance(currentAccount).loadStickersByEmojiOrName(AndroidUtilities.STICKERS_PLACEHOLDER_PACK_NAME, false, set == null);
                stickerView.getImageReceiver().clearImage();
        ```
    * **Telegram\TMessagesProj\jni\tgnet\ConnectionsManager.cpp** Replace server ip address and port using gateway server's ip and port  
    * **Telegram\TMessagesProj\jni\tgnet\Datacenter.cpp** Replace RSA public key in method `decodeSimpleConfig`
    * **Telegram\TMessagesProj\jni\tgnet\Handshake.cpp** Replace RSA public key and fingerprint in method `processHandshakeResponse`  

3. Build telegram iOS client  
    * Coming soon
4. Build telegram Web client
https://github.com/morethanwords/tweb 
   *  **src\lib\mtproto\rsaKeysManager.ts** Replace **modulus** with the following value
        ```
        bbededbec7160c0944bd5ca54de32be45a54d808e0ab3a101cf8f3a7af6bd1802dab46bcad7d0c51eefc17f15102a05a11b656e960731770233a5358a4eb6fbf01a197dac60a0ce2ba76ddf67c1c28904c0d64bd3bb333ffcc63cffb30201e15e7a5dc8ce86b8d41c9fc69e214aa2e9b4d317847189ebe719cb7acbe954cabdec66ba6fec6ddc745fb4763f672d5d1b9cecf2ea6e8803a51222a2961bb522d85f323146dcd17a4e21ab3bd614dd88b115b272ebb8ed1e4bf915aaec70cd9f0b989643678fd72ea35d1eb8b065374239dcbe8cd839e3eb1fd8c67279b35268f8db1fc7dbc223250f448c4736dac3ceb9ab8ad0817642208687e4dfb0a08ad7cf7
       ```
    *    **src\lib\mtproto\dcConfigurator.ts** Replace the domain and server address
    *    **src\config\app.ts** Replace the domain
    *    **webpack.common.js** Replace the domain and server address
## Feedback
Contact author:[https://t.me/mytelegram666](https://t.me/mytelegram666)  
Join telegram group:[https://t.me/+S-aNBoRvCRpPyXrR](https://t.me/+S-aNBoRvCRpPyXrR)