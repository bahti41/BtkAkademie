using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record TokenDTO
    {
        public String AccessToken { get; set; }
        public String RefreshToken { get; set; }
    }
}
