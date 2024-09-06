using System.Text.Json.Serialization;
using FluentValidation;
using tennismanager.api.Extensions;
using tennismanager.service.DTO.Session;
using tennismanager.shared.Models;

namespace tennismanager.api.Models.Session;

public class SessionCreateRequest
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }
    public Guid? CoachId { get; set; }
    
    public SessionMetaDto SessionMeta { get; set; }
}

public class SessionCreateRequestValidator : AbstractValidator<SessionCreateRequest>
{
    public SessionCreateRequestValidator()
    {
        RuleFor(x => x.Type).NotNull().IsEnumName(typeof(SessionType))
            .WithMessage("Invalid session type");
        
        RuleFor(x => x.Name).NotEmpty();

        When(x => x.Type is nameof(SessionType.TennisPrivate) or nameof(SessionType.PicklePrivate),
            () => { RuleFor(x => x.CoachId).NotNull(); });
        
        RuleFor(s => s.SessionMeta).NotNull();
        When(s => s.SessionMeta.Recurring, () =>
        {
            RuleFor(s => s.SessionMeta.SessionInterval).NotEmpty();
        });
        
    }
}