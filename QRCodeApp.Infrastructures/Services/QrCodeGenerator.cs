
using QRCodeApp.Core.Abstractions.Interfaces;
using QRCoder;
using System.Text.Json;

namespace QRCodeApp.Infrastructures.Services
{
    public class QrCodeGenerator : IQrCodeGenerator
    {
        public string GenerateQrCodeAsBase64(string content)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new Base64QRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }
    }
}
