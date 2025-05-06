namespace QRCodeApp.Core.Models
{
    public class QrDetails
    {
        private QrDetails(DateTime startDate, DateTime endDate, int guessCount, string password)
        {
            StartDate = startDate;
            EndDate = endDate;
            GuestCount = guessCount;
            Password = password;
        }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int GuestCount { get; }
        public string Password { get; } = string.Empty;

        public static (QrDetails qrDetails, string error) Create(DateTime startDate, DateTime endDate, int guessCount, string password)
        {
            var errors = new List<string>();

            /** Валидация **/
            if (endDate < startDate)
                errors.Add("Дата начала не должна быть позже даты окончания.");

            if (endDate < DateTime.UtcNow)
                errors.Add("Дата конца не должна быть в прошлом.");

            if (guessCount <= 0)
                errors.Add("Количество гостей должно быть больше нуля.");

            if (string.IsNullOrEmpty(password))
                errors.Add("Пароль должен быть указан.");

            var qrDetails = new QrDetails(startDate, endDate, guessCount, password);

            if (errors.Count > 0)
                return (qrDetails, string.Join("; ", errors));

            return (qrDetails, string.Empty);
        }
    }
}
