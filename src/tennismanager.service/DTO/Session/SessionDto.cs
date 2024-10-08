﻿namespace tennismanager.service.DTO.Session;

public class SessionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }
    public string Type { get; set; }
    public Guid? CoachId { get; set; }

    public SessionMetaDto SessionMeta { get; set; }
}