# MyTelegram

[![API Layer](https://img.shields.io/badge/API_Layer-211-blueviolet)](https://corefork.telegram.org/methods)
[![MTProto](https://img.shields.io/badge/MTProto_Protocol-2.0-green)](https://corefork.telegram.org/mtproto/)
[![MyTelegram Channel](https://img.shields.io/badge/Subscribe-_MyTelegram_Channel-0088cc)](https://t.me/+9wMJrMqLTIoyYzM8)
[![MyTelegram Discussion Group](https://img.shields.io/badge/Join_-MyTelegram_Discussion_Group-0088cc)](https://t.me/+S-aNBoRvCRpPyXrR)
MyTelegram is telegram server side api implementation written in c#, support private deployment

## Features

- API Layer: **`211`**
- [MTProto transports](https://corefork.telegram.org/mtproto/mtproto-transports): **`Abridged`**,**`Intermediate`**
- Private chat
- Group chat
- Supergroup chat
- Channel
- End-to-end-encryption chat(Pro version)
- Voice/video call(Pro version)
- Bot(Partial support, Pro version)
- 2FA(Pro version)
- Stickers(Pro version)
- Reactions(Pro version)
- ForumTopics(Pro version)
- Themes/Wallpapers/Auto-Delete Messages/Scheduled Messages/Chatlist/Telegram Business/Stories/Email Login/Email Sender/Direct messages/Push Server(Firebase) (Pro version)

## Run MyTelegram server

- ### Run MyTelegram server with docker

1. Download docker-compose configuration files
   ```
   https://raw.githubusercontent.com/loyldg/mytelegram/dev/docker/compose/docker-compose.yml

   https://raw.githubusercontent.com/loyldg/mytelegram/dev/docker/compose/.env
   ```
2. Replace `192.168.1.100` with your own server IP in `.env`
3. Run the following command in the directory where the docker-compose.yml file is located
   ```
      mkdir -p ./data/mytelegram
      chmod -R a+w ./data/mytelegram
      docker compose up
   ```
4. Default verification code is `22222`

## Build MyTelegram server docker images

- ### Linux/amd64 (build)
```
build-all-amd64.sh
```
- ### Linux/arm64 (build)
```
build-all-arm64.sh
```

## MyTelegram clients
[TDesktop for mytelegram](https://github.com/loyldg/mytelegram-tdesktop)

[Android client for mytelegram](https://github.com/loyldg/mytelegram-android)

[iOS client for mytelegram](https://github.com/loyldg/mytelegram-iOS)

[WebK for mytelegram](https://github.com/loyldg/mytelegram-webk)

[WebA for mytelegram](https://github.com/loyldg/mytelegram-weba)

1. Git clone the client source code
2. Search for the keyword **192.168.1.100** in all files, then replace it with your own IP.


## Support MyTelegram

Love MyTelegram? Please give a star to this repository ⭐

## Feedback

Contact author: [https://t.me/mytelegram666](https://t.me/mytelegram666)  

MyTelegram channel: [https://t.me/+9wMJrMqLTIoyYzM8](https://t.me/+9wMJrMqLTIoyYzM8)

Mytelegram discussion group: [https://t.me/+S-aNBoRvCRpPyXrR](https://t.me/+S-aNBoRvCRpPyXrR)

