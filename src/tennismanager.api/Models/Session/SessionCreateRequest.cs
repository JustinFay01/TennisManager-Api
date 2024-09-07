﻿using System.Text.Json.Serialization;
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
    public SessionMetaRequest SessionMeta { get; set; }
}

public class SessionCreateRequestValidator : AbstractValidator<SessionCreateRequest>
{
    public SessionCreateRequestValidator()
    {
        RuleFor(x => x.Type).NotNull().IsEnumName(typeof(SessionType))
            .WithMessage("Invalid session type");

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Duration).GreaterThan(0);

        RuleFor(x => x.Capacity).GreaterThan(0);

        When(x => x.Type is nameof(SessionType.TennisPrivate) or nameof(SessionType.PicklePrivate),
            () =>
            {
                RuleFor(x => x.CoachId).NotNull()
                    .WithMessage("Coach id must not be null when session type is private");
            });

        RuleFor(s => s.SessionMeta).NotNull()
            .WithMessage("Session meta must not be null");

        When(s => s.SessionMeta.Recurring, () =>
        {
            RuleFor(s => s.SessionMeta.SessionIntervals).NotEmpty();
            RuleForEach(s => s.SessionMeta.SessionIntervals)
                .ChildRules(interval =>
                {
                    interval.RuleFor(i => i.RepeatInterval).GreaterThanOrEqualTo(86400)
                        .WithMessage("Repeat interval must be greater than or equal to 86400 seconds");
                });
        }).Otherwise(() =>
        {
            RuleFor(s => s.SessionMeta.SessionIntervals).Empty()
                .WithMessage("Session intervals must be empty when recurring is false");
        });
    }
}