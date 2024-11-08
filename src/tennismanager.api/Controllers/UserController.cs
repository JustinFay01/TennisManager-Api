using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.User.Abstract;
using tennismanager.api.Models.User.Requests;
using tennismanager.service.DTO;
using tennismanager.service.Services;
using tennismanager.shared.Types;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    private readonly IValidator<UserCreateRequest> _userCreateRequestValidator;
    private readonly IValidator<UserCheckInRequest> _userCheckInRequestValidator;
    private readonly IValidator<UserUpdateRequest> _userUpdateRequestValidator;

    public UserController(ILogger<UserController> logger, IValidator<UserCreateRequest> userCreateRequestValidator,
        IMapper mapper, IUserService userService, IValidator<UserCheckInRequest> userCheckInRequestValidator, IValidator<UserUpdateRequest> userUpdateRequestValidator)
    {
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
        
        _userCheckInRequestValidator = userCheckInRequestValidator;
        _userUpdateRequestValidator = userUpdateRequestValidator;
        _userCreateRequestValidator = userCreateRequestValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        _logger.LogInformation("Get users request received");
        
        var userDtos =  await _userService.GetUsersAsync();
        _logger.LogInformation("Returning {Count} users", userDtos.Count);
        var response = userDtos.Select(userDto => _mapper.Map<UserResponse>(userDto)).ToList();
        return new OkObjectResult(response);
    }
    
    [HttpGet("${id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        _logger.LogInformation("Get user by id request received");
        
        var userDto = await _userService.GetUserAsync(id);
        if (userDto == null) return new NotFoundResult();
        return new OkObjectResult(userDto);
    }
    
    /// <summary>
    /// PUT best practice is to completely replace the resource with the new one.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("${id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserUpdateRequest request)
    {
        _logger.LogInformation("Update user request received");
        
        var userDto = await _userService.GetUserAsync(id);
        if (userDto == null) return new NotFoundResult();
        
        await _userUpdateRequestValidator.ValidateAndThrowAsync(request);
        
        var newUserDto = _mapper.Map<UserDto>(request);
        newUserDto.Id = id;
        
        await _userService.UpdateUserAsync(newUserDto);
            
        return new OkResult();
    }
    
    [HttpDelete("${id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        _logger.LogInformation("Delete user request received");
        
        await _userService.DeleteUserAsync(id);
        
        _logger.LogInformation("User with id {Id} deleted", id);
        
        return new NoContentResult();
    }
    
    /**
     * Check in the user after the Auth0 login.
     * If the user is already in the database, return the user.
     * If the user is not in the database, return a 204 No Content.
     */
    [HttpPost("check-in")]
    public async Task<IActionResult> UserCheckIn([FromBody] UserCheckInRequest request)
    {
       _logger.LogInformation("UserCheckIn request received");
       
         await _userCheckInRequestValidator.ValidateAndThrowAsync(request);
         
        var userDto = await _userService.GetUserAsync(request.Id);
        return userDto != null ? new OkObjectResult(userDto) : new NoContentResult();
    }

    
    /**
     * Create a new user when the Auth0 login is received.
     * This method is then called by the Auth0 login callback, if the CheckIn method fails.
     */
    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer([FromBody] UserCreateRequest request)
    {
        _logger.LogInformation("Create user request received");
        
        await _userCreateRequestValidator.ValidateAndThrowAsync(request);

        UserDto? userDto = request.Type switch
        {
            UserType.Customer => _mapper.Map<CustomerDto>(request),
            UserType.Coach => _mapper.Map<CoachDto>(request),
            _ => throw new ValidationException("Invalid user type")
        };

        userDto = await _userService.CreateUserAsync(userDto);

        if (userDto == null) return new BadRequestObjectResult("User could not be created");

        return new CreatedResult($"api/user/{userDto.Id}", userDto);
    }
}