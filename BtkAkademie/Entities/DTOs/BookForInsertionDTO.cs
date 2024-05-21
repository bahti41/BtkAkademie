using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public record BookForInsertionDTO: BookForManipulationDTO
    {
        [Required(ErrorMessage ="CategoryId is required.")]
        public int CategoryId { get; set; }
    }
}
