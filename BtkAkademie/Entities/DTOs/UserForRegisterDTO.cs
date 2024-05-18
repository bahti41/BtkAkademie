using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record UserForRegisterDTO
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage ="User name is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password{ get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<string>? Roles { get; set; }
    }
}
