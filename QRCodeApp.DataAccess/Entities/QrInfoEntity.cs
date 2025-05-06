namespace QRCodeApp.DataAccess.Entities
{
    public class QrInfoEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Content { get; set; } = string.Empty;
    }
}
