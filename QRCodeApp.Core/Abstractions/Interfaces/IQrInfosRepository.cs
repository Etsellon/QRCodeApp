using QRCodeApp.Core.Models;

namespace QRCodeApp.Core.Abstractions.Interfaces
{
    public interface IQrInfosRepository
    {
        Task<Guid> Add(QrInfo qrcode);
        Task<List<QrInfo>> GetAll();
        Task<QrInfo?> GetById(Guid id);
        Task<List<QrInfo>> GetByPage(int page, int pageSize);
    }
}