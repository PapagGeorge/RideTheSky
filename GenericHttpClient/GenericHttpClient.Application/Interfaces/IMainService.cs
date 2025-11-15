using GenericHttpClient.Domain.Models.GetDailyRates;
using GenericHttpClient.Domain.Models.GetEmailReputation;
using GenericHttpClient.Domain.Models.ValidateVat;

namespace GenericHttpClient.Application.Interfaces;

public interface IMainService
{
    Task<GetEmailReputationResponseDto> GetEmailReputationAsync(GetEmailReputationRequest request);
    Task<VallidateVatResponse> ValidateVatAsync(VallidateVatRequest request);
    Task<GesmesEnvelope> GetDailyRatesAsync();
}