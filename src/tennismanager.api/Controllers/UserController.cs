using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
    private readonly IValidator<UserCreateRequest> _userCreateRequestValidator;
    private readonly IValidator<UserCheckInRequest> _userCheckInRequestValidator;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IValidator<UserCreateRequest> userCreateRequestValidator,
        IMapper mapper, IUserService userService, IValidator<UserCheckInRequest> userCheckInRequestValidator)
    {
        _logger = logger;
        _userCreateRequestValidator = userCreateRequestValidator;
        _mapper = mapper;
        _userService = userService;
        _userCheckInRequestValidator = userCheckInRequestValidator;
    }

    [HttpGet("${id}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var userDto = await _userService.GetUserByIdAsync(id);
        if (userDto == null) return new NotFoundResult();
        return new OkObjectResult(userDto);
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
         
        var userDto = await _userService.GetUserByAuth0Sub(request.Sub);
        return userDto != null ? new OkObjectResult(userDto) : new NoContentResult();
    }

    
    /**
     * Create a new user when the Auth0 login is received.
     * This method is then called by the Auth0 login callback, if the CheckIn method fails.
     */
    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer([FromBody] UserCreateRequest request)
    {
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