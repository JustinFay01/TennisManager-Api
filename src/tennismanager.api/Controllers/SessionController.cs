using AutoMapper;
using Dawn;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.Session.Requests;
using tennismanager.service.DTO.Session;
using tennismanager.service.Services;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/sessions")]
public class SessionController
{
    private readonly ILogger<SessionController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<SessionAddCustomersRequest> _sessionAddCustomersRequestValidator;
    private readonly IValidator<SessionRequest> _sessionRequestValidator;
    private readonly ISessionService _sessionService;

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

    [HttpPost]
    public async Task<IActionResult> CreateSession([FromBody] SessionRequest request)
    {
        _sessionRequestValidator.ValidateAndThrow(request);

        var sessionDto = _mapper.Map<SessionDto>(request);

        var session = await _sessionService.CreateSessionAsync(sessionDto);

        return new CreatedResult($"api/session/{session.Id}", session);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSessions([FromQuery] int page, [FromQuery] int pageSize)
    {
        Guard.Argument(page, nameof(page)).NotNegative().NotZero();
        Guard.Argument(pageSize, nameof(pageSize)).NotNegative().NotZero();

        var sessions = await _sessionService.GetSessionsAsync(page, pageSize);

        return new OkObjectResult(sessions);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSessionById(Guid id)
    {
        var session = await _sessionService.GetSessionByIdAsync(id);
        if (session == null) return new NotFoundResult();

        return new OkObjectResult(session);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateSession([FromRoute] Guid id,
        [FromBody] JsonPatchDocument<SessionRequest> request)
    {
        _logger.LogInformation("Update session request received");

        // Fetch the session
        var session = await _sessionService.GetSessionByIdAsync(id);
        if (session == null) return new NotFoundResult();

        // Map the session to the update request
        var sessionUpdateRequest = _mapper.Map<SessionRequest>(session);

        // Apply the patch
        request.ApplyTo(sessionUpdateRequest);

        // Validate the updated request
        await _sessionRequestValidator.ValidateAndThrowAsync(sessionUpdateRequest);

        // Map the updated request back to a session DTO
        var sessionDto = _mapper.Map<SessionDto>(sessionUpdateRequest);

        // Update the session
        await _sessionService.UpdateSessionAsync(sessionDto);

        return new OkResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession([FromRoute] Guid id)
    {
        await _sessionService.DeleteSessionAsync(id);
        return new NoContentResult();
    }
    
    [HttpPatch("add-customers")]
    public async Task<IActionResult> AddCustomersToSession([FromBody] SessionAddCustomersRequest request)
    {
        await _sessionAddCustomersRequestValidator.ValidateAndThrowAsync(request);

        var customerSessions = _mapper.Map<List<CustomerSessionDto>>(request.Requests);

        await _sessionService.AddCustomersToSessionAsync(customerSessions);

        return new OkResult();
    }
}