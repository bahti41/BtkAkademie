﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String? CategoryName { get; set; }

        //Ref :Collection Navigation Property
        //public ICollection<Book> Books { get; set; }
    }
}
