namespace Aspekt.Loans.PaidOffLoan;

using FluentValidation;

public sealed class PaidOffLoanRequestValidator : AbstractValidator<PaidOffLoanRequest>
{
    public PaidOffLoanRequestValidator() => RuleFor(paidOffLoanRequest => paidOffLoanRequest.PaidOffAt)
            .NotEmpty();
}
