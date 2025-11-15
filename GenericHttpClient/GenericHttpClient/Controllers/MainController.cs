using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.GetDailyRates;
using GenericHttpClient.Domain.Models.GetEmailReputation;
using GenericHttpClient.Domain.Models.ValidateVat;
using Microsoft.AspNetCore.Mvc;

namespace GenericHttpClient.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly IMainService _mainService;

        public MainController(IMainService mainService)
        {
            _mainService = mainService;
        }

        [HttpPost("getEmailReputation")]
        public async Task<ActionResult<GetEmailReputationResponseDto>> GetEmailReputation(GetEmailReputationRequest request)
        {
            var response = await _mainService.GetEmailReputationAsync(request);
            return Ok(response);
        }
        
        [HttpPost("validateVat")]
        public async Task<ActionResult<VallidateVatResponse>> VallidateVat(VallidateVatRequest request)
        {
            var response = await _mainService.ValidateVatAsync(request);
            return Ok(response);
        }
        
        [HttpPost("getDailyRates")]
        public async Task<ActionResult<GesmesEnvelope>> GetDailyRates()
        {
            var response = await _mainService.GetDailyRatesAsync();
            return Ok(response);
        }
    }
}