using FluentValidation;
using Newtonsoft.Json;
using tennismanager.shared.Extensions;
using tennismanager.shared.Types;

namespace tennismanager.api.Models.User.Abstract;

public abstract class UserRequest
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

//TODO: Need to make these completely abstract and add child classes for Customer and Coach
// We need to model the same inheritance structure as the DTOs in order for automapper to work
// Lastly, we can then add a helper method which chooses the DTO to map to based on the Type property
// In addition, we need to add a validator for each child class and a method to 
// choose the correct Response to map to based on the Type property

public abstract class UserRequestValidator<T> : AbstractValidator<T>
    where T : UserRequest
{
    protected UserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Type).NotNull().IsEnumName(typeof(UserType))
            .WithMessage(EnumExtensions.ErrorMessage<UserType>());
    }
}