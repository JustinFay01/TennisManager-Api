using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.User;
using tennismanager.api.Models.User.Requests;
using tennismanager.api.Models.User.Responses;
using tennismanager.service.DTO;
using tennismanager.service.Services;
using tennismanager.shared;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IValidator<UserCreateRequest> _userCreateRequestValidator;
    private readonly IValidator<PackagePriceRequest> _packagePriceRequestValidator;
    private readonly IMapper _mapper;

    public UserController(
        ILogger<UserController> logger,
        IUserService coachService,
        IValidator<UserCreateRequest> createUserRequestValidator,
        IMapper mapper, IValidator<PackagePriceRequest> packagePriceRequestValidator)
    {
        _logger = logger;
        _userService = coachService;
        _userCreateRequestValidator = createUserRequestValidator;
        _mapper = mapper;
        _packagePriceRequestValidator = packagePriceRequestValidator;
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
                    var coachDto = _mapper.Map<CoachDto>(request);
                    
                    coachDto = await _userService.CreateCoachAsync(coachDto);
                    
                    var coachResponse = _mapper.Map<CoachResponse>(coachDto);
                    
                    return new CreatedResult($"api/user/{coachResponse.Id}", coachResponse);
                
                case UserTypes.Customer:
                    var customer = _mapper.Map<CustomerDto>(request);
                    customer = await _userService.CreateCustomerAsync(customer);
                    return new CreatedResult($"api/user/{customer.Id}", customer);
                
                default:
                    throw new ValidationException("This property can only be 'coach' or 'customer'");
            }
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if(user is CoachDto coach)
            {
                return new OkObjectResult(_mapper.Map<CoachResponse>(coach));
            }
            
            return new OkObjectResult(user);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return StatusCode(500, exception.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return new OkResult();
        }
        catch(ValidationException validationException)
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
    
    [HttpPut("put/package/price")]
    public async Task<IActionResult> PutPackagePrice([FromBody] PackagePriceRequest request)
    {
        try
        {
            _packagePriceRequestValidator.ValidateAndThrow(request);
            var user = await _userService.PutPackagePriceAsync(request.Price, new Guid(request.CoachId), new Guid(request.PackageId));
            return new OkObjectResult(user);
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