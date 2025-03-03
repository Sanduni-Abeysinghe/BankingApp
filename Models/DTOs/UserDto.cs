using System.ComponentModel.DataAnnotations;

namespace BankingSystemAPI.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

    }
}
