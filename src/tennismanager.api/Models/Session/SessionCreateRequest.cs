using System.Text.Json.Serialization;
using FluentValidation;
using tennismanager_api.tennismanager.constants.Utilities;

namespace tennismanager_api.tennismanager.api.Models.Session;

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
                "This property can only be 'event', 'tennisPrivate', 'tennisDrill', 'tennisHitting, 'picklePrivate', 'pickleDrill', 'pickleHitting'"
            );
    }
}