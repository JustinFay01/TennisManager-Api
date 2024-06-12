using System.Text.Json.Serialization;
using FluentValidation;

namespace tennismanager.api.Models.Package;

public class PackageCreateRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("uses")]
    public int Uses { get; set; }
    [JsonPropertyName("defaultPrice")]
    public decimal DefaultPrice { get; set; }
}

public class PackageCreateRequestValidator : AbstractValidator<PackageCreateRequest>
{
    public PackageCreateRequestValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required.")
        .Length(2, 30).WithMessage("Name should be between 2 to 30 characters.");

        RuleFor(x => x.Uses)
        .GreaterThan(0).WithMessage("Usages should be more than 0.");

        RuleFor(x => x.DefaultPrice)
        .GreaterThanOrEqualTo(0).WithMessage("Default price should be greater than or equal to 0.");
    }
}