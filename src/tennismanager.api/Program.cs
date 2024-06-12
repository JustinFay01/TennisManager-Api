using FluentValidation;
using tennismanager.api.Models.User;
using tennismanager.api.Profiles;
using tennismanager.service.Extensions;

namespace tennismanager.api;

public class Program
{
    public static void Main(string[] args)
    {
       
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.UseTennisManagerServices(builder.Configuration);
        
        // Injects all Validators
        builder.Services.AddValidatorsFromAssemblyContaining<UserCreateRequestValidator>();
        
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
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
        
    }
}