namespace tennismanager.service.DTO.Session;

public class SessionMetaDto
{
    public bool Recurring { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<SessionIntervalDto> SessionIntervals { get; set; } = [];
}