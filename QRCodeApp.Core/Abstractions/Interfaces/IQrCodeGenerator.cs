namespace QRCodeApp.Core.Abstractions.Interfaces
{
    public interface IQrCodeGenerator
    {
        string GenerateQrCodeAsBase64(string qrcodeDetails);
    }
}