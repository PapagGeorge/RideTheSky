using GenericHttpClient.Domain.Models.ValidateVat;

namespace GenericHttpClient.Application.Interfaces;

public interface IValidationService
{
    Task <VallidateVatResponse> VallidateVatAsync(VallidateVatRequest request);
}