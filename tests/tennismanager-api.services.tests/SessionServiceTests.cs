using AutoFixture;
using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.service.DTO.Session;
using tennismanager.service.Profiles;
using tennismanager.service.Services;
using tennismanager.shared.Types;

namespace tennismanager_api.services.tests;

public class SessionServiceTests : BaseInMemoryTest
{
    private readonly Fixture Fixture;
    private readonly IMapper Mapper;
    private readonly SessionService TestFixture;

    public SessionServiceTests()
    {
        Fixture = new Fixture();
        Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<SessionDtoProfile>()));

        TestFixture = new SessionService(Context, Mapper);
    }

    [Fact]
    public void ValidateMappings()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<SessionDtoProfile>(); });
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public async Task GetSessionsAsync()
    {
        // Arrange
        Context.Sessions.Add(BaseSession());
        await Context.SaveChangesAsync();
        Assert.Single(Context.Sessions);

        // Act
        var sessions = await TestFixture.GetSessionsAsync(1, 1);

        // Assert
        Assert.NotNull(sessions);
        Assert.Single(sessions.Items);
    }

    [Fact]
    public async Task CreateSessionAsync()
    {
        // Arrange
        var session = BaseSession();

        // Act
        var createdSession = await TestFixture.CreateSessionAsync(Mapper.Map<SessionDto>(session));

        // Assert
        Assert.NotNull(createdSession);
        Assert.NotEqual(Guid.Empty, createdSession.Id);
        Assert.NotEmpty(Context.SessionMetas);
        Assert.NotEmpty(Context.SessionIntervals);
    }


    private Session BaseSession()
    {
        return new Session
        {
            Name = "Test Session",
            Type = SessionType.TennisDrill,
            Duration = 60,
            Capacity = 4,
            SessionMeta = new SessionMeta
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                Recurring = true,
                SessionIntervals =
                [
                    new SessionInterval
                    {
                        RecurringStartDate = DateTime.Now,
                        RepeatInterval = 86400
                    }
                ]
            }
        };
    }
}