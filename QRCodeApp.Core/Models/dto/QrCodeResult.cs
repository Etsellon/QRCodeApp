namespace QRCodeApp.Core.Models.dto
{
    public class QrCodeResult
    {
        public Guid Id { get; set; }
        public string QrCodeAsBase64 { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
