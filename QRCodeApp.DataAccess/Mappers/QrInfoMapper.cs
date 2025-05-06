using QRCodeApp.Core.Models;
using QRCodeApp.DataAccess.Entities;

namespace QRCodeApp.DataAccess.Mappers
{
    public static class QrInfoMapper
    {
        public static QrInfoEntity ToEntity(QrInfo qrcode) => new()
        {
            Id = qrcode.Id,
            CreatedAt = qrcode.CreatedAt,
            Content = qrcode.Content,
        };

        public static QrInfo ToDomain(QrInfoEntity qrcodeEntity) =>
            QrInfo.Create(qrcodeEntity.Id, qrcodeEntity.CreatedAt, qrcodeEntity.Content).qrInfo;
    }
}