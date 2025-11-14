using GenericHttpClient.Domain.Models.GetEmailReputation;

namespace GenericHttpClient.Application.Interfaces;

public interface IAbstractService
{
    Task <GetEmailReputationResponse> GetEmailReputationAsync(GetEmailReputationRequest request);
}