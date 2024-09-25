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
[Route("/api/packages")]
public class PackageController : ControllerBase
{
    private readonly ILogger<PackageController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<PackageCreateRequest> _packageCreateRequestValidator;
    private readonly IPackageService _packageService;

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

        _packageCreateRequestValidator.ValidateAndThrow(request);

        var packageDto = _mapper.Map<PackageDto>(request);

        var package = await _packageService.CreatePackageAsync(packageDto);

        return new CreatedResult($"api/package/{packageDto.Id}", package);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPackageById(Guid id)
    {
        _logger.LogInformation("Getting package by id.");

        var package = await _packageService.GetPackageByIdAsync(id);
        return package != null ? new OkObjectResult(package) : new NotFoundResult();
    }

    [HttpGet("purchased/{afterDate}")]
    public async Task<IActionResult> GetPackagesPurchasedAfterDate(DateTime afterDate)
    {
        _logger.LogInformation("Getting packages purchased this month.");

        Guard.Argument(afterDate.Date, nameof(afterDate.Date))
            .NotDefault()
            .IsValidDateTime(DateTimeConstants.DateFormat)
            .DateTimeNotGreaterThanOrEqualTo(DateTimeFactory.UtcNow.Date,
                "Date must not be less than current date time.");

        var packages = await _packageService.GetPackagesPurchasedAfterDateAsync(afterDate);

        if (!packages.Any()) return new NoContentResult();

        return new OkObjectResult(packages);
    }
}