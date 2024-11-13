using System.Text.Json.Serialization;
using FluentValidation;
using tennismanager.shared.Extensions;
using tennismanager.shared.Types;

namespace tennismanager.api.Models.User.Requests;

public class UserRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
    
    [JsonPropertyName("picture")]
    public string? Picture { get; set; }
    
    [JsonPropertyName("nickname")]
    public string? Nickname { get; set; }
    
    // Customer Fields
    
    // Coach Fields
    [JsonPropertyName("hourlyRate")]
    public decimal? HourlyRate { get; set; }
}

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Type).NotNull().Must(x => Enum.TryParse<UserType>(x, true, out _))
            .WithMessage(EnumExtensions.ErrorMessage<UserType>());
    }
}