namespace Aspekt.Loans.GetAllLoans;

using Data;

internal record GetAllLoansResponse(IReadOnlyCollection<LoanDto> Loans)
{
    internal static GetAllLoansResponse Create(IReadOnlyCollection<LoanDto> loans) => new(loans);
}

internal record LoanDto(Guid Id, Guid ContactId)
{
    internal static LoanDto From(Loan loan) => new(loan.Id, loan.ContactId);
}