using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tennismanager.api.Models.User.Abstract;
using tennismanager.api.Models.User.Requests;
using tennismanager.api.Models.User.Responses;
using tennismanager.service.DTO;
using tennismanager.service.DTO.Users;
using tennismanager.service.Services;
using tennismanager.shared.Types;

namespace tennismanager.api.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    private readonly IValidator<UserRequest> _userRequestValidator;

    public UserController(ILogger<UserController> logger, IMapper mapper, IUserService userService, IValidator<UserRequest> userRequestValidator)
    {
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
        _userRequestValidator = userRequestValidator;
    }


    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        _logger.LogInformation("Get users request received");
        
        var userDtos =  await _userService.GetUsersAsync();
        _logger.LogInformation("Returning {Count} users", userDtos.Count);
        var response = userDtos.Select(userDto => _mapper.Map<UserResponse>(userDto)).ToList();
        return new OkObjectResult(response);
    }
    
    [HttpGet("${id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        _logger.LogInformation("Get user by id request received");
        
        var userDto = await _userService.GetUserAsync(id);
        if (userDto == null) return new NotFoundResult();
        return new OkObjectResult(userDto);
    }
    
    /// <summary>
    /// PUT best practice is to completely replace the resource with the new one.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("${id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserRequest request)
    {
        _logger.LogInformation("Update user request received");
        
        var userDto = await _userService.GetUserAsync(id);
        if (userDto == null) return new NotFoundResult();
        
        await _userRequestValidator.ValidateAndThrowAsync(request);
        
        var newUserDto = _mapper.Map<UserDto>(request);
        newUserDto.Id = id;
        
        await _userService.UpdateUserAsync(newUserDto);
            
        return new OkResult();
    }
    
    [HttpDelete("${id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        _logger.LogInformation("Delete user request received");
        
        await _userService.DeleteUserAsync(id);
        
        _logger.LogInformation("User with id {Id} deleted", id);
        
        return new NoContentResult();
    }
    
    /**
     * Create a new user when the Auth0 login is received.
     * This method is then called by the Auth0 login callback, if the CheckIn method fails.
     */
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] UserRequest request)
    {
        _logger.LogInformation("Create user request received");
        
        await _userRequestValidator.ValidateAndThrowAsync(request);

        var userDto = _mapper.Map<UserDto>(request);
        
        userDto = await _userService.CreateUserAsync(userDto);

        if (userDto == null) return new BadRequestObjectResult("User could not be created");

        return new CreatedResult($"api/user/{userDto.Id}", userDto);
    }
}