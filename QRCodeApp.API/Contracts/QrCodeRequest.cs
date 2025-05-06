using System.ComponentModel.DataAnnotations;

namespace QRCodeApp.API.Contracts
{
    public record QrCodeRequest(
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required] int GuestCount,
        [Required] string Password
        );
}
