﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public abstract record BookForManipulationDTO
    {
        [Required(ErrorMessage ="Title is required field.")]
        [MinLength(2, ErrorMessage = "Title must consist od at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must consist od at maximum 50 characters")]
        public String Title { get; set; }

        [Required(ErrorMessage = "Title is required field.")]
        [Range(10,1000)]
        public decimal Price { get; set; }
    }
}
