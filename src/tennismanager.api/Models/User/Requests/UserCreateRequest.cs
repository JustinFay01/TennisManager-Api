using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using tennismanager.shared.Types;

namespace tennismanager.api.Models.User.Requests;

/**
 * This class is used to create a new user when the Auth0 login is received.
 * This method is then called by the Auth0 login callback, if the CheckIn method fails.
 */
public class UserCreateRequest : UserCheckInRequest
{
    [JsonPropertyName("type")] public string Type { get; set; }
    
}

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
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