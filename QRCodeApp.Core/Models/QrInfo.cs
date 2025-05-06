namespace QRCodeApp.Core.Models
{
    public class QrInfo
    {
        private QrInfo(Guid id, DateTime createdAt, string content)
        {
            Id = id;
            CreatedAt = createdAt;
            Content = content;
        }

        public Guid Id { get; }

        public DateTime CreatedAt { get; }

        public string Content { get; } = string.Empty;

        public static (QrInfo qrInfo, string Error) Create
            (Guid id, DateTime createdAt, string content)
        {
            var errors = new List<String>();

            /** Валидация **/
            if (createdAt > DateTime.UtcNow) 
                errors.Add("Дата создания не должна быть в будущем.");
            if (string.IsNullOrEmpty(content) || content.Length < 10) 
                errors.Add("Поле <content> не должно быть пустым.");

            var qrInfo = new QrInfo(id, createdAt, content);

            if (errors.Count > 0)
                return (qrInfo, string.Join("; ", errors));

            return (qrInfo, string.Empty);
        }
    }
}
