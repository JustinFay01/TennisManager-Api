using Dawn;
using Microsoft.AspNetCore.Mvc;
using tennismanager.service.Services;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;

    public CustomerController(ILogger<CustomerController> logger,
        ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetCustomers([FromQuery] int page, [FromQuery] int pageSize)
    {
        try
        {
            Guard.Argument(page, nameof(page)).NotNegative().NotZero();
            Guard.Argument(pageSize, nameof(pageSize)).NotNegative().NotZero();

            var customers = await _customerService.GetCustomersAsync(page, pageSize);
            
            return new OkObjectResult(customers);
        }
        catch (ArgumentException exception)
        {
            _logger.LogError(exception, exception.Message);
            return new BadRequestObjectResult(exception.Message);
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