using Dawn;
using Microsoft.AspNetCore.Mvc;
using tennismanager.service.Services;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ILogger<CustomerController> logger,
        ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCustomers([FromQuery] int page, [FromQuery] int pageSize)
    {
        Guard.Argument(page, nameof(page)).NotNegative().NotZero();
        Guard.Argument(pageSize, nameof(pageSize)).NotNegative().NotZero();

        var customers = await _customerService.GetCustomersAsync(page, pageSize);

        return new OkObjectResult(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var customerDto = await _customerService.GetCustomerByIdAsync(id);
        if (customerDto == null) return new NotFoundResult();

        return new OkObjectResult(customerDto);
    }
}