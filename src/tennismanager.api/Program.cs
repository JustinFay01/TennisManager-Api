using FluentValidation;
using tennismanager.api.ExceptionHandlers;
using tennismanager.api.Models.Session.Requests;
using tennismanager.api.Models.User.Abstract;
using tennismanager.api.Models.User.Requests;
using tennismanager.service.Extensions;

namespace tennismanager.api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Allow cors
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        // Exception handling in chain order (first one to catch the exception will handle it)
        builder.Services.AddExceptionHandler<ArgumentExceptionHandler>();
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<EntityNotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<ExceptionHandler>();

        // Add services to the container.
        builder.Services.AddControllers()
            .AddNewtonsoftJson();
        builder.Services.UseTennisManagerServices(builder.Configuration);

        // Injects all Validators
        builder.Services.AddValidatorsFromAssemblyContaining<SessionRequestValidator>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Use ExceptionHandler Middleware
        app.UseExceptionHandler(options => { });

        app.UseHttpsRedirection();

        // Needs to be before UseAuthorization
        app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}