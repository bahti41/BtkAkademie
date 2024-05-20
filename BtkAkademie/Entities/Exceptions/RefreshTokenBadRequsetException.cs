using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class RefreshTokenBadRequsetException : BadRequestException
    {
        public RefreshTokenBadRequsetException() : base($"Invalid client requset hes some invalid values.")
        {
        }
    }
}
