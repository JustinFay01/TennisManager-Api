using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager_api.tennismanager.api.Models.Package;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager_api.tennismanager.services.Services;

namespace tennismanager_api.tennismanager.api.Controllers;

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
    public async Task<IActionResult> CreateUser([FromBody] PackageCreateRequest request)
    {
        try
        {
            _packageCreateRequestValidator.ValidateAndThrow(request);

            var packageDto = _mapper.Map<PackageDto>(request);

            await _packageService.CreatePackageAsync(packageDto);
            
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
}