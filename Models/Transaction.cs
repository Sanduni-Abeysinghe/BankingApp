public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; } // Deposit, Withdrawal, Transfer, etc.

    // Foreign Key to Account
    public int AccountId { get; set; }
    public Account Account { get; set; }

    // Foreign Key to BankBranch (Optional - depends on your design)
    public int BankBranchId { get; set; }
    public BankBranch BankBranch { get; set; }
}
