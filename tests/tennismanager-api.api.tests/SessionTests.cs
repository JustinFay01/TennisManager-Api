using AutoFixture;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
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
        var mapper = new MapperConfiguration(cfg => { cfg.AddProfile<SessionCreateProfile>(); }).CreateMapper();
        var mockSessionCreateRequestValidator = new Mock<IValidator<SessionRequest>>();
        var mockSessionAddCustomersRequestValidator = new Mock<IValidator<SessionAddCustomersRequest>>();

        TestFixture = new SessionController(
            mockLogger.Object,
            mapper,
            _mockSessionService.Object,
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


    # region Update Session Tests

    [Fact]
    public async Task UpdateSession_ValidRequest_ReturnsNoContent()
    {
        // Arrange
        var session = CreateSessionPatchDocument();

        _mockSessionService.Setup(x => x.GetSessionByIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync(Fixture.Create<SessionDto>());
        _mockSessionService.Setup(x => x.UpdateSessionAsync(It.IsAny<SessionDto>()));

        // Act
        var result = await TestFixture.UpdateSession(Guid.NewGuid(), session);


        // Assert
        _mockSessionService.Verify(x => x.UpdateSessionAsync(It.IsAny<SessionDto>()), Times.Once);
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task UpdateSession_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var session = CreateSessionPatchDocument();
        
        _mockSessionService.Setup(x => x.GetSessionByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.Create<SessionDto>());
        _mockSessionService.Setup(x => x.UpdateSessionAsync(It.IsAny<SessionDto>()))
            .ThrowsAsync(new ValidationException("Invalid request"));

        // Act
        var result = await TestFixture.UpdateSession(Guid.NewGuid(), session);

        // Assert
        _mockSessionService.Verify(x => x.UpdateSessionAsync(It.IsAny<SessionDto>()), Times.Once);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    
    private JsonPatchDocument<SessionRequest> CreateSessionPatchDocument()
    {
        var patchDocument = new JsonPatchDocument<SessionRequest>();
        patchDocument.Operations.Add(new Operation<SessionRequest>
        {
            op = "replace",
            path = "/name",
            value = "New Name"
        });
        return patchDocument;
    }
    
    # endregion
}