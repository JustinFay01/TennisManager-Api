using System.Text.Json.Serialization;
using FluentValidation;
using tennismanager.api.Extensions;

namespace tennismanager.api.Models.Session;

public static class SessionCreateRequestType
{
    public const string Event = "Event";
    public const string TennisPrivate = "TennisPrivate";
    public const string TennisDrill = "TennisDrill";
    public const string TennisHitting = "TennisHitting";
    public const string PicklePrivate = "PicklePrivate";
    public const string PickleDrill = "PickleDrill";
    public const string PickleHitting = "PickleHitting";
}

public class SessionCreateRequest
{
    [JsonPropertyName("type")] public string Type { get; set; }
    
    public string Name { get; set; }
    public string? Description { get; set; }

    [JsonPropertyName("coachId")] public string? CoachId { get; set; }

    [JsonPropertyName("customerAndPrice")] public Dictionary<string, decimal>? CustomerAndPrice { get; set; }
    [JsonPropertyName("date")] public DateTime Date { get; set; }
}

public class SessionCreateRequestValidator : AbstractValidator<SessionCreateRequest>
{
    public SessionCreateRequestValidator()
    {
        var integrations = new List<string>()
        {
            SessionCreateRequestType.Event,
            SessionCreateRequestType.TennisPrivate,
            SessionCreateRequestType.TennisDrill,
            SessionCreateRequestType.TennisHitting,
            SessionCreateRequestType.PicklePrivate,
            SessionCreateRequestType.PickleDrill,
            SessionCreateRequestType.PickleHitting
        };

        RuleFor(x => x.Type).NotNull()
            .Must(x => integrations.Contains(x)).WithMessage(
                $"Type must be one of the following: {string.Join(", ", integrations)}");
        
        RuleFor(x => x.Name).NotEmpty();

        When(x => x.Type == SessionCreateRequestType.TennisPrivate || x.Type == SessionCreateRequestType.PicklePrivate,
            () => { RuleFor(x => x.CoachId).NotNull(); });

        When(x => x.CustomerAndPrice != null, () =>
        {
            RuleFor(x => x.CustomerAndPrice).NotEmpty();
            RuleForEach(x => x.CustomerAndPrice)
                .ChildRules(c =>
                {
                    c.RuleFor(x => x.Key).IsValidGuid();
                    c.RuleFor(x => x.Value).NotNull();
                });
        });
    }
}