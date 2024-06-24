using AutoMapper;
using Dawn;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.Package;
using tennismanager.service.DTO;
using tennismanager.service.Services;
using tennismanager.shared;
using tennismanager.shared.Extensions;
using tennismanager.shared.Utilities;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/package")]
public class PackageController : ControllerBase
{
    private readonly ILogger<PackageController> _logger;
    private readonly IMapper _mapper;
    private readonly IPackageService _packageService;
    private readonly IValidator<PackageCreateRequest> _packageCreateRequestValidator;

    public PackageController(
    ILogger<PackageController> logger,
    IMapper mapper,
    IValidator<PackageCreateRequest> packageCreateRequestValidator,
    IPackageService packageService)
    {
        _logger = logger;
        _mapper = mapper;
        _packageCreateRequestValidator = packageCreateRequestValidator;
        _packageService = packageService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePackage([FromBody] PackageCreateRequest request)
    {
        _logger.LogInformation("Creating a new package.");
        try
        {
            _packageCreateRequestValidator.ValidateAndThrow(request);

            var packageDto = _mapper.Map<PackageDto>(request);

            var package = await _packageService.CreatePackageAsync(packageDto);
            
            return new CreatedResult($"api/package/{packageDto.Id}", package);
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
    public async Task<IActionResult> GetPackageById(Guid id)
    {
        _logger.LogInformation("Getting package by id.");
        try
        {
            var package = await _packageService.GetPackageByIdAsync(id);
            return package != null ? new OkObjectResult(package) : new NotFoundResult();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return StatusCode(500, exception.Message);
        }
    }

    [HttpGet("purchased/{afterDate}")]
    public async Task<IActionResult> GetPackagesPurchasedAfterDate(DateTime afterDate)
    {
        _logger.LogInformation("Getting packages purchased this month.");
        try
        {
            Guard.Argument(afterDate.Date, nameof(afterDate.Date))
                .NotDefault()
                .IsValidDateTime(DateTimeConstants.DateFormat)
                .DateTimeNotGreaterThanOrEqualTo(DateTimeFactory.UtcNow.Date, "Date must not be less than current date time.");

            var packages = await _packageService.GetPackagesPurchasedAfterDateAsync(afterDate);
            return new OkObjectResult(packages);
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
}