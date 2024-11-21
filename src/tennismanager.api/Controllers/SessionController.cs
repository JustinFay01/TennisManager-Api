using System.Diagnostics.Contracts;
using AutoMapper;
using Dawn;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.Session.Requests;
using tennismanager.service.DTO.Session;
using tennismanager.service.Services;
using tennismanager.shared.Extensions;

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
       await _sessionRequestValidator.ValidateAndThrowAsync(request);

       if (request.SessionMeta.StartDate.HasValue)
       {
           request.SessionMeta.StartDate =
               DateTime.SpecifyKind((DateTime)request.SessionMeta.StartDate, DateTimeKind.Utc);
       }

       if (request.SessionMeta.EndDate.HasValue)
       {
           request.SessionMeta.EndDate = DateTime.SpecifyKind((DateTime)request.SessionMeta.EndDate, DateTimeKind.Utc);
       }

       var sessionDto = _mapper.Map<SessionDto>(request);

        var session = await _sessionService.CreateSessionAsync(sessionDto);

        return new CreatedResult($"api/session/{session.Id}", session);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSessions(
        [FromQuery] int? page, 
        [FromQuery] int? pageSize, 
        [FromQuery] DateOnly? startDate = null,
        [FromQuery] DateOnly? endDate = null)
     {
        if(pageSize != null || page != null)
        {
            Guard.Argument(pageSize, nameof(pageSize)).NotNull().NotZero();
            Guard.Argument(page, nameof(page)).NotNull().NotZero();
        }

        if (startDate != null && endDate != null)
        {
            // Deal with this later
            if(startDate > endDate)
                throw new ArgumentException("Start date must be less than end date.");
        }

        var sessions = await _sessionService.GetSessionsAsync(page, pageSize, startDate, endDate);

        return new OkObjectResult(sessions);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSessionById(Guid id)
    {
        var session = await _sessionService.GetSessionByIdAsync(id);
        if (session == null) return new NotFoundResult();

        return new OkObjectResult(session);
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