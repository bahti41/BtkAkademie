using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Category
{
    public record CategoryDorUpdateDTO: CategoryForManipulationDTO
    {
        [Required]
        public int CategoryId { get; set; }
    }
}
