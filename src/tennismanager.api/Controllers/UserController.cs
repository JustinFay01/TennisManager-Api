using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.User.Requests;
using tennismanager.service.DTO;
using tennismanager.service.Services;
using tennismanager.shared;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly IValidator<UserCreateRequest> _userCreateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(ILogger<CustomerController> logger, IValidator<UserCreateRequest> userCreateRequestValidator,
        IMapper mapper, IUserService userService)
    {
        _logger = logger;
        _userCreateRequestValidator = userCreateRequestValidator;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet("${id}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        try
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null) return new NotFoundResult();
            return new OkObjectResult(userDto);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return StatusCode(500, exception.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer([FromBody] UserCreateRequest request)
    {
        try
        {
            _userCreateRequestValidator.ValidateAndThrow(request);

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
        catch (ValidationException validationException)
        {
            _logger.LogError(validationException, validationException.Message);
            return new BadRequestObjectResult(validationException.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return StatusCode(500, exception.Message);
        }
    }
}