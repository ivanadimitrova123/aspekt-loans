namespace Aspekt.Applications.ApproveApplication;

using FluentValidation;

internal sealed class ApproveApplicationRequestValidator : AbstractValidator<ApproveApplicationRequest>
{
    public ApproveApplicationRequestValidator() => RuleFor(approveApplicationRequest => approveApplicationRequest.ApprovedAt)
            .NotEmpty();
}