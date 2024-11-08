using FluentValidation;
using Newtonsoft.Json;
using tennismanager.api.Models.User.Abstract;
using tennismanager.shared.Types;

namespace tennismanager.api.Models.User.Requests;

public class UserUpdateRequest : UserRequest
{
}

public class UserUpdateRequestValidator : UserRequestValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
    }
}