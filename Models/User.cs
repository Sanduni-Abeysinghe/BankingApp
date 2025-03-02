public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }

    // Relationships
    public ICollection<Account> Accounts { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } // Many-to-many relationship with Role
}
