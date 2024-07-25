namespace Aspekt.Applications.ApproveApplication;

using FluentValidation;

public sealed class ApproveApplicationRequestValidator : AbstractValidator<ApproveApplicationRequest>
{
    public ApproveApplicationRequestValidator() => RuleFor(approveApplicationRequest => approveApplicationRequest.ApprovedAt)
            .NotEmpty();
}