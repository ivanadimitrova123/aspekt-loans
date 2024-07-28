namespace Aspekt.Loans.PaidOffLoan;

using FluentValidation;

internal sealed class PaidOffLoanRequestValidator : AbstractValidator<PaidOffLoanRequest>
{
    public PaidOffLoanRequestValidator() => RuleFor(paidOffLoanRequest => paidOffLoanRequest.PaidOffAt)
            .NotEmpty();
}
