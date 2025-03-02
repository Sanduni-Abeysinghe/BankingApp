public class BankBranch
{
    public int Id { get; set; }
    public string BranchName { get; set; }
    public string Location { get; set; }
    public string BranchManager { get; set; }
    public string PhoneNumber { get; set; }

    // Relationships
    public ICollection<Account> Accounts { get; set; }
}
