namespace QRCodeApp.API.Contracts
{
    public record QrCodeResponse(
        Guid Id,
        string QrCodeAsBase64Format,
        DateTime CreatedAt
        );
}
