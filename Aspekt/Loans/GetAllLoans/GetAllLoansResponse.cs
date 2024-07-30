namespace Aspekt.Loans.GetAllLoans;

using Data;

public record GetAllLoansResponse(IReadOnlyCollection<LoanDto> Loans)
{
    public static GetAllLoansResponse Create(IReadOnlyCollection<LoanDto> loans) => new(loans);
}

public record LoanDto(Guid Id, Guid ContactId)
{
    public static LoanDto From(Loan loan) => new(loan.Id, loan.ContactId);
}