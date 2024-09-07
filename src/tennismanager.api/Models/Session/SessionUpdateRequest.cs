namespace tennismanager.api.Models.Session;

// JSON Merge Patch Update Request
// https://datatracker.ietf.org/doc/html/rfc7396
public class SessionUpdateRequest
{
    public string? Type { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Duration { get; set; }
    public int? Capacity { get; set; }
    public Guid? CoachId { get; set; }
    public SessionMetaRequest? SessionMeta { get; set; }
}