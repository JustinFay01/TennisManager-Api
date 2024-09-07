using AutoMapper;
using tennismanager.api.Profiles;

namespace tennismanager_api.api.tests;

public class SessionTests
{
    
    [Fact]
    public void ValidateMappings()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<SessionCreateProfile>();
        });
        config.AssertConfigurationIsValid();
    }
}