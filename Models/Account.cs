public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public DateTime DateOpened { get; set; }
    public string AccountType { get; set; } // Checking, Savings, etc.

    // Foreign Key to BankBranch
    public int BankBranchId { get; set; }
    public BankBranch BankBranch { get; set; }
}

