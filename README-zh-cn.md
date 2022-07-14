# MyTelegram 中文|[English](README.md)

MyTelegram是使用C#编写的[Telegram服务端Api](https://core.telegram.org/api)的开源实现,支持私有化部署服务端,客户端使用官方的开源客户端

## 特性
* 支持的MTProto Layer:**`140`**  
* 支持的MTProto协议(2.0):**`Abridged`**,**`Intermediate`**,支持通过普通`Tcp`和`Websocket`进行传输  
* 私聊
* 普通群
* 超级群
* 频道
* 点对点加密聊天(Pro版本)
* 语音视频聊天(Pro版本)
* 机器人(部分支持,Pro版本)
* 2FA(Pro版本)

## 运行MyTelegram服务端
- ### 在docker中运行MyTelegram服务端
1. 下载docker-compose配置文件
    ```
    git clone https://github.com/loyldg/mytelegram.git
    ```
2. 修改**mytelegram/docker/compose/.env**文件里的服务器IP地址
3. 在compose目录里运行docker-compose
    ```
    docker compose up
    ```

- ### 手动配置MyTelegram服务端
1. 下载服务端程序,支持win-x64和linux-x64 **https://github.com/loyldg/mytelegram/releases**
2. 解压服务端程序到当前目录
3. 安装Redis服务端
4. 安装MongoDB
5. 安装RabbitMQ
6. 修改目录里的start-all.bat/start-all.sh配置信息
7. 运行start-all.bat/start-all.sh
8. 使用Telegram客户端进行测试,如果要使用[已经编译好的桌面客户端(win-x64)](https://github.com/loyldg/mytelegram/releases/download/v0.6.628/Telegram-3.7.4-win-x64.zip),需要将Gateway Server服务端的IP地址加入`%SystemRoot%/system32/drivers/etc/hosts`,比如网关服务端的IP地址为`192.168.1.100`,那么需要在hosts文件里添加以下内容  
```
192.168.1.100    demos.telegram2.com
```

## 编译Telegram客户端
所有客户端编译方法均参考官方客户端的编译方法,只需要替换掉服务器地址和RSA公钥

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
如果要使用自己的公钥,需要一并替换AuthServer的私钥:`private.pkcs8.key`

1. 编译telegram桌面客户端 https://github.com/telegramdesktop/tdesktop   
    * 切换到v3.7.4分支版本(支持Layer140的最高版本) `git checkout v3.7.4`
    * 替换**Telegram/SourceFiles/mtproto/mtproto_dc_options.cpp**里的服务器地址和RSA公钥

2. 编译telegram安卓客户端 https://github.com/DrKLO/Telegram
    * 切换到v.8.7.4分支版本(支持Layer140的最高版本) `git checkout 32aef724`
    * **Telegram\TMessagesProj\src\main\java\org\telegram\ui\Components\StickerEmptyView.java** 由于目前不支持Stickers相关功能,需要注释setSticker方法的以下代码,否则客户端会不停的调用获取Stickers的方法
        ```
        MediaDataController.getInstance(currentAccount).loadStickersByEmojiOrName(AndroidUtilities.STICKERS_PLACEHOLDER_PACK_NAME, false, set == null);
                stickerView.getImageReceiver().clearImage();
        ```
    * **Telegram\TMessagesProj\jni\tgnet\ConnectionsManager.cpp** 修改服务器IP地址和端口为GatewayServer的IP址和端口  
    * **Telegram\TMessagesProj\jni\tgnet\Datacenter.cpp** 修改decodeSimpleConfig方法内的RSA公钥
    * **Telegram\TMessagesProj\jni\tgnet\Handshake.cpp** 修改processHandshakeResponse方法内的RSA公钥和fingerprint

3. 编译telegram iOS客户端  
    * 新版本暂时未进行编译,后面会补上需要修改的内容
4. 编译telegram Web客户端
   这里以 https://github.com/morethanwords/tweb 为例,其他版本请自行尝试
   *  **src\lib\mtproto\rsaKeysManager.ts**文件里**modulus**的值替换为
        ```
        bbededbec7160c0944bd5ca54de32be45a54d808e0ab3a101cf8f3a7af6bd1802dab46bcad7d0c51eefc17f15102a05a11b656e960731770233a5358a4eb6fbf01a197dac60a0ce2ba76ddf67c1c28904c0d64bd3bb333ffcc63cffb30201e15e7a5dc8ce86b8d41c9fc69e214aa2e9b4d317847189ebe719cb7acbe954cabdec66ba6fec6ddc745fb4763f672d5d1b9cecf2ea6e8803a51222a2961bb522d85f323146dcd17a4e21ab3bd614dd88b115b272ebb8ed1e4bf915aaec70cd9f0b989643678fd72ea35d1eb8b065374239dcbe8cd839e3eb1fd8c67279b35268f8db1fc7dbc223250f448c4736dac3ceb9ab8ad0817642208687e4dfb0a08ad7cf7
       ```
    *    **src\lib\mtproto\dcConfigurator.ts**替换里面的服务器地址和域名
    *    **src\config\app.ts** 替换里面的域名
    *    **webpack.common.js** 替换里面的域名和IP地址
## 反馈
联系作者:[https://t.me/mytelegram666](https://t.me/mytelegram666)  
加入电报群:[https://t.me/+S-aNBoRvCRpPyXrR](https://t.me/+S-aNBoRvCRpPyXrR)
