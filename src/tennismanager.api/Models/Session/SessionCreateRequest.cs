using System.Text.Json.Serialization;
using FluentValidation;
using tennismanager.api.Extensions;

namespace tennismanager.api.Models.Session;

public static class SessionCreateRequestType
{
    public const string Event = "event";
    public const string TennisPrivate = "tennisPrivate";
    public const string TennisDrill = "tennisDrill";
    public const string TennisHitting = "tennisHitting";
    public const string PicklePrivate = "picklePrivate";
    public const string PickleDrill = "pickleDrill";
    public const string PickleHitting = "pickleHitting";
}

public class SessionCreateRequest
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("CoachId")] public string? CoachId { get; set; }

    [JsonPropertyName("CustomerAndPrice")] public Dictionary<string, decimal>? CustomerAndPrice { get; set; }
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