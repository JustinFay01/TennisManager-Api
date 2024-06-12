using System.Text.Json.Serialization;
using FluentValidation;

namespace tennismanager.api.Models.User.Requests;

public class PackagePriceRequest
{
    [JsonPropertyName("price")] 
    public required decimal Price { get; set; }
    
    [JsonPropertyName("coachId")] 
    public string CoachId { get; set; }
    
    [JsonPropertyName("packageId")] 
    public string PackageId { get; set; }
}

public class PackagePriceRequestValidator : AbstractValidator<PackagePriceRequest>
{
    public PackagePriceRequestValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PackageId).NotEmpty().Must(BeAValidGuid).WithMessage("Invalid PackageId Format.");
        RuleFor(x => x.CoachId).NotEmpty().Must(BeAValidGuid).WithMessage("Invalid CoachId Format.");
    }
    
    private bool BeAValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}
