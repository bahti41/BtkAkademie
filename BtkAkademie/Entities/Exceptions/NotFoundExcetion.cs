using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class NotFoundExcetion:Exception
    {
        protected NotFoundExcetion(string message): base(message)
        {
            
        }
    }
}
