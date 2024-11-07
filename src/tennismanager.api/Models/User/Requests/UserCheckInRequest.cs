using FluentValidation;
using Newtonsoft.Json;

namespace tennismanager.api.Models.User.Requests;

public class UserCheckInRequest
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    
    [JsonProperty("firstName")]
    public string FirstName { get; set; }
    
    [JsonProperty("familyName")]
    public string LastName { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("phoneNumber")]
    public string? PhoneNumber { get; set; }
    
    [JsonProperty("picture")]
    public string? Picture { get; set; }
    
    [JsonProperty("nickname")]
    public string? Nickname { get; set; }
}

public class UserCheckInRequestValidator : AbstractValidator<UserCheckInRequest>
{
    public UserCheckInRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}