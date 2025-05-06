# QRCodeApp


Приложение для генерации и хранения qr-кодовю Позволяет создавать, сохранять и просматривать историю созданных qr кодов. Это простое web.api приложение, которое использует asp.net core 8, postgresql, ef core, docker.

## Функциональность
- генерация qr кода - происходит на основе данных "заезда" - по сути создаем пропуск на основе следующих данных:
	- дата начала действия пропуска
	- дата окончания действия пропуска
	- количество гостей
	- пароль
- получение qr-кодов через api 
	- возвращаем изображение в формате base64 а так же метаданные:
		- дату создания пропуска
		- его uuid
- хранение в базе данных
	- все qr сохраняем в базе данных, включая метаданные
		- uuid - уникальный айдишник
		- дата создания
		- данные из пропуска в json формате
- просмотр истории
	- получение всех qr-кодов
	- получение по uuid 
	- получение с пагинацией

## Быстрый запуск
1. запуск postgres через docker
```bash
cd QRCodeApp.API
docker compose up -d
```

2. применение миграции
```bash
dotnet ef database update -s QRCodeApp.API -p QRCodeApp.DataAccess
```

3. запуск приложения
```bash
dotnet run --project QRCodeApp.API
```

## Swagger документация:
http://localhost:5123/swagger/

## примеры запросов

- генерация qr-кода `POST /QrCode`
```json
{
  "startDate": "2025-05-06T22:33:06.228Z",
  "endDate": "2025-05-10T22:33:06.228Z",
  "guestCount": 3,
  "password": "mySecretPassword"
}
```

- получение всех qr-кодов `GET /QrCode/all`
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "qrCodeAsBase64Format": "aGVsbG8gd29ybGQh",
    "createdAt": "2025-05-06T22:36:19.179Z"
  },
  ...
]
```

- получение qr по uuid `GET /QrCode/{uuid}`
```json
{
  "id": "uuid",
  "qrCodeAsBase64Format": "aGVsbG8gd29ybGQh",
  "createdAt": "2025-05-06T22:38:58.588Z"
}
```

- пагинация `GET /QrCode/paged?page=1&pageSize=10`
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "qrCodeAsBase64Format": "aGVsbG8gd29ybGQh",
    "createdAt": "2025-05-06T22:36:19.179Z"
  },
  ...
]
```
