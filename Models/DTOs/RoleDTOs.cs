namespace BankingSystemAPI.DTOs
{
    public class CreateRoleDTO
    {
        public string RoleName { get; set; }
    }

    public class UpdateRoleDTO
    {
        public string RoleId { get; set; }
        public string NewRoleName { get; set; }
    }

    public class AssignRoleDTO
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
