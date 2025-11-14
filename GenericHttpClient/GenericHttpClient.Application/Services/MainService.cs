using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.GetEmailReputation;
using GenericHttpClient.Domain.Models.ValidateVat;


namespace GenericHttpClient.Application.Services;

public class MainService : IMainService
{
    private readonly IAbstractService _abstractService;
    private readonly IValidationService _validationService;

    public MainService(IAbstractService abstractService, IValidationService validationService)
    {
        _abstractService = abstractService;
        _validationService = validationService;
    }
    public async Task<GetEmailReputationResponseDto> GetEmailReputationAsync(GetEmailReputationRequest request)
    {
        var response = await _abstractService.GetEmailReputationAsync(request);
        return response.ToMainResponse();
    }
    
    public async Task<VallidateVatResponse> ValidateVatAsync(VallidateVatRequest request)
    {
        var response = await _validationService.VallidateVatAsync(request);
        return response;
    }
}