using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using tennismanager.shared;

namespace tennismanager.api.Models.User.Requests;

public class UserCreateRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("first")] 
    public string FirstName { get; set; }
    
    [JsonPropertyName("last")] 
    public string LastName { get; set; }
    
    [JsonPropertyName("email")] 
    public string? Email { get; set; }
    
    [JsonPropertyName("phone")] 
    public string? PhoneNumber { get; set; }
}

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        var integrations = new List<string>()
        {
            UserTypes.Coach,
            UserTypes.Customer
        };

        RuleFor(x => x.Type).NotNull()
            .Must(x => integrations.Contains(x)).WithMessage(
            "This property can only be 'coach' or 'customer'"
            );
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        
        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email).EmailAddress();
        });
        // Accepts:
        // (123) 456-7890
        // 123-456-7890
        // 456-7890
        RuleFor(x => x.PhoneNumber)
        .Must(phone => string.IsNullOrEmpty(phone) || Regex.IsMatch(phone, @"(((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4})"))
        .WithMessage("Phone number can only contain digits, plus sign, minus sign, parentheses and spaces or be null.");

        // TODO: Remove when sure that the package prices are not needed
        // When(x => x.Type.Equals(UserTypes.Coach) && x.PackagePrices.Length != 0, () =>
        // {
        //     RuleForEach(x => x.PackagePrices).SetValidator(new PackagePriceRequestValidator());
        // }).Otherwise(() =>
        // {
        //     RuleFor(x => x.PackagePrices.Length).Equal(0)
        //     .WithMessage("Only coaches may include a list of package prices.");
        // });

    }
}
