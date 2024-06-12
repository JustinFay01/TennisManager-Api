﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager_api.tennismanager.services.Services;
using tennismanager.api.Models.Session;
using tennismanager.service.DTO;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/session")]
public class SessionController
{
    private readonly ILogger<SessionController> _logger;
    private readonly IMapper _mapper;
    private readonly ISessionService _sessionService;
    private readonly IValidator<SessionCreateRequest> _sessionCreateRequestValidator;

    public SessionController(
        ILogger<SessionController> logger,
        IMapper mapper, ISessionService sessionService, IValidator<SessionCreateRequest> sessionCreateRequestValidator)
    {
        _logger = logger;
        _mapper = mapper;
        _sessionService = sessionService;
        _sessionCreateRequestValidator = sessionCreateRequestValidator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateSession([FromBody] SessionCreateRequest request)
    {
        try
        {
            _sessionCreateRequestValidator.ValidateAndThrow(request);
            
            var sessionDto = _mapper.Map<SessionDto>(request);
            
            var session = await _sessionService.CreateSessionAsync(sessionDto);
         
            return new CreatedResult($"api/session/{sessionDto.Id}", session);
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