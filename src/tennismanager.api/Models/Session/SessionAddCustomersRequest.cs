using FluentValidation;
using tennismanager.api.Extensions;

namespace tennismanager.api.Models.Session;

public class SessionAddCustomersRequest
{
    public Guid SessionId { get; set; }
    public Dictionary<string, decimal> CustomersAndPrice { get; set; }
}

public class SessionAddCustomersRequestValidator : AbstractValidator<SessionAddCustomersRequest>
{
    public SessionAddCustomersRequestValidator()
    {
        RuleFor(x => x.SessionId).NotEmpty();
        RuleFor(x => x.CustomersAndPrice).NotEmpty();

        RuleForEach(x => x.CustomersAndPrice)
            .ChildRules(c =>
            {
                c.RuleFor(x => x.Key).IsValidGuid();
                c.RuleFor(x => x.Value).NotNull();
            });
    }
}