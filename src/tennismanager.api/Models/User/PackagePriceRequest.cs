using System.Text.Json.Serialization;
using FluentValidation;

namespace tennismanager.api.Models.User;

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
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.CoachId).NotEmpty();
        RuleFor(x => x.PackageId).NotEmpty();
    }
}
