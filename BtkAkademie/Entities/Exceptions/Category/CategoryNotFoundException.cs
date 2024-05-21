using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Category
{
    public sealed class CategoryNotFoundException : NotFoundExcetion
    {
        public CategoryNotFoundException(int id) : base($"Category with id:{id} could not found.")
        {
        }
    }
}
