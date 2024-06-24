using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.User.Requests;
using tennismanager.service.DTO;
using tennismanager.service.Services;
using tennismanager.shared;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly IValidator<UserCreateRequest> _userCreateRequestValidator;
    private readonly IMapper _mapper;
    private readonly ICustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger,
        IValidator<UserCreateRequest> userCreateRequestValidator,IMapper mapper, ICustomerService customerService)
    {
        _logger = logger;
        _userCreateRequestValidator = userCreateRequestValidator;
        _mapper = mapper;
        _customerService = customerService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer([FromBody] UserCreateRequest request)
    {
        try
        {
            _userCreateRequestValidator.ValidateAndThrow(request);
            // TODO: Clean this up
            if (request.Type != UserTypes.Customer)
                return new BadRequestObjectResult("User type must be 'customer'");
            
            var customerDto = _mapper.Map<CustomerDto>(request);
            
            customerDto = await _customerService.CreateCustomerAsync(customerDto);
            
            return new CreatedResult($"api/user/{customerDto.Id}", customerDto);
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
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        try
        {
            var customerDto = await _customerService.GetCustomerByIdAsync(id);
            if (customerDto == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(customerDto);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return StatusCode(500, exception.Message);
        }
    }
}