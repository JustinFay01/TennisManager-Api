using AutoMapper;
using tennismanager.service.Profiles;

namespace tennismanager_api.services.tests;

public class SessionServiceTests
{
    [Fact]
    public void ValidateMappings()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SessionDtoProfile>();
        });
        config.AssertConfigurationIsValid();
    }
}