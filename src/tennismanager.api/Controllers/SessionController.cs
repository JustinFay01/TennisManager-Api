using AutoMapper;
using Dawn;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.Session;
using tennismanager.service.DTO.Session;
using tennismanager.service.Services;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/sessions")]
public class SessionController
{
    private readonly ILogger<SessionController> _logger;
    private readonly IMapper _mapper;
    private readonly ISessionService _sessionService;
    private readonly IValidator<SessionRequest> _sessionRequestValidator;
    private readonly IValidator<SessionAddCustomersRequest> _sessionAddCustomersRequestValidator;

    public SessionController(
        ILogger<SessionController> logger,
        IMapper mapper, ISessionService sessionService,
        IValidator<SessionRequest> sessionRequestValidator,
        IValidator<SessionAddCustomersRequest> sessionAddCustomersRequestValidator)
    {
        _logger = logger;
        _mapper = mapper;
        _sessionService = sessionService;
        _sessionRequestValidator = sessionRequestValidator;
        _sessionAddCustomersRequestValidator = sessionAddCustomersRequestValidator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateSession([FromBody] SessionRequest request)
    {
        try
        {
            _sessionRequestValidator.ValidateAndThrow(request);

            var sessionDto = _mapper.Map<SessionDto>(request);

            var session = await _sessionService.CreateSessionAsync(sessionDto);

            return new CreatedResult($"api/session/{session.Id}", session);
        }
        catch (ValidationException validationException)
        {
            _logger.LogError(validationException, validationException.Message);
            return new BadRequestObjectResult(validationException.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return new StatusCodeResult(500);
        }
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateSession([FromRoute] Guid id, [FromBody] JsonPatchDocument<SessionRequest> request)
    {
        _logger.LogInformation("Update session request received");
        try
        {
            // Fetch the session
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
            {
                return new NotFoundResult();
            }
            
            // Map the session to the update request
            var sessionUpdateRequest = _mapper.Map<SessionRequest>(session);
            
            // Apply the patch
            request.ApplyTo(sessionUpdateRequest);
            
            // Validate the updated request
            _sessionRequestValidator.ValidateAndThrow(sessionUpdateRequest);
            
            // Map the updated request back to a session DTO
            var sessionDto = _mapper.Map<SessionDto>(sessionUpdateRequest);
            
            // Update the session
            await _sessionService.UpdateSessionAsync(sessionDto);
            
            return new OkResult();
        }
        catch (ValidationException validationException)
        {
            _logger.LogError(validationException, validationException.Message);
            return new BadRequestObjectResult(validationException.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return new StatusCodeResult(500);
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetSessions([FromQuery] int page, [FromQuery] int pageSize)
    {
        try
        {
            Guard.Argument(page, nameof(page)).NotNegative().NotZero();
            Guard.Argument(pageSize, nameof(pageSize)).NotNegative().NotZero();

            var sessions = await _sessionService.GetSessionsAsync(page, pageSize);

            return new OkObjectResult(sessions);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return new StatusCodeResult(500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSessionById(Guid id)
    {
        try
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(session);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return new StatusCodeResult(500);
        }
    }

    [HttpPatch("add-customers")]
    public async Task<IActionResult> AddCustomersToSession([FromBody] SessionAddCustomersRequest request)
    {
        try
        {
            _sessionAddCustomersRequestValidator.ValidateAndThrow(request);

            var customerSessions = _mapper.Map<List<CustomerSessionDto>>(request.Requests);

            await _sessionService.AddCustomersToSessionAsync(customerSessions);

            return new OkResult();
        }
        catch (ArgumentException exception)
        {
            _logger.LogError(exception, exception.Message);
            return new BadRequestObjectResult(exception.Message);
        }
        catch (ValidationException validationException)
        {
            _logger.LogError(validationException, validationException.Message);
            return new BadRequestObjectResult(validationException.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Something went wrong!");
            return new StatusCodeResult(500);
        }
    }
}