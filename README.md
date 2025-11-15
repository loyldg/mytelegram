# MyTelegram

[![API Layer](https://img.shields.io/badge/API_Layer-216-blueviolet)](https://corefork.telegram.org/methods)
[![MTProto](https://img.shields.io/badge/MTProto_Protocol-2.0-green)](https://corefork.telegram.org/mtproto/)
[![MyTelegram Channel](https://img.shields.io/badge/Subscribe-_MyTelegram_Channel-0088cc)](https://t.me/+9wMJrMqLTIoyYzM8)
[![MyTelegram Discussion Group](https://img.shields.io/badge/Join_-MyTelegram_Discussion_Group-0088cc)](https://t.me/+S-aNBoRvCRpPyXrR)

MyTelegram is a self-hosted C# implementation of the Telegram server-side API, designed for private deployments and extensibility.

## Supported Features

### Open Source Features
- API Layer: `216`
- MTProto Transports: `Abridged`, `Intermediate`
- Private Chat
- Supergroup Chat
- Channel

### Pro Version Features
- End-to-End Encrypted Chat
- Voice & Video Calls
- Bot Support (Partial)
- 2FA (Two-Factor Authentication)
- Stickers
- Reactions
- Star Gifts
- Forum Topics
- Themes & Wallpapers
- Auto-Delete Messages
- Scheduled Messages
- Chatlist
- Telegram Business
- Stories
- Email Login
- Email Sender
- Direct Messages
- Push Server (Firebase)

---

## Running the MyTelegram Server

### Run with Docker

1. Download the Docker Compose configuration files:

https://raw.githubusercontent.com/loyldg/mytelegram/dev/docker/compose/docker-compose.yml  
https://raw.githubusercontent.com/loyldg/mytelegram/dev/docker/compose/.env

2. Edit `.env` and replace `192.168.1.100` with your own server IP address.

3. Start the server:

```
mkdir -p ./data/mytelegram
chmod -R a+w ./data/mytelegram
docker compose up
```
4. Default verification code (for testing only): `22222`

## Building Docker Images

### Linux / amd64
`./build-all-amd64.sh`

### Linux / arm64
`./build-all-arm64.sh`

## MyTelegram Clients

| Platform | Repository |
|----------|------------|
| Desktop (TDesktop) | https://github.com/loyldg/mytelegram-tdesktop |
| Android | https://github.com/loyldg/mytelegram-android |
| iOS | https://github.com/loyldg/mytelegram-iOS |
| WebK | https://github.com/loyldg/mytelegram-webk |
| WebA | https://github.com/loyldg/mytelegram-weba |

### Configure Clients
1. Clone the client source code.  
2. Search for `192.168.1.100` in all files and replace it with your own server IP.

---

## Support MyTelegram

If you find MyTelegram helpful, please consider giving the project a ⭐.


## Feedback

- Contact author: https://t.me/mytelegram666  
- MyTelegram Channel: https://t.me/+9wMJrMqLTIoyYzM8  
- Discussion Group: https://t.me/+S-aNBoRvCRpPyXrR
