using QRCodeApp.Core.Models;
using QRCodeApp.Core.Models.dto;

namespace QRCodeApp.Core.Abstractions.Interfaces
{
    public interface IQrCodesService
    {
        Task<Guid> GenerateQrCode(QrDetails qrcodeDetails);
        Task<List<QrCodeResult>> GetAllQrInfos();
        Task<QrCodeResult> GetQrInfoById(Guid id);
        Task<List<QrCodeResult>> GetQrInfosByPage(int page, int pageSize);
    }
}