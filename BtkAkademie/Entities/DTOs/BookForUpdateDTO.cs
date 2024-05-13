using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public record BookForUpdateDTO : BookForManipulationDTO
    {
        [Required]
        public int Id { get; set; }
    }
}

