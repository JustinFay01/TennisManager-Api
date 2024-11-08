using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using tennismanager.api.Models.User.Abstract;
using tennismanager.shared.Types;

namespace tennismanager.api.Models.User.Requests;


public class UserCreateRequest : UserRequest
{
    
}

public class UserCreateRequestValidator : UserRequestValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        
    }
}