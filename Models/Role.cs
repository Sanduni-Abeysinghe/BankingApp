public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }

    // Many-to-many relationship with User
    public ICollection<UserRole> UserRoles { get; set; }
}
