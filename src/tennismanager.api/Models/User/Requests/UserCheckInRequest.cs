using FluentValidation;
using Newtonsoft.Json;

namespace tennismanager.api.Models.User.Requests;

public class UserCheckInRequest
{
    // Comes after Auth0 Login (that's why json schema is different)
    
    [JsonProperty("given_name")]
    public string GivenName { get; set; }
    
    [JsonProperty("family_name")]
    public string FamilyName { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
    
    [JsonProperty("sub")]
    public string Sub { get; set; }
    
    [JsonProperty("phone_number")]
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
        RuleFor(x => x.GivenName).NotEmpty();
        RuleFor(x => x.FamilyName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Sub).NotEmpty();
    }
}