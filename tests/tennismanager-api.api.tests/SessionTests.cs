using System.Text;
using System.Text.Json;
using AutoFixture;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using tennismanager.api.Controllers;
using tennismanager.api.Models.Session;
using tennismanager.api.Profiles;
using tennismanager.service.DTO.Session;
using tennismanager.service.Services;

namespace tennismanager_api.api.tests;

public class SessionTests : IDisposable
{
    private readonly SessionController TestFixture;
    private readonly Mock<ISessionService> _mockSessionService;
    private readonly Fixture Fixture;

    public SessionTests()
    {
        Fixture = new Fixture();
        
        _mockSessionService = new Mock<ISessionService>();
        var mockLogger = new Mock<ILogger<SessionController>>();
        var mockMapper = new Mock<IMapper>();
        var mockSessionService = new Mock<ISessionService>();
        var mockSessionCreateRequestValidator = new Mock<IValidator<SessionCreateRequest>>();
        var mockSessionAddCustomersRequestValidator = new Mock<IValidator<SessionAddCustomersRequest>>();

        TestFixture = new SessionController(
            mockLogger.Object,
            mockMapper.Object,
            mockSessionService.Object,
            mockSessionCreateRequestValidator.Object,
            mockSessionAddCustomersRequestValidator.Object
        );
    }

    public void Dispose()
    {
        _mockSessionService.VerifyAll();
    }


    [Fact]
    public void ValidateMappings()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<SessionCreateProfile>(); });
        config.AssertConfigurationIsValid();
    }


    # region Update Session Tests (Json Merge Patch)

    [Fact]
    public async Task UpdateSession_ValidRequest_ReturnsNoContent()
    {
        // Arrange
       var session = Fixture.Build<SessionUpdateRequest>()
            .Create();

        _mockSessionService.Setup(x => x.UpdateSessionAsync(It.IsAny<Guid>(), It.IsAny<SessionDto>()));

        // Act
        var result = await TestFixture.UpdateSession(Guid.NewGuid(), session);


        // Assert
        _mockSessionService.Verify(x => x.UpdateSessionAsync(It.IsAny<Guid>(), It.IsAny<SessionDto>()), Times.Once);
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async Task UpdateSession_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var session = Fixture.Build<SessionUpdateRequest>()
            .Create();
        
        _mockSessionService.Setup(x => x.UpdateSessionAsync(It.IsAny<Guid>(), It.IsAny<SessionDto>()))
            .ThrowsAsync(new ValidationException("Invalid request"));

        // Act
        var result = await TestFixture.UpdateSession(Guid.NewGuid(), session);

        // Assert
        _mockSessionService.Verify(x => x.UpdateSessionAsync(It.IsAny<Guid>(), It.IsAny<SessionDto>()), Times.Once);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    
    

    # endregion
}