using AutoFixture;
using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;
using tennismanager.service.DTO;
using tennismanager.service.Profiles;
using tennismanager.service.Services;

namespace tennismanager_api.services.tests;

public class UserServiceTests : BaseInMemoryTest
{
    protected readonly IMapper Mapper;
    protected readonly IUserService TestFixture;

    public UserServiceTests()
    {
        Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UserDtoProfile>()));

        TestFixture = new UserService(Mapper, Context);
    }

    [Fact]
    public void ValidateMappings()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<UserDtoProfile>(); });
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void TestUserAfterMapAction()
    {
        // Arrange
        var testCoach = Fixture.Create<Coach>();
        var testCustomer = Fixture.Build<Customer>().Without(c => c.Sessions).Create();

        // Act
        var coachDto = Mapper.Map<UserDto>(testCoach);
        var customerDto = Mapper.Map<UserDto>(testCustomer);

        // Assert
        Assert.NotNull(coachDto as CoachDto);
        Assert.NotNull(customerDto as CustomerDto);
    }

    [Fact]
    public void TestUserReverseMapAction()
    {
        // Arrange
        var testCoachDto = Fixture.Create<CoachDto>();
        var testCustomerDto = Fixture.Build<CustomerDto>().Create();

        // Act
        var coach = Mapper.Map<User>(testCoachDto);
        var customer = Mapper.Map<User>(testCustomerDto);

        // Assert
        Assert.NotNull(coach as Coach);
        Assert.NotNull(customer as Customer);
    }
}