using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Category
{
    public record CategoryDTO
    {
        public int CategoryId { get; set; }
        public String? CategoryName { get; set; }
    }
}
