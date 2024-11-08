using FluentValidation;
using Newtonsoft.Json;
using tennismanager.api.Models.User.Abstract;

namespace tennismanager.api.Models.User.Requests;

public class UserCheckInRequest : UserRequest
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
}

public class UserCheckInRequestValidator : UserRequestValidator<UserCheckInRequest>
{
    public UserCheckInRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}