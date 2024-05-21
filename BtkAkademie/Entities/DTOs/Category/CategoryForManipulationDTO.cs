using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Category
{
    public abstract record CategoryForManipulationDTO
    {
        [Required(ErrorMessage = "Title is required field.")]
        [MinLength(2, ErrorMessage = "Title must consist od at least 2 characters")]
        [MaxLength(90, ErrorMessage = "Title must consist od at maximum 50 characters")]
        public String CategoryName { get; set; }
    }
}
