using FluentValidation;
using Newtonsoft.Json;
using tennismanager.shared.Extensions;
using tennismanager.shared.Types;

namespace tennismanager.api.Models.User.Requests;

public class UserRequest
{
    [JsonProperty("type")]
    public string Type { get; set; }
    
    [JsonProperty("firstName")]
    public string FirstName { get; set; }
    
    [JsonProperty("lastName")]
    public string LastName { get; set; }
    
    [JsonProperty("email")]
    public string? Email { get; set; }
    
    [JsonProperty("phoneNumber")]
    public string? PhoneNumber { get; set; }
    
    [JsonProperty("picture")]
    public string? Picture { get; set; }
    
    [JsonProperty("nickname")]
    public string? Nickname { get; set; }
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