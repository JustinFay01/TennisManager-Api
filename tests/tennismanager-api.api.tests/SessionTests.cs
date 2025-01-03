using AutoFixture;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using tennismanager.api.Controllers;
using tennismanager.api.Models.Session.Requests;
using tennismanager.api.Profiles;
using tennismanager.service.DTO.Session;
using tennismanager.service.Exceptions;
using tennismanager.service.Services;

namespace tennismanager_api.api.tests;

public class SessionTests : BaseApiTest<SessionController>
{
    private readonly Mock<ISessionService> _mockSessionService;
    private readonly IMapper _mapper;
    private readonly SessionController _testSubject;

    public SessionTests()
    {
        _mockSessionService = new Mock<ISessionService>();
        var mockLogger = new Mock<ILogger<SessionController>>();

        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SessionCreateProfile>();
            cfg.AddProfile<CustomerSessionProfile>();
        }).CreateMapper();

        var mockSessionCreateRequestValidator = new Mock<IValidator<SessionRequest>>();
        var mockSessionAddCustomersRequestValidator = new Mock<IValidator<SessionAddCustomersRequest>>();
        
        _testSubject = new SessionController(
            mockLogger.Object,
            _mapper,
            _mockSessionService.Object,
            mockSessionCreateRequestValidator.Object,
            mockSessionAddCustomersRequestValidator.Object
        );
    }

    public override void Dispose()
    {
        _mockSessionService.VerifyAll();
        base.Dispose();
    }


    [Fact]
    public void ValidateMappings()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    # region Add Customers To Session Tests

    [Fact]
    public async Task AddCustomersToSession_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var sessionAddCustomersRequest = Fixture.Create<SessionAddCustomersRequest>();

        _mockSessionService.Setup(x => x.AddCustomersToSessionAsync(It.IsAny<List<CustomerSessionDto>>()));

        // Act
        var result = await _testSubject.AddCustomersToSession(sessionAddCustomersRequest);

        // Assert
        _mockSessionService.Verify(x => x.AddCustomersToSessionAsync(It.IsAny<List<CustomerSessionDto>>()), Times.Once);
        Assert.IsType<OkResult>(result);
    }

    # endregion
    
    # region Delete Session Tests
    
    [Fact]
    public async Task DeleteSession_ValidRequest_ReturnsNoContent()
    {
        // Arrange
        _mockSessionService.Setup(x => x.DeleteSessionAsync(It.IsAny<Guid>()));

        // Act
        var result = await _testSubject.DeleteSession(Guid.NewGuid());

        // Assert
        _mockSessionService.Verify(x => x.DeleteSessionAsync(It.IsAny<Guid>()), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact(Skip = "No way to test global exception handling")]
    public async Task DeleteSession_InvalidGuid_ReturnsNotFound()
    {
        // Arrange
        _mockSessionService.Setup(x => x.DeleteSessionAsync(Guid.Empty))
            .ThrowsAsync(new SessionNotFoundException());

        // Act
        var result = await _testSubject.DeleteSession(Guid.NewGuid());

        // Assert
        _mockSessionService.Verify(x => x.DeleteSessionAsync(It.IsAny<Guid>()), Times.Once);
        Assert.IsType<NotFoundResult>(result);
    }
    
    # endregion
    
}