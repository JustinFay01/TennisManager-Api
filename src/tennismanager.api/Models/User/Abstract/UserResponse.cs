using FluentValidation;
using Newtonsoft.Json;
using tennismanager.shared.Types;

namespace tennismanager.api.Models.User.Abstract;


//TODO: Mark abstract when Customer Fields are added to Customer Response
public class UserResponse
{
    public Guid Id { get; set; }
    public string UserType { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? NickName { get; set; }
}

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

public abstract class UserRequestValidator<T> : AbstractValidator<T>
where T : UserRequest
{
    protected UserRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        
        var integrations = new List<string>
        {
            UserType.Coach,
            UserType.Customer
        };

        RuleFor(x => x.Type).NotNull()
            .Must(x => integrations.Contains(x)).WithMessage(
                "This property can only be 'coach' or 'customer'"
            );
    }
}