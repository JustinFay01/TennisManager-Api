using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.User.Requests;
using tennismanager.api.Models.User.Responses;
using tennismanager.service.DTO;
using tennismanager.service.Services;
using tennismanager.shared;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/coaches")]
public class CoachController : ControllerBase
{
    
    private readonly ILogger<CoachController> _logger;
    private readonly IValidator<UserCreateRequest> _userCreateRequestValidator;
    private readonly IValidator<PackagePriceRequest> _packagePriceRequestValidator;
    private readonly IMapper _mapper;
    private readonly ICoachService _coachService;

    public CoachController(
        ILogger<CoachController> logger,
        IValidator<UserCreateRequest> createUserRequestValidator,
        IMapper mapper, IValidator<PackagePriceRequest> packagePriceRequestValidator, ICoachService coachService)
    {
        _logger = logger;
        _userCreateRequestValidator = createUserRequestValidator;
        _mapper = mapper;
        _packagePriceRequestValidator = packagePriceRequestValidator;
        _coachService = coachService;
    }
    
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateCoach([FromBody] UserCreateRequest request)
    {
        try
        {
            _userCreateRequestValidator.ValidateAndThrow(request);
            // TODO: Clean this up
            if (request.Type != UserTypes.Coach)
                return new BadRequestObjectResult("User type must be 'coach'");
            
            var coachDto = _mapper.Map<CoachDto>(request);
            
            coachDto = await _coachService.CreateCoachAsync(coachDto);
            
            var coachResponse = _mapper.Map<CoachResponse>(coachDto);
            
            return new CreatedResult($"api/coach/{coachResponse.Id}", coachResponse);
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
            var coachDto = await _coachService.GetCoachByIdAsync(id);
            if (coachDto == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(_mapper.Map<CoachResponse>(coachDto));
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
            await _coachService.DeleteUserAsync(id);
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
            var user = await _coachService.PutPackagePriceAsync(request.Price, new Guid(request.CoachId), new Guid(request.PackageId));
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

    [HttpGet("all")]
    public async Task<IActionResult> GetCoaches()
    {
        try
        {
            var coaches = await _coachService.GetCoachesAsync();

            return new OkObjectResult(coaches);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return StatusCode(500, exception.Message);
        }
    }
    
}