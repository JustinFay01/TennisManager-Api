using AutoFixture;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using tennismanager.api.ExceptionHandlers;
using tennismanager.service.Extensions;

namespace tennismanager_api.api.tests;

public class BaseApiTest<T> : IDisposable
    where T : class
{
    private readonly IHost _hostBuilder;

    protected readonly Mock<ILogger<T>> Logger;
    protected readonly Fixture Fixture;


    protected BaseApiTest()
    {
        Fixture = new Fixture();
        Logger = new Mock<ILogger<T>>();

        _hostBuilder = new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

                        services.UseTennisManagerServices(config);
                        services.AddExceptionHandler<ArgumentExceptionHandler>();
                        services.AddExceptionHandler<ValidationExceptionHandler>();
                        services.AddExceptionHandler<EntityNotFoundExceptionHandler>();
                        services.AddExceptionHandler<ExceptionHandler>();
                    }).Configure(app =>
                    {
                        app.UseExceptionHandler(options => { });
                    });
            }).Build();
        
        _hostBuilder.Start();
    }
    
    public virtual void Dispose()
    {
        _hostBuilder.Dispose();
    }
}