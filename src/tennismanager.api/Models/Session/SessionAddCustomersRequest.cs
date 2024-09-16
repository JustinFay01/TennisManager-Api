using FluentValidation;
using tennismanager.api.Extensions;

namespace tennismanager.api.Models.Session;

public class SessionAddCustomersRequest
{
    public List<CustomerSessionRequest> Requests { get; set; }
}

public class SessionAddCustomersRequestValidator : AbstractValidator<SessionAddCustomersRequest>
{
    public SessionAddCustomersRequestValidator()
    {
        RuleFor(x => x.Requests)
            .NotEmpty()
            .WithMessage("At least one customer session request must be provided.");

        RuleForEach(x => x.Requests)
            .SetValidator(new CustomerSessionRequestValidator());
    }
}

public class CustomerSessionRequestValidator : AbstractValidator<CustomerSessionRequest>
{
    public CustomerSessionRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .IsValidGuid()
            .WithMessage("Customer ID must be provided.");


        RuleFor(x => x.SessionId)
            .NotEmpty()
            .IsValidGuid()
            .WithMessage("Session ID must be provided.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date must be provided.");

        RuleFor(x => x.CustomPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Custom price must be greater than or equal to 0.");
    }
}