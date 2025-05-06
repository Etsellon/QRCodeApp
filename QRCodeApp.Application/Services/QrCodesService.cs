using QRCodeApp.Application.Exceptions;
using QRCodeApp.Core.Abstractions.Interfaces;
using QRCodeApp.Core.Models;
using QRCodeApp.Core.Models.dto;
using System.Text.Json;

namespace QRCodeApp.Application.Services
{
    public class QrCodesService(IQrInfosRepository qrcodesRepository, IQrCodeGenerator qrCodeGenerator) : IQrCodesService
    {
        private readonly IQrInfosRepository _qrcodesRepository = qrcodesRepository;
        private readonly IQrCodeGenerator _qrCodeGenerator = qrCodeGenerator;


        public async Task<Guid> GenerateQrCode(QrDetails qrcodeDetails)
        {
            var contents = JsonSerializer.Serialize(qrcodeDetails);

            var id = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;

            await _qrcodesRepository.Add(QrInfo.Create(id, createdAt, contents).qrInfo);

            return id;
        }

        public async Task<QrCodeResult> GetQrInfoById(Guid id)
        {
            var result = await _qrcodesRepository.GetById(id) 
                ?? throw new NotFoundException("QR-код с указанным ID не найден.");
            var qrcodeAsBase64 = _qrCodeGenerator.GenerateQrCodeAsBase64(result.Content);

            return new QrCodeResult
            {
                Id = result.Id,
                QrCodeAsBase64 = qrcodeAsBase64,
                CreatedAt = result.CreatedAt

            };
        }
        public async Task<List<QrCodeResult>> GetAllQrInfos()
        {
            var qrinfos = await _qrcodesRepository.GetAll();

            var qrCodeResults = qrinfos.Select(qrInfo => new QrCodeResult
            {
                Id = qrInfo.Id,
                CreatedAt = qrInfo.CreatedAt,
                QrCodeAsBase64 = _qrCodeGenerator.GenerateQrCodeAsBase64(qrInfo.Content)
            })
                .ToList();

            return qrCodeResults;
        }

        public async Task<List<QrCodeResult>> GetQrInfosByPage(int page, int pageSize)
        {
            var qrinfos = await _qrcodesRepository.GetByPage(page, pageSize);

            var qrCodeResults = qrinfos.Select(qrInfo => new QrCodeResult
            {
                Id = qrInfo.Id,
                CreatedAt = qrInfo.CreatedAt,
                QrCodeAsBase64 = _qrCodeGenerator.GenerateQrCodeAsBase64(qrInfo.Content)
            })
                .ToList();

            return qrCodeResults;
        }
    }
}
