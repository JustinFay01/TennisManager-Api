using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager_api.tennismanager.constants;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager_api.tennismanager.services.Services;
using tennismanager.api.Models.User;
using tennismanager.service.Services;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IValidator<UserCreateRequest> _userCreateRequestValidator;
    private readonly IMapper _mapper;

    public UserController(
        ILogger<UserController> logger, 
        IUserService coachService, 
        IValidator<UserCreateRequest> createUserRequestValidator, 
        IMapper mapper)
    {
        _logger = logger;
        _userService = coachService;
        _userCreateRequestValidator = createUserRequestValidator;
        _mapper = mapper;
    }
    
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
    {
        try
        {
            _userCreateRequestValidator.ValidateAndThrow(request);

            switch (request.Type)
            {
                case UserTypes.Coach:
                    var coach = _mapper.Map<CoachDto>(request);
                    await _userService.CreateCoachAsync(coach);
                    
                    break;
                case UserTypes.Customer:
                    var customer = _mapper.Map<CustomerDto>(request);
                    await _userService.CreateCustomerAsync(customer);
                    
                    break;
                default:
                    throw new ValidationException("This property can only be 'coach' or 'customer'");
            }
            
            return new CreatedResult();
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
    
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            return new OkObjectResult(user);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return StatusCode(500, exception.Message);
        }
    }
}