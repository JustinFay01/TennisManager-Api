using Microsoft.EntityFrameworkCore;
using tennismanager.data;

namespace tennismanager_api.services.tests;

public class BaseInMemoryTest
{
    protected readonly TennisManagerContext Context;

    public BaseInMemoryTest()
    {
        var options = new DbContextOptionsBuilder<TennisManagerContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new TennisManagerContext(options);
        Context.Database.EnsureCreated();
    }
}