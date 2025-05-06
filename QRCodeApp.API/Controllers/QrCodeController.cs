using Microsoft.AspNetCore.Mvc;
using QRCodeApp.API.Contracts;
using QRCodeApp.Application.Exceptions;
using QRCodeApp.Core.Abstractions.Interfaces;
using QRCodeApp.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace QRCodeApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QrCodeController(IQrCodesService qrCodesService) : ControllerBase
    {
        private readonly IQrCodesService _qrCodesService = qrCodesService;

        [HttpPost]
        public async Task<ActionResult<Guid>> GenerateQrCode([FromBody] QrCodeRequest request)
        {
            var (qrcodeDetails, error) = QrDetails.Create(
                request.StartDate,
                request.EndDate,
                request.GuestCount,
                request.Password);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            await _qrCodesService.GenerateQrCode(qrcodeDetails);

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<QrCodeResponse>>> GetAllQrs()
        {
            var response = await _qrCodesService.GetAllQrInfos();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QrCodeResponse>> GetQrById(Guid id)
        {
            try
            {
                var response = await _qrCodesService.GetQrInfoById(id);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult<QrCodeResponse>> GetByPages(
            [FromQuery, Range(1, int.MaxValue, ErrorMessage = "Page должен быть не меньше 1")] int page = 1, 
            [FromQuery, Range(1,1000, ErrorMessage = "PageSize должен быть в пределах 1 < x < 1000")] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }
            var response = await _qrCodesService.GetQrInfosByPage(page, pageSize);
            return Ok(response);
        }

    }
}
